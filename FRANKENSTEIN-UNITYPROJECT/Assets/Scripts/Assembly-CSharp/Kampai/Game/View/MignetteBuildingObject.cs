namespace Kampai.Game.View
{
	public class MignetteBuildingObject : global::Kampai.Game.View.TaskableBuildingObject
	{
		private global::System.Collections.Generic.List<global::Kampai.Game.BuildingAnimationDefinition> AnimationDefinitions;

		internal override void Init(global::Kampai.Game.Building building, global::Kampai.Util.ILogger logger, global::System.Collections.Generic.IDictionary<string, global::UnityEngine.RuntimeAnimatorController> controllers, global::Kampai.Game.IDefinitionService definitionService)
		{
			base.Init(building, logger, controllers, definitionService);
			global::Kampai.Game.AnimatingBuildingDefinition animatingBuildingDefinition = building.Definition as global::Kampai.Game.AnimatingBuildingDefinition;
			if (animatingBuildingDefinition != null && animatingBuildingDefinition.AnimationDefinitions != null)
			{
				AnimationDefinitions = (global::System.Collections.Generic.List<global::Kampai.Game.BuildingAnimationDefinition>)animatingBuildingDefinition.AnimationDefinitions;
			}
			else
			{
				logger.Fatal(global::Kampai.Util.FatalCode.BV_NO_DEFAULT_ANIMATION_CONTROLLER, animatingBuildingDefinition.ID.ToString());
			}
		}

		public global::Kampai.Game.View.TaskingMinionObject GetChildMinion(int index)
		{
			return childQueue[index];
		}

		public global::UnityEngine.Vector3 GetRouteLocation(int routeIndex)
		{
			if (routeIndex >= 0 && routeIndex < routes.Length)
			{
				return routes[routeIndex].position;
			}
			return global::UnityEngine.Vector3.zero;
		}

		public global::UnityEngine.Vector3 GetRouteForward(int routeIndex)
		{
			if (routeIndex >= 0 && routeIndex < routes.Length)
			{
				return routes[routeIndex].forward;
			}
			return global::UnityEngine.Vector3.forward;
		}

		public int GetMignetteMinionCount()
		{
			return GetActiveMinionCount();
		}

		public void LoadMignetteAnimationControllers(global::System.Collections.Generic.Dictionary<string, global::UnityEngine.RuntimeAnimatorController> animationControllers)
		{
			foreach (global::Kampai.Game.BuildingAnimationDefinition animationDefinition in AnimationDefinitions)
			{
				if (!buildingControllers.ContainsKey(animationDefinition.CostumeId) && !string.IsNullOrEmpty(animationDefinition.BuildingController))
				{
					global::UnityEngine.RuntimeAnimatorController runtimeAnimatorController = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(animationDefinition.BuildingController);
					if (runtimeAnimatorController == null)
					{
						logger.Fatal(global::Kampai.Util.FatalCode.BV_NO_DEFAULT_ANIMATION_CONTROLLER, animationDefinition.ID.ToString());
						break;
					}
					buildingControllers.Add(animationDefinition.CostumeId, runtimeAnimatorController);
					if (!animationControllers.ContainsKey(animationDefinition.BuildingController))
					{
						animationControllers.Add(animationDefinition.BuildingController, runtimeAnimatorController);
					}
				}
				if (!minionControllers.ContainsKey(animationDefinition.CostumeId) && !string.IsNullOrEmpty(animationDefinition.MinionController))
				{
					global::UnityEngine.RuntimeAnimatorController runtimeAnimatorController2 = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.RuntimeAnimatorController>(animationDefinition.MinionController);
					if (runtimeAnimatorController2 == null)
					{
						logger.Fatal(global::Kampai.Util.FatalCode.BV_NO_DEFAULT_ANIMATION_CONTROLLER, animationDefinition.ID.ToString());
						break;
					}
					minionControllers.Add(animationDefinition.CostumeId, runtimeAnimatorController2);
					if (!animationControllers.ContainsKey(animationDefinition.MinionController))
					{
						animationControllers.Add(animationDefinition.MinionController, runtimeAnimatorController2);
					}
				}
			}
		}

		public override void StartAnimating()
		{
		}

		protected override void SetupChild(int routingIndex, global::Kampai.Game.View.TaskingMinionObject taskingChild, global::UnityEngine.RuntimeAnimatorController controller = null)
		{
			taskingChild.RoutingIndex = routingIndex;
			global::Kampai.Game.View.MinionObject minion = taskingChild.Minion;
			if (controller != null)
			{
				minion.SetAnimController(controller);
			}
			int numberOfStations = GetNumberOfStations();
			if (routingIndex < numberOfStations)
			{
				minion.ApplyRootMotion(true);
				minion.EnableRenderers(false);
				MoveToRoutingPosition(minion, routingIndex);
			}
		}

		public override void UntrackChild(int minionId, global::Kampai.Game.TaskableBuilding building)
		{
			if (childAnimators.ContainsKey(minionId))
			{
				global::Kampai.Game.View.TaskingMinionObject taskingMinionObject = childAnimators[minionId];
				global::Kampai.Game.View.MinionObject minion = taskingMinionObject.Minion;
				minion.SetRenderLayer(10);
				minion.transform.position = ((building == null) ? base.gameObject.transform.position : GetRandomExit(building));
				minion.transform.rotation = global::UnityEngine.Quaternion.identity;
				minion.ApplyRootMotion(false);
				minion.UnshelveActionQueue();
				minion.EnableBlobShadow(true);
				minion.SetAnimatorCullingMode(global::UnityEngine.AnimatorCullingMode.BasedOnRenderers);
				minion.EnqueueAction(new global::Kampai.Game.View.SetLayerAction(minion, 8, logger, 2));
				UnlinkChild(minionId);
			}
		}
	}
}
