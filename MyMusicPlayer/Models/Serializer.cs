using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace MyMusicPlayer.Models
{
    public class Serializer:ISerializer
    {
        public string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public string fileName = "\\file.xml";

        public string GetFilePathXML(string filename)
        {
            string filePathXML = string.Empty;
            string directoryPath = Path.GetDirectoryName(exePath);
            filePathXML = directoryPath + filename;
            return filePathXML;
        }

        public void SerializeSongPathToFile(List<string> songPathList, string filename = "\\file.xml")
        {
            var xmlSerializer = new XmlSerializer(typeof(List<string>));
            string pathXML = GetFilePathXML(filename);

            try
            {
                using (var streamWriter = new StreamWriter(pathXML))
                {
                    xmlSerializer.Serialize(streamWriter, songPathList);
                }
            }
            catch (Exception ex) { MessageBox.Show(Properties.Resources.SerializeFileError + " " + ex.Message); }
        }

        public List<string> DeserializeFromFile(string filename = "\\file.xml")
        {
            string inputXmlPath = GetFilePathXML(filename);
            List<string> playerList = new List<string>();

            if (File.Exists(inputXmlPath))
            {
                try
                {
                    using (var streamReader = new StreamReader(inputXmlPath))
                    {
                        var playerListSerializer = new XmlSerializer(typeof(List<string>));
                        playerList = (List<string>)playerListSerializer.Deserialize(streamReader);

                    }
                }
                catch (Exception ex) { MessageBox.Show(Properties.Resources.SerializeFileError + " " + ex.Message); }
            }
            return playerList;
        }
    }
}