namespace Kampai.Game
{
	public class RestoreVillainCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.NamedCharacter character { get; set; }

		[Inject]
		public bool onBoat { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.CruiseShipService shipService { get; set; }

		[Inject]
		public global::Kampai.Game.CreateVillainViewSignal createViewSignal { get; set; }

		[Inject]
		public global::Kampai.Game.VillainGotoCabanaSignal gotoCabanaSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.Villain villain = character as global::Kampai.Game.Villain;
			routineRunner.StartCoroutine(MoveVillainAfterFrame(villain));
		}

		private global::System.Collections.IEnumerator MoveVillainAfterFrame(global::Kampai.Game.Villain villain)
		{
			yield return null;
			if (onBoat)
			{
				shipService.MoveIn(villain.ID);
				yield break;
			}
			createViewSignal.Dispatch(villain.ID);
			gotoCabanaSignal.Dispatch(villain.ID, villain.CabanaBuildingId);
		}
	}
}
