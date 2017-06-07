using System.IO;

namespace MyMusicPlayer.Models
{
	public static class SongInfo
	{
		public static int GetIndexOfCharacter(string path)
		{
			return path.IndexOf("-");
		}

		public static string GetArtistSong(string path)
		{
			if (File.Exists(path))
			{
				string titleWithArtist = Path.GetFileNameWithoutExtension(path);
				int index = GetIndexOfCharacter(titleWithArtist);

				if (index > -1)
				{
					return titleWithArtist.Substring(0, index);
				}

				return titleWithArtist;
			}

			return null;
		}

		public static string GetTitle(string path)
		{
			if (File.Exists(path))
			{
				string fileName = Path.GetFileNameWithoutExtension(path);
				int index = GetIndexOfCharacter(fileName);

				if (index > 1)
				{
					return fileName.Substring(index + 1);
				}

				return fileName;
			}

			return null;			
		}

		public static string RemoveStringFromPath(string path)
		{
			if (path.StartsWith("file:///"))
			{
				return path.Substring(8, path.Length - 8);
			}

			return path;
		}

		public static string GetDurationSong(string path)
		{
			string duration = "00:00";

			path = RemoveStringFromPath(path);
			
			if (File.Exists(path))
			{
				TagLib.File tagFile = TagLib.File.Create(path);
				duration = tagFile.Properties.Duration.ToString(@"mm\:ss");
			}

			return duration;
		}

		public static string GetArtistsSong(string path)
		{
			string artistSong = string.Empty;

			if (File.Exists(path))
			{
				TagLib.File tagFile = TagLib.File.Create(path);
				string[] artists = tagFile.Tag.Performers;

				foreach (var artist in artists)
				{
					artistSong += artist + ", ";
				}

				if (artistSong != string.Empty)
				{
					return artistSong;
				}

				artistSong = GetArtistSong(path);

				return artistSong;
			}

			return null;
		}

		public static string GetTitleSong(string path)
		{
			if (File.Exists(path))
			{
				TagLib.File tagFile = TagLib.File.Create(path);
				var titleSong = tagFile.Tag.Title;

				if (titleSong != null)
				{
					return titleSong;
				}

				titleSong = GetTitle(path);
				return titleSong.Trim();
			}

			return null;			
		}
	}
}