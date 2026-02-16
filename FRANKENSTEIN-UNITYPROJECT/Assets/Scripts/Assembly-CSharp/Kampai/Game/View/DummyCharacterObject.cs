namespace Kampai.Game.View
{
	public class DummyCharacterObject : global::Kampai.Game.View.ActionableObject
	{
		protected global::UnityEngine.RuntimeAnimatorController cachedDefaultController;

		protected global::UnityEngine.RuntimeAnimatorController cachedRuntimeController;

		protected bool animatorControllersAreEqual;

		private global::Kampai.Util.IRoutineRunner routineRunner;

		private global::Kampai.Common.IRandomService randomService;

		private global::Kampai.Game.CharacterUIAnimationDefinition characterUIAnimationDefinition;

		private global::System.Collections.IEnumerator NextAnim;

		private bool markedForDestory;

		public void Init(global::Kampai.Game.Character character, global::Kampai.Util.ILogger logger, global::Kampai.Util.IRoutineRunner routineRunner, global::Kampai.Common.IRandomService randomService)
		{
			ID = character.ID;
			base.name = character.Name;
			base.logger = logger;
			this.routineRunner = routineRunner;
			this.randomService = randomService;
			SetName(character);
			animators = new global::System.Collections.Generic.List<global::UnityEngine.Animator>(base.gameObject.GetComponentsInChildren<global::UnityEngine.Animator>());
			global::Kampai.Game.Minion minion = character as global::Kampai.Game.Minion;
			if (minion != null && minion.CostumeID == 99)
			{
				global::Kampai.Game.View.MinionObject.SetEyes(base.transform, minion.Definition.Eyes);
				global::Kampai.Game.View.MinionObject.SetBody(base.transform, minion.Definition.Body);
				global::Kampai.Game.View.MinionObject.SetHair(base.transform, minion.Definition.Hair);
			}
			else if (character is global::Kampai.Game.KevinCharacter)
			{
				global::Kampai.Game.View.MinionObject.SetEyes(base.transform, 2u);
				global::Kampai.Game.View.MinionObject.SetBody(base.transform, global::Kampai.Game.MinionBody.TALL);
				global::Kampai.Game.View.MinionObject.SetHair(base.transform, global::Kampai.Game.MinionHair.SPROUT);
			}
		}

		public global::Kampai.Game.View.DummyCharacterObject Build(global::Kampai.Game.Character character, global::Kampai.Game.CharacterUIAnimationDefinition characterUIAnimationDefinition, global::UnityEngine.Transform parent, global::Kampai.Util.ILogger logger, bool forceHighLOD, global::UnityEngine.Vector3 villainScale, global::UnityEngine.Vector3 villainPositionOffset, global::Kampai.Util.IMinionBuilder minionBuilder)
		{
			this.characterUIAnimationDefinition = characterUIAnimationDefinition;
			if (parent != null)
			{
				base.gameObject.transform.SetParent(parent, false);
			}
			base.gameObject.transform.localEulerAngles = global::UnityEngine.Vector3.zero;
			if (character is global::Kampai.Game.Villain)
			{
				base.gameObject.transform.localPosition = villainPositionOffset;
				base.gameObject.transform.localScale = villainScale;
			}
			else
			{
				base.gameObject.transform.localPosition = global::UnityEngine.Vector3.zero;
				base.gameObject.transform.localScale = global::UnityEngine.Vector3.one;
				SetupLODs(forceHighLOD);
			}
			string stateMachine = characterUIAnimationDefinition.StateMachine;
			global::UnityEngine.Animator component = base.gameObject.GetComponent<global::UnityEngine.Animator>();
			component.applyRootMotion = true;
			component.runtimeAnimatorController = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(stateMachine);
			component.cullingMode = global::UnityEngine.AnimatorCullingMode.BasedOnRenderers;
			if (component.runtimeAnimatorController == null)
			{
				logger.Error("Failed to get default runtime animator controller for {0}: asm name: {1}", character.Name, stateMachine);
			}
			global::UnityEngine.Transform rootBone = base.gameObject.transform.Find(GetRootJointPath());
			global::UnityEngine.SkinnedMeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<global::UnityEngine.SkinnedMeshRenderer>();
			foreach (global::UnityEngine.SkinnedMeshRenderer skinnedMeshRenderer in componentsInChildren)
			{
				skinnedMeshRenderer.rootBone = rootBone;
			}
			SetupBlobShadow(minionBuilder);
			base.gameObject.SetLayerRecursively(5);
			SetupCharacterObject(component.runtimeAnimatorController);
			return this;
		}

		public void SetStenciledShader()
		{
			int num = 1;
			global::UnityEngine.SkinnedMeshRenderer componentInChildren = GetComponentInChildren<global::UnityEngine.SkinnedMeshRenderer>();
			global::UnityEngine.Material[] materials = componentInChildren.materials;
			foreach (global::UnityEngine.Material material in materials)
			{
				switch (material.shader.name)
				{
				case "Kampai/Standard/Texture":
					global::Kampai.Util.Graphics.ShaderUtils.EnableStencilShader(material, 2, num++);
					break;
				case "Kampai/Standard/Minion":
					global::Kampai.Util.Graphics.ShaderUtils.EnableStencilShader(material, 2, num++);
					break;
				}
			}
		}

		private void SetupBlobShadow(global::Kampai.Util.IMinionBuilder minionBuilder)
		{
			if (minionBuilder.GetLOD() != global::Kampai.Util.TargetPerformance.LOW && minionBuilder.GetLOD() != global::Kampai.Util.TargetPerformance.VERYLOW)
			{
				global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load("MinionBlobShadow_UI") as global::UnityEngine.GameObject;
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
				gameObject.transform.SetParent(base.gameObject.transform, false);
				gameObject.transform.localScale = global::UnityEngine.Vector3.one;
				gameObject.transform.localPosition = global::UnityEngine.Vector3.zero;
				gameObject.transform.localEulerAngles = new global::UnityEngine.Vector3(90f, 180f, 0f);
			}
		}

		public void RemoveCoroutine(bool isDestorying = true)
		{
			if (NextAnim != null && routineRunner != null)
			{
				routineRunner.StopCoroutine(NextAnim);
				NextAnim = null;
			}
			if (isDestorying)
			{
				markedForDestory = true;
			}
		}

		public void StartingState(global::Kampai.UI.DummyCharacterAnimationState targetState, bool startingFromCoroutine = false)
		{
			if (markedForDestory && startingFromCoroutine)
			{
				markedForDestory = false;
				return;
			}
			int num = 0;
			switch (targetState)
			{
			case global::Kampai.UI.DummyCharacterAnimationState.Idle:
				num = randomService.NextInt(characterUIAnimationDefinition.IdleCount);
				SetAnimBool("IsIdle", true);
				SetAnimBool("IsHappy", false);
				SetAnimInteger("IdleRandomizer", num);
				break;
			case global::Kampai.UI.DummyCharacterAnimationState.Happy:
				num = randomService.NextInt(characterUIAnimationDefinition.HappyCount);
				SetAnimBool("IsIdle", false);
				SetAnimBool("IsHappy", true);
				SetAnimInteger("HappyRandomizer", num);
				break;
			case global::Kampai.UI.DummyCharacterAnimationState.SelectedIdle:
				num = randomService.NextInt(characterUIAnimationDefinition.SelectedCount);
				SetAnimTrigger("IsSelected");
				SetAnimBool("IsIdle", true);
				SetAnimBool("IsHappy", false);
				SetAnimInteger("SelectedRandomizer", num);
				break;
			case global::Kampai.UI.DummyCharacterAnimationState.SelectedHappy:
				num = randomService.NextInt(characterUIAnimationDefinition.SelectedCount);
				SetAnimTrigger("IsSelected");
				SetAnimBool("IsIdle", false);
				SetAnimBool("IsHappy", true);
				SetAnimInteger("SelectedRandomizer", num);
				break;
			}
			NextAnim = PlayNextAnim(targetState);
			routineRunner.StartCoroutine(NextAnim);
		}

		private global::System.Collections.IEnumerator PlayNextAnim(global::Kampai.UI.DummyCharacterAnimationState targetState)
		{
			yield return new global::UnityEngine.WaitForSeconds(2f);
			switch (targetState)
			{
			case global::Kampai.UI.DummyCharacterAnimationState.SelectedIdle:
				targetState = global::Kampai.UI.DummyCharacterAnimationState.Idle;
				break;
			case global::Kampai.UI.DummyCharacterAnimationState.SelectedHappy:
				targetState = global::Kampai.UI.DummyCharacterAnimationState.Happy;
				break;
			}
			StartingState(targetState, true);
		}

		protected void SetupLODs(bool forceHigh)
		{
			global::UnityEngine.Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<global::UnityEngine.Renderer>();
			global::System.Array.Sort(componentsInChildren, (global::UnityEngine.Renderer x, global::UnityEngine.Renderer y) => x.gameObject.name.CompareTo(y.gameObject.name));
			for (int num = 0; num < componentsInChildren.Length; num++)
			{
				componentsInChildren[num].gameObject.SetActive(false);
				componentsInChildren[num].castShadows = false;
				componentsInChildren[num].receiveShadows = false;
			}
			if (forceHigh)
			{
				componentsInChildren[0].gameObject.SetActive(true);
			}
			else
			{
				componentsInChildren[componentsInChildren.Length - 1].gameObject.SetActive(true);
			}
		}

		public override void SetAnimController(global::UnityEngine.RuntimeAnimatorController controller)
		{
			animators[0].runtimeAnimatorController = controller;
		}

		private void SetupCharacterObject(global::UnityEngine.RuntimeAnimatorController defaultController)
		{
			SetDefaultAnimController(defaultController);
		}

		protected string GetRootJointPath()
		{
			return "minion:ROOT/minion:pelvis_jnt";
		}

		protected void SetName(global::Kampai.Game.Character character)
		{
			if (base.gameObject.name.Length == 0)
			{
				base.gameObject.name = string.Format("Minion_{0}", character.ID);
			}
		}
	}
}
