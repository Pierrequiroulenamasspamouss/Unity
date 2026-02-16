namespace Kampai.UI.View
{
	public class FloatingTextMediator : global::strange.extensions.mediation.impl.Mediator
	{
		[Inject]
		public global::Kampai.UI.View.FloatingTextView view { get; set; }

		[Inject]
		public global::Kampai.UI.IPositionService positionService { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localizationService { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveFloatingTextSignal removeFloatingTextSignal { get; set; }

		public override void OnRegister()
		{
			view.Init(positionService, gameContext, logger, playerService, localizationService);
			view.OnRemoveSignal.AddListener(OnRemoveFloatingText);
		}

		public override void OnRemove()
		{
			view.OnRemoveSignal.RemoveListener(OnRemoveFloatingText);
		}

		private void OnRemoveFloatingText()
		{
			removeFloatingTextSignal.Dispatch(view.TrackedId);
		}
	}
}
