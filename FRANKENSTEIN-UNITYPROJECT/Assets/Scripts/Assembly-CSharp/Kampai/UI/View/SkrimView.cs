namespace Kampai.UI.View
{
	public class SkrimView : global::strange.extensions.mediation.impl.View
	{
		public global::Kampai.UI.View.ButtonView ClickButton;

		public global::UnityEngine.GameObject DarkSkrim;

		public bool singleSkrimClose { get; set; }

		internal void Init()
		{
			(base.transform as global::UnityEngine.RectTransform).offsetMax = global::UnityEngine.Vector2.zero;
			(base.transform as global::UnityEngine.RectTransform).offsetMin = global::UnityEngine.Vector2.zero;
			ClickButton.PlaySoundOnClick = false;
		}

		public void EnableSkrimButton(bool enable)
		{
			ClickButton.GetComponent<global::UnityEngine.UI.Button>().interactable = enable;
		}
	}
}
