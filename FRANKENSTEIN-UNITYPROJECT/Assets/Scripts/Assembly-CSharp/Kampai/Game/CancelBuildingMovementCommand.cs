namespace Kampai.Game
{
	public class CancelBuildingMovementCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public bool InvalidLocation { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel pickControllerModel { get; set; }

		[Inject]
		public global::Kampai.Game.CancelPurchaseSignal cancelPurchaseSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetBuildingPositionSignal setBuildingPositionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DeselectBuildingSignal deselectBuildingSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		public override void Execute()
		{
			if (pickControllerModel.SelectedBuilding.HasValue)
			{
				if (pickControllerModel.SelectedBuilding == -1)
				{
					cancelPurchaseSignal.Dispatch(InvalidLocation);
				}
				else
				{
					global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(pickControllerModel.SelectedBuilding.Value);
					setBuildingPositionSignal.Dispatch(byInstanceId.ID, new global::UnityEngine.Vector3(byInstanceId.Location.x, 0f, byInstanceId.Location.y));
					deselectBuildingSignal.Dispatch(byInstanceId.ID);
				}
				pickControllerModel.SelectedBuilding = null;
			}
		}
	}
}
