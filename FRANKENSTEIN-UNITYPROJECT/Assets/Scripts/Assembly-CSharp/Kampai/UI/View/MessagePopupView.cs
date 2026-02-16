namespace Kampai.UI.View
{
	public class MessagePopupView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.RectTransform PopupBox;

		private global::UnityEngine.UI.Text popupText;

		private global::Kampai.UI.View.KampaiImage popupImage;

		private float timer;

		private float disableTime = 2f;

		private float fadeTime = 0.1f;

		private global::UnityEngine.Color popupTextColorOn;

		private global::UnityEngine.Color popupTextColorOff;

		private global::UnityEngine.Color popupImageColorOn;

		private global::UnityEngine.Color popupImageColorOff;

		internal void Init()
		{
			popupText = PopupBox.GetComponentInChildren<global::UnityEngine.UI.Text>();
			popupImage = PopupBox.GetComponent<global::Kampai.UI.View.KampaiImage>();
			popupTextColorOn = new global::UnityEngine.Color(popupText.color.r, popupText.color.g, popupText.color.b, 1f);
			popupTextColorOff = new global::UnityEngine.Color(popupText.color.r, popupText.color.g, popupText.color.b, 0f);
			popupImageColorOn = new global::UnityEngine.Color(popupImage.color.r, popupImage.color.g, popupImage.color.b, 1f);
			popupImageColorOff = new global::UnityEngine.Color(popupImage.color.r, popupImage.color.g, popupImage.color.b, 0f);
			popupText.color = popupTextColorOff;
			popupImage.color = popupImageColorOff;
			base.gameObject.SetActive(false);
		}

		internal void Display(string text, global::Kampai.UI.View.MessagePopUpAnchor anchor, global::UnityEngine.Vector2 anchorPosition)
		{
			switch (anchor)
			{
			case global::Kampai.UI.View.MessagePopUpAnchor.TOP_LEFT:
				PopupBox.anchorMin = global::UnityEngine.Vector2.up;
				PopupBox.anchorMax = global::UnityEngine.Vector2.up;
				PopupBox.pivot = global::UnityEngine.Vector2.up;
				break;
			case global::Kampai.UI.View.MessagePopUpAnchor.TOP_RIGHT:
				PopupBox.anchorMin = global::UnityEngine.Vector2.one;
				PopupBox.anchorMax = global::UnityEngine.Vector2.one;
				PopupBox.pivot = global::UnityEngine.Vector2.one;
				break;
			case global::Kampai.UI.View.MessagePopUpAnchor.BOTTOM_LEFT:
				PopupBox.anchorMin = global::UnityEngine.Vector2.zero;
				PopupBox.anchorMax = global::UnityEngine.Vector2.zero;
				PopupBox.pivot = global::UnityEngine.Vector2.zero;
				break;
			case global::Kampai.UI.View.MessagePopUpAnchor.BOTTOM_RIGHT:
				PopupBox.anchorMin = global::UnityEngine.Vector2.right;
				PopupBox.anchorMax = global::UnityEngine.Vector2.right;
				PopupBox.pivot = global::UnityEngine.Vector2.right;
				break;
			case global::Kampai.UI.View.MessagePopUpAnchor.CENTER:
			{
				global::UnityEngine.Vector2 vector = new global::UnityEngine.Vector2(0.5f, 0.5f);
				PopupBox.anchorMin = vector;
				PopupBox.anchorMax = vector;
				PopupBox.pivot = vector;
				break;
			}
			case global::Kampai.UI.View.MessagePopUpAnchor.CUSTOM:
				PopupBox.anchorMin = anchorPosition;
				PopupBox.anchorMax = anchorPosition;
				PopupBox.pivot = new global::UnityEngine.Vector2(0.5f, 0.5f);
				break;
			}
			popupText.color = popupTextColorOff;
			popupImage.color = popupImageColorOff;
			popupText.text = text;
			base.gameObject.SetActive(true);
			base.transform.SetAsLastSibling();
			FadeIn();
		}

		private void Update()
		{
			if (timer > 0f)
			{
				timer -= global::UnityEngine.Time.deltaTime;
				if (timer <= 0f)
				{
					FadeOut();
				}
			}
		}

		private void FadeIn()
		{
			Go.to(popupText, fadeTime, new GoTweenConfig().colorProp("color", popupTextColorOn).onComplete(delegate
			{
				timer = disableTime;
			}));
			Go.to(popupImage, fadeTime, new GoTweenConfig().colorProp("color", popupImageColorOn));
		}

		private void FadeOut()
		{
			Go.to(popupText, fadeTime, new GoTweenConfig().colorProp("color", popupTextColorOff).onComplete(delegate
			{
				popupText.text = string.Empty;
				base.gameObject.SetActive(false);
			}));
			Go.to(popupImage, fadeTime, new GoTweenConfig().colorProp("color", popupImageColorOff));
		}
	}
}
