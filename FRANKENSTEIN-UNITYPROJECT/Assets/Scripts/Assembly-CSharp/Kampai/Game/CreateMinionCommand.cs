namespace Kampai.Game
{
	public class CreateMinionCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Minion minion { get; set; }

		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject]
		public global::Kampai.Util.IMinionBuilder minionBuilder { get; set; }

		[Inject]
		public global::Kampai.Util.PathFinder pathFinder { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.InitCharacterObjectSignal initSignal { get; set; }

		[Inject]
		public global::Kampai.Game.AddMinionSignal addMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetPartyStatesSignal setPartyStateSignal { get; set; }

		public override void Execute()
		{
			int costumeID = minion.CostumeID;
			global::Kampai.Game.CostumeItemDefinition costumeItemDefinition = definitionService.Get<global::Kampai.Game.CostumeItemDefinition>(99);
			if (costumeItemDefinition == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.PS_MISSING_DEFAULT_COSTUME, "ERROR: Minion costume ID: {0} - Could not create default costume!!!", costumeID);
			}
			string baseFBX = costumeItemDefinition.baseFBX;
			if (costumeID > 0 && costumeID != 99)
			{
				global::Kampai.Game.CostumeItemDefinition costumeItemDefinition2 = definitionService.Get<global::Kampai.Game.CostumeItemDefinition>(costumeID);
				if (costumeItemDefinition2 != null)
				{
					baseFBX = costumeItemDefinition2.baseFBX;
				}
				else
				{
					logger.Warning("Minion costume ID: {0} is not a costume item, Reverting back to generic minion", costumeID);
				}
			}
			else
			{
				minion.CostumeID = 99;
			}
			global::Kampai.Game.View.MinionObject minionObject = minionBuilder.BuildMinion(baseFBX, "asm_minion_movement");
			initSignal.Dispatch(minionObject, minion);
			minionObject.transform.parent = minionManager.transform;
			minionObject.transform.position = pathFinder.RandomPosition(minion.Partying);
			addMinionSignal.Dispatch(minionObject);
		}
	}
}
