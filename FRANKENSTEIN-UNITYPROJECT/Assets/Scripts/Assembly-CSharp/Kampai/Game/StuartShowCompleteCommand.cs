namespace Kampai.Game
{
	public class StuartShowCompleteCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject]
		public global::Kampai.Game.TemporaryMinionsService tempMinionService { get; set; }

		[Inject]
		public global::Kampai.Game.MoveMinionFinishedSignal moveMinionFinishedSignal { get; set; }

		[Inject]
		public global::Kampai.Util.PathFinder pathFinder { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Game.IZoomCameraModel zoomCameraModel { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowHUDSignal showHUDSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowStoreSignal showStoreSignal { get; set; }

		public override void Execute()
		{
			showHUDSignal.Dispatch(true);
			showStoreSignal.Dispatch(true);
			global::System.Collections.Generic.IDictionary<int, global::Kampai.Game.View.MinionObject> temporaryMinions = tempMinionService.getTemporaryMinions();
			if (temporaryMinions.Count <= 0)
			{
				return;
			}
			moveMinionFinishedSignal.AddListener(TemporaryMinionFinishedMoving);
			foreach (global::Kampai.Game.View.MinionObject value in temporaryMinions.Values)
			{
				global::UnityEngine.Vector3 position = value.transform.position;
				global::UnityEngine.Vector3 goalPos = new global::UnityEngine.Vector3(position.x + 4.8f, position.y, position.z);
				bool muteStatus = true;
				global::System.Collections.Generic.IList<global::UnityEngine.Vector3> path = pathFinder.FindPath(position, goalPos, 4, true);
				global::Kampai.Game.View.MinionManagerView component = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
				component.StartPathing(value, path, 4.5f, muteStatus, moveMinionFinishedSignal, 0f);
			}
		}

		private void TemporaryMinionFinishedMoving(int id)
		{
			global::System.Collections.Generic.IDictionary<int, global::Kampai.Game.View.MinionObject> temporaryMinions = tempMinionService.getTemporaryMinions();
			foreach (global::Kampai.Game.View.MinionObject value in temporaryMinions.Values)
			{
				global::UnityEngine.Object.Destroy(value.gameObject);
			}
			temporaryMinions.Clear();
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.BuildingZoomSignal>().Dispatch(new global::Kampai.Game.BuildingZoomSettings(global::Kampai.Game.ZoomType.OUT, zoomCameraModel.LastZoomBuildingType));
			moveMinionFinishedSignal.RemoveListener(TemporaryMinionFinishedMoving);
		}
	}
}
