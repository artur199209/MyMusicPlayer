using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Media;
using MyMusicPlayer.Models;
using System.IO;

namespace MyMusicPlayer.ViewModels
{
    public class PlayControlViewModel : NotifyPropertyChanged, IPlayControl
	{
		public MediaPlayer mediaPlayer;
		private AppConfiguration appConfiguration;
		public int index = 0;
		private PlayListViewModel playListViewModel;

		public PlayControlViewModel()
		{
			mediaPlayer = new MediaPlayer();
			DurationMP3 = "00:00";
			ElapsedTimeMP3 = "00:00";
			isStop = true;
			appConfiguration = new AppConfiguration();
			SliderVolumeValue = appConfiguration.ReadSetting("Volume");
			mediaPlayer.Volume = SliderDurationValue;
			playListViewModel = new PlayListViewModel();
        }

		private bool isPlay;
		public bool IsPlay
		{
			get { return isPlay; }
			set
			{
				if (value != isPlay)
				{
					isPlay = value;
					OnPropertyChanged();
				}
			}
		}

		private bool isRepeatSong;
		public bool IsRepeatSong
		{
			get { return isRepeatSong; }
			set
			{
				if (value != isRepeatSong)
				{
					isRepeatSong = value;
					OnPropertyChanged();
				}
			}
		}

		private bool isRandomSong;
		public bool IsRandomSong
		{
			get { return isRandomSong; }
			set
			{
				if (value != isRandomSong)
				{
					isRandomSong = value;
					OnPropertyChanged();
				}
			}
		}

		private bool isStop;
		public bool IsStop
		{
			get { return isStop; }
			set
			{
				if (value != isStop)
				{
					isStop = value;
					OnPropertyChanged();
				}
			}
		}

		private double sliderVolumeValue;
		public double SliderVolumeValue
		{
			get { return sliderVolumeValue; }
			set
			{
				if (value != sliderVolumeValue)
				{
					sliderVolumeValue = value;
					OnPropertyChanged();
				}
			}
		}

		private double sliderDurationValue;
		public double SliderDurationValue
		{
			get { return sliderDurationValue; }
			set
			{
				if (value != sliderDurationValue)
				{
					sliderDurationValue = value;
					OnPropertyChanged();
				}
			}
		}

		private string durationMP3;
		public string DurationMP3
		{
			get { return durationMP3; }
			set
			{
				if (value != durationMP3)
				{
					durationMP3 = value;
					OnPropertyChanged();
				}
			}
		}

		private string elapsedTimeMP3;
		public string ElapsedTimeMP3
		{
			get { return elapsedTimeMP3; }
			set
			{
				if (value != elapsedTimeMP3)
				{
					elapsedTimeMP3 = value;
					OnPropertyChanged();
				}
			}
		}

		private RelayCommand volumeMaxCommand;
		public RelayCommand VolumeMaxCommand
		{
			get
			{
				return volumeMaxCommand ?? (volumeMaxCommand =
					new RelayCommand(() => SliderVolumeValue = 100));
			}
		}

		private RelayCommand volumeMuteCommand;
		public RelayCommand VolumeMuteCommand
		{
			get
			{
				return volumeMuteCommand ?? ( volumeMuteCommand =
					new RelayCommand(() => SliderVolumeValue = 0));
			}
		}

		private RelayCommand playSongCommand;
		public RelayCommand PlaySongCommand
		{
			get
			{
				return playSongCommand ?? (playSongCommand =
					new RelayCommand(() => PlayOrPauseSong()));
			}
		}

		private RelayCommand stopSongCommand;
		public RelayCommand StopSongCommand
		{
			get
			{
				return stopSongCommand ?? (stopSongCommand =
					new RelayCommand(() => StopSong()));
			}
		}

		private RelayCommand playNextSongCommand;
		public RelayCommand PlayNextSongCommand
		{
			get
			{
				return playNextSongCommand ?? (playNextSongCommand =
					new RelayCommand(() => PlayNextSong()));
			}
		}

		private RelayCommand playPreviousSongCommand;
		public RelayCommand PlayPreviousSongCommand
		{
			get
			{
				return playPreviousSongCommand ?? (playPreviousSongCommand =
					new RelayCommand(() => PlayPreviousSong()));
			}
		}

		private RelayCommand repeatSongCommand;
		public RelayCommand RepeatSongCommand
		{
			get
			{
				return repeatSongCommand ?? (repeatSongCommand =
					new RelayCommand(() => RepeatSong()));
			}
		}

		private RelayCommand randomSongCommand;
		public RelayCommand RandomSongCommand
		{
			get
			{
				return randomSongCommand ?? (randomSongCommand =
					new RelayCommand(() => RandomSong()));
			}
		}

		public void RepeatSong()
		{
			if (IsRepeatSong)
			{
				IsRepeatSong = false;
			}
			else
			{
				IsRepeatSong = true;
			}
		}

		public void RandomSong()
		{
			if (IsRandomSong)
			{
				IsRandomSong = false;
			}
			else
			{
				IsRandomSong = true;
			}
		}

		public void PlayNextSongAfterPrevious()
		{
			if (!IsRepeatSong)
			{
				index++;
			}

            PlaySongWithSpecificIndex(index);
		}

		public void PlayNextSong()
		{
			if (PlayListViewModel.ListBoxItems.Count - 1 <= index)
			{
				index = 0;
			}
			else
			{
				index++;
			}
			
			if (isRandomSong)
			{
				index = RandomGenerator.GetRandomSongIndex();
			}

            PlaySongWithSpecificIndex(index);
		}

		public void PlayPreviousSong()
		{
			index--;

			if (index >= PlayListViewModel.ListBoxItems.Count || index < 0)
			{
				index = 0;
			}

            PlaySongWithSpecificIndex(index);
		}

		public void PlaySong()
		{
			if (mediaPlayer.Source == null)
			{
				if (isRandomSong)
				{
                    PlaySongWithSpecificIndex(RandomGenerator.GetRandomSongIndex());
				}
				else
				{
                    PlaySongWithSpecificIndex(index);
				}
			}
			else
			{
				mediaPlayer.Play();
				IsPlay = true;
				IsStop = false;
			}
		}

		public void PlaySongWithSpecificIndex(int index)
		{
			var nextSong = playListViewModel.GetNextSongPath(index);
			if (nextSong != null && File.Exists(nextSong))
			{
				mediaPlayer.Open(new Uri(nextSong));
				mediaPlayer.Play();
				IsPlay = true;
				IsStop = false;
				DurationMP3 = SongInfo.GetDurationSong(nextSong);
			}
            else
            {
                StopSong();
            }
		}

		public void PauseSong()
		{
			if (mediaPlayer.CanPause)
			{
				mediaPlayer.Pause();
				IsPlay = false;
			}
		}

		public void PlayOrPauseSong()
		{
            if (PlayListViewModel.ListBoxItems.Count > 0)
			{
				if (!IsPlay)
				{
					PlaySong();
				}
				else
				{
					PauseSong();
				}
			}
		}

		public void SetMediaPlayerVolume(double volumeValue)
		{
			SliderVolumeValue = volumeValue;
			mediaPlayer.Volume = volumeValue / 100.0;
		}

		public void SaveVolumeSetting(double volume)
		{
			appConfiguration.AddOrUpdateAppSettings("Volume", volume.ToString());
		}

		public int GetSongSecondsCount()
		{
			if (mediaPlayer.NaturalDuration.HasTimeSpan)
			{
				int songSecondsCount = mediaPlayer.NaturalDuration.TimeSpan.Minutes * 60 +
					mediaPlayer.NaturalDuration.TimeSpan.Seconds;
				return songSecondsCount;
			}
			return 0;
		}

		public int GetActualSongSecond()
		{
			var actualPositionInSeconds = mediaPlayer.Position.Minutes * 60 + mediaPlayer.Position.Seconds;
			return actualPositionInSeconds;
		}

		public int CalculateSliderValue()
		{	
			if (GetSongSecondsCount() != 0)
			{
				return 100 * GetActualSongSecond() / GetSongSecondsCount();
			}
			return 0;
		}

		public void SetMediaPlayerSongPosition()
		{
			int songSeconds = GetSongSecondsCount();
			mediaPlayer.Position = TimeSpan.FromSeconds((songSeconds * SliderDurationValue) / 100.0);
		}

		public void StopSong()
		{
            SliderDurationValue = 0;
            mediaPlayer.Close();
			mediaPlayer.Stop();
			IsPlay = false;
			IsStop = true;
			DurationMP3 = "00:00";
		}
	}
}