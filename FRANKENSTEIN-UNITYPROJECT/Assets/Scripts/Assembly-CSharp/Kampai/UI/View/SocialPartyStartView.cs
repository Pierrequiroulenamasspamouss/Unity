namespace Kampai.UI.View
{
	public class SocialPartyStartView : global::Kampai.UI.View.PopupMenuView
	{
		public ScrollableButtonView goButton;

		public global::UnityEngine.UI.Text goButtonText;

		public global::UnityEngine.UI.Text itemTitle;

		public global::UnityEngine.UI.Text description;

		public global::UnityEngine.UI.Text timeRemaining;

		public global::UnityEngine.UI.Text timeTitle;

		public global::UnityEngine.UI.Text itemAmount1;

		public global::UnityEngine.UI.Text itemAmount2;

		public global::UnityEngine.UI.Text itemAmount3;

		public global::UnityEngine.UI.Text itemAmount4;

		public global::Kampai.UI.View.KampaiImage itemImage1;

		public global::Kampai.UI.View.KampaiImage itemImage2;

		public global::Kampai.UI.View.KampaiImage itemImage3;

		public global::Kampai.UI.View.KampaiImage itemImage4;

		public global::UnityEngine.UI.Image itemCheckmark1;

		public global::UnityEngine.UI.Image itemCheckmark2;

		public global::UnityEngine.UI.Image itemCheckmark3;

		public global::UnityEngine.UI.Image itemCheckmark4;

		public global::UnityEngine.UI.Image itemBacking1;

		public global::UnityEngine.UI.Image itemBacking2;

		public global::UnityEngine.UI.Image itemBacking3;

		public global::UnityEngine.UI.Image itemBacking4;

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		public override void Init()
		{
			base.Init();
			Open();
		}
	}
}
