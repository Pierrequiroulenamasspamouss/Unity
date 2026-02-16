namespace Kampai.UI.View
{
	public class QuestDialogView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.UI.Text DialogText;

		public global::UnityEngine.RectTransform DialogBackground;

		public global::UnityEngine.RectTransform QuestTextTransform;

		public global::Kampai.UI.View.ButtonView QuestButton;

		public global::Kampai.UI.View.KampaiImage DialogIcon;

		private string dialogText;

		private int currentPageIndex;

		private global::System.Collections.Generic.IList<string> dialogPages;

		private global::Kampai.Util.IRoutineRunner routineRunner;

		private global::Kampai.Main.ILocalizationService localizationService;

		public void Init(global::Kampai.Util.IRoutineRunner routineRunner, global::Kampai.Main.ILocalizationService localizationService)
		{
			this.routineRunner = routineRunner;
			this.localizationService = localizationService;
			global::UnityEngine.RectTransform rectTransform = base.transform as global::UnityEngine.RectTransform;
			rectTransform.offsetMax = global::UnityEngine.Vector2.zero;
			rectTransform.offsetMin = global::UnityEngine.Vector2.zero;
			dialogPages = new global::System.Collections.Generic.List<string>();
			base.gameObject.SetActive(false);
		}

		internal void SetDialogIcon(string maskPath)
		{
			DialogIcon.maskSprite = UIUtils.LoadSpriteFromPath(maskPath);
		}

		public void ShowPreviousDialog()
		{
			if (dialogPages.Count != 0)
			{
				currentPageIndex = 0;
				UpdateDialog();
			}
			base.gameObject.SetActive(true);
		}

		public void ShowDialog(string dialog)
		{
			currentPageIndex = 0;
			dialogPages.Clear();
			dialogText = dialog;
			DialogText.text = dialog;
			MoveOffDialog();
			base.gameObject.SetActive(true);
			routineRunner.StartCoroutine(ProcessDialog());
		}

		public bool IsPageOver()
		{
			if (dialogPages.Count == 0 || currentPageIndex == dialogPages.Count)
			{
				return true;
			}
			return false;
		}

		public void UpdateDialog()
		{
			DialogText.text = dialogPages[currentPageIndex++];
			routineRunner.StartCoroutine(UpdateDisplay());
		}

		private void ProcessText()
		{
			float height = QuestTextTransform.rect.height;
			if (DialogSizeCheck(height))
			{
				ProcessDialog_Normal();
			}
			else
			{
				BreakDialogByTextBoxSize(height);
			}
		}

		private void ProcessDialog_Normal()
		{
			dialogText = string.Empty;
			MoveBackDialog();
		}

		private void ProcessDialog_AfterBreak(global::System.Collections.Generic.List<int> breakIndices)
		{
			for (int i = 0; i < breakIndices.Count - 1; i++)
			{
				string item = dialogText.Substring(breakIndices[i] + ((i != 0) ? 1 : 0), breakIndices[i + 1] - breakIndices[i] + ((i != breakIndices.Count - 2) ? 1 : (-1)));
				dialogPages.Add(item);
			}
			UpdateDialog();
		}

		private void BreakDialogByTextBoxSize(float height)
		{
			int num = global::UnityEngine.Mathf.CeilToInt(height / DialogBackground.rect.height);
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
			list.Add(0);
			for (int i = 1; i < num; i++)
			{
				int endIndex = dialogText.Length / num * i;
				int item = FindCloestIndexOfWordBreaker(0, endIndex);
				list.Add(item);
			}
			list.Add(dialogText.Length);
			ProcessDialog_AfterBreak(list);
		}

		private bool DialogSizeCheck(float height)
		{
			if (string.IsNullOrEmpty(dialogText))
			{
				return true;
			}
			if (DialogBackground.rect.height < height)
			{
				return false;
			}
			return true;
		}

		private int FindCloestIndexOfWordBreaker(int startIndex, int endIndex)
		{
			string text = localizationService.GetString("PunctuationDelimiters");
			if (string.IsNullOrEmpty(text))
			{
				text = ",.?!";
			}
			text += " \n\r\t";
			char[] anyOf = text.ToCharArray();
			int num = dialogText.LastIndexOfAny(anyOf, endIndex, endIndex - startIndex);
			if (num == -1)
			{
				return endIndex;
			}
			return num;
		}

		private global::System.Collections.IEnumerator ProcessDialog()
		{
			yield return null;
			yield return null;
			ProcessText();
		}

		private global::System.Collections.IEnumerator UpdateDisplay()
		{
			yield return null;
			if (currentPageIndex == 1)
			{
				MoveBackDialog();
			}
		}

		private void MoveOffDialog()
		{
			base.transform.localPosition = new global::UnityEngine.Vector3(0f, 2 * global::UnityEngine.Screen.height, 0f);
		}

		private void MoveBackDialog()
		{
			base.transform.localPosition = new global::UnityEngine.Vector3(0f, 0f, 0f);
		}
	}
}
