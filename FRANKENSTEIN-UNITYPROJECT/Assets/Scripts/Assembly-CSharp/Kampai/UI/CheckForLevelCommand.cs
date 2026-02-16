namespace Kampai.UI
{
	public class CheckForLevelCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		public override void Execute()
		{
			int quantity = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.XP_ID);
			int quantity2 = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.XP_TO_LEVEL_UP_ID);
			if (quantity >= quantity2)
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.LevelUpSignal>().Dispatch();
				playerService.AlterQuantity(global::Kampai.Game.StaticItem.XP_ID, -quantity2);
				int quantity3 = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.XP_TO_LEVEL_UP_ID);
				global::Kampai.Game.LevelXPTable levelXPTable = definitionService.Get<global::Kampai.Game.LevelXPTable>(99999);
				int quantity4 = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
				global::System.Collections.Generic.IList<int> xpNeededList = levelXPTable.xpNeededList;
				int amount = ((quantity4 - 1 >= xpNeededList.Count) ? (xpNeededList[xpNeededList.Count - 1] - quantity3) : (xpNeededList[quantity4 - 1] - quantity3));
				playerService.AlterQuantity(global::Kampai.Game.StaticItem.XP_TO_LEVEL_UP_ID, amount);
			}
		}
	}
}
