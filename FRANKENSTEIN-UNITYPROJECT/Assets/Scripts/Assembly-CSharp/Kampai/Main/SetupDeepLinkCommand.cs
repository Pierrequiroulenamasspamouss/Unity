namespace Kampai.Main
{
	public class SetupDeepLinkCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::Kampai.Main.MainElement.MANAGER_PARENT)]
		public global::UnityEngine.GameObject managers { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.MoveBuildMenuSignal moveBuildMenuSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowPremiumStoreSignal showPremiumStoreSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowGrindStoreSignal showGrindStoreSignal { get; set; }

		public override void Execute()
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("DeepLink");
			gameObject.transform.parent = managers.transform;
			gameObject.SetActive(false);
			global::Kampai.UI.View.DeepLinkHandler deepLinkHandler = gameObject.AddComponent<global::Kampai.UI.View.DeepLinkHandler>();
			deepLinkHandler.logger = logger;
			deepLinkHandler.moveBuildMenuSignal = moveBuildMenuSignal;
			deepLinkHandler.showPremiumStoreSignal = showPremiumStoreSignal;
			deepLinkHandler.showGrindStoreSignal = showGrindStoreSignal;
			gameObject.SetActive(true);
		}
	}
}
