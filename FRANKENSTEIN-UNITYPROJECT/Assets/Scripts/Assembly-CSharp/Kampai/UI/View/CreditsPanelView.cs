namespace Kampai.UI.View
{
	public class CreditsPanelView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.UI.ScrollRect scrollRect;

		public global::UnityEngine.RectTransform container;

		public global::Kampai.UI.View.ButtonView closeButton;

		public global::UnityEngine.UI.Text creditText;

		internal global::System.Collections.Generic.List<global::UnityEngine.UI.Text> textList = new global::System.Collections.Generic.List<global::UnityEngine.UI.Text>();

		private global::System.Collections.Generic.List<global::UnityEngine.UI.Text> top = new global::System.Collections.Generic.List<global::UnityEngine.UI.Text>();

		private global::System.Collections.Generic.List<global::UnityEngine.UI.Text> topBottom = new global::System.Collections.Generic.List<global::UnityEngine.UI.Text>();

		private global::System.Collections.Generic.List<global::UnityEngine.UI.Text> bottom = new global::System.Collections.Generic.List<global::UnityEngine.UI.Text>();

		private global::System.Collections.Generic.List<global::UnityEngine.UI.Text> relevantList;

		internal void SetupDivisions(float firstTextHeight)
		{
			int count = textList.Count;
			int num = (int)((float)count * 0.33f);
			int num2 = num / 2 + 1;
			float num3 = 0f - firstTextHeight;
			for (int i = 0; i < count; i++)
			{
				global::UnityEngine.UI.Text item = textList[i];
				float height = (textList[i].gameObject.transform as global::UnityEngine.RectTransform).rect.height;
				textList[i].rectTransform.anchoredPosition = new global::UnityEngine.Vector2(0f, num3);
				num3 -= height;
				if (i < num)
				{
					top.Add(item);
					if (i < num2)
					{
						topBottom.Add(item);
					}
				}
				if (i > count - num)
				{
					bottom.Add(item);
					if (i > count - num2)
					{
						topBottom.Add(item);
					}
				}
			}
			container.sizeDelta = new global::UnityEngine.Vector2(0f, 0f - num3);
			textList.Clear();
			relevantList = bottom;
		}

		public void Update()
		{
			if (relevantList == null)
			{
				return;
			}
			foreach (global::UnityEngine.UI.Text relevant in relevantList)
			{
				relevant.gameObject.SetActive(true);
			}
			if (scrollRect.verticalNormalizedPosition > 0.66f)
			{
				relevantList = bottom;
			}
			else if (scrollRect.verticalNormalizedPosition < 0.66f && scrollRect.verticalNormalizedPosition > 0.33f)
			{
				relevantList = topBottom;
			}
			else
			{
				relevantList = top;
			}
			foreach (global::UnityEngine.UI.Text relevant2 in relevantList)
			{
				relevant2.gameObject.SetActive(false);
			}
		}

		internal void Cleanup()
		{
			top.Clear();
			topBottom.Clear();
			bottom.Clear();
			relevantList.Clear();
		}
	}
}
