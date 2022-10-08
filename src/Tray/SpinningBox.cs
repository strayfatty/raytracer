using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;

namespace Tray;

public sealed class SpinningBox : CompositeDrawable
{
    private Container? box;

    public SpinningBox()
    {
        AutoSizeAxes = Axes.Both;
        Origin = Anchor.Centre;
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();
        _ = box.Loop(x => x.RotateTo(0).RotateTo(360, 2500));
    }

    [BackgroundDependencyLoader]
    private void Load()
    {
        this.box = new Container();
        this.box.AutoSizeAxes = Axes.Both;
        this.box.Anchor = Anchor.Centre;
        this.box.Origin = Anchor.Centre;

        var childBox = new Box();
        childBox.RelativeSizeAxes = Axes.Both;
        childBox.Anchor = Anchor.Centre;
        childBox.Origin = Anchor.Centre;

        var childText = new SpriteText
        {
            Text = "Main Screen",
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Font = FontUsage.Default.With(size: 40),
        };

        this.box.Children = new Drawable[] {childText};
        this.InternalChild = this.box;
    }
}
