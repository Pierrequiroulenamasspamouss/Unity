namespace Kampai.Game
{
	public class SynergyService : global::Kampai.Game.ISynergyService
	{
		private NimbleBridge_NotificationListener synergyEnvListener;

		[Inject]
		public global::Kampai.Game.UpdateUserSignal updateUserSignal { get; set; }

        //public string userID
        //{
        //	get
        //	{
        //		if (NimbleBridge_SynergyEnvironment.GetComponent().IsDataAvailable())
        //		{
        //			return NimbleBridge_SynergyEnvironment.GetComponent().GetSynergyId();
        //		}
        //		if (synergyEnvListener == null)
        //		{
        //			synergyEnvListener = new NimbleBridge_NotificationListener(OnNimbleSynergyEnvNotification);
        //			NimbleBridge_NotificationCenter.RegisterListener("nimble.environment.notification.startup_requests_finished", synergyEnvListener);
        //		}
        //		return string.Empty;
        //	}
        //}
        public string userID
        {
            get
            {
                return "user_12345"; // TODO LATER : set the server to reply with the proper user ID
            }
        }

        private void OnNimbleSynergyEnvNotification(string name, global::System.Collections.Generic.Dictionary<string, object> userData, NimbleBridge_NotificationListener listener)
		{
			if (name == "nimble.environment.notification.startup_requests_finished")
			{
				string synergyId = NimbleBridge_SynergyEnvironment.GetComponent().GetSynergyId();
				updateUserSignal.Dispatch(synergyId);
			}
		}
	}
}
