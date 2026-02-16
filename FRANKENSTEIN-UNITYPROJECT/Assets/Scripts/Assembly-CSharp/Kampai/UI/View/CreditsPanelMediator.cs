namespace Kampai.UI.View
{
	public class CreditsPanelMediator : global::Kampai.UI.View.UIStackMediator<global::Kampai.UI.View.CreditsPanelView>
	{
		private global::System.Collections.Generic.List<global::UnityEngine.GameObject> textObjects = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService locService { get; set; }

		[Inject]
		public global::Kampai.Util.ClientVersion clientVersion { get; set; }

		public override void OnRegister()
		{
			base.view.closeButton.ClickedSignal.AddListener(Close);
			base.OnRegister();
		}

		private void Start()
		{
			base.view.scrollRect.verticalNormalizedPosition = 1f;
		}

		public override void OnRemove()
		{
			base.view.closeButton.ClickedSignal.RemoveListener(Close);
			base.OnRemove();
		}

		protected override void Close()
		{
			soundFXSignal.Dispatch("Play_button_click_01");
			base.gameObject.SetActive(false);
		}

		protected override void OnEnable()
		{
			for (int i = 0; i < 31; i++)
			{
				string key = string.Format("{0}{1:00}", "CreditContents", i);
				if (locService.Contains(key))
				{
					string text = locService.GetString(key);
					if (!string.IsNullOrEmpty(text))
					{
						PopulateCredits(text);
					}
				}
			}
			base.view.creditText.text = locService.GetString("CreditContents", "©", clientVersion.GetClientVersion());
			StartCoroutine(WaitAFrame());
		}

		private global::System.Collections.IEnumerator WaitAFrame()
		{
			yield return null;
			base.view.SetupDivisions(base.view.creditText.rectTransform.rect.height);
		}

		protected override void OnDisable()
		{
			base.view.Cleanup();
			foreach (global::UnityEngine.GameObject textObject in textObjects)
			{
				global::UnityEngine.Object.Destroy(textObject);
			}
		}

		private void PopulateCredits(string content)
		{
			global::UnityEngine.GameObject gameObject = (global::UnityEngine.GameObject)global::UnityEngine.Object.Instantiate(base.view.creditText.gameObject);
			gameObject.transform.SetParent(base.view.scrollRect.content.transform, false);
			textObjects.Add(gameObject);
			global::UnityEngine.UI.Text component = gameObject.GetComponent<global::UnityEngine.UI.Text>();
			component.text = content;
			base.view.textList.Add(component);
		}
	}
}
