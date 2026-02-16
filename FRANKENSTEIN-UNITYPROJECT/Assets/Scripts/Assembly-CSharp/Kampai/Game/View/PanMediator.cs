namespace Kampai.Game.View
{
	public class PanMediator : global::strange.extensions.mediation.impl.Mediator, global::Kampai.Game.View.CameraMediator
	{
		protected bool blocked;

		protected float previousFraction;

		protected int toReenable;

		protected bool isAutoPanning;

		[Inject]
		public global::Kampai.Game.ICameraControlsService cameraControlsService { get; set; }

		[Inject]
		public global::Kampai.Game.DisableCameraBehaviourSignal disableCameraSignal { get; set; }

		[Inject]
		public global::Kampai.Game.EnableCameraBehaviourSignal enableCameraSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UnitializeCameraSignal unitializeSignal { get; set; }

		[Inject]
		public global::Kampai.Game.View.CameraUtils cameraUtils { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoPanSignal autoPanSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraCinematicPanSignal cinematicPanSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraModel model { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowBuildingDetailMenuSignal showDetailMenuSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowQuestPanelSignal showQuestPanelSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowQuestRewardSignal showQuestRewardSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowProceduralQuestPanelSignal showProceduralQuestSignal { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel pickModel { get; set; }

		[Inject]
		public global::Kampai.Game.CameraAutoPanCompleteSignal cameraAutoPanCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CameraResetPanVelocitySignal cameraResetPanVelocitySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.StopAutopanSignal stopAutopanSignal { get; set; }

		[Inject]
		public global::Kampai.Util.DeviceInformation deviceInformation { get; set; }

		public float Fraction { get; set; }

		public override void OnRegister()
		{
			cameraControlsService.RegisterListener(OnGameInput);
			disableCameraSignal.AddListener(OnDisableBehaviour);
			enableCameraSignal.AddListener(OnEnableBehaviour);
			unitializeSignal.AddListener(Uninitialize);
			autoPanSignal.AddListener(OnAutoPan);
			cinematicPanSignal.AddListener(OnCinematicPan);
			cameraResetPanVelocitySignal.AddListener(OnResetPanVelocity);
			stopAutopanSignal.AddListener(OnStopAutopan);
		}

		public override void OnRemove()
		{
			cameraControlsService.UnregisterListener(OnGameInput);
			disableCameraSignal.RemoveListener(OnDisableBehaviour);
			enableCameraSignal.RemoveListener(OnEnableBehaviour);
			unitializeSignal.RemoveListener(Uninitialize);
			autoPanSignal.RemoveListener(OnAutoPan);
			cinematicPanSignal.RemoveListener(OnCinematicPan);
			cameraResetPanVelocitySignal.RemoveListener(OnResetPanVelocity);
			stopAutopanSignal.RemoveListener(OnStopAutopan);
		}

		private void OnStopAutopan()
		{
			isAutoPanning = false;
		}

		public virtual void OnResetPanVelocity()
		{
		}

		public virtual void OnGameInput(global::UnityEngine.Vector3 position, int input)
		{
		}

		public virtual void Uninitialize()
		{
		}

		public virtual void OnDisableBehaviour(int behaviour)
		{
		}

		public virtual void OnEnableBehaviour(int behaviour)
		{
		}

		public virtual void OnAutoPan(global::UnityEngine.Vector3 panTo, global::Kampai.Game.CameraMovementSettings.Settings modalSettings, global::Kampai.Util.Boxed<global::Kampai.Game.Building> building, global::Kampai.Util.Boxed<global::Kampai.Game.Quest> quest)
		{
			OnCinematicPan(global::Kampai.Util.Tuple.Create(panTo, 0.8f), modalSettings, building, quest);
		}

		public virtual void OnCinematicPan(global::Kampai.Util.Tuple<global::UnityEngine.Vector3, float> panInfo, global::Kampai.Game.CameraMovementSettings.Settings modalSettings, global::Kampai.Util.Boxed<global::Kampai.Game.Building> building, global::Kampai.Util.Boxed<global::Kampai.Game.Quest> quest)
		{
		}

		public virtual void ReenablePickService()
		{
			pickModel.PanningCameraBlocked = false;
		}

		protected void OnComplete()
		{
			cameraAutoPanCompleteSignal.Dispatch();
		}

		public virtual void SetupAutoPan(global::UnityEngine.Vector3 panTo)
		{
		}

		public virtual void PerformAutoPan(float delta)
		{
		}

		public void ShowMenu(global::Kampai.Game.CameraMovementSettings.Settings modalSettings, global::Kampai.Game.Building building, global::Kampai.Game.Quest quest)
		{
			switch (modalSettings)
			{
			case global::Kampai.Game.CameraMovementSettings.Settings.ShowMenu:
				showDetailMenuSignal.Dispatch(building);
				break;
			case global::Kampai.Game.CameraMovementSettings.Settings.Quest:
				if (quest.GetActiveDefinition().SurfaceType == global::Kampai.Game.QuestSurfaceType.ProcedurallyGenerated)
				{
					showProceduralQuestSignal.Dispatch(quest.ID);
				}
				else if (quest.state == global::Kampai.Game.QuestState.Harvestable || quest.state == global::Kampai.Game.QuestState.Complete)
				{
					showQuestRewardSignal.Dispatch(quest.ID);
				}
				else
				{
					showQuestPanelSignal.Dispatch(quest.ID);
				}
				break;
			}
		}

		protected int GetFingerID()
		{
			int num = ((global::UnityEngine.Input.touchCount <= 0) ? (-1) : global::UnityEngine.Input.GetTouch(0).fingerId);
			if (num == -1 && (deviceInformation.IsSamsung() || global::UnityEngine.Application.isEditor) && global::UnityEngine.Input.GetMouseButton(0))
			{
				num = int.MaxValue;
			}
			return num;
		}
	}
}
