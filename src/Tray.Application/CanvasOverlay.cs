using System.Numerics;
using System.Text;

using Tray.Core;

using Veldrid;
using Veldrid.SPIRV;

namespace Tray.Application
{
    public sealed class CanvasOverlay
    {
        private readonly GraphicsDevice device;

        private readonly ResourceFactory factory;

        private readonly TextureCanvas canvas;

        private DeviceBuffer identityMatrix;

        private DeviceBuffer vertexBuffer;

        private DeviceBuffer indexBuffer;

        private Pipeline pipeline;

        private ResourceSet projectionSet;

        private ResourceLayout textureLayout;

        private ResourceSet textureSet;

        public CanvasOverlay(
            GraphicsDevice device,
            ResourceFactory factory,
            int width,
            int height)
        {
            this.device = device;
            this.factory = factory;
            this.canvas = new TextureCanvas(device, factory, width, height);
        }

        public ICanvas Canvas => this.canvas;

        public void CreateResources()
        {
            var projMatrix = this.factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            this.device.UpdateBuffer(projMatrix, 0, Matrix4x4.CreateOrthographic(2, 2, -1.0f, 2.0f));

            var viewMatrix = this.factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            this.device.UpdateBuffer(viewMatrix, 0, Matrix4x4.CreateLookAt(Vector3.UnitZ * 2.5f, Vector3.Zero, Vector3.UnitY));

            this.identityMatrix = this.factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer));
            this.device.UpdateBuffer(this.identityMatrix, 0, Matrix4x4.Identity);

            this.vertexBuffer = this.factory.CreateBuffer(new BufferDescription(20 * sizeof(float), BufferUsage.VertexBuffer));
            this.device.UpdateBuffer(this.vertexBuffer, 0, new float[]
            {
                -1,  1, 1, 0, 0,
                 1,  1, 1, 1, 0,
                 1, -1, 1, 1, 1,
                -1, -1, 1, 0, 1
            });

            this.indexBuffer = this.factory.CreateBuffer(new BufferDescription(6 * sizeof(ushort), BufferUsage.IndexBuffer));
            this.device.UpdateBuffer(this.indexBuffer, 0, new ushort[] { 0, 1, 2, 2, 3, 0 });

            var shaderSet = new ShaderSetDescription(
                new[]
                {
                    new VertexLayoutDescription(
                        new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float3),
                        new VertexElementDescription("TexCoords", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2))
                },
                this.factory.CreateFromSpirv(
                    new ShaderDescription(ShaderStages.Vertex, Encoding.UTF8.GetBytes(VertexCode), "main"),
                    new ShaderDescription(ShaderStages.Fragment, Encoding.UTF8.GetBytes(FragmentCode), "main")
                )
            );

            var projectionLayout = this.factory.CreateResourceLayout(
                new ResourceLayoutDescription(
                    new ResourceLayoutElementDescription("ProjectionBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex),
                    new ResourceLayoutElementDescription("ViewBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex)
                )
            );

            this.textureLayout = this.factory.CreateResourceLayout(
                new ResourceLayoutDescription(
                    new ResourceLayoutElementDescription("WorldBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex),
                    new ResourceLayoutElementDescription("SurfaceTexture", ResourceKind.TextureReadOnly, ShaderStages.Fragment),
                    new ResourceLayoutElementDescription("SurfaceSampler", ResourceKind.Sampler, ShaderStages.Fragment)
                )
            );

            this.pipeline = factory.CreateGraphicsPipeline(
                new GraphicsPipelineDescription(
                    BlendStateDescription.SingleOverrideBlend,
                    DepthStencilStateDescription.DepthOnlyLessEqual,
                    RasterizerStateDescription.Default,
                    PrimitiveTopology.TriangleList,
                    shaderSet,
                    new[] { projectionLayout, textureLayout },
                    this.device.MainSwapchain.Framebuffer.OutputDescription
                )
            );

            this.projectionSet = factory.CreateResourceSet(
                new ResourceSetDescription(
                    projectionLayout,
                    projMatrix,
                    viewMatrix
                )
            );

            this.textureSet = this.factory.CreateResourceSet(
                new ResourceSetDescription(
                    this.textureLayout,
                    this.identityMatrix,
                    this.canvas.View,
                    this.device.Aniso4xSampler
                )
            );
        }

        public void Update(float deltaSeconds, InputSnapshot snapshot)
        {
        }

        public void UpdateTexture()
        {
            this.canvas.UpdateTexture();
        }

        public void Draw(CommandList commandList)
        {
            commandList.SetPipeline(this.pipeline);
            commandList.SetVertexBuffer(0, this.vertexBuffer);
            commandList.SetIndexBuffer(this.indexBuffer, IndexFormat.UInt16);
            commandList.SetGraphicsResourceSet(0, this.projectionSet);
            commandList.SetGraphicsResourceSet(1, this.textureSet);
            commandList.DrawIndexed(6, 1, 0, 0, 0);
        }

        public void Resize(int width, int height)
        {
            this.canvas.Resize(width, height);

            this.textureSet = this.factory.CreateResourceSet(
                new ResourceSetDescription(
                    this.textureLayout,
                    this.identityMatrix,
                    this.canvas.View,
                    this.device.Aniso4xSampler
                )
            );
        }

        private const string VertexCode = @"
#version 450
layout(set = 0, binding = 0) uniform ProjectionBuffer
{
    mat4 Projection;
};
layout(set = 0, binding = 1) uniform ViewBuffer
{
    mat4 View;
};
layout(set = 1, binding = 0) uniform WorldBuffer
{
    mat4 World;
};
layout(location = 0) in vec3 Position;
layout(location = 1) in vec2 TexCoords;
layout(location = 0) out vec2 fsin_texCoords;
void main()
{
    vec4 worldPosition = World * vec4(Position, 1);
    vec4 viewPosition = View * worldPosition;
    vec4 clipPosition = Projection * viewPosition;
    gl_Position = clipPosition;
    fsin_texCoords = TexCoords;
}";

        private const string FragmentCode = @"
#version 450
layout(location = 0) in vec2 fsin_texCoords;
layout(location = 0) out vec4 fsout_color;
layout(set = 1, binding = 1) uniform texture2D SurfaceTexture;
layout(set = 1, binding = 2) uniform sampler SurfaceSampler;
void main()
{
    fsout_color =  texture(sampler2D(SurfaceTexture, SurfaceSampler), fsin_texCoords);
}";
    }
}
