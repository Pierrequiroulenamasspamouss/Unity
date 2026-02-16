namespace Kampai.Game
{
	public class UpsightPromoTrigger
	{
		public enum Placement
		{
			Unknown = 0,
			GameLaunch = 1,
			Mailbox = 2
		}

		public global::Kampai.Game.UpsightPromoTrigger.Placement placement;

		public bool enabled;
	}
}
