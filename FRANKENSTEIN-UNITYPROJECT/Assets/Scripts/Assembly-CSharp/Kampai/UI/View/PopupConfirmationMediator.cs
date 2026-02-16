namespace Kampai.UI.View
{
	public class PopupConfirmationMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.PopupConfirmationView>
	{
		private bool result;

		private global::strange.extensions.signal.impl.Signal<bool> CallbackSignal;

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.CloseConfirmationSignal closeConfirmationSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideSkrimSignal hideSkrim { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		public override void OnRegister()
		{
			base.OnRegister();
			closeConfirmationSignal.AddListener(Cancel);
			base.view.Decline.ClickedSignal.AddListener(Cancel);
			base.view.Accept.ClickedSignal.AddListener(Proceed);
			base.view.OnMenuClose.AddListener(OnMenuClose);
		}

		public override void OnRemove()
		{
			base.OnRemove();
			closeConfirmationSignal.RemoveListener(Cancel);
			base.view.Decline.ClickedSignal.RemoveListener(Cancel);
			base.view.Accept.ClickedSignal.RemoveListener(Proceed);
			base.view.OnMenuClose.RemoveListener(OnMenuClose);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>> input = args.Get<global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>>>();
			Init(input);
		}

		protected override void Close()
		{
			soundFXSignal.Dispatch("Play_main_menu_close_01");
			base.view.Close();
			CallbackSignal.Dispatch(result);
			result = false;
		}

		private void Cancel()
		{
			result = false;
			Close();
		}

		private void Proceed()
		{
			result = true;
			Close();
		}

		private void Init(global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>> input)
		{
			base.view.title.text = localService.GetString(input.Item1);
			base.view.description.text = localService.GetString(input.Item2);
			string item = input.Item3;
			if (item == null || string.IsNullOrEmpty(input.Item3))
			{
				item = "img_char_Min_FeedbackPositive01";
			}
			CallbackSignal = input.Item4;
			base.closeAllOtherMenuSignal.Dispatch(base.gameObject);
			base.view.Init();
		}

		private void OnMenuClose()
		{
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Unload, "popup_Confirmation");
			hideSkrim.Dispatch("ConfirmationSkrim");
		}
	}
}
