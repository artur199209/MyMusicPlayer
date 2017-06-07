using System.Collections.Generic;

namespace MyMusicPlayer
{
    public interface ISerializer
    {
        string GetFilePathXML(string filename);
        void SerializeSongPathToFile(List<string> songPathList, string filename = "\\file.xml");
        List<string> DeserializeFromFile(string filename = "\\file.xml");
    }
}