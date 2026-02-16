namespace Kampai.Game
{
	public class SetupMailboxPromoCommand : global::strange.extensions.command.impl.Command
	{
		private global::Kampai.Main.TriggerUpsightPreloadPromoSignal triggerUpsightPreloadPromoSignal;

		private global::Kampai.Game.UpsightPromoTrigger.Placement placement;

		[Inject(global::Kampai.Main.MainElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable MainContext { get; set; }

		[Inject]
		public global::Kampai.Main.IUpsightService UpsightService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner RoutineRunner { get; set; }

		[Inject]
		public global::Kampai.UI.IMailboxIconService MailboxService { get; set; }

		[Inject]
		public global::Kampai.Main.UpsightContentPreloadedSignal UpsightContentPreloadedSignal { get; set; }

		public override void Execute()
		{
			placement = global::Kampai.Game.UpsightPromoTrigger.Placement.Mailbox;
			triggerUpsightPreloadPromoSignal = MainContext.injectionBinder.GetInstance<global::Kampai.Main.TriggerUpsightPreloadPromoSignal>();
			UpsightContentPreloadedSignal.AddListener(OnContentPreloaded);
			if (UpsightService.CanLoadContent(placement))
			{
				RoutineRunner.StartCoroutine(PreloadContent());
			}
		}

		private void OnContentPreloaded(global::Kampai.Game.UpsightPromoTrigger.Placement preloadedPlacement)
		{
			if (preloadedPlacement == placement)
			{
				MailboxService.CreateMailboxIcon();
			}
		}

		private global::System.Collections.IEnumerator PreloadContent()
		{
			while (true)
			{
				if (!UpsightService.HasPreloadedContent(placement))
				{
					triggerUpsightPreloadPromoSignal.Dispatch(placement);
					MailboxService.RemoveMailboxIcon();
				}
				else
				{
					MailboxService.CreateMailboxIcon();
				}
				yield return new global::UnityEngine.WaitForSeconds(MailboxService.GetRefreshFrequencyInSeconds());
			}
		}
	}
}
