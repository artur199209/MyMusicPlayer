using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMusicPlayer.Models;
using System.IO;

namespace MyMusicPlayerTest
{
    /// <summary>
    /// Summary description for SerializerTest
    /// </summary>
    [TestClass]
    public class SerializerTest
    {
        List<string> songPathList = new List<string>
        {
            "song1", "song2", "song3"
        };

        [TestMethod]
        public void SerializeSongPathToFileTest()
        {
            var serializer = new Serializer();
            serializer.SerializeSongPathToFile(songPathList);
            string directoryPath = Path.GetDirectoryName(serializer.exePath);
            var IsFileExists = File.Exists(directoryPath + serializer.fileName);
            Assert.IsTrue(IsFileExists);
        }

        [TestMethod]
        public void DeserializeSongPathToFileTest()
        {
            var serializer = new Serializer();
            var result = serializer.DeserializeFromFile("\\file2.xml");
            Assert.IsNotNull(result);
        }
    }
}