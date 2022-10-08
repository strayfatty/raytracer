using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;

namespace Tray;

public sealed class MainScreen : Screen
{
    [BackgroundDependencyLoader]
    private void Load()
    {
        this.InternalChildren = new Drawable[]
        {
            new Box { Colour = Colour4.Black, RelativeSizeAxes = Axes.Both },
            new SpriteText
            {
                Y = 20,
                Text = "Main Screen",
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Font = FontUsage.Default.With(size: 40),
            },
            new SpinningBox { Anchor = Anchor.Centre },
        };
    }
}
