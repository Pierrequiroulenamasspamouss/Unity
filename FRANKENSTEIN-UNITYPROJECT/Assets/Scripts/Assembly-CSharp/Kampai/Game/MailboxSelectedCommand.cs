namespace Kampai.Game
{
	public class MailboxSelectedCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::Kampai.Main.MainElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable MainContext { get; set; }

		[Inject]
		public global::Kampai.Main.IUpsightService UpsightService { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal PopupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService LocalizationService { get; set; }

		[Inject]
		public global::Kampai.UI.IMailboxIconService MailboxService { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.UpsightPromoTrigger.Placement placement = global::Kampai.Game.UpsightPromoTrigger.Placement.Mailbox;
			if (UpsightService.HasPreloadedContent(placement))
			{
				MainContext.injectionBinder.GetInstance<global::Kampai.Main.TriggerUpsightPromoSignal>().Dispatch(placement);
			}
			else
			{
				PopupMessageSignal.Dispatch(LocalizationService.GetString("NoUpsightContentAvailable"));
			}
			MailboxService.RemoveMailboxIcon();
		}
	}
}
