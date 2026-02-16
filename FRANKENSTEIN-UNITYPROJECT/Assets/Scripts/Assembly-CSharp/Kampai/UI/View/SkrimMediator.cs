namespace Kampai.UI.View
{
	public class SkrimMediator : global::Kampai.UI.View.KampaiMediator
	{
		[Inject]
		public global::Kampai.UI.View.SkrimView view { get; set; }

		[Inject]
		public global::Kampai.UI.View.UIModel model { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		[Inject]
		public global::Kampai.UI.View.EnableSkrimSignal enableSkrimSignal { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel PickControllerModel { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		public override void OnRegister()
		{
			PickControllerModel.Enabled = false;
			enableSkrimSignal.AddListener(EnableSkrim);
		}

		public override void OnRemove()
		{
			view.ClickButton.ClickedSignal.RemoveListener(Close);
			hideSkrim.RemoveListener(HideSkrim);
			enableSkrimSignal.RemoveListener(EnableSkrim);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			bool flag = args.Get<bool>();
			hideSkrim.AddListener(HideSkrim);
			enableSkrimSignal.AddListener(EnableSkrim);
			view.Init();
			view.ClickButton.ClickedSignal.AddListener(Close);
			view.ClickButton.gameObject.SetActive(true);
			view.DarkSkrim.SetActive(flag);
		}

		private void EnableSkrim()
		{
			view.EnableSkrimButton(true);
		}

		private void Close()
		{
			if (!model.UIOpen)
			{
				return;
			}
			global::System.Action action = model.RemoveTopUI();
			if (!global::UnityEngine.Input.GetKeyDown(global::UnityEngine.KeyCode.Escape))
			{
				if (view.singleSkrimClose)
				{
					action();
					return;
				}
				while (action != null)
				{
					action();
					action = model.RemoveTopUI();
				}
			}
			else if (action != null)
			{
				action();
			}
			else
			{
				HideSkrim(guiLabel);
			}
		}

		private void HideSkrim(string skrimName)
		{
			if (guiLabel == null || guiLabel.Equals(skrimName))
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.ShowHiddenBuildingsSignal>().Dispatch();
				global::Kampai.UI.View.IGUICommand command = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Unload, "Skrim", guiLabel);
				guiService.Execute(command);
				PickControllerModel.Enabled = true;
				PickControllerModel.InvalidateMovement = false;
			}
		}
	}
}
