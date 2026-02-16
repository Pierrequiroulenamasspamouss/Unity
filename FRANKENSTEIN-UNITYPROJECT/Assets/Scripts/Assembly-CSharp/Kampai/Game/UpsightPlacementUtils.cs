namespace Kampai.Game
{
	public static class UpsightPlacementUtils
	{
		public static string GetPlacementId(global::Kampai.Game.UpsightPromoTrigger.Placement placement)
		{
			switch (placement)
			{
			case global::Kampai.Game.UpsightPromoTrigger.Placement.GameLaunch:
				return "game_launch";
			case global::Kampai.Game.UpsightPromoTrigger.Placement.Mailbox:
				return "mailbox";
			default:
				return string.Empty;
			}
		}

		public static global::Kampai.Game.UpsightPromoTrigger.Placement GetPlacement(string placementId)
		{
			switch (placementId)
			{
			case "game_launch":
				return global::Kampai.Game.UpsightPromoTrigger.Placement.GameLaunch;
			case "mailbox":
				return global::Kampai.Game.UpsightPromoTrigger.Placement.Mailbox;
			default:
				return global::Kampai.Game.UpsightPromoTrigger.Placement.Unknown;
			}
		}
	}
}
