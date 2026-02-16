namespace Kampai.Game
{
	internal sealed class SetupBuildingManagerCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW)]
		public global::UnityEngine.GameObject contextView { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Util.PathFinder pathFinder { get; set; }

		public override void Execute()
		{
			global::UnityEngine.Debug.Log("[SetupBuildingManagerCommand] Starting execution...");
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("Buildings");
			global::Kampai.Game.View.BuildingManagerView buildingManagerView = gameObject.AddComponent<global::Kampai.Game.View.BuildingManagerView>();
			buildingManagerView.Init(logger, definitionService);
			base.injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(gameObject).ToName(global::Kampai.Game.GameElement.BUILDING_MANAGER);
			gameObject.transform.parent = contextView.transform;
			logger.Debug("SetupBuildingManagerCommand: Building Manager created.");
			global::UnityEngine.Debug.Log("[SetupBuildingManagerCommand] Starting WaitAFrame coroutine...");
			routineRunner.StartCoroutine(WaitAFrame());
			global::UnityEngine.Debug.Log("[SetupBuildingManagerCommand] Coroutine started, command execution complete.");
		}

		private global::System.Collections.IEnumerator WaitAFrame()
		{
			global::UnityEngine.Debug.Log("[SetupBuildingManagerCommand.WaitAFrame] Waiting one frame...");
			yield return null;
			global::UnityEngine.Debug.Log("[SetupBuildingManagerCommand.WaitAFrame] Dispatching PopulateBuildingSignal...");
			base.injectionBinder.GetInstance<global::Kampai.Game.PopulateBuildingSignal>().Dispatch();
			global::UnityEngine.Debug.Log("[SetupBuildingManagerCommand.WaitAFrame] PopulateBuildingSignal dispatched!");
			pathFinder.AllowWalkableUpdates();
			pathFinder.UpdateWalkableRegion();
		}
	}
}
