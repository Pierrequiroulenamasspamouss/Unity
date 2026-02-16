namespace Kampai.UI.View
{
	public class GUIService : global::Kampai.UI.View.IGUIService
	{
		private sealed class GUICommand : global::Kampai.UI.View.IGUICommand
		{
			public global::Kampai.UI.View.GUIOperation operation { get; set; }

			public global::Kampai.UI.View.GUIPriority priority { get; private set; }

			public string prefab { get; private set; }

			public bool WorldCanvas { get; set; }

			public global::Kampai.UI.View.GUIArguments Args { get; set; }

			public string GUILabel { get; set; }

			public string skrimScreen { get; set; }

			public bool darkSkrim { get; set; }

			public bool disableSkrimButton { get; set; }

			public bool singleSkrimClose { get; set; }

			public global::Kampai.UI.View.ShouldShowPredicateDelegate ShouldShowPredicate { get; set; }

			public GUICommand(global::Kampai.UI.View.GUIOperation operation, string prefab, global::Kampai.Util.ILogger logger, string guiLabel)
			{
				this.operation = operation;
				priority = global::Kampai.UI.View.GUIPriority.Lowest;
				this.prefab = prefab;
				WorldCanvas = false;
				Args = new global::Kampai.UI.View.GUIArguments(logger);
				GUILabel = guiLabel;
				disableSkrimButton = false;
				singleSkrimClose = false;
			}

			public GUICommand(global::Kampai.UI.View.GUIOperation operation, global::Kampai.UI.View.GUIPriority priority, string prefab, global::Kampai.Util.ILogger logger)
			{
				this.operation = operation;
				this.priority = priority;
				this.prefab = prefab;
				WorldCanvas = false;
				Args = new global::Kampai.UI.View.GUIArguments(logger);
				GUILabel = prefab;
				disableSkrimButton = false;
				singleSkrimClose = false;
			}
		}

		private global::UnityEngine.GameObject lastActiveInstance;

		private global::System.Collections.Generic.Dictionary<string, global::UnityEngine.GameObject> instances = new global::System.Collections.Generic.Dictionary<string, global::UnityEngine.GameObject>();

		private global::System.Collections.Generic.Queue<global::Kampai.UI.View.IGUICommand> priorityQueue = new global::System.Collections.Generic.Queue<global::Kampai.UI.View.IGUICommand>();

		private global::Kampai.UI.View.GUIArguments overrides;

		[Inject(global::Kampai.Main.MainElement.UI_WORLDCANVAS)]
		public global::UnityEngine.GameObject worldCanvas { get; set; }

		[Inject(global::Kampai.Main.MainElement.UI_GLASSCANVAS)]
		public global::UnityEngine.GameObject glassCanvas { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public global::Kampai.UI.View.IGUICommand BuildCommand(global::Kampai.UI.View.GUIOperation operation, string prefab)
		{
			return new global::Kampai.UI.View.GUIService.GUICommand(operation, prefab, logger, prefab);
		}

		public global::Kampai.UI.View.IGUICommand BuildCommand(global::Kampai.UI.View.GUIOperation operation, string prefab, string guiLabel)
		{
			return new global::Kampai.UI.View.GUIService.GUICommand(operation, prefab, logger, guiLabel);
		}

		public global::Kampai.UI.View.IGUICommand BuildCommand(global::Kampai.UI.View.GUIOperation operation, global::Kampai.UI.View.GUIPriority priority, string prefab)
		{
			return new global::Kampai.UI.View.GUIService.GUICommand(operation, priority, prefab, logger);
		}

		public global::UnityEngine.GameObject Execute(global::Kampai.UI.View.GUIOperation operation, string prefab)
		{
			global::Kampai.UI.View.IGUICommand command = ((global::Kampai.UI.View.IGUIService)this).BuildCommand(operation, prefab);
			return ((global::Kampai.UI.View.IGUIService)this).Execute(command);
		}

		public global::UnityEngine.GameObject Execute(global::Kampai.UI.View.GUIOperation operation, global::Kampai.UI.View.GUIPriority priority, string prefab)
		{
			global::Kampai.UI.View.IGUICommand command = ((global::Kampai.UI.View.IGUIService)this).BuildCommand(operation, priority, prefab);
			return ((global::Kampai.UI.View.IGUIService)this).Execute(command);
		}

		public global::UnityEngine.GameObject Execute(global::Kampai.UI.View.IGUICommand command)
		{
			if (command == null)
			{
				logger.Error("GUICommand is null");
				return null;
			}
			EnsureOverrides();
			global::Kampai.UI.View.GUIArguments args = command.Args;
			if (args != null)
			{
				args.AddArguments(overrides);
			}
			else if (overrides.Count > 0)
			{
				command.Args = overrides;
			}
			global::UnityEngine.GameObject result = null;
			switch (command.operation)
			{
			case global::Kampai.UI.View.GUIOperation.LoadStatic:
				result = LoadStatic(command);
				break;
			case global::Kampai.UI.View.GUIOperation.Load:
				result = Load(command);
				break;
			case global::Kampai.UI.View.GUIOperation.LoadUntrackedInstance:
				result = CreateNewInstance(command);
				break;
			case global::Kampai.UI.View.GUIOperation.Unload:
				Unload(command);
				break;
			case global::Kampai.UI.View.GUIOperation.Queue:
				Queue(command);
				break;
			default:
				logger.Error("Invalid GUIOperation");
				break;
			}
			return result;
		}

		private global::UnityEngine.GameObject LoadStatic(global::Kampai.UI.View.IGUICommand command)
		{
			string prefab = command.prefab;
			string gUILabel = command.GUILabel;
			if (string.IsNullOrEmpty(prefab))
			{
				logger.Error("GUISettings.Path is empty");
				return null;
			}
			if (instances.ContainsKey(gUILabel))
			{
				global::UnityEngine.GameObject gameObject = instances[gUILabel];
				if (gameObject != null)
				{
					routineRunner.StartCoroutine(Initialize(gameObject, command.Args, prefab, gUILabel));
					return instances[gUILabel];
				}
				instances.Remove(gUILabel);
			}
			global::UnityEngine.GameObject gameObject2 = CreateNewInstance(command);
			if (gameObject2 != null)
			{
				instances[gUILabel] = gameObject2;
			}
			return gameObject2;
		}

        private global::UnityEngine.GameObject CreateNewInstance(global::Kampai.UI.View.IGUICommand command)
        {
            string baseName = command.prefab;
            string guiLabel = command.GUILabel;

            string targetName = baseName + ((!global::Kampai.Util.DeviceCapabilities.IsTablet()) ? "_Phone" : "_Tablet");

            if (global::Kampai.Util.KampaiResources.FileExists(targetName))
            {
                baseName = targetName;
            }

            global::UnityEngine.GameObject gameObject = null;

            // FIX: Robust Loading Logic
            try
            {
                // 1. Try determined name (e.g. HUD_Phone)
                gameObject = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.GameObject>(baseName);

                // 2. Fallback to base name (e.g. HUD)
                if (gameObject == null && baseName != command.prefab)
                {
                    global::UnityEngine.Debug.LogWarning("[FIX] Suffix load failed. Trying base: " + command.prefab);
                    gameObject = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.GameObject>(command.prefab);
                }
            }
            catch (global::System.Exception e)
            {
                logger.Error("[FIX] Crash loading UI {0}: {1}", baseName, e.Message);
            }

            if (gameObject == null)
            {
                logger.Error("Invalid GUISettings.Path: {0}", baseName);
                return null;
            }

            global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(gameObject) as global::UnityEngine.GameObject;
            if (gameObject2 == null)
            {
                logger.Error("Unable to create instance of {0}", baseName);
                return null;
            }

            if (command.singleSkrimClose)
            {
                global::Kampai.UI.View.SkrimView component = gameObject2.GetComponent<global::Kampai.UI.View.SkrimView>();
                if (component != null)
                {
                    component.singleSkrimClose = command.singleSkrimClose;
                }
            }

            if (command.disableSkrimButton)
            {
                global::Kampai.UI.View.SkrimView component2 = gameObject2.GetComponent<global::Kampai.UI.View.SkrimView>();
                if (component2 != null)
                {
                    component2.EnableSkrimButton(false);
                }
            }
            if (command.WorldCanvas)
            {
                gameObject2.transform.parent = worldCanvas.transform;
            }
            else
            {
                gameObject2.transform.SetParent(glassCanvas.transform, false);
            }
            global::UnityEngine.CanvasGroup canvasGroup = gameObject2.AddComponent<global::UnityEngine.CanvasGroup>();
            canvasGroup.alpha = 0f;
            routineRunner.StartCoroutine(Initialize(gameObject2, command.Args, baseName, guiLabel));
            return gameObject2;
        }
        private global::UnityEngine.GameObject Load(global::Kampai.UI.View.IGUICommand command)
		{
			if (!string.IsNullOrEmpty(command.skrimScreen))
			{
				global::Kampai.UI.View.IGUICommand iGUICommand = BuildCommand(global::Kampai.UI.View.GUIOperation.Load, "Skrim", command.skrimScreen);
				iGUICommand.singleSkrimClose = command.singleSkrimClose;
				iGUICommand.disableSkrimButton = command.disableSkrimButton;
				iGUICommand.Args.Add(command.darkSkrim);
				Execute(iGUICommand);
			}
			global::UnityEngine.GameObject gameObject = LoadStatic(command);
			if (gameObject == null)
			{
				return null;
			}
			if (gameObject.activeInHierarchy)
			{
				lastActiveInstance = gameObject;
			}
			return gameObject;
		}

		private void Unload(global::Kampai.UI.View.IGUICommand command)
		{
			string gUILabel = command.GUILabel;
			if (!instances.ContainsKey(gUILabel))
			{
				logger.Error("Unable to unload instance: {0}", gUILabel);
				return;
			}
			global::UnityEngine.GameObject gameObject = instances[gUILabel];
			if (gameObject == lastActiveInstance)
			{
				lastActiveInstance = null;
			}
			global::UnityEngine.Object.Destroy(gameObject);
			instances.Remove(gUILabel);
			gameObject = null;
			Next();
		}

		private void Queue(global::Kampai.UI.View.IGUICommand command)
		{
			priorityQueue.Enqueue(command);
			Next();
		}

		private void Next()
		{
			if (lastActiveInstance != null || priorityQueue.Count == 0)
			{
				return;
			}
			global::Kampai.UI.View.IGUICommand iGUICommand = priorityQueue.Dequeue();
			if (iGUICommand == null)
			{
				return;
			}
			global::Kampai.UI.View.GUIOperation operation = iGUICommand.operation;
			if (operation != global::Kampai.UI.View.GUIOperation.Queue)
			{
				logger.Error("Invalid operation on the queue: {0}", iGUICommand.operation);
				return;
			}
			if (iGUICommand.ShouldShowPredicate == null || iGUICommand.ShouldShowPredicate())
			{
				Load(iGUICommand);
			}
			Next();
		}

		private global::System.Collections.IEnumerator Initialize(global::UnityEngine.GameObject instance, global::Kampai.UI.View.GUIArguments args, string prefabName, string guiLabel)
		{
			for (int i = 0; i < 5; i++)
			{
				yield return null;
				if (tryInitializeMediator(instance, args, prefabName, guiLabel))
				{
					break;
				}
			}
			if (instance != null)
			{
				global::UnityEngine.CanvasGroup canvasGroup = instance.GetComponent<global::UnityEngine.CanvasGroup>();
				global::UnityEngine.Object.DestroyImmediate(canvasGroup);
			}
		}

		private bool tryInitializeMediator(global::UnityEngine.GameObject instance, global::Kampai.UI.View.GUIArguments args, string prefabName, string guiLabel)
		{
			if (instance == null)
			{
				return false;
			}
			global::Kampai.UI.View.KampaiMediator component = instance.GetComponent<global::Kampai.UI.View.KampaiMediator>();
			if (component != null)
			{
				component.PrefabName = prefabName;
				component.guiLabel = guiLabel;
				component.Initialize(args);
				return true;
			}
			global::strange.extensions.mediation.impl.EventMediator component2 = instance.GetComponent<global::strange.extensions.mediation.impl.EventMediator>();
			if (component2 != null)
			{
				return true;
			}
			return false;
		}

		private void EnsureOverrides()
		{
			if (overrides == null)
			{
				overrides = new global::Kampai.UI.View.GUIArguments(logger);
			}
		}

		public void AddToArguments(object arg)
		{
			EnsureOverrides();
			overrides.Add(arg);
		}

		public void RemoveFromArguments(global::System.Type arg)
		{
			EnsureOverrides();
			overrides.Remove(arg);
		}
	}
}
