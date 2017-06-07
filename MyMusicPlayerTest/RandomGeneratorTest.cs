using MyMusicPlayer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMusicPlayer.ViewModels;

namespace MyMusicPlayerTest
{	
	[TestClass]
	public class RandomGeneratorTest
	{

	    [TestMethod]
		public void GetRandomSongIndexTest()
		{
			int randomNumber = RandomGenerator.GetRandomSongIndex();
			Assert.IsNotNull(randomNumber);
			randomNumber = RandomGenerator.GetRandomSongIndex();
			Assert.IsNotNull(randomNumber);
		}
	}
}