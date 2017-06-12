using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMusicPlayer.Models;

namespace MyMusicPlayerTest
{
    [TestClass]
	public class AppConfigurationTest
	{
		[TestMethod]
		public void ReadSettingTest()
		{
			var appConfig = new AppConfiguration();
			appConfig.AddOrUpdateAppSettings("Slider33Hz", "50");
			double actualValue = appConfig.ReadSetting("Slider33Hz");
			Assert.AreEqual(50, actualValue);

			appConfig.AddOrUpdateAppSettings("Slider32Hz", "60");
			Assert.AreEqual(60, appConfig.ReadSetting("Slider32Hz"));
			appConfig.AddOrUpdateAppSettings("Volume", "50");
			Assert.AreEqual(50, appConfig.ReadSetting("Volume"));

			appConfig.AddOrUpdateAppSettings("Volume", "100");
			Assert.AreEqual(100, appConfig.ReadSetting("Volume"));

			Assert.AreEqual(0, appConfig.ReadSetting("dasdasdasd"));
		}
	}
}