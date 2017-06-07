using MyMusicPlayer.Properties;
using System;
using System.Configuration;
using System.Windows;

namespace MyMusicPlayer.Models
{
    public class AppConfiguration: IAppConfiguration
    {
		public double ReadSetting(string key)
		{
            try
            {
                var configuration = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var settings = configuration.AppSettings.Settings;
                if (settings[key] != null)
                {
                    return Convert.ToDouble(settings[key].Value);
                }
            }
            catch (ConfigurationErrorsException)
            {
                MessageBox.Show(Resources.ErrorWritingAppSettings);
            }

            return 0;
		}

		public void AddOrUpdateAppSettings(string key, string value)
		{
			try
			{
				var configFile = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetExecutingAssembly().Location);
				var settings = configFile.AppSettings.Settings;
				if (settings[key] == null)
				{
					settings.Add(key, value);
				}
				else
				{
					settings[key].Value = value;
				}
				configFile.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
			}
			catch (ConfigurationErrorsException)
			{
				MessageBox.Show(Resources.ErrorWritingAppSettings);
			}
		}
	}
}