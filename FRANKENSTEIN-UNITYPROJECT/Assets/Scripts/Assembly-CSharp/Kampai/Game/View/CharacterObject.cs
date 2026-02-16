namespace Kampai.Game.View
{
	public abstract class CharacterObject : global::Kampai.Game.View.ActionableObject
	{
		private global::UnityEngine.GameObject blobShadow;

		private global::System.Collections.Generic.List<global::UnityEngine.Renderer> renderers;

		protected global::UnityEngine.RuntimeAnimatorController cachedDefaultController;

		protected global::UnityEngine.RuntimeAnimatorController cachedRuntimeController;

		protected bool animatorControllersAreEqual;

		private global::UnityEngine.Renderer defaultRenderer;

		private global::UnityEngine.Transform pelvisTransform;

		public virtual void Init(global::Kampai.Game.Character character, global::Kampai.Util.ILogger logger)
		{
			ID = character.ID;
			base.name = character.Name;
			base.logger = logger;
			SetName(character);
			animators = new global::System.Collections.Generic.List<global::UnityEngine.Animator>(base.gameObject.GetComponentsInChildren<global::UnityEngine.Animator>());
			renderers = new global::System.Collections.Generic.List<global::UnityEngine.Renderer>(3);
			foreach (global::UnityEngine.Transform item in base.transform)
			{
				global::UnityEngine.Renderer component = item.GetComponent<global::UnityEngine.Renderer>();
				if (component != null && !item.name.StartsWith("selectIcon"))
				{
					renderers.Add(component);
				}
			}
			defaultRenderer = base.gameObject.renderer;
			global::UnityEngine.GameObject gameObject = base.transform.FindChild(GetRootJointPath(character)).gameObject;
			global::UnityEngine.BoxCollider boxCollider = gameObject.GetComponent<global::UnityEngine.BoxCollider>();
			if (boxCollider == null)
			{
				boxCollider = gameObject.AddComponent<global::UnityEngine.BoxCollider>();
			}
			boxCollider.center = new global::UnityEngine.Vector3(0f, 0.35f, 0f);
			boxCollider.size = new global::UnityEngine.Vector3(0.75f, 1f, 0.75f);
			pelvisTransform = gameObject.transform;
			InitProps();
		}

		protected virtual string GetRootJointPath(global::Kampai.Game.Character character)
		{
			return "minion:ROOT/minion:pelvis_jnt";
		}

		protected virtual void SetName(global::Kampai.Game.Character character)
		{
			if (base.gameObject.name.Length == 0)
			{
				base.gameObject.name = string.Format("Minion_{0}", character.ID);
			}
		}

		public override void EnableRenderers(bool enabled)
		{
			foreach (global::UnityEngine.Renderer renderer in renderers)
			{
				renderer.enabled = enabled;
			}
			EnableBlobShadow(enabled);
		}

		public virtual void SetBlobShadow(global::UnityEngine.GameObject shadow)
		{
			blobShadow = shadow;
		}

		public virtual void EnableBlobShadow(bool enabled)
		{
			if (blobShadow != null)
			{
				blobShadow.SetActive(enabled);
			}
		}

		public virtual void UpdateBlobShadowPosition()
		{
			if (blobShadow != null)
			{
				blobShadow.GetComponent<global::Kampai.Util.MinionBlobShadowView>().ManualUpdate();
			}
		}

		public override void LateUpdate()
		{
			base.LateUpdate();
			if (animators != null && (animators[0].runtimeAnimatorController != cachedRuntimeController || defaultController != cachedDefaultController))
			{
				cachedRuntimeController = animators[0].runtimeAnimatorController;
				cachedDefaultController = defaultController;
				if ((bool)cachedRuntimeController)
				{
					animatorControllersAreEqual = cachedRuntimeController.name.Equals(defaultController.name);
				}
				else
				{
					animatorControllersAreEqual = false;
				}
			}
		}

		public virtual void SetMuteStatus(bool muteStatus)
		{
			ExecuteAction(new global::Kampai.Game.View.MuteAction(this, muteStatus, logger));
		}

		public virtual void ResetRootPosition()
		{
			global::UnityEngine.Transform transform = base.gameObject.FindChild("minion:ROOT").transform;
			global::UnityEngine.Transform transform2 = base.gameObject.transform;
			transform2.position = VectorUtils.ZeroY(transform2.position + transform.localPosition);
			transform2.rotation = global::UnityEngine.Quaternion.Euler(VectorUtils.ZeroXZ(transform2.rotation.eulerAngles + transform.localRotation.eulerAngles));
			transform.localPosition = global::UnityEngine.Vector3.zero;
			transform.localRotation = global::UnityEngine.Quaternion.Euler(global::UnityEngine.Vector3.zero);
		}

		public virtual void MoveToPelvis()
		{
			global::UnityEngine.Transform transform = base.gameObject.transform;
			global::UnityEngine.Transform transform2 = base.gameObject.FindChild("minion:pelvis_jnt").transform;
			transform.position = VectorUtils.ZeroY(transform.position - transform2.localPosition);
			transform.rotation = global::UnityEngine.Quaternion.Euler(VectorUtils.ZeroXZ(transform.rotation.eulerAngles + transform2.localRotation.eulerAngles));
		}

		public void ResetAnimationController()
		{
			if (animators.Count == 0 || animators[0].runtimeAnimatorController == defaultController)
			{
				return;
			}
			global::System.Collections.Generic.List<global::UnityEngine.Animator>.Enumerator enumerator = animators.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					global::UnityEngine.Animator current = enumerator.Current;
					current.runtimeAnimatorController = defaultController;
				}
			}
			finally
			{
				enumerator.Dispose();
			}
			vfxTrigger = null;
		}

		public void setLocation(global::UnityEngine.Vector3 position)
		{
			base.transform.position = position;
		}

		public void setRotation(global::UnityEngine.Vector3 rotation)
		{
			base.transform.localEulerAngles = rotation;
		}

		public virtual global::UnityEngine.Object GetBlobShadow()
		{
			return blobShadow;
		}

		public virtual global::UnityEngine.Vector3 GetIndicatorPosition()
		{
			float y = 2f;
			if ((bool)defaultRenderer)
			{
				y = defaultRenderer.bounds.max.y;
			}
			global::UnityEngine.Vector3 vector = ((!(blobShadow != null)) ? pelvisTransform.position : blobShadow.transform.position);
			return vector + new global::UnityEngine.Vector3(0f, y, 0f);
		}
	}
}
