using osu.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Tray;

// Anything in this class is shared between the test browser and the game implementation.
// It allows for caching global dependencies that should be accessible to tests, or changing
// the screen scaling for all components including the test browser and framework overlays.
public class TrayGameBase : Game
{
    protected override Container<Drawable> Content { get; }

    protected TrayGameBase()
    {
        var container = new DrawSizePreservingFillContainer();
        container.TargetDrawSize = new Vector2(1366, 768);

        this.Content = container;
        base.Content.Add(container);
    }

    [BackgroundDependencyLoader]
    private void Load()
    {
    }
}
