namespace Kampai.UI.View
{
	public class FloatingTextPanelMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.UI.View.FloatingTextPanelView view { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.DisplayFloatingTextSignal displayFloatingTextSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveFloatingTextSignal removeFloatingTextSignal { get; set; }

		public override void OnRegister()
		{
			view.Init(logger);
			displayFloatingTextSignal.AddListener(CreateFloatingText);
			removeFloatingTextSignal.AddListener(RemoveFloatingText);
		}

		public override void OnRemove()
		{
			view.Cleanup();
			displayFloatingTextSignal.RemoveListener(CreateFloatingText);
			removeFloatingTextSignal.RemoveListener(RemoveFloatingText);
		}

		private void CreateFloatingText(global::Kampai.UI.View.FloatingTextSettings settings)
		{
			view.CreateFloatingText(settings);
		}

		private void RemoveFloatingText(int trackedId)
		{
			view.RemoveFloatingText(trackedId);
		}
	}
}
