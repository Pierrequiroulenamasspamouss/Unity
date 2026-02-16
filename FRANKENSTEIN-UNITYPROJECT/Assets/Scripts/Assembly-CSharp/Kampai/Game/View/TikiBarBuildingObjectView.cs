namespace Kampai.Game.View
{
	public class TikiBarBuildingObjectView : global::Kampai.Game.View.AnimatingBuildingObject, global::strange.extensions.mediation.api.IView
	{
		public global::Kampai.Game.TikiBarBuilding tikiBar;

		private global::UnityEngine.Renderer glowRenderer;

		private global::UnityEngine.Animation glowAnimation;

		private int routeIndex;

		private global::System.Collections.Generic.Dictionary<int, global::UnityEngine.RuntimeAnimatorController> minionControllers;

		private global::System.Collections.Generic.Dictionary<int, global::UnityEngine.RuntimeAnimatorController> unlockedMinionControllers;

		private global::UnityEngine.RuntimeAnimatorController minionWalkStateMachine;

		private int[] layerIndicies;

		private global::UnityEngine.Renderer[] renderers;

		protected global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.View.TaskingCharacterObject> childAnimators = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.View.TaskingCharacterObject>();

		protected global::System.Collections.Generic.List<global::Kampai.Game.View.TaskingCharacterObject> childQueue = new global::System.Collections.Generic.List<global::Kampai.Game.View.TaskingCharacterObject>();

		private global::Kampai.Game.MinionStateChangeSignal stateChangeSignal;

		private global::Kampai.Game.NamedCharacterRemovedFromTikiBarSignal removedFromTikibarSignal;

		private global::Kampai.Game.CharacterIntroCompleteSignal introCompleteSignal;

		private global::Kampai.Game.CharacterDrinkingCompleteSignal drinkingCompelteSignal;

		private bool _requiresContext = true;

		protected bool registerWithContext = true;

		public bool requiresContext
		{
			get
			{
				return _requiresContext;
			}
			set
			{
				_requiresContext = value;
			}
		}

		public bool registeredWithContext { get; set; }

		public virtual bool autoRegisterWithContext
		{
			get
			{
				return registerWithContext;
			}
			set
			{
				registerWithContext = value;
			}
		}

		internal override void Init(global::Kampai.Game.Building building, global::Kampai.Util.ILogger logger, global::System.Collections.Generic.IDictionary<string, global::UnityEngine.RuntimeAnimatorController> controllers, global::Kampai.Game.IDefinitionService definitionService)
		{
			base.Init(building, logger, controllers, definitionService);
			tikiBar = building as global::Kampai.Game.TikiBarBuilding;
			global::Kampai.Game.TaskableBuildingDefinition taskableBuildingDefinition = building.Definition as global::Kampai.Game.TaskableBuildingDefinition;
			if (taskableBuildingDefinition == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.BV_ILLEGAL_TASKABLE_DEFINITION, building.Definition.ID.ToString());
			}
			if (building.IsBuildingRepaired())
			{
				global::UnityEngine.Transform transform = base.transform.Find("Unique_TikiBar_LOD0/Unique_TikiBar:Unique_TikiBar/Unique_TikiBar:Unique_TikiBar_Glow_mesh");
				glowRenderer = transform.renderer;
				glowRenderer.enabled = false;
				glowAnimation = transform.GetComponent<global::UnityEngine.Animation>();
				SetupAnimationControllers();
			}
			renderers = base.gameObject.GetComponentsInChildren<global::UnityEngine.Renderer>();
		}

		internal void SetupInjections(global::Kampai.Game.MinionStateChangeSignal stateChangeSignal, global::Kampai.Game.NamedCharacterRemovedFromTikiBarSignal removedFromTikibarSignal, global::Kampai.Game.CharacterIntroCompleteSignal introCompleteSingal, global::Kampai.Game.CharacterDrinkingCompleteSignal drinkingCompleteSingal)
		{
			this.stateChangeSignal = stateChangeSignal;
			this.removedFromTikibarSignal = removedFromTikibarSignal;
			introCompleteSignal = introCompleteSingal;
			drinkingCompelteSignal = drinkingCompleteSingal;
		}

		private void SetupAnimationControllers()
		{
			minionControllers = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.RuntimeAnimatorController>();
			minionWalkStateMachine = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>("asm_minion_movement");
			minionControllers.Add(0, global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>("asm_unique_tikibar_bartender"));
			minionControllers.Add(1, global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(string.Format("{0}1", "asm_unique_tikibar_patron")));
			minionControllers.Add(2, global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(string.Format("{0}2", "asm_unique_tikibar_patron")));
			unlockedMinionControllers = new global::System.Collections.Generic.Dictionary<int, global::UnityEngine.RuntimeAnimatorController>();
			for (int i = 0; i < 3; i++)
			{
				unlockedMinionControllers.Add(i, global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(string.Format("{0}{1}", "asm_unique_tikibar_newMinion", i + 1)));
			}
		}

		internal void SetupLayers()
		{
			if (animators.Count == 0)
			{
				return;
			}
			int layerCount = animators[0].layerCount;
			layerIndicies = new int[3];
			for (int i = 0; i < layerIndicies.Length; i++)
			{
				layerIndicies[i] = -1;
			}
			for (int j = 0; j < layerCount; j++)
			{
				string layerName = animators[0].GetLayerName(j);
				if (layerName.StartsWith("Pos"))
				{
					string value = layerName.Substring("Pos".Length);
					int num = global::System.Convert.ToInt32(value) - 1;
					if (num < 0 || num >= stations)
					{
						logger.Fatal(global::Kampai.Util.FatalCode.BV_NO_SUCH_WEIGHT_FOR_STATION);
					}
					layerIndicies[num] = j;
				}
			}
			if (stations <= 1)
			{
				return;
			}
			for (int k = 0; k < layerIndicies.Length; k++)
			{
				if (layerIndicies[k] == -1)
				{
					logger.Fatal(global::Kampai.Util.FatalCode.BV_MISSING_LAYER);
				}
			}
		}

		internal void SetupCharacter(global::Kampai.Game.View.CharacterObject characterObject)
		{
			int num = routeIndex % 3;
			routeIndex++;
			MoveToRoutingPosition(characterObject, num + 3);
			characterObject.ShelveActionQueue();
			characterObject.SetAnimController(unlockedMinionControllers[num]);
			characterObject.SetAnimatorCullingMode(global::UnityEngine.AnimatorCullingMode.AlwaysAnimate);
			characterObject.EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(characterObject, GetHashAnimationState("Base Layer.NewMinionIntro"), logger));
			characterObject.EnqueueAction(new global::Kampai.Game.View.SetCullingModeAction(characterObject, global::UnityEngine.AnimatorCullingMode.BasedOnRenderers, logger));
			stateChangeSignal.Dispatch(characterObject.ID, global::Kampai.Game.MinionState.Questing);
		}

		internal void BeginCharacterIntroLoop(global::Kampai.Game.View.CharacterObject characterObject)
		{
			SetAnimTrigger("OnNewMinionAppear");
			characterObject.SetAnimTrigger("OnNewMinionAppear");
			characterObject.SetAnimatorCullingMode(global::UnityEngine.AnimatorCullingMode.AlwaysAnimate);
		}

		internal void BeginCharacterIntro(global::Kampai.Game.View.CharacterObject characterObject, int minionRouteIndex)
		{
			routeIndex = 0;
			SetAnimTrigger("OnNewMinionIntro");
			characterObject.SetAnimTrigger("OnNewMinionIntro");
			characterObject.SetAnimatorCullingMode(global::UnityEngine.AnimatorCullingMode.AlwaysAnimate);
			characterObject.EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(characterObject, GetHashAnimationState("Base Layer.Exit"), logger));
			characterObject.EnqueueAction(new global::Kampai.Game.View.CharacterIntroCompleteAction(characterObject, minionRouteIndex, minionWalkStateMachine, introCompleteSignal, logger));
			characterObject.EnqueueAction(new global::Kampai.Game.View.SetCullingModeAction(characterObject, global::UnityEngine.AnimatorCullingMode.BasedOnRenderers, logger));
		}

		internal void EndCharacterIntro(global::Kampai.Game.View.CharacterObject characterObject, int slotIndex, bool validLocation, global::Kampai.Game.RelocateCharacterSignal relocateCharacterSignal)
		{
			characterObject.ApplyRootMotion(false);
			characterObject.EnableBlobShadow(true);
			characterObject.SetAnimatorCullingMode(global::UnityEngine.AnimatorCullingMode.BasedOnRenderers);
			characterObject.UnshelveActionQueue();
			if (slotIndex < 0)
			{
				characterObject.EnqueueAction(new global::Kampai.Game.View.StateChangeAction(characterObject.ID, stateChangeSignal, global::Kampai.Game.MinionState.Idle, logger), true);
				if (!validLocation)
				{
					relocateCharacterSignal.Dispatch(characterObject);
				}
			}
		}

		internal void RemoveCharacterFromTikiBar(int minionId)
		{
			if (childAnimators.ContainsKey(minionId))
			{
				global::Kampai.Game.View.TaskingCharacterObject taskingCharacterObject = childAnimators[minionId];
				int routingIndex = taskingCharacterObject.RoutingIndex;
				global::Kampai.Game.View.CharacterObject character = taskingCharacterObject.Character;
				switch (routingIndex)
				{
				case 1:
					SetAnimBool("pos1_IsSeated", false);
					break;
				case 2:
					SetAnimBool("pos2_IsSeated", false);
					break;
				}
				character.SetAnimBool("isSeated", false);
				character.EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(character, minionControllers[routingIndex], logger), true);
				character.EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(character, GetHashAnimationState("Base Layer.Idle"), logger));
				character.EnqueueAction(new global::Kampai.Game.View.CharacterDrinkingCompleteAction(character, drinkingCompelteSignal, logger));
			}
		}

		internal void UntrackChild(int minionId)
		{
			if (childAnimators.ContainsKey(minionId))
			{
				global::Kampai.Game.View.TaskingCharacterObject taskingCharacterObject = childAnimators[minionId];
				int routingIndex = taskingCharacterObject.RoutingIndex;
				global::Kampai.Game.View.CharacterObject character = taskingCharacterObject.Character;
				character.ApplyRootMotion(false);
				character.UnshelveActionQueue();
				character.EnableBlobShadow(true);
				character.SetAnimatorCullingMode(global::UnityEngine.AnimatorCullingMode.BasedOnRenderers);
				UnlinkChild(minionId);
				SetEnabledStation(routingIndex, false);
				global::Kampai.Game.View.NamedCharacterObject namedCharacterObject = character as global::Kampai.Game.View.NamedCharacterObject;
				if (namedCharacterObject != null)
				{
					character.ResetAnimationController();
					removedFromTikibarSignal.Dispatch(namedCharacterObject);
				}
				else if (character is global::Kampai.Game.View.MinionObject)
				{
					character.EnqueueAction(new global::Kampai.Game.View.StateChangeAction(character.ID, stateChangeSignal, global::Kampai.Game.MinionState.Idle, logger), true);
					character.EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(character, minionWalkStateMachine, logger));
					character.EnqueueAction(new global::Kampai.Game.View.GotoSideWalkAction(character, tikiBar, logger, definitionService, null));
				}
			}
		}

		internal void UnlinkChild(int minionId)
		{
			int num = -1;
			for (int i = 0; i < childQueue.Count; i++)
			{
				if (childQueue[i].ID == minionId)
				{
					num = i;
					break;
				}
			}
			if (num > -1)
			{
				childQueue.RemoveAt(num);
				childAnimators.Remove(minionId);
			}
			else
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Not found");
			}
		}

		internal void PathCharacterToTikiBar(global::Kampai.Game.View.CharacterObject characterObject, global::System.Collections.Generic.IList<global::UnityEngine.Vector3> path, float rotation, int routeIndex, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.View.CharacterObject, int> addSignal)
		{
			float speed = 4.5f;
			characterObject.EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(characterObject, minionWalkStateMachine, logger), true);
			characterObject.EnqueueAction(new global::Kampai.Game.View.ConstantSpeedPathAction(characterObject, path, speed, logger));
			characterObject.EnqueueAction(new global::Kampai.Game.View.RotateAction(characterObject, rotation, 720f, logger));
			characterObject.EnqueueAction(new global::Kampai.Game.View.PathToBuildingCompleteAction(characterObject, routeIndex, addSignal, logger));
		}

		internal bool ContainsCharacter(int instanceID)
		{
			return childAnimators.ContainsKey(instanceID);
		}

		internal void AddCharacterToBuildingActions(global::Kampai.Game.View.CharacterObject characterObject, int routeIndex)
		{
			switch (routeIndex)
			{
			case 1:
				SetAnimBool("pos1_IsSeated", true);
				break;
			case 2:
				SetAnimBool("pos2_IsSeated", true);
				break;
			}
			TrackChild(characterObject, minionControllers[routeIndex], routeIndex);
			characterObject.SetAnimBool("isSeated", true);
			if (characterObject is global::Kampai.Game.View.MinionObject || characterObject is global::Kampai.Game.View.BobView)
			{
				global::Kampai.Util.AI.Agent component = characterObject.GetComponent<global::Kampai.Util.AI.Agent>();
				if (component != null)
				{
					component.MaxSpeed = 0f;
				}
			}
		}

		public void TrackChild(global::Kampai.Game.View.CharacterObject child, global::UnityEngine.RuntimeAnimatorController controller, int routeIndex)
		{
			global::Kampai.Game.View.TaskingCharacterObject taskingCharacterObject = new global::Kampai.Game.View.TaskingCharacterObject(child, routeIndex);
			if (!childAnimators.ContainsKey(child.ID))
			{
				childAnimators.Add(child.ID, taskingCharacterObject);
				childQueue.Add(taskingCharacterObject);
				child.ShelveActionQueue();
			}
			SetupChild(routeIndex, taskingCharacterObject, controller);
		}

		protected virtual void SetupChild(int routingIndex, global::Kampai.Game.View.TaskingCharacterObject taskingChild, global::UnityEngine.RuntimeAnimatorController controller = null)
		{
			taskingChild.RoutingIndex = routingIndex;
			global::Kampai.Game.View.CharacterObject character = taskingChild.Character;
			if (controller != null)
			{
				character.SetAnimController(controller);
			}
			character.ApplyRootMotion(false);
			character.EnableRenderers(true);
			MoveToRoutingPosition(character, routingIndex);
			SetEnabledStation(routingIndex, true);
		}

		public override void Update()
		{
			base.Update();
			bool flag = false;
			for (int i = 0; i < renderers.Length; i++)
			{
				global::UnityEngine.Renderer renderer = renderers[i];
				if (renderer.isVisible)
				{
					flag = true;
					break;
				}
			}
			global::UnityEngine.AnimatorCullingMode animatorCullingMode = ((!flag) ? global::UnityEngine.AnimatorCullingMode.BasedOnRenderers : global::UnityEngine.AnimatorCullingMode.AlwaysAnimate);
			global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.View.TaskingCharacterObject>.Enumerator enumerator = childAnimators.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					global::Kampai.Game.View.TaskingCharacterObject value = enumerator.Current.Value;
					value.Character.SetAnimatorCullingMode(animatorCullingMode);
				}
			}
			finally
			{
				enumerator.Dispose();
			}
		}

		protected void SetEnabledStation(int station, bool isEnabled)
		{
			foreach (global::UnityEngine.Animator animator in animators)
			{
				SetStationState(animator, station, isEnabled);
			}
		}

		private void SetStationState(global::UnityEngine.Animator animator, int station, bool isEnabled)
		{
			if (GetNumberOfStations() > 1 && station < GetNumberOfStations())
			{
				animator.SetLayerWeight(layerIndicies[station], (!isEnabled) ? 0f : 1f);
			}
		}

		public void ToggleHitbox(bool enable)
		{
			global::UnityEngine.Collider[] components = GetComponents<global::UnityEngine.Collider>();
			foreach (global::UnityEngine.Collider collider in components)
			{
				collider.enabled = enable;
			}
		}

		internal void ToggleStickerbookGlow(bool enable)
		{
			if (enable)
			{
				glowRenderer.enabled = true;
				glowAnimation.Play();
			}
			else
			{
				glowAnimation.Stop();
				glowRenderer.enabled = false;
			}
		}

		protected void Awake()
		{
			if (autoRegisterWithContext && !registeredWithContext)
			{
				bubbleToContext(this, true, false);
			}
		}

		protected void Start()
		{
			if (autoRegisterWithContext && !registeredWithContext)
			{
				bubbleToContext(this, true, true);
			}
		}

		protected void OnDestroy()
		{
			bubbleToContext(this, false, false);
		}

		protected virtual void bubbleToContext(global::UnityEngine.MonoBehaviour view, bool toAdd, bool finalTry)
		{
			int num = 0;
			global::UnityEngine.Transform parent = view.gameObject.transform;
			while (parent.parent != null && num < 100)
			{
				num++;
				parent = parent.parent;
				global::UnityEngine.GameObject key = parent.gameObject;
				global::strange.extensions.context.api.IContext value;
				if (global::strange.extensions.context.impl.Context.knownContexts.TryGetValue(key, out value))
				{
					if (toAdd)
					{
						value.AddView(view);
						registeredWithContext = true;
					}
					else
					{
						value.RemoveView(view);
					}
					return;
				}
			}
			if (requiresContext && finalTry)
			{
				if (global::strange.extensions.context.impl.Context.firstContext == null)
				{
					string text = ((num != 100) ? "A view was added with no context. Views must be added into the hierarchy of their ContextView lest all hell break loose." : "A view couldn't find a context. Loop limit reached.");
					text = text + "\nView: " + view.ToString();
					throw new global::strange.extensions.mediation.impl.MediationException(text, global::strange.extensions.mediation.api.MediationExceptionType.NO_CONTEXT);
				}
				global::strange.extensions.context.impl.Context.firstContext.AddView(view);
				registeredWithContext = true;
			}
		}

		internal void PlayAnimation(string animation, global::System.Type type, object obj)
		{
			if (type == typeof(int))
			{
				SetAnimInteger(animation, (int)obj);
			}
			else if (type == typeof(float))
			{
				SetAnimFloat(animation, (float)obj);
			}
			else if (type == typeof(bool))
			{
				SetAnimBool(animation, (bool)obj);
			}
		}
	}
}
