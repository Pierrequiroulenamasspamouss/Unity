namespace Kampai.UI.View
{
	public class DLCDialogMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.DLCDialogView>
	{
		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.View.CloseAllOtherMenuSignal closeSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Game.IDLCService dlcService { get; set; }

		[Inject(global::Kampai.Main.MainElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable mainContext { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			base.view.acceptButton.ClickedSignal.AddListener(Accept);
			base.view.cancelButton.ClickedSignal.AddListener(Cancel);
			closeSignal.AddListener(Close);
			base.view.Init();
		}

		public override void OnRemove()
		{
			base.OnRemove();
			base.view.acceptButton.ClickedSignal.RemoveListener(Accept);
			base.view.cancelButton.ClickedSignal.RemoveListener(Cancel);
			closeSignal.RemoveListener(Close);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			base.view.dialogText.text = args.Get<string>();
		}

		private void Accept()
		{
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

		private void Cancel()
		{
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "DLC_Dialog");
		}

		protected override void Close()
		{
			Close(null);
		}

		private void Close(global::UnityEngine.GameObject ignore)
		{
			Cancel();
		}
	}
}
