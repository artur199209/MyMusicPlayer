using MyMusicPlayer.Models;
using System.Collections.Generic;

namespace MyMusicPlayer
{
    public interface IPlayList
    {
        void LoadPlayListFromFile();
        void SaveListToFile();
        PlayListModel GetSongInfo(string path);
        List<PlayListModel> GetSongsList(List<string> pathList);
        void RemoveSongFromList();
        string GetNextSongPath(int index);
    }
}