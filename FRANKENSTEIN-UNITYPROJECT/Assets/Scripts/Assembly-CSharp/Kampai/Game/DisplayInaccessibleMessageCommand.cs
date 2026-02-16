namespace Kampai.Game
{
	public class DisplayInaccessibleMessageCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Building hitBuilding { get; set; }

		[Inject]
		public global::Kampai.UI.View.PopupMessageSignal popupMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localeService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal globalSFXSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.FountainBuilding fountainBuilding = hitBuilding as global::Kampai.Game.FountainBuilding;
			if (fountainBuilding != null)
			{
				globalSFXSignal.Dispatch("Play_action_locked_01");
				int preUnlockLevel = (int)definitionService.Get<global::Kampai.Game.PrestigeDefinition>(40002).PreUnlockLevel;
				int quantity = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
				if (quantity < preUnlockLevel)
				{
					string aspirationalMessage = fountainBuilding.Definition.AspirationalMessage;
					popupMessageSignal.Dispatch(localeService.GetString(aspirationalMessage, preUnlockLevel));
				}
				else
				{
					string aspirationalMessageLevelReached = fountainBuilding.Definition.AspirationalMessageLevelReached;
					popupMessageSignal.Dispatch(localeService.GetString(aspirationalMessageLevelReached));
				}
			}
			else
			{
				global::Kampai.Game.StageBuilding stageBuilding = hitBuilding as global::Kampai.Game.StageBuilding;
				if (stageBuilding != null)
				{
					popupMessageSignal.Dispatch(localeService.GetString(stageBuilding.Definition.AspirationalMessage));
				}
			}
		}
	}
}
