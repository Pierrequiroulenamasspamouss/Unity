namespace Kampai.Game.View
{
	public class TouchPanMediator : global::Kampai.Game.View.PanMediator
	{
		private int curFingerID = -1;

		[Inject]
		public global::Kampai.Game.View.TouchPanView view { get; set; }

		public override void OnResetPanVelocity()
		{
			view.Velocity = global::UnityEngine.Vector3.zero;
		}

		public override void OnGameInput(global::UnityEngine.Vector3 position, int input)
		{
			if (blocked)
			{
				return;
			}
			if (input == 1)
			{
				if (curFingerID == -1)
				{
					CacheFingerID();
				}
				if (curFingerID != GetFingerID())
				{
					CacheFingerID();
					view.ResetBehaviour();
				}
			}
			if (input == 1)
			{
				view.CalculateBehaviour(position);
			}
			else
			{
				Uninitialize();
			}
			view.PerformBehaviour(base.cameraUtils);
			view.Decay();
		}

		public override void Uninitialize()
		{
			view.ResetBehaviour();
			curFingerID = -1;
		}

		public override void OnDisableBehaviour(int behaviour)
		{
			int num = 1;
			if ((behaviour & num) == num)
			{
				if (!blocked)
				{
					blocked = true;
					Uninitialize();
				}
				if ((base.model.CurrentBehaviours & num) == num)
				{
					base.model.CurrentBehaviours ^= num;
				}
			}
		}

		public override void OnEnableBehaviour(int behaviour)
		{
			int num = 1;
			if ((behaviour & num) == num)
			{
				if (blocked)
				{
					blocked = false;
				}
				if ((base.model.CurrentBehaviours & num) != num)
				{
					base.model.CurrentBehaviours ^= num;
				}
			}
		}

		private void CacheFingerID()
		{
			curFingerID = GetFingerID();
		}

		public override void SetupAutoPan(global::UnityEngine.Vector3 panTo)
		{
			view.SetupAutoPan(panTo);
		}

		public override void PerformAutoPan(float delta)
		{
			view.PerformAutoPan(delta);
		}

		public override void OnCinematicPan(global::Kampai.Util.Tuple<global::UnityEngine.Vector3, float> panInfo, global::Kampai.Game.CameraMovementSettings.Settings modalSettings, global::Kampai.Util.Boxed<global::Kampai.Game.Building> building, global::Kampai.Util.Boxed<global::Kampai.Game.Quest> quest)
		{
			if (isAutoPanning)
			{
				ReenablePickService();
				return;
			}
			global::UnityEngine.Vector3 panTo = panInfo.Item1;
			float item = panInfo.Item2;
			float num = global::UnityEngine.Vector3.Distance(new global::UnityEngine.Vector3(base.transform.position.x, 0f, base.transform.position.z), panTo);
			if (num <= 1f)
			{
				ShowMenu(modalSettings, building.Value, quest.Value);
				ReenablePickService();
				OnComplete();
				return;
			}
			Go.to(this, item, new GoTweenConfig().floatProp("Fraction", 1f).setEaseType(GoEaseType.SineOut).setUpdateType(GoUpdateType.Update)
				.onBegin(delegate
				{
					isAutoPanning = true;
					toReenable = base.model.CurrentBehaviours;
					base.disableCameraSignal.Dispatch(base.model.CurrentBehaviours);
					previousFraction = base.Fraction;
					SetupAutoPan(panTo);
				})
				.onUpdate(delegate
				{
					if (isAutoPanning)
					{
						float delta = base.Fraction - previousFraction;
						PerformAutoPan(delta);
						previousFraction = base.Fraction;
					}
					else
					{
						Go.killAllTweensWithTarget(this);
						base.enableCameraSignal.Dispatch(toReenable);
						base.Fraction = 0f;
						ReenablePickService();
					}
				})
				.onComplete(delegate
				{
					isAutoPanning = false;
					base.enableCameraSignal.Dispatch(toReenable);
					base.Fraction = 0f;
					if (building.Value != null || quest.Value != null)
					{
						ShowMenu(modalSettings, building.Value, quest.Value);
					}
					ReenablePickService();
					OnComplete();
				}));
		}
	}
}
