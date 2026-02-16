namespace Kampai.UI.View
{
	public class GenericPopupView : global::Kampai.UI.View.PopupMenuView
	{
		public global::UnityEngine.UI.Text itemName;

		public global::UnityEngine.UI.Text itemDuration;

		public global::UnityEngine.UI.Text itemOrigin;

		public global::Kampai.UI.View.KampaiImage itemDurationIcon;

		public float offsetValue = 1.8f;

		internal void Display(global::UnityEngine.Vector3 itemCenter)
		{
			base.Init();
			global::UnityEngine.RectTransform rectTransform = base.transform as global::UnityEngine.RectTransform;
			global::UnityEngine.Vector3 anchoredPosition3D = rectTransform.sizeDelta / offsetValue;
			anchoredPosition3D.x = 0f;
			rectTransform.anchoredPosition3D = anchoredPosition3D;
			rectTransform.anchorMin = itemCenter;
			rectTransform.anchorMax = itemCenter;
			Open();
		}

		internal void SetName(string localizedName)
		{
			itemName.text = localizedName;
		}

		internal void SetTime(int duration)
		{
			itemDuration.text = UIUtils.FormatTime(duration);
		}

		internal void SetItemOrigin(string localizedOrigin)
		{
			itemOrigin.text = localizedOrigin;
		}

		internal void DisableDurationInfo()
		{
			itemDuration.enabled = false;
			itemDurationIcon.enabled = false;
		}
	}
}
