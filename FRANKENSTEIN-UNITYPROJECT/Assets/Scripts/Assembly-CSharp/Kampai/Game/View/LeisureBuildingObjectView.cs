namespace Kampai.Game.View
{
	public class LeisureBuildingObjectView : global::Kampai.Game.View.AnimatingBuildingObject, global::strange.extensions.mediation.api.IView
	{
		public global::Kampai.Game.LeisureBuilding leisureBuilding;

		private global::UnityEngine.RuntimeAnimatorController minionController;

		private global::UnityEngine.RuntimeAnimatorController minionWalkStateMachine;

		protected global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.View.TaskingCharacterObject> childAnimators = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.View.TaskingCharacterObject>();

		protected global::System.Collections.Generic.List<global::Kampai.Game.View.TaskingCharacterObject> childQueue = new global::System.Collections.Generic.List<global::Kampai.Game.View.TaskingCharacterObject>();

		private global::Kampai.Game.MinionStateChangeSignal stateChangeSignal;

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
			leisureBuilding = building as global::Kampai.Game.LeisureBuilding;
			global::Kampai.Game.LeisureBuildingDefintiion leisureBuildingDefintiion = building.Definition as global::Kampai.Game.LeisureBuildingDefintiion;
			if (leisureBuildingDefintiion == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.BV_ILLEGAL_TASKABLE_DEFINITION, building.Definition.ID.ToString());
			}
			buildingState = building.State;
			if (buildingState != global::Kampai.Game.BuildingState.Construction && buildingState != global::Kampai.Game.BuildingState.Complete)
			{
				SetupAnimationControllers(leisureBuildingDefintiion);
			}
		}

		internal void SetupInjections(global::Kampai.Game.MinionStateChangeSignal minionStateChangeSignal)
		{
			stateChangeSignal = minionStateChangeSignal;
		}

		private void SetupAnimationControllers(global::Kampai.Game.LeisureBuildingDefintiion def)
		{
			minionWalkStateMachine = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>("asm_minion_movement");
			if (def.AnimationDefinitions == null || def.AnimationDefinitions.Count == 0 || string.IsNullOrEmpty(def.AnimationDefinitions[0].MinionController))
			{
				logger.Error("Animation Definition NOT defined for the leisure Building");
			}
			else
			{
				minionController = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(def.AnimationDefinitions[0].MinionController);
			}
		}

		internal bool IsMinionInBuilding(int minionID)
		{
			return childAnimators.ContainsKey(minionID);
		}

		internal void FreeAllMinions(int selectedMinionID = 0, global::Kampai.Game.MinionState targetState = global::Kampai.Game.MinionState.Idle)
		{
			global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.View.TaskingCharacterObject>.Enumerator enumerator = childAnimators.GetEnumerator();
			try
			{
				int num = 0;
				while (enumerator.MoveNext())
				{
					global::Kampai.Game.View.TaskingCharacterObject value = enumerator.Current.Value;
					UntrackChild(value.ID, (selectedMinionID != value.ID) ? global::Kampai.Game.MinionState.Idle : targetState, num);
					num++;
				}
			}
			finally
			{
				enumerator.Dispose();
			}
			childAnimators.Clear();
			EnqueueAction(new global::Kampai.Game.View.TriggerBuildingAnimationAction(this, OnlyStateEnabled("OnStop"), logger), true);
		}

		internal void UntrackChild(int minionId, global::Kampai.Game.MinionState targetState = global::Kampai.Game.MinionState.Idle, int index = 0)
		{
			if (childAnimators.ContainsKey(minionId))
			{
				global::Kampai.Game.View.TaskingCharacterObject taskingCharacterObject = childAnimators[minionId];
				global::Kampai.Game.View.CharacterObject character = taskingCharacterObject.Character;
				character.ApplyRootMotion(false);
				character.UnshelveActionQueue();
				character.EnableBlobShadow(true);
				character.SetAnimatorCullingMode(global::UnityEngine.AnimatorCullingMode.BasedOnRenderers);
				UnlinkChild(minionId);
				global::Kampai.Game.View.NamedCharacterObject namedCharacterObject = character as global::Kampai.Game.View.NamedCharacterObject;
				if (namedCharacterObject != null)
				{
					character.ResetAnimationController();
				}
				else if (character is global::Kampai.Game.View.MinionObject)
				{
					character.EnqueueAction(new global::Kampai.Game.View.StateChangeAction(character.ID, stateChangeSignal, targetState, logger));
					character.EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(character, minionWalkStateMachine, logger));
					character.EnqueueAction(new global::Kampai.Game.View.GotoSideWalkAction(character, leisureBuilding, logger, definitionService, null, index));
				}
			}
		}

		protected void UnlinkChild(int minionId)
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
			}
			else
			{
				logger.Log(global::Kampai.Util.Logger.Level.Error, "Not found");
			}
		}

		internal void PathMinionToLeisureBuilding(global::Kampai.Game.View.CharacterObject characterObject, global::System.Collections.Generic.IList<global::UnityEngine.Vector3> path, float rotation, int routeIndex, global::strange.extensions.signal.impl.Signal<global::Kampai.Game.View.CharacterObject, int> addSignal)
		{
			float speed = 4.5f;
			characterObject.EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(characterObject, minionWalkStateMachine, logger), true);
			characterObject.EnqueueAction(new global::Kampai.Game.View.ConstantSpeedPathAction(characterObject, path, speed, logger));
			characterObject.EnqueueAction(new global::Kampai.Game.View.RotateAction(characterObject, rotation, 720f, logger));
			characterObject.EnqueueAction(new global::Kampai.Game.View.PathToBuildingCompleteAction(characterObject, routeIndex, addSignal, logger));
		}

		internal void AddCharacterToBuildingActions(global::Kampai.Game.View.CharacterObject mo, int routeIndex)
		{
			TrackChild(mo, minionController, routeIndex);
		}

		public void TrackChild(global::Kampai.Game.View.CharacterObject child, global::UnityEngine.RuntimeAnimatorController controller, int routeIndex)
		{
			if (!IsAnimating)
			{
				global::System.Collections.Generic.Dictionary<string, object> animationParams = OnlyStateEnabled("OnLoop");
				EnqueueAction(new global::Kampai.Game.View.TriggerBuildingAnimationAction(this, animationParams, logger));
			}
			global::Kampai.Game.View.TaskingCharacterObject taskingCharacterObject = new global::Kampai.Game.View.TaskingCharacterObject(child, routeIndex);
			childAnimators.Add(child.ID, taskingCharacterObject);
			childQueue.Add(taskingCharacterObject);
			child.ShelveActionQueue();
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
			character.ApplyRootMotion(true);
			character.EnableRenderers(true);
			character.ExecuteAction(new global::Kampai.Game.View.SetAnimatorAction(character, null, logger, new global::System.Collections.Generic.Dictionary<string, object> { { "minionPosition", routingIndex } }));
			global::System.Collections.Generic.Dictionary<string, object> animationParams = OnlyStateEnabled("OnLoop");
			int hashAnimationState = GetHashAnimationState("Base Layer.Loop_Pos" + (routingIndex + 1));
			int hashAnimationState2 = GetHashAnimationState("Base Layer.Loop");
			character.EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(character, null, logger, OnlyStateEnabled("OnStop")), true);
			character.EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(character, GetHashAnimationState("Base Layer.Idle"), logger));
			character.EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(character, null, logger, animationParams));
			character.EnqueueAction(new global::Kampai.Game.View.SkipToTimeAction(character, new global::Kampai.Game.View.SkipToTime(0, hashAnimationState, GetCurrentAnimationTimeForState(hashAnimationState2)), logger));
			MoveToRoutingPosition(character, routingIndex);
		}

		public override void Update()
		{
			base.Update();
			bool flag = false;
			for (int i = 0; i < objectRenderers.Length; i++)
			{
				global::UnityEngine.Renderer renderer = objectRenderers[i];
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
	}
}
