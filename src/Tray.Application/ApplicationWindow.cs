using System.Diagnostics;

using ImGuiNET;

using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace Tray.Application
{
    public class ApplicationWindow
    {
        private readonly CommandList commandList;

        private ImGuiRenderer uiRenderer;

        public Sdl2Window Window { get; }

        public GraphicsDevice GraphicsDevice { get; }

        public ResourceFactory ResourceFactory => this.GraphicsDevice.ResourceFactory;

        public Swapchain MainSwapchain => this.GraphicsDevice.MainSwapchain;

        public RgbaFloat ClearColor { get; set; } = new RgbaFloat(0, 0, 0.2f, 1f);

        public ApplicationWindow(string title)
        {
            var info = new WindowCreateInfo(50, 50, 800, 600, WindowState.Normal, title);

            this.Window = VeldridStartup.CreateWindow(info);
            this.GraphicsDevice = VeldridStartup.CreateGraphicsDevice(this.Window);
            this.GraphicsDevice.SyncToVerticalBlank = true;

            this.commandList = this.ResourceFactory.CreateCommandList();
            this.uiRenderer = new ImGuiRenderer(
                this.GraphicsDevice,
                this.MainSwapchain.Framebuffer.OutputDescription,
                this.Window.Width,
                this.Window.Height);

            this.Window.Resized += () =>
            {
                this.uiRenderer.WindowResized(this.Window.Width, this.Window.Height);
                this.MainSwapchain.Resize((uint)this.Window.Width, (uint)this.Window.Height);
                this.Resized();
            };
        }

        public void Run()
        {
            this.CreateResources();

            var stopwatch = Stopwatch.StartNew();
            var previousTicks = stopwatch.ElapsedTicks;

            while (this.Window.Exists)
            {
                var currentTicks = stopwatch.ElapsedTicks;
                var deltaSeconds = (currentTicks - previousTicks) / (float)Stopwatch.Frequency;
                previousTicks = currentTicks;

                var snapshot = this.Window.PumpEvents();
                this.Update(deltaSeconds, snapshot);

                this.uiRenderer.Update(deltaSeconds, snapshot);
                this.DrawImGui();

                this.commandList.Begin();
                this.commandList.SetFramebuffer(this.MainSwapchain.Framebuffer);
                this.commandList.ClearColorTarget(0, this.ClearColor);

                this.Draw(this.commandList);

                this.uiRenderer.Render(this.GraphicsDevice, this.commandList);

                this.commandList.End();

                this.GraphicsDevice.SubmitCommands(this.commandList);
                this.GraphicsDevice.WaitForIdle();

                this.GraphicsDevice.SwapBuffers(this.MainSwapchain);
            }
        }

        protected virtual void CreateResources()
        {
            ImGui.StyleColorsLight();
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0);
            ImGui.PushStyleVar(ImGuiStyleVar.ScrollbarRounding, 0);
            ImGui.PushStyleVar(ImGuiStyleVar.TabRounding, 0);
            ImGui.PushStyleVar(ImGuiStyleVar.FrameBorderSize, 1);
        }

        protected virtual void Update(float deltaSeconds, InputSnapshot snapshot)
        {
        }

        protected virtual void Draw(CommandList commandList)
        {
        }

        protected virtual void DrawImGui()
        {
        }

        protected virtual void Resized()
        {
        }
    }
}