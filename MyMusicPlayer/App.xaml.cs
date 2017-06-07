using System;
using System.Windows;
using MyMusicPlayer.ViewModels;

namespace MyMusicPlayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
	{
		static App()
		{
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            PlayListViewModel.ListBoxItems.Clear();            
		}

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			string errorMessage = string.Format("An unhandled exception occured: {0}");
			MessageBox.Show(errorMessage);
		}
	}
}