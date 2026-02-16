namespace Kampai.Game
{
	public class TikiBarService : global::Kampai.Game.ITikiBarService
	{
		[Inject]
		public global::Kampai.Game.IPlayerService PlayerService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService PrestigeService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger Logger { get; set; }

		public global::Kampai.Game.Prestige GetPrestigeForSeatableCharacter(global::Kampai.Game.Character character)
		{
			global::Kampai.Game.Prestige prestigeFromMinionInstance = PrestigeService.GetPrestigeFromMinionInstance(character);
			if (prestigeFromMinionInstance == null)
			{
				Logger.Warning("Unable to find prestige for character with def id: {0} ", character.Definition.ID);
				return null;
			}
			if (prestigeFromMinionInstance.Definition.ID == 40014)
			{
				Logger.Warning("Ignore prestige for TSM character");
				return null;
			}
			return prestigeFromMinionInstance;
		}

		public bool IsCharacterSitting(global::Kampai.Game.Prestige prestige)
		{
			global::Kampai.Game.TikiBarBuilding firstInstanceByDefinitionId = PlayerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.TikiBarBuilding>(3041);
			if (firstInstanceByDefinitionId == null)
			{
				Logger.Warning("Unable to find tikibar building!");
				return false;
			}
			int minionSlotIndex = firstInstanceByDefinitionId.GetMinionSlotIndex(prestige.Definition.ID);
			return minionSlotIndex >= 0 && minionSlotIndex < 3;
		}

		public bool IsCharacterSitting(global::Kampai.Game.Character character)
		{
			global::Kampai.Game.Prestige prestigeForSeatableCharacter = GetPrestigeForSeatableCharacter(character);
			if (prestigeForSeatableCharacter == null)
			{
				return false;
			}
			return IsCharacterSitting(prestigeForSeatableCharacter);
		}
	}
}
