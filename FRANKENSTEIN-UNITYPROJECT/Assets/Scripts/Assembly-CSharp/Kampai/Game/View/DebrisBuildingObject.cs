namespace Kampai.Game.View
{
	public class DebrisBuildingObject : global::Kampai.Game.View.TaskableBuildingObject
	{
		public global::UnityEngine.Renderer[] objRenderers;

		internal override void Init(global::Kampai.Game.Building building, global::Kampai.Util.ILogger logger, global::System.Collections.Generic.IDictionary<string, global::UnityEngine.RuntimeAnimatorController> controllers, global::Kampai.Game.IDefinitionService definitionService)
		{
			objRenderers = base.gameObject.GetComponentsInChildren<global::UnityEngine.MeshRenderer>();
			base.Init(building, logger, controllers, definitionService);
		}

		public void EnableObjectRenderers(bool enable)
		{
			for (int i = 0; i < objRenderers.Length; i++)
			{
				objRenderers[i].enabled = enable;
			}
		}

		protected override void SetupChild(int routingIndex, global::Kampai.Game.View.TaskingMinionObject taskingChild, global::UnityEngine.RuntimeAnimatorController controller)
		{
			taskingChild.RoutingIndex = routingIndex;
			global::Kampai.Game.View.MinionObject minion = taskingChild.Minion;
			if (controller != null)
			{
				taskingChild.Minion.SetAnimController(controller);
			}
			minion.ApplyRootMotion(true);
			minion.EnableRenderers(true);
			minion.ExecuteAction(new global::Kampai.Game.View.SetAnimatorAction(minion, null, logger, new global::System.Collections.Generic.Dictionary<string, object> { { "minionPosition", routingIndex } }));
			minion.ExecuteAction(new global::Kampai.Game.View.SetAnimatorAction(minion, null, "OnGag", logger));
			minion.EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(minion, GetHashAnimationState("Base Layer.Gag"), logger));
			minion.EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(minion, GetHashAnimationState("Base Layer.Idle"), logger));
			MoveToRoutingPosition(minion, routingIndex);
		}
	}
}
