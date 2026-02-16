namespace Kampai.UI.View
{
	public abstract class DooberCommand : global::strange.extensions.pool.api.IPoolable, global::Kampai.Util.IFastPooledCommandBase
	{
		protected global::UnityEngine.Vector3 iconPosition;

		protected bool fromWorldCanvas;

		private int minSquiggle = 50;

		private int maxSquiggle = 100;

		// [Inject(global::Kampai.Main.MainElement.UI_GLASSCANVAS)]
		public global::UnityEngine.GameObject glassCanvas { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CAMERA)]
		public global::UnityEngine.Camera UICamera { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetXPSignal setXPSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetGrindCurrencySignal setGrindCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetStorageCapacitySignal setStorageSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FireXPVFXSignal fireXpSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FireGrindVFXSignal fireGrindSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.FirePremiumVFXSignal firePremiumSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SpawnDooberModel dooberModel { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.DoobersFlownSignal doobersFlownSignal { get; set; }

		public bool retain { get; private set; }

		public global::Kampai.Util.FastCommandPool commandPool { get; set; }

		protected void TweenToDestination(global::UnityEngine.GameObject go, global::UnityEngine.Vector3 destination, float flyTime, global::Kampai.UI.View.DestinationType tweenType, float delayTime = 0f)
		{
			global::Kampai.UI.View.SpawnDooberModel dooberModel = this.dooberModel;
			dooberModel.DooberCounter++;
			// soundFXSignal.Dispatch("Play_loot_pick_up_01");
			bool isMignette;
			global::UnityEngine.Vector3 worldPositionInUI = GetWorldPositionInUI(tweenType, out isMignette);
			Retain();
			if (!isMignette)
			{
				worldPositionInUI.z = go.transform.position.z;
				global::System.Collections.Generic.List<global::UnityEngine.Vector3> nodes = CreatePath(go.transform.position, worldPositionInUI) as global::System.Collections.Generic.List<global::UnityEngine.Vector3>;
				GoSpline path = new GoSpline(nodes);
				global::System.Collections.Generic.List<global::UnityEngine.Vector3> nodes2 = CreatePath(worldPositionInUI, destination) as global::System.Collections.Generic.List<global::UnityEngine.Vector3>;
				GoSpline path2 = new GoSpline(nodes2);
				GoTween tween = new GoTween(go.transform, flyTime, new GoTweenConfig().setDelay(delayTime).setEaseType(GoEaseType.CubicInOut).positionPath(path)
					.scale(2f));
				GoTween tween2 = new GoTween(go.transform, flyTime, new GoTweenConfig().setDelay(delayTime).setEaseType(GoEaseType.QuartIn).positionPath(path2)
					.scale(0.3f)
					.onComplete(delegate(AbstractGoTween thisTween)
					{
						switch (tweenType)
						{
						case global::Kampai.UI.View.DestinationType.GRIND:
							setGrindCurrencySignal.Dispatch();
							fireGrindSignal.Dispatch();
							break;
						case global::Kampai.UI.View.DestinationType.PREMIUM:
							setPremiumCurrencySignal.Dispatch();
							firePremiumSignal.Dispatch();
							break;
						case global::Kampai.UI.View.DestinationType.XP:
							setXPSignal.Dispatch();
							fireXpSignal.Dispatch();
							break;
						case global::Kampai.UI.View.DestinationType.STORAGE:
							setStorageSignal.Dispatch();
							break;
						}
						if (dooberModel.DooberCounter == 1)
						{
							// soundFXSignal.Dispatch("Play_icon_sparkle_01");
						}
						thisTween.destroy();
						global::UnityEngine.Object.Destroy(go);
						dooberModel.DooberCounter--;
						doobersFlownSignal.Dispatch();
						Release();
					}));
				GoTweenFlow goTweenFlow = new GoTweenFlow();
				goTweenFlow.insert(0f, tween).insert(flyTime + 0.5f, tween2);
				goTweenFlow.play();
				return;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Vector3> nodes3 = CreatePath(go.transform.position, destination) as global::System.Collections.Generic.List<global::UnityEngine.Vector3>;
			GoSpline path3 = new GoSpline(nodes3);
			Go.to(go.transform, flyTime, new GoTweenConfig().setDelay(delayTime).setEaseType(GoEaseType.CubicInOut).positionPath(path3)
				.scale(0.8f)
				.onComplete(delegate(AbstractGoTween thisTween)
				{
					if (dooberModel.DooberCounter == 1)
					{
						// soundFXSignal.Dispatch("Play_icon_sparkle_01");
					}
					thisTween.destroy();
					global::UnityEngine.Object.Destroy(go);
					dooberModel.DooberCounter--;
					Release();
				}));
		}

		private global::UnityEngine.Vector3 GetWorldPositionInUI(global::Kampai.UI.View.DestinationType tweenType, out bool isMignette)
		{
			global::UnityEngine.Vector3 result = global::UnityEngine.Vector3.zero;
			isMignette = false;
			switch (tweenType)
			{
			case global::Kampai.UI.View.DestinationType.XP:
				result = UICamera.ViewportToWorldPoint(dooberModel.expScreenPosition);
				break;
			case global::Kampai.UI.View.DestinationType.PREMIUM:
				result = UICamera.ViewportToWorldPoint(dooberModel.premiumScreenPosition);
				break;
			case global::Kampai.UI.View.DestinationType.GRIND:
				result = UICamera.ViewportToWorldPoint(dooberModel.grindScreenPosition);
				break;
			case global::Kampai.UI.View.DestinationType.STORAGE:
			case global::Kampai.UI.View.DestinationType.STICKER:
				result = UICamera.ViewportToWorldPoint(dooberModel.itemScreenPosition);
				break;
			case global::Kampai.UI.View.DestinationType.MIGNETTE:
				isMignette = true;
				break;
			}
			return result;
		}

		protected global::System.Collections.Generic.IList<global::UnityEngine.Vector3> CreatePath(global::UnityEngine.Vector3 start, global::UnityEngine.Vector3 destination)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Vector3> list = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>();
			global::UnityEngine.Vector3 vector = default(global::UnityEngine.Vector3);
			global::UnityEngine.Vector3 vector2 = default(global::UnityEngine.Vector3);
			vector = start - 0.3f * (start - destination);
			vector2 = start - 0.6f * (start - destination);
			global::System.Random random = new global::System.Random();
			if (random.Next(2) == 0)
			{
				vector.x += (float)random.Next(minSquiggle, maxSquiggle) / 25f;
				vector2.x -= (float)random.Next(minSquiggle, maxSquiggle) / 25f;
			}
			else
			{
				vector.x -= (float)random.Next(minSquiggle, maxSquiggle) / 25f;
				vector2.x += (float)random.Next(minSquiggle, maxSquiggle) / 25f;
			}
			list.Add(start);
			list.Add(vector);
			list.Add(vector2);
			list.Add(destination);
			return list;
		}

		protected global::UnityEngine.Vector2 GetScreenStartPosition()
		{
			global::UnityEngine.Vector2 vector = ((iconPosition == global::UnityEngine.Vector3.zero) ? ((global::UnityEngine.Vector2)UICamera.ViewportToWorldPoint(dooberModel.defaultDooberSpawnLocation)) : ((!fromWorldCanvas) ? ((global::UnityEngine.Vector2)iconPosition) : ((global::UnityEngine.Vector2)global::UnityEngine.Camera.main.WorldToScreenPoint(iconPosition))));
			return vector / UIUtils.GetHeightScale();
		}

		public void Restore()
		{
		}

		public void Retain()
		{
			retain = true;
		}

		public void Release()
		{
			retain = false;
			if (commandPool != null)
			{
				commandPool.ReturnToPool(this);
			}
		}
	}
}
