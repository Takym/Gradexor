using Microsoft.UI.Xaml;

namespace WinUI3
{
	partial class App
	{
		public App()
		{
			this.InitializeComponent();
		}

		protected override void OnLaunched(LaunchActivatedEventArgs args)
		{
			base.OnLaunched(args);
			new MainWindow().Activate();
		}
	}
}
