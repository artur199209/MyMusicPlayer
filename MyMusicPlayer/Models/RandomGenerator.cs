using MyMusicPlayer.ViewModels;
using System;

namespace MyMusicPlayer.Models
{
    public class RandomGenerator
	{
		public static int GetRandomSongIndex()
		{
			var random = new Random();
			int randomIndex = random.Next(PlayListViewModel.ListBoxItems.Count);
			return randomIndex;
		}
	}
}