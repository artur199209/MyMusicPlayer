using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMusicPlayer.ViewModels;
using MyMusicPlayer.Models;
using System.Threading;

namespace MyMusicPlayerTest
{
    /// <summary>
    /// Summary description for PlayControlViewModelTest
    /// </summary>
    [TestClass]
	public class PlayControlViewModelTest
	{
		PlayControlViewModel playControlViewModel = new PlayControlViewModel();
		string path = @"C:\Users\Artur\Downloads\Enrique Iglesias-Yo Sin Ti Official Audio.mp3";

		[TestMethod]
		public void RepeatSongTest()
		{
			Assert.IsFalse(playControlViewModel.IsRepeatSong);
            playControlViewModel.RepeatSongCommand.Execute(new object());
			Assert.IsTrue(playControlViewModel.IsRepeatSong);
            playControlViewModel.RepeatSongCommand.Execute(new object());
            Assert.IsFalse(playControlViewModel.IsRepeatSong);
		}

		[TestMethod]
		public void RandomSongTest()
		{
			Assert.IsFalse(playControlViewModel.IsRandomSong);
            playControlViewModel.RandomSongCommand.Execute(new object());
			Assert.IsTrue(playControlViewModel.IsRandomSong);
			playControlViewModel.RandomSongCommand.Execute(new object());
			Assert.IsFalse(playControlViewModel.IsRandomSong);
		}

		[TestMethod]
		public void PauseSongTest()
		{
            var playControlViewModel = new PlayControlViewModel();
			Assert.IsFalse(playControlViewModel.IsPlay);
			playControlViewModel.PauseSong();
			Assert.IsFalse(playControlViewModel.IsPlay);
			playControlViewModel.IsPlay = true;
			Assert.IsTrue(playControlViewModel.IsPlay);
        }

		[TestMethod]
		public void PlayOrPauseSongTest()
		{
			Assert.AreEqual(0, PlayListViewModel.ListBoxItems.Count);
			playControlViewModel.PlaySongCommand.Execute(new object());
			Assert.AreEqual(false, playControlViewModel.IsPlay);
			PlayListModel playListModel = new PlayListModel();
			playListModel.SongPath = path;
			PlayListViewModel.ListBoxItems.Add(playListModel);
            playControlViewModel.IsPlay = true;
            playControlViewModel.PlaySongCommand.Execute(new object());
            Assert.AreEqual(true, playControlViewModel.IsPlay);
		}

		[TestMethod]
		public void StopSongTest()
		{
			Assert.IsFalse(playControlViewModel.IsPlay);
            playControlViewModel.StopSongCommand.Execute(new object());
            Assert.IsFalse(playControlViewModel.IsPlay);
			Assert.IsTrue(playControlViewModel.IsStop);
			Assert.AreEqual("00:00", playControlViewModel.DurationMP3);
			Assert.AreEqual(0, playControlViewModel.SliderDurationValue);
		}

		[TestMethod]
		public void SetMediaPlayerVolumeTest()
		{
			Assert.AreEqual(100, playControlViewModel.SliderVolumeValue);
			playControlViewModel.SetMediaPlayerVolume(70);
			Assert.AreEqual(70, playControlViewModel.SliderVolumeValue);
			Assert.AreEqual(70, playControlViewModel.mediaPlayer.Volume * 100);
		}

		[TestMethod]
		public void VolumeMaxCommandTest()
		{
			playControlViewModel.VolumeMaxCommand.Execute(new object());
			Assert.AreEqual(100, playControlViewModel.SliderVolumeValue);
		}

		[TestMethod]
		public void VolumeMuteCommandTest()
		{
			playControlViewModel.VolumeMuteCommand.Execute(new object());
			Assert.AreEqual(0, playControlViewModel.SliderVolumeValue);
			Assert.AreEqual(0, playControlViewModel.mediaPlayer.Volume);
		}

		[TestMethod]
		public void GetSongSecondsCountTest()
		{
            PlayControlViewModel playControlViewModel = new PlayControlViewModel();
			Assert.AreEqual(0, playControlViewModel.GetSongSecondsCount());
			playControlViewModel.mediaPlayer.Open(new Uri(path));
			Thread.Sleep(200);
			Assert.AreEqual(254, playControlViewModel.GetSongSecondsCount());
			Assert.IsNotNull(playControlViewModel.mediaPlayer.Source);
			Assert.IsTrue(playControlViewModel.mediaPlayer.NaturalDuration.HasTimeSpan);
		}

		[TestMethod]
		public void GetActualSongSecondTest()
		{
			PlayControlViewModel playControlViewModel = new PlayControlViewModel();
			playControlViewModel.mediaPlayer.Open(new Uri(path));
			Assert.AreEqual(0, playControlViewModel.GetActualSongSecond());
			playControlViewModel.mediaPlayer.Position = TimeSpan.FromSeconds(50);
			Assert.AreEqual(50, playControlViewModel.GetActualSongSecond());
		}

		[TestMethod]
		public void CalculateSliderValueTest()
		{
			PlayControlViewModel playControlViewModel = new PlayControlViewModel();
			Assert.AreEqual(0, playControlViewModel.CalculateSliderValue());
			playControlViewModel.mediaPlayer.Open(new Uri(path));
			Thread.Sleep(200);
			Assert.AreNotEqual(0, playControlViewModel.GetSongSecondsCount());
			playControlViewModel.mediaPlayer.Position = TimeSpan.FromSeconds(127);
			Assert.AreEqual(50, playControlViewModel.CalculateSliderValue());
		}

		[TestMethod]
		public void SetMediaPlayerSongPositionTest()
		{
			PlayControlViewModel playControlViewModel = new PlayControlViewModel();
			playControlViewModel.mediaPlayer.Open(new Uri(path));
			Thread.Sleep(200);
			playControlViewModel.SliderDurationValue = 0;
			playControlViewModel.SetMediaPlayerSongPosition();
			Assert.AreEqual(0, playControlViewModel.GetActualSongSecond());
			playControlViewModel.SliderDurationValue = 2;
			playControlViewModel.SetMediaPlayerSongPosition();
			Assert.AreEqual(5, playControlViewModel.GetActualSongSecond());
		}

		[TestMethod]
		public void PlaySongWithITest()
		{
			PlayControlViewModel playControlViewModel = new PlayControlViewModel();
			playControlViewModel.mediaPlayer.Open(null);
			Assert.IsNull(playControlViewModel.mediaPlayer.Source);
			playControlViewModel.PlaySongWithSpecificIndex(0);
			Assert.AreEqual(true, playControlViewModel.IsPlay);
			PlayListViewModel.ListBoxItems.Clear();
			playControlViewModel.mediaPlayer.Open(null);
			playControlViewModel.PlaySongWithSpecificIndex(0);
			Assert.IsNull(playControlViewModel.mediaPlayer.Source);
		}

		[TestMethod]
		public void PlaySongTest()
		{
			PlayControlViewModel playControlViewModel = new PlayControlViewModel();
			playControlViewModel.mediaPlayer.Open(null);
            Assert.AreEqual("00:00", playControlViewModel.ElapsedTimeMP3);
            playControlViewModel.PlaySong();
			Assert.IsNull(playControlViewModel.mediaPlayer.Source);
			playControlViewModel.mediaPlayer.Open(new Uri(path));
			playControlViewModel.PlaySong();
			Assert.IsNotNull(playControlViewModel.mediaPlayer.Source);
			Assert.IsFalse(playControlViewModel.IsStop);
			playControlViewModel.mediaPlayer.Stop();
		}

		[TestMethod]
		public void PlayPreviousSongTest()
		{
			PlayControlViewModel playControlViewModel = new PlayControlViewModel();
			PlayListModel playListModel = new PlayListModel();
			playListModel.SongPath = path;
			PlayListViewModel.ListBoxItems.Add(playListModel);
			playControlViewModel.PlayPreviousSongCommand.Execute(new object());
			Assert.IsNotNull(playControlViewModel.mediaPlayer.Source);
			playControlViewModel.mediaPlayer.Stop();
		}

		[TestMethod]
		public void PlayNextSongTest()
		{
			PlayControlViewModel playControlViewModel = new PlayControlViewModel();
			PlayListModel playListModel = new PlayListModel();
			playListModel.SongPath = path;
			PlayListViewModel.ListBoxItems.Clear();
			PlayListViewModel.ListBoxItems.Add(playListModel);
			playControlViewModel.index = 10;
            playControlViewModel.PlayNextSongCommand.Execute(new object());
			Assert.AreEqual(0, playControlViewModel.index);
			playControlViewModel.index = 0;
            playControlViewModel.IsRandomSong = true;
            playControlViewModel.PlayNextSongCommand.Execute(new object());
            Assert.AreEqual(0, playControlViewModel.index);	
		}

		[TestMethod]
		public void PlayNextSongAfterPreviousTest()
		{
			playControlViewModel.IsRepeatSong = false;
			playControlViewModel.index = 0;
			playControlViewModel.PlayNextSongAfterPrevious();
			Assert.AreEqual(1, playControlViewModel.index);
		}

        [TestMethod]
        public void SaveVolumeSettingTest()
        {
            var appConfig = new AppConfiguration();
            playControlViewModel.SaveVolumeSetting(60);
            Assert.AreEqual(60, appConfig.ReadSetting("Volume"));
        }

    }
}