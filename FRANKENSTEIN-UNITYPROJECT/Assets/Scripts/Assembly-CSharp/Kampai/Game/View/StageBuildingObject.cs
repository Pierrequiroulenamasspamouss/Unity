namespace Kampai.Game.View
{
	public class StageBuildingObject : global::Kampai.Game.View.AnimatingBuildingObject
	{
		private global::UnityEngine.Transform stageTransform;

		internal override void Init(global::Kampai.Game.Building building, global::Kampai.Util.ILogger logger, global::System.Collections.Generic.IDictionary<string, global::UnityEngine.RuntimeAnimatorController> controllers, global::Kampai.Game.IDefinitionService definitionService)
		{
			base.Init(building, logger, controllers, definitionService);
			if (routes != null && routes.Length > 0)
			{
				stageTransform = routes[0];
			}
			if (stageTransform == null)
			{
				stageTransform = base.transform;
			}
		}

		public global::UnityEngine.Transform GetStageTransform()
		{
			return stageTransform;
		}
	}
}
