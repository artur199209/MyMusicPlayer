namespace MyMusicPlayer
{
    public interface IAppConfiguration
    {
        double ReadSetting(string key);
        void AddOrUpdateAppSettings(string key, string value);
    }
}