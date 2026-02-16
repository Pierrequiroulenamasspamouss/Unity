namespace Kampai.Game
{
	public class MinionStateChangeCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int minionID { get; set; }

		[Inject]
		public global::Kampai.Game.MinionState state { get; set; }

		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject]
		public global::Kampai.Common.MagnetFingerIndicatorSelectSignal selectMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Common.MagnetFingerIndicatorDeselectSignal deselectMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService player { get; set; }

		[Inject]
		public global::Kampai.Game.PartySignal partySignal { get; set; }

		[Inject]
		public global::Kampai.Game.IdleMinionSignal idleSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.Minion byInstanceId = player.GetByInstanceId<global::Kampai.Game.Minion>(minionID);
			if (byInstanceId == null)
			{
				return;
			}
			global::Kampai.Game.View.MinionManagerView component = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
			global::UnityEngine.GameObject gameObject = component.GetGameObject(minionID);
			int availableMinionsCount = component.GetAvailableMinionsCount();
			if (byInstanceId.State != global::Kampai.Game.MinionState.Selected && state == global::Kampai.Game.MinionState.Selected)
			{
				selectMinionSignal.Dispatch(availableMinionsCount);
			}
			else if (byInstanceId.State == global::Kampai.Game.MinionState.Selected && state != global::Kampai.Game.MinionState.Selected)
			{
				deselectMinionSignal.Dispatch(availableMinionsCount);
			}
			byInstanceId.State = state;
			byInstanceId.Partying = false;
			byInstanceId.IsInIncidental = false;
			global::UnityEngine.Collider collider = gameObject.collider;
			if (collider != null)
			{
				if (state == global::Kampai.Game.MinionState.Tasking || state == global::Kampai.Game.MinionState.PlayingMignette)
				{
					collider.enabled = false;
				}
				else
				{
					collider.enabled = true;
				}
			}
			partySignal.Dispatch();
			if (state == global::Kampai.Game.MinionState.Idle)
			{
				idleSignal.Dispatch();
			}
		}
	}
}
