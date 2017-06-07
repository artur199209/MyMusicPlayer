using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using MyMusicPlayer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyMusicPlayer.ViewModels
{
    public class PlayListViewModel : NotifyPropertyChanged, IPlayList
	{
		public PlayListModel playListModel;

		private static ObservableCollection<PlayListModel> listBoxItems = new ObservableCollection<PlayListModel>();
		public static ObservableCollection<PlayListModel> ListBoxItems
		{
			get { return listBoxItems; }
			set
			{
				if (listBoxItems != value)
				{
					listBoxItems = value;
				}
			}
		}

		private List<PlayListModel> selectedItems = new List<PlayListModel>();
		public List<PlayListModel> SelectedItems
		{
			get { return selectedItems; }
			set
			{
				if (selectedItems != value)
				{
					selectedItems = value;
					OnPropertyChanged();
				}
			}
		}

		private RelayCommand addSongCommand;
		public RelayCommand AddSongCommand
		{
			get
			{
				return addSongCommand ?? (addSongCommand =
					new RelayCommand(() => OpenDialogAndChooseFiles()));
			}
		}

		private RelayCommand removeSongCommand;
		public RelayCommand RemoveSongCommand
		{
			get
			{
				return removeSongCommand ?? (removeSongCommand =
					new RelayCommand(() => RemoveSongFromList()));
			}
		}

        public void LoadPlayListFromFile()
        {
            Serializer serializer = new Serializer();
            var pathList = serializer.DeserializeFromFile();

            foreach (var item in pathList)
            {
                var songItem = GetSongInfo(item);
                ListBoxItems.Add(songItem);
            }
        }

        private RelayCommand savePlayListInFileCommand;
        public RelayCommand SavePlayListInFileCommand
        {
            get
            {
                return savePlayListInFileCommand ?? (savePlayListInFileCommand = new RelayCommand(() =>
                {
                    SaveListToFile();
                }));
            }
        }

        public void SaveListToFile()
        {
            Serializer serializer = new Serializer();
            var lista = new List<string>();

            foreach (var item in ListBoxItems)
            {
                lista.Add(item.SongPath);
            }

            serializer.SerializeSongPathToFile(lista);
        }

        private RelayCommand loadPlayListInFileCommand;
        public RelayCommand LoadPlayListInFileCommand
        {
            get
            {
                return loadPlayListInFileCommand ?? (loadPlayListInFileCommand = new RelayCommand(() =>
                {
                    LoadPlayListFromFile();
                }));
            }
        }

        public PlayListModel GetSongInfo(string path)
		{
			var song = new PlayListModel();
			song.FirstPerformerAndAlbum = SongInfo.GetArtistsSong(path);
			song.SongPath = path;
			song.Duration = SongInfo.GetDurationSong(path);
			song.Title = SongInfo.GetTitleSong(path);
			song.PlayListIndex = listBoxItems.Count + 1;
			return song;
		}

        public List<PlayListModel> GetSongsList(List<string> pathList)
        {
            var songList = new List<PlayListModel>();

            foreach(var item in pathList)
            {
                songList.Add(GetSongInfo(item));
            }

            return songList;
        }

        public bool? result;

        private void OpenDialogAndChooseFiles()
		{
			var openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "MP3 Files (*.mp3)|*.mp3";
			openFileDialog.Multiselect = true;

			result = openFileDialog.ShowDialog();

			if (result == true)
			{
				string [] fileNames = openFileDialog.FileNames;

				foreach(var fileName in fileNames)
				{
					var newSong = GetSongInfo(fileName);					
					listBoxItems.Add(newSong);
				}
			}
		}

		public void RemoveSongFromList()
		{
			foreach (var item in selectedItems)
			{
				listBoxItems.Remove(item);
			}

			selectedItems = null;

			for (int i = 0; i < listBoxItems.Count; i++)
			{
				listBoxItems[i].PlayListIndex = i + 1;
			}
		}

		public string GetNextSongPath(int index)
		{
			if (index >= listBoxItems.Count || index < 0)
			{
				index = 0;
			}
			
			if (listBoxItems.Count == 0)
			{
				return null;
			}
	
			return listBoxItems[index].SongPath.ToString();
		}
	}
}