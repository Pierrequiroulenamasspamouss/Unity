namespace Kampai.Game
{
	public class SetupObjectManagersCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject(global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW)]
		public global::UnityEngine.GameObject contextView { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public override void Execute()
		{
            // Logic moved to GameContext.MapBindings to prevent race conditions.
			logger.Debug("SetupObjectManagersCommand: Managers should already be bound by GameContext.");
		}

		private T CreateManager<T>(string goName, global::Kampai.Game.GameElement bindingName) where T : global::UnityEngine.MonoBehaviour
		{
            global::UnityEngine.GameObject gameObject = null;
            T result = null;

            // 1. Try to find existing manager in the scene (to prevent race condition)
            global::UnityEngine.Transform existing = contextView.transform.Find(goName);
            if (existing != null)
            {
                gameObject = existing.gameObject;
                result = gameObject.GetComponent<T>();
                if (result == null)
                {
                    result = gameObject.AddComponent<T>();
                }
                logger.Debug("SetupObjectManagersCommand: Found existing " + goName);
            }
            else
            {
                // 2. Create new if not found
			    gameObject = new global::UnityEngine.GameObject(goName);
			    result = gameObject.AddComponent<T>();
			    gameObject.transform.parent = contextView.transform;
            }

            // 3. Bind it
			base.injectionBinder.Bind<global::UnityEngine.GameObject>().ToValue(gameObject).ToName(bindingName);
			return result;
		}
	}
}
