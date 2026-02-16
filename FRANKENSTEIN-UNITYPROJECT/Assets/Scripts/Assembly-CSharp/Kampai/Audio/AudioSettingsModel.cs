namespace Kampai.Audio
{
	internal static class AudioSettingsModel
	{
		public static bool MusicMute { get; set; }

		public static bool NeedMute { get; set; }

		static AudioSettingsModel()
		{
			MusicMute = false;
			NeedMute = true;
		}
	}
}
