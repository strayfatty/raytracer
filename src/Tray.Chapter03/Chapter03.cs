using System.Numerics;
using ImGuiNET;
using Tray.Application;
using Tray.Core;
using Veldrid;

namespace Tray.Chapter03
{
    public sealed class Chapter03 : ApplicationWindow
    {
        private readonly CanvasOverlay canvasOverlay;

        private readonly Scene scene;

        private bool showSettingsWindow = true;

        private bool showMetricsWindow = false;

        private float frequency;

        private Vector3 colorClock = Vector3.One;

        private Vector3 colorHour = Vector3.One;

        private Vector3 colorMinute = Vector3.One;

        private Vector3 colorSecond = Vector3.One;

        private Vector3 colorMillisecond = Vector3.One;

        public Chapter03()
            : base("Tray.Chapter03")
        {
            this.canvasOverlay = new CanvasOverlay(
                this.GraphicsDevice,
                this.ResourceFactory,
                this.Window.Width,
                this.Window.Height);

            this.scene = new Scene(this.canvasOverlay.Canvas);
            this.frequency = this.scene.Frequency;
        }

        protected override void CreateResources()
        {
            base.CreateResources();
            this.canvasOverlay.CreateResources();
        }

        protected override void Update(float deltaSeconds, InputSnapshot snapshot)
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
            this.DrawSettingsWindow();

            if (this.showMetricsWindow)
                ImGui.ShowMetricsWindow(ref this.showMetricsWindow);
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

            ImGui.InputFloat("Update frequency", ref this.frequency);
            ImGui.ColorEdit3("Color clock", ref this.colorClock);
            ImGui.ColorEdit3("Color hour hand", ref this.colorHour);
            ImGui.ColorEdit3("Color minute hand", ref this.colorMinute);
            ImGui.ColorEdit3("Color second hand", ref this.colorSecond);
            ImGui.ColorEdit3("Color millisecond hand", ref this.colorMillisecond);

            if (ImGui.Button("Update"))
            {
                this.scene.Frequency = this.frequency;
                this.scene.ColorClock = new Color(this.colorClock.X, this.colorClock.Y, this.colorClock.Z);
                this.scene.ColorHour = new Color(this.colorHour.X, this.colorHour.Y, this.colorHour.Z);
                this.scene.ColorMinute = new Color(this.colorMinute.X, this.colorMinute.Y, this.colorMinute.Z);
                this.scene.ColorSecond = new Color(this.colorSecond.X, this.colorSecond.Y, this.colorSecond.Z);
                this.scene.ColorMillisecond = new Color(this.colorMillisecond.X, this.colorMillisecond.Y, this.colorMillisecond.Z);
            }

            ImGui.End();
        }
    }
}
