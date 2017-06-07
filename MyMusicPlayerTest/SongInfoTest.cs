using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMusicPlayer.Models;

namespace MyMusicPlayerTest
{
	/// <summary>
	/// Summary description for SongInfoTest
	/// </summary>
	[TestClass]
	public class SongInfoTest
	{
		string path = @"C:\Users\Artur\Downloads\Enrique Iglesias-Yo Sin Ti Official Audio.mp3";
		string path2 = @"C:\Users\Artur\Downloads\Enrique Iglesias Yo Sin Ti Official Audio.mp3";
		string path3 = "file:///C:/Users/Artur/Downloads/Enrique Iglesias-Yo Sin Ti Official Audio.mp3";
        string path4 = @"C:\Users\Artur\Downloads\Margaret  Heartbeat.mp3";

		[TestMethod]
		public void GetIndexOfCharacterTest()
		{
			Assert.AreEqual(41, SongInfo.GetIndexOfCharacter(path));
			Assert.AreEqual(-1, SongInfo.GetIndexOfCharacter(path2));
		}

		[TestMethod]
		public void GetArtistSongTest()
		{
			Assert.AreEqual("Enrique Iglesias", SongInfo.GetArtistSong(path));
            Assert.AreEqual("Margaret  Heartbeat", SongInfo.GetArtistSong(path4));
            Assert.AreEqual("Enrique Iglesias", SongInfo.GetArtistsSong(path));
			Assert.IsNull(SongInfo.GetArtistSong(path2));
			Assert.IsNull(SongInfo.GetArtistsSong(path2));
		}

		[TestMethod]
		public void GetTitleTest()
		{
			Assert.AreEqual("Yo Sin Ti Official Audio", SongInfo.GetTitle(path));
            Assert.AreEqual("Margaret  Heartbeat", SongInfo.GetTitle(path4));
            Assert.AreEqual("Yo Sin Ti Official Audio", SongInfo.GetTitleSong(path));
			Assert.IsNull(SongInfo.GetTitleSong(path2));
			Assert.IsNull(SongInfo.GetTitle(path2));
		}

		[TestMethod]
		public void GetDurationSongTest()
		{
			Assert.AreEqual("04:14", SongInfo.GetDurationSong(path));
			Assert.AreEqual("00:00", SongInfo.GetDurationSong(path2));
		}

		[TestMethod]
		public void RemoveStringFromPathTest()
		{
			Assert.AreEqual(path, SongInfo.RemoveStringFromPath(path));
			Assert.AreEqual(path2, SongInfo.RemoveStringFromPath(path2));
			Assert.AreEqual(path.Replace(@"\", "/"), SongInfo.RemoveStringFromPath(path3));
		}
	}
}