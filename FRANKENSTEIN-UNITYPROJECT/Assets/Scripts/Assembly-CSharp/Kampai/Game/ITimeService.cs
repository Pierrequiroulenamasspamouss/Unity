namespace Kampai.Game
{
	public interface ITimeService
	{
		int GameTimeSeconds();

		int DeviceTimeSeconds();

		int SecondsSinceAppStart();

		float SecondsSinceAppStartAsFloat();

		global::System.DateTime EpochToDateTime(int seconds);

		global::System.Globalization.CultureInfo GetCultureInfo();

		void GameStarted();

		int SecondsSinceGameStart();
	}
}
