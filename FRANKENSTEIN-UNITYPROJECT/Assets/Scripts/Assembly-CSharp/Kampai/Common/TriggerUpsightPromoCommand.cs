namespace Kampai.Common
{
	public class TriggerUpsightPromoCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.UpsightPromoTrigger.Placement placement { get; set; }

		[Inject]
		public global::Kampai.Main.IUpsightService upsightService { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Common.ICoppaService coppaService { get; set; }

		public override void Execute()
		{
			if (upsightService.CanLoadContent(placement))
			{
				upsightService.SendContentRequest(placement);
			}
		}
	}
}
