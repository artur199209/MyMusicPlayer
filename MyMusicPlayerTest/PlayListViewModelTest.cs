using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMusicPlayer.ViewModels;
using System.Windows;
using MyMusicPlayer.Models;
using System.Collections.Generic;
using System.IO;

namespace MyMusicPlayerTest
{
	[TestClass]
	public class PlayListViewModelTest
	{
		private readonly string path = @"C:\Users\Artur\Downloads\Enrique Iglesias-Yo Sin Ti Official Audio.mp3";
		PlayListViewModel playListViewModel = new PlayListViewModel();

		private void InitCollections()
		{
			var song1 = new PlayListModel { SongPath = path, Duration = "4:00", FirstPerformerAndAlbum = "Iglesiae", PlayListIndex = 0, Title = "Dream" };
			var song2 = new PlayListModel { SongPath = path, Duration = "5:00", FirstPerformerAndAlbum = "Iglesiae", PlayListIndex = 1, Title = "Sky" };
			var song3 = new PlayListModel { SongPath = path, Duration = "3:00", FirstPerformerAndAlbum = "Enrique", PlayListIndex=3, Title="Blue Sky"};

			playListViewModel.SelectedItems.Clear();
			PlayListViewModel.ListBoxItems.Clear();
			playListViewModel.SelectedItems.Add(song1);
			playListViewModel.SelectedItems.Add(song2);
			PlayListViewModel.ListBoxItems.Add(song1);
			PlayListViewModel.ListBoxItems.Add(song2);
			PlayListViewModel.ListBoxItems.Add(song3);
		}

		[TestMethod]
		public void GetNextSongPathTest()
		{
			PlayListModel playListModel = new PlayListModel
			{
				SongPath = path
			};

			PlayListViewModel.ListBoxItems.Add(playListModel);

			Assert.AreEqual(path, playListViewModel.GetNextSongPath(-1));
			Assert.AreEqual(path, playListViewModel.GetNextSongPath(5));
			PlayListViewModel.ListBoxItems.Clear();

			Assert.IsNull(playListViewModel.GetNextSongPath(0));
		}

		[TestMethod]
		public void RemoveSongFromListTest()
		{
			InitCollections();
			playListViewModel.RemoveSongCommand.Execute(new object());
			Assert.IsNull(playListViewModel.SelectedItems);
			Assert.AreEqual(1, PlayListViewModel.ListBoxItems.Count);
		}

        [TestMethod]
        public void GetSongInfoTest()
        {
            var songInfo = playListViewModel.GetSongInfo(path);
            Assert.AreEqual("Enrique Iglesias", songInfo.FirstPerformerAndAlbum);
            Assert.AreEqual("Yo Sin Ti Official Audio", songInfo.Title);
        }

        [TestMethod]
        public void OpenDialogAndChooseFilesTest()
        {
            playListViewModel.AddSongCommand.Execute(new object());
            Assert.IsNotNull(playListViewModel.result);
        }

        [TestMethod]
        public void LoadPlayListInFileTest()
        {
            playListViewModel.LoadPlayListInFileCommand.Execute(new object());
            PlayListViewModel.ListBoxItems.Clear();
            Assert.IsNotNull(PlayListViewModel.ListBoxItems);
        }

        [TestMethod]
        public void GetSongsListTest()
        {
            List<string> pathList = new List<string>()
            {
                @"C:\Users\Artur\Downloads\Enrique Iglesias-Yo Sin Ti Official Audio.mp3",
                @"C:\Users\Artur\Downloads\Margaret  Heartbeat.mp3"
            };

            var result = playListViewModel.GetSongsList(pathList);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SavePlayListInFileCommandTest()
        {
            Serializer serializer = new Serializer();
            var xmlPath = serializer.GetFilePathXML(serializer.fileName);
            Assert.IsNotNull(PlayListViewModel.ListBoxItems);
            PlayListViewModel.ListBoxItems.Add(new PlayListModel()
            {
                SongPath = path
            });
            playListViewModel.SavePlayListInFileCommand.Execute(new object());
            var IsFileExists = File.Exists(xmlPath);
            Assert.IsTrue(IsFileExists);
        }

    }
}