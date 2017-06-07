using System.Windows;
using MyMusicPlayer.ViewModels;
using MyMusicPlayer.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyMusicPlayer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private PlayListViewModel playListViewModel;
		List<PlayListModel> listBoxSelectedItems;

		public MainWindow()
		{
			InitializeComponent();
			WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
			playListViewModel =  new PlayListViewModel();
			this.DataContext = playListViewModel;
			playlistLstBox.ItemsSource = PlayListViewModel.ListBoxItems;
			playlistLstBox.SelectedIndex = 0;
			playlistLstBox.AllowDrop = true;
			playlistLstBox.DragEnter += playlistLstBox_DragEnter;
			playlistLstBox.Drop += playlistLstBox_Drop;
			playlistLstBox.KeyUp += playlistLstBox_KeyUp;
		}

		void playlistLstBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == Key.Delete)
			{
				playListViewModel.RemoveSongFromList();
			}
		}

		void playlistLstBox_Drop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

			foreach (string file in files)
			{
				PlayListModel newSong = new PlayListModel();
				newSong.FirstPerformerAndAlbum = SongInfo.GetArtistsSong(file);
				newSong.SongPath = file;
				newSong.Duration = SongInfo.GetDurationSong(file);
				newSong.Title = SongInfo.GetTitleSong(file);
				newSong.PlayListIndex = PlayListViewModel.ListBoxItems.Count + 1;
				PlayListViewModel.ListBoxItems.Add(newSong);
			}	
		}

		void playlistLstBox_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(".mp3"))
			{
				e.Effects = DragDropEffects.Copy;
			}
		}

		private void playlistLstBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			ListBox listBox = sender as ListBox;

			PlayListViewModel playListViewModel = DataContext as PlayListViewModel;
			listBoxSelectedItems = new List<PlayListModel>();

			foreach (var item in listBox.SelectedItems)
			{
				listBoxSelectedItems.Add((PlayListModel)item);
			}

			playListViewModel.SelectedItems = null;
			playListViewModel.SelectedItems = listBoxSelectedItems;
		}
	}
}