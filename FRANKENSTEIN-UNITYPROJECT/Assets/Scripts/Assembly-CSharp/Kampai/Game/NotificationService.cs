namespace Kampai.Game
{
	public class NotificationService : global::Kampai.Game.INotificationService
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public void Initialize()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.ea.gp.minions.utils.FileUtils"))
			{
				try
				{
					androidJavaClass.CallStatic("unpackAssets", "Audio/", global::Kampai.Util.GameConstants.PERSISTENT_DATA_PATH);
				}
				catch (global::UnityEngine.AndroidJavaException ex)
				{
					logger.Error("Failed to unpack audio assets: {0}", ex.ToString());
				}
			}
			string text = global::Kampai.Util.GameConstants.PERSISTENT_DATA_PATH + "/Audio/";
			if (global::System.IO.Directory.Exists(text))
			{
				string path = text + ".nomedia";
				if (!global::System.IO.File.Exists(path))
				{
					global::System.IO.File.Create(path).Dispose();
				}
			}
#endif
		}

		public void ScheduleLocalNotification(global::Kampai.Game.Notification notification)
		{
			global::Kampai.Util.Native.ScheduleLocalNotification(notification.type, notification.secondsFromNow, notification.title, notification.text, notification.stackedTitle, notification.stackedText, string.Empty, string.IsNullOrEmpty(notification.sound) ? null : string.Format("{0}{1}.mp3", "Audio/", notification.sound), string.Empty, notification.badgeNumber);
		}

		public void CancelLocalNotification(string type)
		{
			global::Kampai.Util.Native.CancelLocalNotification(type);
		}

		public void CancelAllNotifications()
		{
			global::Kampai.Util.Native.CancelAllLocalNotifications();
		}
	}
}
