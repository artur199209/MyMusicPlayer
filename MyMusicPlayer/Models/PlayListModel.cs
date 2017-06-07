namespace MyMusicPlayer.Models
{
    public class PlayListModel : NotifyPropertyChanged
	{
		private string title;
		private string firstPerformerAndAlbum;
		private string duration;
		private int playListIndex;
		private string songPath;

		public string Title
		{
			get { return title; }
			set
			{ 
				if (value != title)
				{
					title = value;
                    OnPropertyChanged();
				}
			}
		}

		public string FirstPerformerAndAlbum
		{
			get { return firstPerformerAndAlbum; }
			set 
			{ 
				if (value != firstPerformerAndAlbum)
				{
					firstPerformerAndAlbum = value;
                    OnPropertyChanged();
				}
			}
		}

		public string Duration
		{
			get { return duration; }
			set
			{ 
				if (value != duration)
				{
					duration = value;
                    OnPropertyChanged();
				}
			}
		}

		public int PlayListIndex
		{
			get { return playListIndex; }
			set 
			{ 
				if (value != playListIndex)
				{
					playListIndex = value;
                    OnPropertyChanged();
				}
			}
		}

		public string SongPath
		{
			get { return songPath; }
			set
			{
				if (value != songPath)
				{
					songPath = value;
                    OnPropertyChanged();
				}
			}
		}
	}
}