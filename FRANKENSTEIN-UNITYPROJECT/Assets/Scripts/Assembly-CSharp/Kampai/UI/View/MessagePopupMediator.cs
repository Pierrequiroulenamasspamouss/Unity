namespace Kampai.UI.View
{
	public class MessagePopupMediator : global::Kampai.UI.View.KampaiMediator
	{
		[Inject]
		public global::Kampai.UI.View.MessagePopupView view { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void OnRegister()
		{
			view.Init();
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			string text = args.Get<string>();
			global::Kampai.UI.View.MessagePopUpAnchor anchor = ((!args.Contains<global::Kampai.UI.View.MessagePopUpAnchor>()) ? global::Kampai.UI.View.MessagePopUpAnchor.TOP_RIGHT : args.Get<global::Kampai.UI.View.MessagePopUpAnchor>());
			global::UnityEngine.Vector2 anchorPosition = ((!args.Contains<global::UnityEngine.Vector2>()) ? global::UnityEngine.Vector2.zero : args.Get<global::UnityEngine.Vector2>());
			view.Display(text, anchor, anchorPosition);
		}
	}
}
