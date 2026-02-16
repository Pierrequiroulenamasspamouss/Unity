namespace Kampai.UI.View
{
	public abstract class AbstractWayFinderMediator : global::strange.extensions.mediation.impl.Mediator
	{
		protected global::Kampai.Game.WayFinderDefinition wayFinderDefinition;

		private global::Kampai.Game.OpenBuildingMenuSignal openBuildingMenuSignal;

		private global::Kampai.Game.View.BuildingManagerView buildingManagerView;

		private global::Kampai.UI.View.ButtonView goToButton;

		private int trackedId;

		public abstract global::Kampai.UI.View.IWayFinderView View { get; }

		[Inject]
		public global::Kampai.UI.IPositionService PositionService { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable GameContext { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner RoutineRunner { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService PlayerService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService PrestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoMoveToInstanceSignal CameraAutoMoveToInstanceSignal { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService LocalizationService { get; set; }

		[Inject]
		public global::Kampai.UI.View.CreateWayFinderSignal CreateWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveWayFinderSignal RemoveWayFinderSignal { get; set; }

		[Inject(global::Kampai.Main.MainElement.UI_GLASSCANVAS)]
		public global::UnityEngine.GameObject GlassCanvas { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger Logger { get; set; }

		[Inject]
		public global::Kampai.Game.ITikiBarService TikiBarService { get; set; }

		[Inject]
		public global::Kampai.Game.IZoomCameraModel ZoomCameraModel { get; set; }

		[Inject]
		public global::Kampai.UI.View.UpdateWayFinderPrioritySignal UpdateWayFinderPrioritySignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService DefinitionService { get; set; }

		public override void OnRegister()
		{
			CameraAutoMoveToInstanceSignal = GameContext.injectionBinder.GetInstance<global::Kampai.Game.CameraAutoMoveToInstanceSignal>();
			openBuildingMenuSignal = GameContext.injectionBinder.GetInstance<global::Kampai.Game.OpenBuildingMenuSignal>();
			buildingManagerView = GameContext.injectionBinder.GetInstance<global::UnityEngine.GameObject>(global::Kampai.Game.GameElement.BUILDING_MANAGER).GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			global::Kampai.UI.View.WayFinderModal component = GetComponent<global::Kampai.UI.View.WayFinderModal>();
			goToButton = component.GoToButton;
			trackedId = component.Settings.TrackedId;
			global::Kampai.UI.View.AbstractWayFinderView abstractWayFinderView = View as global::Kampai.UI.View.AbstractWayFinderView;
			goToButton.ClickedSignal.AddListener(GoToClicked);
			abstractWayFinderView.UpdateWayFinderPrioritySignal.AddListener(UpdateWayFinderPriority);
			abstractWayFinderView.RemoveWayFinderSignal.AddListener(RemoveWayFinder);
			abstractWayFinderView.SimulateClickSignal.AddListener(GoToClicked);
			wayFinderDefinition = DefinitionService.Get<global::Kampai.Game.WayFinderDefinition>(1000008086);
			abstractWayFinderView.Init(PositionService, GameContext, Logger, ZoomCameraModel, TikiBarService, PlayerService, LocalizationService, wayFinderDefinition);
		}

		public override void OnRemove()
		{
			global::Kampai.UI.View.AbstractWayFinderView abstractWayFinderView = View as global::Kampai.UI.View.AbstractWayFinderView;
			abstractWayFinderView.Clear();
			goToButton.ClickedSignal.RemoveListener(GoToClicked);
			abstractWayFinderView.UpdateWayFinderPrioritySignal.RemoveListener(UpdateWayFinderPriority);
			abstractWayFinderView.RemoveWayFinderSignal.RemoveListener(RemoveWayFinder);
			abstractWayFinderView.SimulateClickSignal.RemoveListener(GoToClicked);
		}

		private void RemoveWayFinder()
		{
			RemoveWayFinderSignal.Dispatch(trackedId);
		}

		private void UpdateWayFinderPriority()
		{
			UpdateWayFinderPrioritySignal.Dispatch();
		}

		protected virtual void GoToClicked()
		{
			if (View.IsTargetObjectVisible())
			{
				global::Kampai.Game.Building byInstanceId = PlayerService.GetByInstanceId<global::Kampai.Game.Building>(trackedId);
				if (byInstanceId == null)
				{
					return;
				}
				global::Kampai.Game.View.ScaffoldingBuildingObject scaffoldingBuildingObject = buildingManagerView.GetScaffoldingBuildingObject(trackedId);
				if (scaffoldingBuildingObject != null)
				{
					GameContext.injectionBinder.GetInstance<global::Kampai.Game.RevealBuildingSignal>().Dispatch(byInstanceId);
					return;
				}
				global::Kampai.Game.View.BuildingObject buildingObject = buildingManagerView.GetBuildingObject(trackedId);
				if (buildingObject != null)
				{
					openBuildingMenuSignal.Dispatch(buildingObject, byInstanceId);
				}
			}
			else
			{
				PanToInstance();
			}
		}

		protected virtual void PanToInstance()
		{
			global::Kampai.Game.Instance byInstanceId = PlayerService.GetByInstanceId<global::Kampai.Game.Instance>(trackedId);
			if (byInstanceId != null && !ZoomCameraModel.ZoomedIn)
			{
				CameraAutoMoveToInstanceSignal.Dispatch(new global::Kampai.Game.PanInstructions(byInstanceId));
			}
		}
	}
}
