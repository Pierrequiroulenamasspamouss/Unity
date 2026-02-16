namespace Kampai.UI.View
{
	public class LocalizeView : global::strange.extensions.mediation.impl.View
	{
		public string Key;

		[Inject]
		public global::Kampai.Main.ILocalizationService service { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		protected override void Start()
		{
			base.Start();
			Translate();
		}

		private void Translate()
		{
			global::UnityEngine.UI.Text componentInParent = GetComponentInParent<global::UnityEngine.UI.Text>();
			if (componentInParent == null)
			{
				logger.Error("LocalizeView: GameObject {0} is missing parent component Text!", base.name);
			}
			else
			{
				GetComponentInParent<global::UnityEngine.UI.Text>().text = service.GetString(Key);
			}
		}
	}
}
