using osu.Framework;

using var host = Host.GetSuitableDesktopHost("Tray");
using var game = new Tray.TrayGame();

host.Run(game);
