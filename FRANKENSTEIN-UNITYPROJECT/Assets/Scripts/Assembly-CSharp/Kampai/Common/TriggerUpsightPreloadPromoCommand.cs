namespace Kampai.Common
{
	public class TriggerUpsightPreloadPromoCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.UpsightPromoTrigger.Placement placement { get; set; }

		[Inject]
		public global::Kampai.Main.IUpsightService upsightService { get; set; }

		public override void Execute()
		{
			if (upsightService.CanLoadContent(placement))
			{
				upsightService.PreloadContentRequest(placement);
			}
		}
	}
}
