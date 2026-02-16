namespace Kampai.Game.View
{
	public abstract class BuildingObject : global::Kampai.Game.View.BuildingDefinitionObject
	{
		private global::strange.extensions.signal.impl.Signal<int, global::Kampai.Game.View.MinionTaskInfo> updateSignal;

		private readonly global::strange.extensions.signal.impl.Signal stopBuildingAudioInIdleStateSignal = new global::strange.extensions.signal.impl.Signal();

		private global::UnityEngine.Collider minColliderY;

		private global::UnityEngine.Renderer maxRendererY;

		private global::UnityEngine.Color highlightColor = global::UnityEngine.Color.grey;

		private global::UnityEngine.Animation cachedAnimation;

		protected global::Kampai.Game.IDefinitionService definitionService;

		public global::UnityEngine.Vector3 IndicatorPosition
		{
			get
			{
				return GetIndicatorPosition(false);
			}
		}

		public global::UnityEngine.Vector3 ResourceIconPosition
		{
			get
			{
				return GetResourceIconPosition();
			}
		}

		public global::UnityEngine.Vector3 Center
		{
			get
			{
				return GetCenter();
			}
		}

		public global::strange.extensions.signal.impl.Signal StopBuildingAudioInIdleStateSignal
		{
			get
			{
				return stopBuildingAudioInIdleStateSignal;
			}
		}

		protected global::strange.extensions.signal.impl.Signal<int, global::Kampai.Game.View.MinionTaskInfo> GetUpdateSignal()
		{
			if (updateSignal == null)
			{
				updateSignal = base.transform.parent.GetComponent<global::Kampai.Game.View.BuildingManagerView>().updateMinionSignal;
			}
			return updateSignal;
		}

		internal void Highlight(bool enabled)
		{
			global::UnityEngine.Color materialColor = ((!enabled) ? global::UnityEngine.Color.white : highlightColor);
			SetMaterialColor(materialColor);
		}

		internal virtual void Init(global::Kampai.Game.Building building, global::Kampai.Util.ILogger logger, global::System.Collections.Generic.IDictionary<string, global::UnityEngine.RuntimeAnimatorController> controllers, global::Kampai.Game.IDefinitionService definitionService)
		{
			base.logger = logger;
			this.definitionService = definitionService;
			ID = building.ID;
			Init(building.Definition, definitionService);
			UpdateColliderState(building.State);
			cachedAnimation = GetComponent<global::UnityEngine.Animation>();
			if (colliders.Length > 0)
			{
				minColliderY = colliders[0];
				global::UnityEngine.Collider[] array = colliders;
				foreach (global::UnityEngine.Collider collider in array)
				{
					if (collider.bounds.min.y < minColliderY.bounds.min.y)
					{
						minColliderY = collider;
					}
				}
			}
			if (objectRenderers.Length <= 0)
			{
				return;
			}
			maxRendererY = objectRenderers[0];
			global::UnityEngine.Renderer[] array2 = objectRenderers;
			foreach (global::UnityEngine.Renderer renderer in array2)
			{
				if (renderer.bounds.max.y > maxRendererY.bounds.max.y)
				{
					maxRendererY = renderer;
				}
			}
		}

		public virtual void UpdateColliderState(global::Kampai.Game.BuildingState state)
		{
			base.IsInteractable = state != global::Kampai.Game.BuildingState.Disabled;
			UpdateColliders(base.IsInteractable);
		}

		private global::UnityEngine.Vector3 GetCenter()
		{
			if (minColliderY != null)
			{
				return minColliderY.bounds.center;
			}
			return global::UnityEngine.Vector3.zero;
		}

		private global::UnityEngine.Vector3 GetResourceIconPosition()
		{
			global::UnityEngine.Vector3 position = base.transform.position;
			return new global::UnityEngine.Vector3(position.x + (float)Width / 2f, 0f, position.z - (float)Depth / 2f);
		}

		private global::UnityEngine.Vector3 GetIndicatorPosition(bool centerY)
		{
			if (maxRendererY != null)
			{
				return new global::UnityEngine.Vector3(maxRendererY.bounds.center.x, (!centerY) ? maxRendererY.bounds.max.y : maxRendererY.bounds.center.y, maxRendererY.bounds.center.z);
			}
			if (minColliderY != null)
			{
				return new global::UnityEngine.Vector3(minColliderY.bounds.center.x, (!centerY) ? minColliderY.bounds.max.y : minColliderY.bounds.center.y, minColliderY.bounds.center.z);
			}
			return global::UnityEngine.Vector3.zero;
		}

		public virtual void Bounce()
		{
			if (cachedAnimation != null)
			{
				cachedAnimation.Play();
			}
		}

		public virtual void Cleanup()
		{
		}
	}
}
