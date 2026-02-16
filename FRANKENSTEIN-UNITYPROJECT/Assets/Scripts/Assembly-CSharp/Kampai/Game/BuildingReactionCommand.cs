namespace Kampai.Game
{
	public class BuildingReactionCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.Boxed<global::UnityEngine.Vector3> buildingPos { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.MinionReactSignal reactSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PhilCelebrateSignal philSignal { get; set; }

		public override void Execute()
		{
			global::System.Collections.Generic.ICollection<global::Kampai.Game.Minion> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Minion>();
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
			foreach (global::Kampai.Game.Minion item in instancesByType)
			{
				if (item.State == global::Kampai.Game.MinionState.Idle)
				{
					list.Add(item.ID);
				}
			}
			reactSignal.Dispatch(list, buildingPos);
			philSignal.Dispatch();
		}
	}
}
