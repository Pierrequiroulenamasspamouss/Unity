namespace Kampai.Game
{
	public class DisplaySaleCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.SaleDefinition saleDefinition { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public override void Execute()
		{
			guiService.Execute(global::Kampai.UI.View.GUIOperation.Load, "popup_Sale");
			routineRunner.StartCoroutine(NotifyMediator(saleDefinition));
		}

		private global::System.Collections.IEnumerator NotifyMediator(global::Kampai.Game.SaleDefinition sale)
		{
			yield return null;
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.InitializeSaleSignal>().Dispatch(sale);
		}
	}
}
