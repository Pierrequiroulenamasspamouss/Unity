namespace Kampai.Main
{
	internal sealed class SetupEventSystemCommand : global::strange.extensions.command.impl.Command
	{
		private global::UnityEngine.GameObject eventSystem;

		[Inject]
		public global::Kampai.UI.View.LoadUICompleteSignal uiLoadCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Util.DeviceInformation deviceInformation { get; set; }

		public override void Execute()
		{
			logger.EventStart("SetupEventSystemCommand.Execute");
			eventSystem = new global::UnityEngine.GameObject("EventSystem");
			eventSystem.AddComponent<global::UnityEngine.EventSystems.EventSystem>();
			eventSystem.AddComponent<global::UnityEngine.EventSystems.StandaloneInputModule>();
			eventSystem.AddComponent<global::UnityEngine.EventSystems.TouchInputModule>();
			if (deviceInformation.IsSamsung())
			{
				eventSystem.GetComponent<global::UnityEngine.EventSystems.StandaloneInputModule>().allowActivationOnMobileDevice = true;
				eventSystem.GetComponent<global::UnityEngine.EventSystems.TouchInputModule>().enabled = false;
			}
			base.injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(eventSystem).ToName(global::Kampai.Main.MainElement.UI_EVENTSYSTEM)
				.CrossContext();
			uiLoadCompleteSignal.AddListener(SetParent);
			logger.EventStop("SetupEventSystemCommand.Execute");
		}

		private void SetParent(global::UnityEngine.GameObject contextView)
		{
			if (!(eventSystem == null))
			{
				eventSystem.transform.parent = contextView.transform;
			}
		}
	}
}
