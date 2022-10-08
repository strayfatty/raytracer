using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace Tray;

public sealed class TrayGame : TrayGameBase
{
    private ScreenStack? screenStack;

    protected override void LoadComplete()
    {
        base.LoadComplete();

        this.screenStack.Push(new MainScreen());
    }

    [BackgroundDependencyLoader]
    private void Load()
    {
        this.screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };
        this.Child = this.screenStack;
    }
}
