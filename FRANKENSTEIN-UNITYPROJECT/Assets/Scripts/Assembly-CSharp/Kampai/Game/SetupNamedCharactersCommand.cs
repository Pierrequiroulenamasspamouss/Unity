namespace Kampai.Game
{
	public class SetupNamedCharactersCommand : global::strange.extensions.command.impl.Command
	{
		private global::System.Collections.Generic.ICollection<global::Kampai.Game.NamedCharacter> namedCharacters;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService player { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randomService { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreNamedCharacterSignal restoreNamedCharacterSignal { get; set; }

		[Inject]
		public global::Kampai.Game.RestoreVillainSignal restoreVillainSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Util.TimeProfiler.StartSection("named characters");
			namedCharacters = player.GetInstancesByType<global::Kampai.Game.NamedCharacter>();
			global::System.Collections.Generic.List<global::Kampai.Game.NamedCharacter> list = new global::System.Collections.Generic.List<global::Kampai.Game.NamedCharacter>();
			foreach (global::Kampai.Game.NamedCharacter namedCharacter in namedCharacters)
			{
				global::Kampai.Game.Prestige prestige = GetPrestige(namedCharacter);
				if (prestige == null)
				{
					continue;
				}
				global::Kampai.Game.PrestigeState state = prestige.state;
				if (namedCharacter.Definition.Type == global::Kampai.Game.NamedCharacterType.VILLAIN)
				{
					if (prestige.state == global::Kampai.Game.PrestigeState.Questing)
					{
						restoreVillainSignal.Dispatch(namedCharacter, false);
					}
					else if (state == global::Kampai.Game.PrestigeState.Taskable || prestige.CurrentPrestigeLevel > 0)
					{
						list.Add(namedCharacter);
					}
				}
				else
				{
					restoreNamedCharacterSignal.Dispatch(namedCharacter, prestige);
				}
			}
			RestoreVillains(list);
			global::Kampai.Util.TimeProfiler.EndSection("named characters");
		}

		private global::Kampai.Game.Prestige GetPrestige(global::Kampai.Game.NamedCharacter namedCharacter)
		{
			global::Kampai.Game.Prestige prestigeFromMinionInstance = prestigeService.GetPrestigeFromMinionInstance(namedCharacter);
			if (prestigeFromMinionInstance == null && !FixPrestige(namedCharacter))
			{
				logger.Fatal(global::Kampai.Util.FatalCode.CMD_RESTORE_NAMED_CHARACTER_NO_PRESTIGE);
			}
			return prestigeFromMinionInstance;
		}

		private void RestoreVillains(global::System.Collections.Generic.List<global::Kampai.Game.NamedCharacter> villains)
		{
			global::Kampai.Util.ListUtil.Shuffle(randomService, villains);
			int i = 0;
			for (int count = villains.Count; i < count; i++)
			{
				global::Kampai.Game.NamedCharacter type = villains[i];
				restoreVillainSignal.Dispatch(type, true);
			}
		}

		private bool FixPrestige(global::Kampai.Game.NamedCharacter affectedCharacter)
		{
			int iD = affectedCharacter.Definition.ID;
			foreach (global::Kampai.Game.NamedCharacter namedCharacter in namedCharacters)
			{
				if (namedCharacter == affectedCharacter || namedCharacter.Definition.ID != iD)
				{
					continue;
				}
				player.Remove(affectedCharacter);
				return true;
			}
			return false;
		}
	}
}
