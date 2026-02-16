namespace Kampai.Game
{
	public class NamedCharacterRemovedFromTikiBarCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.View.NamedCharacterObject namedCharacterObject { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject]
		public global::Kampai.Game.PointBobLandExpansionSignal pointBobLandExpansionSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.NamedCharacter byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.NamedCharacter>(namedCharacterObject.ID);
			global::Kampai.Game.NamedCharacterDefinition definition = byInstanceId.Definition;
			if (!(byInstanceId is global::Kampai.Game.KevinCharacter) && !(byInstanceId is global::Kampai.Game.StuartCharacter))
			{
				if (byInstanceId is global::Kampai.Game.BobCharacter)
				{
					pointBobLandExpansionSignal.Dispatch();
				}
				else if (definition.Location != null)
				{
					global::Kampai.Game.Location location = definition.Location;
					namedCharacterObject.transform.position = new global::UnityEngine.Vector3(location.x, 0f, location.y);
				}
			}
		}
	}
}
