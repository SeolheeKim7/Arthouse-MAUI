using Arthouse_MAUI.Models;

namespace Arthouse_MAUI;

public partial class App : Application
{
    public bool needArtTypeRefresh;
    public List<ArtType> AllArtTypes;
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
