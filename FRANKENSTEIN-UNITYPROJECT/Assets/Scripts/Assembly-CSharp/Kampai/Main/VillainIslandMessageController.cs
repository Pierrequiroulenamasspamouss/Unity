namespace Kampai.Main
{
	public class VillainIslandMessageController : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public bool showMessage { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			int prestigeDefinitionId = 40004;
			global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(prestigeDefinitionId, false);
			int quantity = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			int preUnlockLevel = (int)definitionService.Get<global::Kampai.Game.PrestigeDefinition>(40004).PreUnlockLevel;
			if (quantity < preUnlockLevel)
			{
				if (showMessage)
				{
					popupMessageSignal.Dispatch(localService.GetString("AspirationalMessageVillainIsland", preUnlockLevel));
				}
			}
			else if (prestige != null && (prestige.CurrentPrestigeLevel > 0 || prestige.state != global::Kampai.Game.PrestigeState.Prestige))
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find("Terrain_VillainIsland");
				if (gameObject != null)
				{
					global::Kampai.Game.VillainIslandLocation component = gameObject.GetComponent<global::Kampai.Game.VillainIslandLocation>();
					if (component != null)
					{
						component.removeAllColliders();
					}
					else
					{
						logger.Log(global::Kampai.Util.Logger.Level.Error, "Could not remove colliders from Villain Island: VillainIslandLocation script not found.");
					}
				}
				else
				{
					logger.Log(global::Kampai.Util.Logger.Level.Error, "Could not remove colliders from Villain Island: prefab Terrain_VillainIsland could not be found.");
				}
			}
			else if (showMessage)
			{
				popupMessageSignal.Dispatch(localService.GetString("UnlockKevinForVillainIsland", preUnlockLevel));
			}
		}
	}
}
