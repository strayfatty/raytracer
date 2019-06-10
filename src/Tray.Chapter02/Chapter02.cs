using System;
using System.Numerics;
using ImGuiNET;
using Tray.Application;
using Tray.Core;
using Veldrid;

namespace Tray.Chapter02
{
    public sealed class Chapter02 : ApplicationWindow
    {
        private readonly CanvasOverlay canvasOverlay;

        private bool showSettingsWindow = true;

        private bool showMetricsWindow = false;

        private Vector2 origin = new Vector2(0, 1);

        private Vector2 direction = new Vector2(1, 1.8f);

        private float velocity = 11.25f;

        private float gravity = 0.1f;

        private float wind = -0.01f;

        private int stepCount = 250;

        private Scene scene;

        public Chapter02()
            : base("Tray.Chapter02")
        {
            this.canvasOverlay = new CanvasOverlay(
                this.GraphicsDevice,
                this.ResourceFactory,
                this.Window.Width,
                this.Window.Height);

            this.scene = new Scene(this.canvasOverlay.Canvas);
            this.scene.Init(
                this.origin,
                this.direction,
                this.velocity,
                this.gravity,
                this.wind,
                this.stepCount);
        }

        protected override void CreateResources()
        {
            base.CreateResources();
            this.canvasOverlay.CreateResources();
        }

        protected override void Update(
            float deltaSeconds,
            InputSnapshot snapshot)
        {
            base.Update(deltaSeconds, snapshot);
            this.canvasOverlay.Update(deltaSeconds, snapshot);

            if (this.scene.Update(deltaSeconds))
                this.canvasOverlay.UpdateTexture();
        }

        protected override void Draw(CommandList commandList)
        {
            base.Draw(commandList);
            this.canvasOverlay.Draw(commandList);
        }

        protected override void DrawImGui()
        {
            this.DrawMainMenu();

            if (this.showMetricsWindow)
                ImGui.ShowMetricsWindow(ref this.showMetricsWindow);

            this.DrawSettingsWindow();
        }

        protected override void Resized()
        {
            base.Resized();
            this.canvasOverlay.Resize(
                this.Window.Width,
                this.Window.Height);
            this.scene.Reset();
        }

        private void DrawMainMenu()
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("File"))
                {
                    if (ImGui.MenuItem("Settings", null, this.showSettingsWindow))
                        this.showSettingsWindow = !this.showSettingsWindow;

                    if (ImGui.MenuItem("ImGui Metrics", null, this.showMetricsWindow))
                        this.showMetricsWindow = !this.showMetricsWindow;

                    ImGui.Separator();

                    if (ImGui.MenuItem("Quit"))
                        this.Window.Close();

                    ImGui.EndMenu();
                }

                ImGui.EndMainMenuBar();
            }
        }

        private void DrawSettingsWindow()
        {
            if (!this.showSettingsWindow)
                return;

            if (!ImGui.Begin("Settings", ref this.showSettingsWindow))
                return;

            var width = this.Window.Width;
            ImGui.InputInt("Width", ref width, 0, 0, ImGuiInputTextFlags.ReadOnly);

            var height = this.Window.Height;
            ImGui.InputInt("Height", ref height, 0, 0, ImGuiInputTextFlags.ReadOnly);

            ImGui.InputFloat2("Origin", ref this.origin);
            ImGui.InputFloat2("Direction", ref this.direction);
            ImGui.InputFloat("Velocity", ref this.velocity);
            ImGui.InputFloat("Gravity", ref this.gravity);
            ImGui.InputFloat("Wind", ref this.wind);
            ImGui.InputInt("Step count", ref this.stepCount);

            if (ImGui.Button("Update"))
            {
                this.scene.Init(
                    this.origin,
                    this.direction,
                    this.velocity,
                    this.gravity,
                    this.wind,
                    this.stepCount);
                this.canvasOverlay.Canvas.Clear(Color.Black);
            }

            ImGui.End();
        }
    }
}
