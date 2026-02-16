namespace Kampai.UI.View
{
	public class DisplayDLCDialogCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public string displayText { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject(global::Kampai.Main.MainElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable mainContext { get; set; }

		[Inject]
		public global::Kampai.Game.IDLCService dlcService { get; set; }

		[Inject]
		public global::Kampai.Game.SavePlayerSignal savePlayerSignal { get; set; }

		public override void Execute()
		{
			global::strange.extensions.signal.impl.Signal<bool> signal = new global::strange.extensions.signal.impl.Signal<bool>();
			signal.AddListener(delegate(bool result)
			{
				ConfirmationCallback(result);
			});
			global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>> type = new global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>>("popupConfirmationDefaultTitle", "DLCConfirmationDialog", "img_char_Min_FeedbackChecklist01", signal);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.QueueConfirmationSignal>().Dispatch(type);
		}

		public void ConfirmationCallback(bool result)
		{
			if (result)
			{
				savePlayerSignal.Dispatch(new global::Kampai.Util.Tuple<string, string, bool>("remote", string.Empty, true));
				string displayQualityLevel = dlcService.GetDisplayQualityLevel();
				if (displayQualityLevel.Equals("DLCHDPack"))
				{
					dlcService.SetDisplayQualityLevel("DLCSDPack");
				}
				else
				{
					dlcService.SetDisplayQualityLevel("DLCHDPack");
				}
				mainContext.injectionBinder.GetInstance<global::Kampai.Main.ReloadGameSignal>().Dispatch();
			}
		}
	}
}
