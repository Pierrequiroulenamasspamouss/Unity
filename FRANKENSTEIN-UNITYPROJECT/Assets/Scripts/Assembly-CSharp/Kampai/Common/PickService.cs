namespace Kampai.Common
{
	public class PickService : global::Kampai.Common.IPickService
	{
		private global::UnityEngine.RaycastHit hit;

		private global::UnityEngine.Vector3 inputPosition;

		private int input;

		[Inject]
		public global::Kampai.Common.PickControllerModel model { get; set; }

		[Inject(global::Kampai.Main.MainElement.CAMERA)]
		public global::UnityEngine.Camera camera { get; set; }

		[Inject]
		public global::Kampai.Game.ICameraControlsService cameraControlsService { get; set; }

		[Inject]
		public global::Kampai.Game.ShowHiddenBuildingsSignal showHiddenBuildingsSignal { get; set; }

		[Inject]
		public global::Kampai.Common.MinionPickSignal minionSignal { get; set; }

		[Inject]
		public global::Kampai.Common.EnvironmentalMignetteTappedSignal environmentalMignetteTappedSignal { get; set; }

		[Inject]
		public global::Kampai.Common.BuildingPickSignal buildingSignal { get; set; }

		[Inject]
		public global::Kampai.Common.DeselectPickSignal deselectSignal { get; set; }

		[Inject]
		public global::Kampai.Common.MagnetFingerPickSignal magnetFingerSignal { get; set; }

		[Inject]
		public global::Kampai.Common.DragAndDropPickSignal dragAndDropSignal { get; set; }

		[Inject]
		public global::Kampai.Common.LandExpansionPickSignal landExpansionSignal { get; set; }

		[Inject]
		public global::Kampai.Common.DoubleClickPickSignal doubleClickSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Common.Service.HealthMetrics.ITapEventMetricsService tapEventMetricsService { get; set; }

		[Inject]
		public global::Kampai.Common.TikiBarViewPickSignal tikiBarViewPickSignal { get; set; }

		[Inject]
		public global::Kampai.Common.DeselectAllMinionsSignal deselectAllMinionsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IZoomCameraModel zoomCameraModel { get; set; }

		[Inject]
		public global::Kampai.Game.StuartShowCompleteSignal showCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.StuartGetOnStageImmediateSignal stuartGetOnStageImmediateSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.MoveBuildMenuSignal moveBuildMenuSignal { get; set; }

		[Inject]
		public global::Kampai.Common.VillainIslandMessageSignal villainIslandMessageSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimedSocialEventService timedSocialEventService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingZoomSignal buildingZoomSignal { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.MignetteGameModel mignetteGameModel { get; set; }

		public void OnGameInput(global::UnityEngine.Vector3 inputPosition, int input, bool pressed)
		{
			if (model.Enabled && !model.PanningCameraBlocked)
			{
				this.inputPosition = inputPosition;
				this.input = input;
				if (model.CameraControlEnabled && (input == 0 || (!model.InvalidateMovement && model.DetectedMovement)))
				{
					cameraControlsService.OnGameInput(inputPosition, input);
				}
				if (pressed && !model.InvalidateMovement && !zoomCameraModel.ZoomedIn)
				{
					showHiddenBuildingsSignal.Dispatch();
				}
				if (model.CurrentMode == global::Kampai.Common.PickControllerModel.Mode.DragAndDrop && model.InvalidateMovement)
				{
					model.InvalidateMovement = false;
				}
				if (!model.PreviousPressState && pressed && !model.InvalidateMovement && model.CurrentMode == global::Kampai.Common.PickControllerModel.Mode.None)
				{
					TouchStart();
				}
				else if (model.PreviousPressState && pressed && !model.InvalidateMovement)
				{
					TouchHold();
				}
				else if (model.PreviousPressState && !pressed)
				{
					EndTouch();
				}
				model.PreviousPressState = pressed;
				if ((input & 2) != 0 && zoomCameraModel.ZoomedIn)
				{
					ZoomOutOfBuilding();
				}
			}
		}

		private void EndTouch()
		{
			if (!model.DetectedMovement)
			{
				float num = global::UnityEngine.Mathf.Abs(inputPosition.magnitude - model.StartTouchPosition.magnitude);
				if (num >= model.maxTouchDelta)
				{
					model.DetectedMovement = true;
				}
				else if (!model.InvalidateMovement)
				{
					moveBuildMenuSignal.Dispatch(false);
				}
			}
			bool autorotateToLandscapeRight = (global::UnityEngine.Screen.autorotateToLandscapeLeft = global::Kampai.Util.Native.AutorotationIsOSAllowed());
			global::UnityEngine.Screen.autorotateToLandscapeRight = autorotateToLandscapeRight;
			if (model.CurrentMode != global::Kampai.Common.PickControllerModel.Mode.Building && model.CurrentMode != global::Kampai.Common.PickControllerModel.Mode.Minion && model.CurrentMode != global::Kampai.Common.PickControllerModel.Mode.LandExpansion)
			{
				if (model.CurrentMode == global::Kampai.Common.PickControllerModel.Mode.MagnetFinger || model.CurrentMode == global::Kampai.Common.PickControllerModel.Mode.DragAndDrop)
				{
					TouchEnd();
				}
				else
				{
					SingleOrDouble();
				}
			}
			else
			{
				TouchEnd();
			}
		}

		private void SingleOrDouble()
		{
			if (!model.WaitingForDouble)
			{
				model.WaitingForDouble = true;
				routineRunner.StartCoroutine(EndClick());
			}
			if (global::UnityEngine.Time.time - model.LastClickTime < 0.2f)
			{
				if (model.InvalidateMovement)
				{
					return;
				}
				doubleClickSignal.Dispatch();
				model.WaitingForDouble = false;
				tapEventMetricsService.Mark();
			}
			model.LastClickTime = global::UnityEngine.Time.time;
		}

		private global::System.Collections.IEnumerator EndClick()
		{
			global::UnityEngine.Vector3 firstTouchPosition = inputPosition;
			yield return new global::UnityEngine.WaitForSeconds(0.2f);
			if (model.WaitingForDouble)
			{
				inputPosition = firstTouchPosition;
				TouchEnd();
				model.WaitingForDouble = false;
			}
		}

		private void Reset()
		{
			model.StartHitObject = null;
			model.EndHitObject = null;
			model.InvalidateMovement = false;
			model.Blocked = false;
			model.HeldTimer = 0f;
			model.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.None;
			model.DetectedMovement = false;
			model.ValidLocation = true;
		}

		private void TouchStart()
		{
			global::UnityEngine.Screen.autorotateToLandscapeLeft = false;
			global::UnityEngine.Screen.autorotateToLandscapeRight = false;
			model.StartTouchPosition = inputPosition;
			model.StartTouchTimeMs = global::UnityEngine.Time.time;
			global::UnityEngine.Ray ray = camera.ScreenPointToRay(inputPosition);
			if (global::UnityEngine.Physics.Raycast(ray, out hit))
			{
				model.StartHitObject = hit.collider.gameObject;
			}
			if (zoomCameraModel.ZoomedIn)
			{
				if (zoomCameraModel.LastZoomBuildingType == global::Kampai.Game.BuildingZoomType.TIKIBAR)
				{
					model.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.TikiBarView;
					return;
				}
				if (zoomCameraModel.LastZoomBuildingType == global::Kampai.Game.BuildingZoomType.STAGE)
				{
					model.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.StageView;
					return;
				}
			}
			if (!(model.StartHitObject != null))
			{
				return;
			}
			switch (model.StartHitObject.layer)
			{
			case 9:
			case 14:
				TouchStartHitBuilding();
				break;
			case 8:
				model.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.Minion;
				break;
			case 11:
				model.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.EnvironmentalMignette;
				break;
			case 12:
				if (!model.SelectedBuilding.HasValue)
				{
					model.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.LandExpansion;
				}
				break;
			case 15:
				model.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.VillainIsland;
				break;
			case 10:
			case 13:
				break;
			}
		}

		private void TouchHold()
		{
			if (!model.DetectedMovement)
			{
				float num = global::UnityEngine.Mathf.Abs(inputPosition.magnitude - model.StartTouchPosition.magnitude);
				if (num >= model.maxTouchDelta)
				{
					model.DetectedMovement = true;
					if (model.CameraControlEnabled)
					{
						cameraControlsService.OnGameInput(inputPosition, input);
					}
				}
			}
			if (!model.Blocked)
			{
				model.Blocked = true;
			}
			model.HeldTimer += global::UnityEngine.Time.deltaTime;
			int type = 2;
			switch (model.CurrentMode)
			{
			case global::Kampai.Common.PickControllerModel.Mode.TikiBarView:
			case global::Kampai.Common.PickControllerModel.Mode.StageView:
				if (model.DetectedMovement)
				{
					ZoomOutOfBuilding();
				}
				break;
			case global::Kampai.Common.PickControllerModel.Mode.Building:
				buildingSignal.Dispatch(type, inputPosition);
				break;
			case global::Kampai.Common.PickControllerModel.Mode.DragAndDrop:
				dragAndDropSignal.Dispatch(type, inputPosition);
				break;
			case global::Kampai.Common.PickControllerModel.Mode.MagnetFinger:
				if (model.ValidLocation && !model.SelectedBuilding.HasValue)
				{
					magnetFingerSignal.Dispatch(type, inputPosition);
				}
				break;
			case global::Kampai.Common.PickControllerModel.Mode.None:
			case global::Kampai.Common.PickControllerModel.Mode.Minion:
				if (mignetteGameModel.IsMignetteActive)
				{
					model.Enabled = false;
				}
				else
				{
					HandleNoneAndMinion();
				}
				break;
			case global::Kampai.Common.PickControllerModel.Mode.EnvironmentalMignette:
			case global::Kampai.Common.PickControllerModel.Mode.LandExpansion:
				break;
			}
		}

		private void HandleNoneAndMinion()
		{
			if (playerService.GetHighestFtueCompleted() >= 7 && model.HeldTimer > model.MagnetFingerTheshold && !model.DetectedMovement && input == 1 && !model.SelectedBuilding.HasValue)
			{
				model.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.MagnetFinger;
				magnetFingerSignal.Dispatch(1, inputPosition);
				global::UnityEngine.Vector3 groundPosition = gameContext.injectionBinder.GetInstance<global::Kampai.Game.View.CameraUtils>().GroundPlaneRaycast(inputPosition);
				MoveMinion(groundPosition);
			}
		}

		private void TouchEnd()
		{
			deselectAllMinionsSignal.Dispatch();
			if (model.minionMoveIndicator != null)
			{
				global::UnityEngine.Object.Destroy(model.minionMoveIndicator);
			}
			if (!model.InvalidateMovement)
			{
				global::UnityEngine.Ray ray = camera.ScreenPointToRay(inputPosition);
				if (global::UnityEngine.Physics.Raycast(ray, out hit))
				{
					model.EndHitObject = hit.collider.gameObject;
				}
				switch (model.CurrentMode)
				{
				case global::Kampai.Common.PickControllerModel.Mode.Building:
					buildingSignal.Dispatch(3, inputPosition);
					break;
				case global::Kampai.Common.PickControllerModel.Mode.DragAndDrop:
					dragAndDropSignal.Dispatch(3, inputPosition);
					break;
				case global::Kampai.Common.PickControllerModel.Mode.MagnetFinger:
					magnetFingerSignal.Dispatch(3, inputPosition);
					break;
				case global::Kampai.Common.PickControllerModel.Mode.EnvironmentalMignette:
					if (model.EndHitObject != null)
					{
						environmentalMignetteTappedSignal.Dispatch(model.EndHitObject);
					}
					break;
				case global::Kampai.Common.PickControllerModel.Mode.LandExpansion:
					landExpansionSignal.Dispatch(3, inputPosition);
					break;
				case global::Kampai.Common.PickControllerModel.Mode.TikiBarView:
					TikiBarViewClick();
					break;
				case global::Kampai.Common.PickControllerModel.Mode.StageView:
					StageViewClick();
					break;
				case global::Kampai.Common.PickControllerModel.Mode.VillainIsland:
					if (model.EndHitObject != null)
					{
						VillainIslandClick();
					}
					break;
				case global::Kampai.Common.PickControllerModel.Mode.None:
				case global::Kampai.Common.PickControllerModel.Mode.Minion:
					NoneClick();
					break;
				}
			}
			tapEventMetricsService.Mark();
			Reset();
		}

		private void ZoomOutOfBuilding()
		{
			if (zoomCameraModel.ZoomedIn)
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.BuildingZoomSignal>().Dispatch(new global::Kampai.Game.BuildingZoomSettings(global::Kampai.Game.ZoomType.OUT, zoomCameraModel.LastZoomBuildingType));
			}
		}

		private void TikiBarViewClick()
		{
			if (!model.DetectedMovement)
			{
				global::UnityEngine.GameObject endHitObject = model.EndHitObject;
				if (endHitObject != null)
				{
					tikiBarViewPickSignal.Dispatch(endHitObject);
				}
			}
		}

		public void StageRewardSkipConfirmationResponse(bool bAccept)
		{
			if (bAccept)
			{
				stuartGetOnStageImmediateSignal.Dispatch(false);
				timedSocialEventService.setRewardCutscene(false);
				showCompleteSignal.Dispatch();
			}
		}

		private void VillainIslandClick()
		{
			if (!model.DetectedMovement)
			{
				villainIslandMessageSignal.Dispatch(true);
			}
		}

		private void StageViewClick()
		{
			if (timedSocialEventService.isRewardCutscene())
			{
				global::strange.extensions.signal.impl.Signal<bool> signal = new global::strange.extensions.signal.impl.Signal<bool>();
				signal.AddOnce(StageRewardSkipConfirmationResponse);
				global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>> type = new global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>>("socialpartyskiprewardconcertconfirmationtitle", "socialpartyskiprewardconcertconfirmationdescription", "img_char_Min_FeedbackChecklist01", signal);
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.DisplayConfirmationSignal>().Dispatch(type);
			}
			else
			{
				ZoomOutOfBuilding();
			}
		}

		private void NoneClick()
		{
			if (model.DetectedMovement)
			{
				return;
			}
			global::UnityEngine.GameObject startHitObject = model.StartHitObject;
			global::UnityEngine.GameObject endHitObject = model.EndHitObject;
			if (startHitObject != null && endHitObject != null && startHitObject == endHitObject)
			{
				if (model.CurrentMode != global::Kampai.Common.PickControllerModel.Mode.None)
				{
					minionSignal.Dispatch(model.EndHitObject);
				}
				return;
			}
			global::UnityEngine.Vector3 xZProjection = gameContext.injectionBinder.GetInstance<global::Kampai.Game.View.CameraUtils>().GroundPlaneRaycast(inputPosition);
			global::Kampai.Util.Point p = new global::Kampai.Util.Point
			{
				XZProjection = xZProjection
			};
			global::Kampai.Game.Environment instance = gameContext.injectionBinder.GetInstance<global::Kampai.Game.Environment>();
			if (!instance.Contains(p) || !instance.IsWalkable(p.x, p.y))
			{
				model.ValidLocation = false;
			}
		}

		private void MoveMinion(global::UnityEngine.Vector3 groundPosition)
		{
			if (model.MinionMoveToIndicator == null)
			{
				model.MinionMoveToIndicator = global::Kampai.Util.KampaiResources.Load("MinionMoveToIndicator");
			}
			if (model.minionMoveIndicator != null)
			{
				global::UnityEngine.Object.Destroy(model.minionMoveIndicator);
			}
			model.minionMoveIndicator = global::UnityEngine.Object.Instantiate(model.MinionMoveToIndicator) as global::UnityEngine.GameObject;
			model.minionMoveIndicator.transform.position = groundPosition;
		}

		private void TouchStartHitBuilding()
		{
			if (model.SelectedBuilding.HasValue)
			{
				global::UnityEngine.Ray ray = camera.ScreenPointToRay(inputPosition);
				if (global::UnityEngine.Physics.Raycast(ray, out hit, float.PositiveInfinity, 16384))
				{
					model.StartHitObject = hit.collider.gameObject;
				}
				global::Kampai.Game.View.BuildingObject component = model.StartHitObject.GetComponent<global::Kampai.Game.View.BuildingObject>();
				if (component != null)
				{
					int? selectedBuilding = model.SelectedBuilding;
					if (selectedBuilding.GetValueOrDefault() == component.ID && selectedBuilding.HasValue)
					{
						model.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.DragAndDrop;
						dragAndDropSignal.Dispatch(1, inputPosition);
					}
				}
			}
			else
			{
				model.CurrentMode = global::Kampai.Common.PickControllerModel.Mode.Building;
			}
		}

		public void SetIgnoreInstanceInput(int instanceId, bool isIgnored)
		{
			model.SetIgnoreInstance(instanceId, isIgnored);
		}

		public global::Kampai.Common.PickState GetPickState()
		{
			global::Kampai.Common.PickState pickState = new global::Kampai.Common.PickState();
			pickState.MinionsSelected = new global::System.Collections.Generic.List<int>(model.SelectedMinions.Keys);
			return pickState;
		}
	}
}
