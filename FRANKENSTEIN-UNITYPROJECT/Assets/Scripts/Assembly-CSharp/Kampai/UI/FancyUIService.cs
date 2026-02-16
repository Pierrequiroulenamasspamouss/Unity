namespace Kampai.UI
{
	public class FancyUIService : global::Kampai.UI.IFancyUIService
	{
		[Inject]
		public global::Kampai.Util.IDummyCharacterBuilder builder { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randomService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public global::Kampai.Game.View.DummyCharacterObject CreateCharacter(global::Kampai.UI.DummyCharacterType type, global::Kampai.UI.DummyCharacterAnimationState startingState, global::UnityEngine.Transform parent, global::UnityEngine.Vector3 villainScale, global::UnityEngine.Vector3 villainPositionOffset, int prestigeDefinitionID = 0, bool isHighLOD = true, bool adjustMaterial = false)
		{
			global::Kampai.Game.Prestige prestige = null;
			if (prestigeDefinitionID != 0)
			{
				prestige = prestigeService.GetPrestige(prestigeDefinitionID);
			}
			switch (type)
			{
			case global::Kampai.UI.DummyCharacterType.Minion:
			{
				global::System.Collections.Generic.IList<global::Kampai.Game.MinionDefinition> all = definitionService.GetAll<global::Kampai.Game.MinionDefinition>();
				int count = all.Count;
				int index = randomService.NextInt(count);
				global::Kampai.Game.MinionDefinition def = all[index];
				global::Kampai.Game.Minion minion = new global::Kampai.Game.Minion(def);
				if (prestige != null)
				{
					minion.CostumeID = prestige.Definition.TrackedDefinitionID;
				}
				else
				{
					minion.CostumeID = 99;
				}
				global::Kampai.Game.CostumeItemDefinition costumeItemDefinition = definitionService.Get<global::Kampai.Game.CostumeItemDefinition>(minion.CostumeID);
				if (costumeItemDefinition == null)
				{
					logger.Fatal(global::Kampai.Util.FatalCode.PS_MISSING_DEFAULT_COSTUME, "ERROR: Minion costume ID: {0} - Could not create default costume!!!", minion.CostumeID);
				}
				string baseFBX = costumeItemDefinition.baseFBX;
				global::Kampai.Game.View.DummyCharacterObject dummyCharacterObject2 = builder.BuildMinion(minion, baseFBX, costumeItemDefinition.characterUIAnimationDefinition, parent, isHighLOD, villainScale, villainPositionOffset);
				if (adjustMaterial)
				{
					dummyCharacterObject2.SetStenciledShader();
				}
				dummyCharacterObject2.StartingState(startingState);
				return dummyCharacterObject2;
			}
			case global::Kampai.UI.DummyCharacterType.NamedCharacter:
			{
				global::Kampai.Game.NamedCharacterDefinition namedCharacterDefinition = definitionService.Get<global::Kampai.Game.NamedCharacterDefinition>(prestige.Definition.TrackedDefinitionID);
				if (namedCharacterDefinition != null)
				{
					global::Kampai.Game.View.DummyCharacterObject dummyCharacterObject = CreateNamedCharacter(namedCharacterDefinition, parent, villainScale, villainPositionOffset, isHighLOD);
					if (adjustMaterial)
					{
						dummyCharacterObject.SetStenciledShader();
					}
					dummyCharacterObject.StartingState(startingState);
					return dummyCharacterObject;
				}
				break;
			}
			}
			return null;
		}

		public global::Kampai.UI.DummyCharacterType GetCharacterType(int prestigeDefinitionID)
		{
			global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(prestigeDefinitionID);
			global::Kampai.UI.DummyCharacterType result = global::Kampai.UI.DummyCharacterType.Minion;
			if (prestige.Definition.Type == global::Kampai.Game.PrestigeType.Minion)
			{
				global::Kampai.Game.Definition definition = definitionService.Get<global::Kampai.Game.Definition>(prestige.Definition.TrackedDefinitionID);
				if (definition is global::Kampai.Game.NamedCharacterDefinition)
				{
					result = global::Kampai.UI.DummyCharacterType.NamedCharacter;
				}
			}
			else
			{
				result = global::Kampai.UI.DummyCharacterType.NamedCharacter;
			}
			return result;
		}

		private global::Kampai.Game.View.DummyCharacterObject CreateNamedCharacter(global::Kampai.Game.NamedCharacterDefinition namedCharacterDefinition, global::UnityEngine.Transform parent, global::UnityEngine.Vector3 villainScale, global::UnityEngine.Vector3 villainPositionOffset, bool isHighLOD)
		{
			global::Kampai.Game.NamedCharacter namedCharacter = null;
			global::Kampai.Game.PhilCharacterDefinition philCharacterDefinition = namedCharacterDefinition as global::Kampai.Game.PhilCharacterDefinition;
			if (philCharacterDefinition != null)
			{
				namedCharacter = new global::Kampai.Game.PhilCharacter(philCharacterDefinition);
			}
			global::Kampai.Game.BobCharacterDefinition bobCharacterDefinition = namedCharacterDefinition as global::Kampai.Game.BobCharacterDefinition;
			if (bobCharacterDefinition != null)
			{
				namedCharacter = new global::Kampai.Game.BobCharacter(bobCharacterDefinition);
			}
			global::Kampai.Game.KevinCharacterDefinition kevinCharacterDefinition = namedCharacterDefinition as global::Kampai.Game.KevinCharacterDefinition;
			if (kevinCharacterDefinition != null)
			{
				namedCharacter = new global::Kampai.Game.KevinCharacter(kevinCharacterDefinition);
			}
			global::Kampai.Game.StuartCharacterDefinition stuartCharacterDefinition = namedCharacterDefinition as global::Kampai.Game.StuartCharacterDefinition;
			if (stuartCharacterDefinition != null)
			{
				namedCharacter = new global::Kampai.Game.StuartCharacter(stuartCharacterDefinition);
			}
			global::Kampai.Game.VillainDefinition villainDefinition = namedCharacterDefinition as global::Kampai.Game.VillainDefinition;
			if (villainDefinition != null)
			{
				namedCharacter = new global::Kampai.Game.Villain(villainDefinition);
			}
			return builder.BuildNamedChacter(namedCharacter, parent, isHighLOD, villainScale, villainPositionOffset);
		}
	}
}
