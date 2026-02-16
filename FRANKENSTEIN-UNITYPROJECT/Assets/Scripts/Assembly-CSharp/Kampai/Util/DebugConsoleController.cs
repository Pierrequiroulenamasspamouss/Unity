namespace Kampai.Util
{
	public class DebugConsoleController
	{
		private readonly global::System.Text.StringBuilder outBuilder = new global::System.Text.StringBuilder();

		private readonly global::System.Collections.Generic.Dictionary<string, global::Kampai.Util.Tuple<global::Kampai.Util.DebugCommandAttribute, global::Kampai.Util.DebugCommand>> commands = new global::System.Collections.Generic.Dictionary<string, global::Kampai.Util.Tuple<global::Kampai.Util.DebugCommandAttribute, global::Kampai.Util.DebugCommand>>();

		private readonly global::Kampai.Util.ILogger logger;

		private readonly global::Kampai.Game.QuestScriptInstance consoleQuestScript = new global::Kampai.Game.QuestScriptInstance();

		private global::Kampai.Game.View.ZoomView zoomView;

		private global::UnityEngine.Camera cameraComponent;

		private int uniqueID = 90000;

		private global::System.Collections.Generic.IList<global::Kampai.Game.TimedSocialEventDefinition> localSocialEvents;

		private global::System.Collections.Generic.IList<global::Kampai.Game.SocialEventInvitation> localInvitations;

		public readonly global::strange.extensions.signal.impl.Signal CloseConsoleSignal = new global::strange.extensions.signal.impl.Signal();

		public readonly global::strange.extensions.signal.impl.Signal FlushSignal = new global::strange.extensions.signal.impl.Signal();

		public readonly global::strange.extensions.signal.impl.Signal EnableQuestDebugSignal = new global::strange.extensions.signal.impl.Signal();

		private static bool showTransparent = true;

		private global::System.Collections.Generic.List<global::UnityEngine.GameObject> HiddenTransparentObjects;

		private global::System.Collections.Generic.Dictionary<string, global::UnityEngine.GameObject> hiddenGameObjectsMap;

		private global::Kampai.UI.View.HUDView hudView;

		[Inject]
		public global::Kampai.Game.IQuestService questService { get; set; }

		[Inject]
		public global::Kampai.Common.IVideoService videoService { get; set; }

		[Inject]
		public global::Kampai.Util.IMinionBuilder minionBuilder { get; set; }

		[Inject]
		public global::Kampai.Common.LogClientMetricsSignal metricsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ScheduleNotificationSignal notificationSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject(global::Kampai.Game.GameElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

		[Inject(global::Kampai.Main.MainElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable mainContext { get; set; }

		[Inject]
		public ILocalPersistanceService localPersistService { get; set; }

		[Inject]
		public IEncryptionService encryptionService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configurationsService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.FACEBOOK)]
		public global::Kampai.Game.ISocialService facebookService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.GAMECENTER)]
		public global::Kampai.Game.ISocialService gamecenterService { get; set; }

		[Inject(global::Kampai.Game.SocialServices.GOOGLEPLAY)]
		public global::Kampai.Game.ISocialService googlePlayService { get; set; }

		[Inject(global::Kampai.Main.MainElement.CAMERA)]
		public global::UnityEngine.GameObject cameraGO { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IMarketplaceService marketplaceService { get; set; }

		[Inject]
		public global::Kampai.Game.PurchaseLandExpansionSignal purchaseLandExpansionSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ILandExpansionService landExpansionService { get; set; }

		[Inject]
		public global::Kampai.Game.ILandExpansionConfigService landExpansionConfigService { get; set; }

		[Inject]
		public global::Kampai.Game.IPrestigeService prestigeService { get; set; }

		[Inject]
		public global::Kampai.Game.LoadAnimatorStateInfoSignal loadAnimatorStateInfoSignal { get; set; }

		[Inject]
		public global::Kampai.Game.UnloadAnimatorStateInfoSignal unloadAnimatorStateInfoSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ABTestSignal ABTestSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IConfigurationsService configService { get; set; }

		[Inject]
		public global::Kampai.Game.SavePlayerSignal savePlayerSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLoginSignal socialLoginSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SocialInitSignal socialInitSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SocialLogoutSignal socialLogoutSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject(global::Kampai.Game.QuestRunnerLanguage.Lua)]
		public global::Kampai.Game.IQuestScriptRunner luaRunner { get; set; }

		[Inject]
		public global::Kampai.Game.IDLCService dlcService { get; set; }

		[Inject]
		public global::Kampai.Game.UnlinkAccountSignal unlinkAccountSignal { get; set; }

		[Inject]
		public global::Kampai.Main.IUpsightService upsightService { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.MignetteGameModel mignetteGameModel { get; set; }

		[Inject]
		public global::Kampai.Game.MignetteCollectionService mignetteCollectionService { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingCooldownCompleteSignal cooldownCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.Game.BuildingCooldownUpdateViewSignal cooldownUpdateViewSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SpawnMignetteDooberSignal spawnDooberSignal { get; set; }

		[Inject]
		public global::Kampai.Game.Mignette.ChangeMignetteScoreSignal changeScoreSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ICurrencyService currencyService { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowClientUpgradeDialogSignal showClientUpgradeDialogSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowForcedClientUpgradeScreenSignal showForcedClientUpgradeScreenSignal { get; set; }

		[Inject]
		public global::Kampai.Game.CompositeBuildingPieceAddedSignal compositeBuildingPieceAddedSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimedSocialEventService timedSocialEventService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.SocialOrderBoardCompleteSignal socialOrderBoardCompleteSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CreateWayFinderSignal createWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RemoveWayFinderSignal removeWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.Game.SetupPushNotificationsSignal setupPushNotificationsSignal { get; set; }

		[Inject]
		public global::Kampai.Game.DebugKeyHitSignal debugKeyHitSignal { get; set; }

		[Inject(global::Kampai.Main.MainElement.UI_GLASSCANVAS)]
		public global::UnityEngine.GameObject glassCanvas { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkModel networkModel { get; set; }

		[Inject]
		public global::Kampai.Common.NetworkConnectionLostSignal networkConnectionLostSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.RushDialogConfirmationSignal dialogConfirmedSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OpenBuildingMenuSignal openBuildingMenuSignal { get; set; }

		public DebugConsoleController(global::Kampai.Util.ILogger logger)
		{
			this.logger = logger;
			try
			{
				global::System.Type typeFromHandle = typeof(global::Kampai.Util.DebugConsoleController);
				global::System.Reflection.MethodInfo[] methods = typeFromHandle.GetMethods(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
				int i = 0;
				for (int num = methods.Length; i < num; i++)
				{
					global::System.Reflection.MethodInfo methodInfo = methods[i];
					object[] customAttributes = methodInfo.GetCustomAttributes(typeof(global::Kampai.Util.DebugCommandAttribute), false);
					if (customAttributes.Length < 1)
					{
						continue;
					}
					global::Kampai.Util.DebugCommand second = null;
					try
					{
						second = global::System.Delegate.CreateDelegate(typeof(global::Kampai.Util.DebugCommand), methodInfo) as global::Kampai.Util.DebugCommand;
					}
					catch (global::System.ArgumentException ex)
					{
						outBuilder.AppendFormat("Failed to grab command method {0}: {1}", methodInfo.Name, ex.Message);
					}
					int j = 0;
					for (int num2 = customAttributes.Length; j < num2; j++)
					{
						global::Kampai.Util.DebugCommandAttribute debugCommandAttribute = customAttributes[j] as global::Kampai.Util.DebugCommandAttribute;
						string text = debugCommandAttribute.Name;
						if (text == null)
						{
							text = methodInfo.Name.ToLower();
						}
						commands.Add(text, new global::Kampai.Util.Tuple<global::Kampai.Util.DebugCommandAttribute, global::Kampai.Util.DebugCommand>(debugCommandAttribute, second));
					}
				}
			}
			catch (global::System.Exception ex2)
			{
				logger.Error("{0}, {1}, {2}", ex2.ToString(), ex2.Message, ex2.StackTrace);
				throw;
			}
		}

		[PostConstruct]
		public void Init()
		{
			zoomView = cameraGO.GetComponent<global::Kampai.Game.View.ZoomView>();
			cameraComponent = cameraGO.GetComponent<global::UnityEngine.Camera>();
			debugKeyHitSignal.AddListener(DebugKeyHit);
		}

		public global::Kampai.Util.DebugCommandError GetCommand(string[] args, out global::Kampai.Util.DebugCommand command)
		{
			int i = 0;
			for (int num = args.Length; i < num; i++)
			{
				string key = string.Join(" ", args, 0, i + 1);
				global::Kampai.Util.Tuple<global::Kampai.Util.DebugCommandAttribute, global::Kampai.Util.DebugCommand> value;
				commands.TryGetValue(key, out value);
				if (value != null)
				{
					if (value.Item1.RequiresAllArgs && args.Length - i - 1 < value.Item1.Args.Length)
					{
						command = null;
						return global::Kampai.Util.DebugCommandError.NotEnoughArguments;
					}
					command = value.Item2;
					return global::Kampai.Util.DebugCommandError.NoError;
				}
			}
			command = null;
			return global::Kampai.Util.DebugCommandError.NotFound;
		}

		public string GetOutput()
		{
			string result = outBuilder.ToString();
			outBuilder.Length = 0;
			return result;
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "command_name" })]
		[global::Kampai.Util.DebugCommand(Name = "?")]
		public void Help(string[] args)
		{
			outBuilder.AppendLine("commands:");
			string text = string.Join(" ", args, 1, args.Length - 1);
			global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> dictionary = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>>();
			foreach (string key in commands.Keys)
			{
				if (key.StartsWith(text))
				{
					string commandName;
					string subcommandName;
					SplitCommandKey(key, text, out commandName, out subcommandName);
					global::System.Collections.Generic.List<string> value;
					dictionary.TryGetValue(commandName, out value);
					if (value == null)
					{
						value = new global::System.Collections.Generic.List<string>();
						dictionary.Add(commandName, value);
					}
					if (subcommandName != null)
					{
						value.Add(subcommandName);
					}
				}
			}
			int count = dictionary.Count;
			int num = 0;
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<string>> item in dictionary)
			{
				outBuilder.Append(item.Key);
				if (item.Value.Count < 1)
				{
					OutputArguments(commands[item.Key].Item1.Args);
				}
				else
				{
					OutputSubcommands(item.Value);
				}
				if (num++ < count - 1)
				{
					outBuilder.Append(" | ");
				}
			}
			outBuilder.AppendLine("\n Use help {{command_name}} to see more details of a command.");
		}

		[global::Kampai.Util.DebugCommand]
		public void Help2(string[] args)
		{
			string text = "quantity items:\n    ";
			int num = 0;
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Kampai.Game.Definition> allDefinition in definitionService.GetAllDefinitions())
			{
				if (allDefinition.Value.LocalizedKey != null)
				{
					text += string.Format("{0}  |  ", allDefinition.Value.LocalizedKey.Replace(' ', '_').ToLower());
					num++;
					if (num > 4)
					{
						text += "\n    ";
						num = 0;
					}
				}
			}
			outBuilder.Append(text);
		}

		[global::Kampai.Util.DebugCommand]
		public void Exit(string[] args)
		{
			CloseConsoleSignal.Dispatch();
		}

		[global::Kampai.Util.DebugCommand]
		public void AutoOrderBoard(string[] args)
		{
			global::Kampai.Game.View.OrderBoardBuildingObjectView orderBoardBuildingObjectView = global::UnityEngine.Object.FindObjectOfType<global::Kampai.Game.View.OrderBoardBuildingObjectView>();
			if (orderBoardBuildingObjectView != null)
			{
				global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(orderBoardBuildingObjectView.ID);
				if (byInstanceId != null)
				{
					openBuildingMenuSignal.Dispatch(orderBoardBuildingObjectView, byInstanceId);
				}
			}
			global::Kampai.Util.DebugButton debugButton = global::UnityEngine.Object.FindObjectOfType<global::Kampai.Util.DebugButton>();
			if (debugButton != null)
			{
				debugButton.OnClick(null);
			}
			int result = 1;
			if (args.Length > 1)
			{
				int.TryParse(args[1], out result);
			}
			routineRunner.StartCoroutine(AutoOrderBoardLoop(result));
		}

		private global::System.Collections.IEnumerator AutoOrderBoardLoop(int orderCount)
		{
			yield return new global::UnityEngine.WaitForSeconds(2.5f);
			global::UnityEngine.GameObject go = global::UnityEngine.GameObject.Find("btn_FillOrder_normal");
			if (go == null)
			{
				yield return null;
			}
			FillOrderButtonView fillOrderButtonView = go.GetComponent<FillOrderButtonView>();
			if (fillOrderButtonView == null)
			{
				yield return null;
			}
			for (int i = 0; i < orderCount; i++)
			{
				global::Kampai.UI.View.OrderBoardTicketView[] obtView = global::UnityEngine.Object.FindObjectsOfType<global::Kampai.UI.View.OrderBoardTicketView>();
				if (obtView.Length > 0)
				{
					obtView[global::UnityEngine.Random.Range(0, obtView.Length)].TicketButton.ClickedSignal.Dispatch();
				}
				yield return new global::UnityEngine.WaitForSeconds(0.1f);
				if (fillOrderButtonView.gameObject.activeInHierarchy)
				{
					fillOrderButtonView.OnClickEvent();
				}
				yield return new global::UnityEngine.WaitForSeconds(2f);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void Auth(string[] args)
		{
			global::Kampai.Util.Native.getAuthToken();
		}

		[global::Kampai.Util.DebugCommand]
		public void Stream(string[] args)
		{
			videoService.playVideo("https://archive.org/download/Pbtestfilemp4videotestmp4/video_test.mp4", true, true);
		}

		[global::Kampai.Util.DebugCommand]
		public void Lod(string[] args)
		{
			outBuilder.Append(string.Join(" ", args) + " " + minionBuilder.GetLOD());
		}

		[global::Kampai.Util.DebugCommand]
		public void Crash(string[] args)
		{
			logger.Log(global::Kampai.Util.Logger.Level.Error, true, "Crashing...");
			string text = null;
			text.IndexOf("a");
		}

		[global::Kampai.Util.DebugCommand]
		public void Health(string[] args)
		{
			metricsSignal.Dispatch(false);
		}

		[global::Kampai.Util.DebugCommand]
		public void Notify(string[] args)
		{
			global::Kampai.Game.NotificationDefinition notificationDefinition = new global::Kampai.Game.NotificationDefinition();
			notificationDefinition.ID = 8;
			notificationDefinition.Seconds = 30;
			notificationDefinition.Title = "Oh";
			notificationDefinition.Text = "Debug console notification";
			notificationDefinition.Type = global::Kampai.Game.NotificationType.DebugConsole.ToString();
			notificationSignal.Dispatch(notificationDefinition);
		}

		[global::Kampai.Util.DebugCommand]
		public void Telemetry(string[] args)
		{
			telemetryService.Send_Telemetry_EVT_GAME_ERROR_GAMEPLAY("error", "test");
		}

		[global::Kampai.Util.DebugCommand]
		public void Fps(string[] args)
		{
			global::Kampai.UI.View.ToggleFPSGraphSignal instance = uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ToggleFPSGraphSignal>();
			instance.Dispatch();
		}

		[global::Kampai.Util.DebugCommand]
		public void Quality(string[] args)
		{
			outBuilder.Append(global::UnityEngine.QualitySettings.names[global::UnityEngine.QualitySettings.GetQualityLevel()]);
		}

		[global::Kampai.Util.DebugCommand]
		public void ap(string[] args)
		{
			global::UnityEngine.Time.timeScale = 1f - global::UnityEngine.Time.timeScale;
		}

		[global::Kampai.Util.DebugCommand]
		public void ToggleHUD(string[] args)
		{
			if (hudView == null)
			{
				hudView = global::UnityEngine.Object.FindObjectOfType<global::Kampai.UI.View.HUDView>();
			}
			if (hudView != null)
			{
				hudView.gameObject.SetActive(!hudView.gameObject.activeSelf);
			}
			else
			{
				logger.Error("Can't find HUD view");
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void ToggleAnimators(string[] args)
		{
			global::UnityEngine.Animator[] array = global::UnityEngine.Object.FindObjectsOfType<global::UnityEngine.Animator>();
			foreach (global::UnityEngine.Animator animator in array)
			{
				animator.enabled = false;
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void ToggleActive(string[] args)
		{
			if (args.Length < 2)
			{
				return;
			}
			global::UnityEngine.GameObject value = null;
			if (hiddenGameObjectsMap == null)
			{
				hiddenGameObjectsMap = new global::System.Collections.Generic.Dictionary<string, global::UnityEngine.GameObject>();
			}
			bool flag = hiddenGameObjectsMap.TryGetValue(args[1], out value);
			if (value == null)
			{
				value = global::UnityEngine.GameObject.Find(args[1]);
			}
			if (!(value == null))
			{
				if (!flag)
				{
					hiddenGameObjectsMap.Add(args[1], value);
				}
				value.SetActive(!value.activeSelf);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void ToggleTransparent(string[] args)
		{
			showTransparent = !showTransparent;
			if (!showTransparent)
			{
				HiddenTransparentObjects = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();
				global::UnityEngine.Renderer[] array = global::UnityEngine.Object.FindObjectsOfType<global::UnityEngine.Renderer>();
				foreach (global::UnityEngine.Renderer renderer in array)
				{
					if (renderer.material.renderQueue >= 3000)
					{
						renderer.gameObject.SetActive(false);
						HiddenTransparentObjects.Add(renderer.gameObject);
					}
				}
			}
			if (!showTransparent)
			{
				return;
			}
			foreach (global::UnityEngine.GameObject hiddenTransparentObject in HiddenTransparentObjects)
			{
				hiddenTransparentObject.SetActive(true);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void HideShader(string[] args)
		{
			if (args.Length <= 0)
			{
				return;
			}
			global::UnityEngine.Renderer[] array = global::UnityEngine.Object.FindObjectsOfType<global::UnityEngine.Renderer>();
			foreach (global::UnityEngine.Renderer renderer in array)
			{
				if (renderer.material.shader.name.ToLower().Contains(args[1].ToLower()))
				{
					renderer.gameObject.SetActive(false);
				}
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void System(string[] args)
		{
			global::UnityEngine.Resolution currentResolution = global::UnityEngine.Screen.currentResolution;
			outBuilder.AppendLine("\t Model: " + global::UnityEngine.SystemInfo.deviceModel);
			outBuilder.AppendLine("\t resolution: " + currentResolution.width + "x" + currentResolution.height + ", " + currentResolution.refreshRate);
			outBuilder.AppendLine("\t processor count: " + global::UnityEngine.SystemInfo.processorCount);
			outBuilder.AppendLine("\t ram: " + global::UnityEngine.SystemInfo.systemMemorySize);
			outBuilder.AppendLine("\t vram: " + global::UnityEngine.SystemInfo.graphicsMemorySize);
			outBuilder.AppendLine("\t shader level: " + global::UnityEngine.SystemInfo.graphicsShaderLevel);
			outBuilder.AppendLine("\t fill rate: " + global::UnityEngine.SystemInfo.graphicsPixelFillrate);
			outBuilder.AppendLine("\t gpu vendor: " + global::UnityEngine.SystemInfo.graphicsDeviceVendor);
			outBuilder.AppendLine("\t gpu name: " + global::UnityEngine.SystemInfo.graphicsDeviceName);
		}

		[global::Kampai.Util.DebugCommand]
		public void User(string[] args)
		{
			string data = localPersistService.GetData("UserID");
			outBuilder.AppendLine(string.Join(" ", args) + " -> UserID: " + data);
			string plainText = localPersistService.GetData("AnonymousID");
			encryptionService.TryDecrypt(plainText, "Kampai!", out plainText);
			outBuilder.AppendLine("\tAnonynousID: " + plainText);
			string userID = facebookService.userID;
			outBuilder.AppendLine("\tFacebookID: " + userID);
			string synergyId = NimbleBridge_SynergyIdManager.GetComponent().GetSynergyId();
			outBuilder.AppendLine("\tSynergyID: " + synergyId);
			string userID2 = googlePlayService.userID;
			outBuilder.AppendLine("\tGooglePlayID: " + userID2);
		}

		[global::Kampai.Util.DebugCommand]
		public void GC(string[] args)
		{
		}

		[global::Kampai.Util.DebugCommand]
		public void CBO(string[] args)
		{
			global::UnityEngine.Ray ray = new global::UnityEngine.Ray(cameraGO.transform.position, cameraGO.transform.forward);
			global::UnityEngine.RaycastHit hitInfo;
			if (global::UnityEngine.Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, 512))
			{
				global::UnityEngine.Vector3 center = hitInfo.collider.gameObject.GetComponent<global::Kampai.Game.View.BuildingObject>().Center;
				outBuilder.AppendLine("Offset: " + (cameraGO.transform.position - center).ToString() + " Zoom: " + zoomView.GetCurrentPercentage());
				return;
			}
			ray = new global::UnityEngine.Ray(cameraGO.transform.position + new global::UnityEngine.Vector3(2f, 0f, 2f), cameraGO.transform.forward);
			if (global::UnityEngine.Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, 512))
			{
				global::UnityEngine.Vector3 center2 = hitInfo.collider.gameObject.GetComponent<global::Kampai.Game.View.BuildingObject>().Center;
				outBuilder.AppendLine("Offset: " + (cameraGO.transform.position - center2).ToString() + " Zoom: " + zoomView.GetCurrentPercentage());
			}
			else
			{
				outBuilder.AppendLine("Unable to determine building");
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void AddMinion(string[] args)
		{
			int minionsToAdd;
			if (args.Length == 1)
			{
				minionsToAdd = 1;
			}
			else
			{
				int result;
				if (!int.TryParse(args[1], out result))
				{
					result = 1;
				}
				minionsToAdd = result;
			}
			AddMinions(minionsToAdd);
		}

		private void AddMinions(int minionsToAdd)
		{
			for (int i = 0; i < minionsToAdd; i++)
			{
				int id = global::UnityEngine.Random.Range(601, 607);
				global::Kampai.Game.MinionDefinition def = definitionService.Get<global::Kampai.Game.MinionDefinition>(id);
				global::Kampai.Game.Minion minion = new global::Kampai.Game.Minion(def);
				minion.CostumeID = 0;
				playerService.Add(minion);
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.CreateMinionSignal>().Dispatch(minion);
			}
		}

		[global::Kampai.Util.DebugCommand(Name = "addingredients", Args = new string[] { "ingredients_id", "amount" }, RequiresAllArgs = true)]
		public void AddIngredients(string[] args)
		{
			int result;
			uint result2;
			if (int.TryParse(args[1], out result) && uint.TryParse(args[2], out result2))
			{
				global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = new global::Kampai.Game.Transaction.TransactionDefinition();
				transactionDefinition.Inputs = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
				transactionDefinition.Outputs = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
				global::Kampai.Util.QuantityItem item = new global::Kampai.Util.QuantityItem(result, result2);
				transactionDefinition.Outputs.Add(item);
				playerService.RunEntireTransaction(transactionDefinition, global::Kampai.Game.TransactionTarget.INGREDIENT, TransactionCallback);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void LevelUp(string[] args)
		{
			int num;
            num = 1; //added this line to remove use of unassigned variable
            if (args.Length == 1)
			{
				num = 1;
			}
			else
			{
				int result;
				if (!int.TryParse(args[1], out result))
				{
					result = 1;
				}
				num = result - (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			}
			if (num < 0)
			{
				outBuilder.AppendLine("You can't Level Down silly.");
				return;
			}
			for (int i = 0; i < num; i++)
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.LevelUpSignal>().Dispatch();
			}
			global::System.Action<int, int> callback = delegate
			{
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.EnableCameraBehaviourSignal>().Dispatch(3);
			};
			gameContext.injectionBinder.GetInstance<global::Kampai.UI.View.PromptReceivedSignal>().AddOnce(callback);
			global::Kampai.Game.LevelXPTable levelXPTable = definitionService.Get<global::Kampai.Game.LevelXPTable>(99999);
			int quantity = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.LEVEL_ID);
			global::System.Collections.Generic.IList<int> xpNeededList = levelXPTable.xpNeededList;
			int amount = ((quantity - 1 >= xpNeededList.Count) ? xpNeededList[xpNeededList.Count - 1] : xpNeededList[quantity - 1]);
			playerService.AlterQuantity(global::Kampai.Game.StaticItem.XP_TO_LEVEL_UP_ID, (int)(0 - playerService.GetQuantity(global::Kampai.Game.StaticItem.XP_TO_LEVEL_UP_ID)));
			playerService.AlterQuantity(global::Kampai.Game.StaticItem.XP_TO_LEVEL_UP_ID, amount);
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.SetXPSignal>().Dispatch();
		}

		[global::Kampai.Util.DebugCommand]
		public void Experience(string[] args)
		{
			int result;
			if (!int.TryParse(args[1], out result))
			{
				result = 50;
			}
			playerService.CreateAndRunCustomTransaction(2, result, global::Kampai.Game.TransactionTarget.NO_VISUAL);
		}

		[global::Kampai.Util.DebugCommand]
		public void Points(string[] args)
		{
			int quantity = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.XP_ID);
			int quantity2 = (int)playerService.GetQuantity(global::Kampai.Game.StaticItem.XP_TO_LEVEL_UP_ID);
			outBuilder.AppendLine("You current have " + quantity + " experience.");
			outBuilder.AppendLine("You need " + quantity2 + " (or another action) to level up again.");
		}

		[global::Kampai.Util.DebugCommand]
		public void HelpMe(string[] args)
		{
			mainContext.injectionBinder.GetInstance<global::Kampai.Main.OpenHelpSignal>().Dispatch();
		}

		[global::Kampai.Util.DebugCommand]
		public void GPA(string[] args)
		{
			outBuilder.AppendLine(global::Kampai.Util.Native.GetGoogleAccountCount().ToString());
		}

		[global::Kampai.Util.DebugCommand]
		public void LandExpand(string[] args)
		{
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
			foreach (global::Kampai.Game.LandExpansionBuilding allExpansionBuilding in landExpansionService.GetAllExpansionBuildings())
			{
				if (!list.Contains(allExpansionBuilding.ExpansionID))
				{
					list.Add(allExpansionBuilding.ExpansionID);
				}
			}
			foreach (int item in list)
			{
				purchaseLandExpansionSignal.Dispatch(item, false);
			}
			foreach (int item2 in list)
			{
				landExpansionService.RemoveForSaleSign(item2);
			}
			outBuilder.AppendLine("Purchased " + list.Count + " expansions");
		}

		[global::Kampai.Util.DebugCommand]
		public void ClearDebris(string[] args)
		{
			global::Kampai.Game.CleanupDebrisSignal instance = gameContext.injectionBinder.GetInstance<global::Kampai.Game.CleanupDebrisSignal>();
			global::System.Collections.Generic.List<global::Kampai.Game.DebrisBuilding> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.DebrisBuilding>();
			foreach (global::Kampai.Game.DebrisBuilding item in instancesByType)
			{
				instance.Dispatch(item.ID, true);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void AddBuddy(string[] args)
		{
			global::Kampai.Game.PrestigeDefinition prestigeDefinition = definitionService.Get<global::Kampai.Game.PrestigeDefinition>(40002);
			playerService.AssignNextInstanceId(prestigeDefinition);
			global::Kampai.Game.Prestige prestige = new global::Kampai.Game.Prestige(prestigeDefinition);
			prestige.state = global::Kampai.Game.PrestigeState.Prestige;
			prestige.CurrentPrestigePoints = 100;
			prestigeService.AddPrestige(prestige);
		}

		[global::Kampai.Util.DebugCommand]
		public void CreateLocalSocialEventInvitations(string[] args)
		{
			if (localInvitations == null)
			{
				localInvitations = new global::System.Collections.Generic.List<global::Kampai.Game.SocialEventInvitation>();
			}
			global::Kampai.Game.SocialEventInvitation socialEventInvitation = new global::Kampai.Game.SocialEventInvitation();
			socialEventInvitation.EventID = 101;
			socialEventInvitation.Team = new global::Kampai.Game.SocialTeamInvitationView();
			socialEventInvitation.Team.TeamID = 12421342345234346L;
			socialEventInvitation.inviter = new global::Kampai.Game.UserIdentity();
			socialEventInvitation.inviter.ExternalID = "1374474932861011";
			socialEventInvitation.inviter.ID = "1374474932861011";
			socialEventInvitation.inviter.Type = global::Kampai.Game.IdentityType.facebook;
			localInvitations.Add(socialEventInvitation);
		}

		public void OnGetSocialEventStateResponse(global::Kampai.Game.SocialTeamResponse response, global::Kampai.Game.ErrorResponse error)
		{
			if (response.UserEvent != null && !response.UserEvent.RewardClaimed && response.Team.OrderProgress.Count == timedSocialEventService.GetCurrentSocialEvent().Orders.Count)
			{
				uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowSocialPartyRewardSignal>().Dispatch(timedSocialEventService.GetCurrentSocialEvent().ID);
			}
			else if (response.Team != null)
			{
				uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowSocialPartyFillOrderSignal>().Dispatch();
			}
			else if (response.UserEvent != null && response.UserEvent.Invitations != null && response.UserEvent.Invitations.Count > 0 && facebookService.isLoggedIn)
			{
				uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowSocialPartyInviteAlertSignal>().Dispatch();
			}
			else
			{
				uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowSocialPartyStartSignal>().Dispatch();
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void ShowSocialPartyFlow(string[] args)
		{
			if (timedSocialEventService.GetCurrentSocialEvent() != null)
			{
				parseSocialEventInvitiations();
				global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse> signal = new global::strange.extensions.signal.impl.Signal<global::Kampai.Game.SocialTeamResponse, global::Kampai.Game.ErrorResponse>();
				signal.AddListener(OnGetSocialEventStateResponse);
				timedSocialEventService.GetSocialEventState(timedSocialEventService.GetCurrentSocialEvent().ID, signal);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void AddStageBuilding(string[] args)
		{
			global::Kampai.Game.Location location = new global::Kampai.Game.Location(125, 158);
			global::Kampai.Game.StageBuildingDefinition stageBuildingDefinition = new global::Kampai.Game.StageBuildingDefinition();
			stageBuildingDefinition = definitionService.Get<global::Kampai.Game.StageBuildingDefinition>(3054);
			global::Kampai.Game.Building building = new global::Kampai.Game.StageBuilding(stageBuildingDefinition);
			building.ID = 356568568;
			building.Location = location;
			building.SetState(global::Kampai.Game.BuildingState.Idle);
			playerService.Add(building);
			global::Kampai.Game.CreateInventoryBuildingSignal instance = gameContext.injectionBinder.GetInstance<global::Kampai.Game.CreateInventoryBuildingSignal>();
			instance.Dispatch(building, location);
		}

		[global::Kampai.Util.DebugCommand]
		public void NutRush(string[] args)
		{
			EtceteraAndroid.showCustomWebView("http://192.168.64.190/~bbangerter/nut_rush/index.htm", true, false);
		}

		[global::Kampai.Util.DebugCommand]
		public void SprintClub(string[] args)
		{
			EtceteraAndroid.showCustomWebView("http://192.168.64.190/~bbangerter/sprint_club_nitro/index.htm", true, false);
		}

		[global::Kampai.Util.DebugCommand(Name = "createwayfinder", Args = new string[] { "trackedId" }, RequiresAllArgs = true)]
		[global::Kampai.Util.DebugCommand(Name = "removewayfinder", Args = new string[] { "trackedId" }, RequiresAllArgs = true)]
		public void CreateOrRemoveWayFinder(string[] args)
		{
			bool flag = args[0] == "createwayfinder";
			int num = int.Parse(args[1]);
			if (flag)
			{
				createWayFinderSignal.Dispatch(new global::Kampai.UI.View.WayFinderSettings(num));
			}
			else
			{
				removeWayFinderSignal.Dispatch(num);
			}
		}

		[global::Kampai.Util.DebugCommand(Name = "add", Args = new string[] { "quantity_item", "ammount" }, RequiresAllArgs = true)]
		[global::Kampai.Util.DebugCommand(Name = "remove", Args = new string[] { "quantity_item", "ammount" }, RequiresAllArgs = true)]
		public void AddOrRemove(string[] args)
		{
			bool add = args[0] == "add";
			int id = 0;
			if (!IdOf(args[1], out id))
			{
				outBuilder.AppendLine(string.Join(" ", args) + " -> DEFINITION NOT FOUND");
			}
			else
			{
				ProcessTransaction(args[2], id, add);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void Anim(string[] args)
		{
			if (args[1].Equals("on"))
			{
				loadAnimatorStateInfoSignal.Dispatch();
			}
			else if (args[1].Equals("off"))
			{
				unloadAnimatorStateInfoSignal.Dispatch();
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void Transaction(string[] args)
		{
			ProcessTransaction(args);
		}

		[global::Kampai.Util.DebugCommand(Name = "set lod")]
		public void SetLod(string[] args)
		{
			if (args[2].Equals("low"))
			{
				minionBuilder.SetLOD(global::Kampai.Util.TargetPerformance.LOW);
			}
			else if (args[2].Equals("medium"))
			{
				minionBuilder.SetLOD(global::Kampai.Util.TargetPerformance.MED);
			}
			else if (args[2].Equals("high"))
			{
				minionBuilder.SetLOD(global::Kampai.Util.TargetPerformance.HIGH);
			}
			outBuilder.AppendLine(string.Join(" ", args) + " -> LOD SET " + minionBuilder.GetLOD());
			mainContext.injectionBinder.GetInstance<global::Kampai.Main.ReloadGameSignal>().Dispatch();
		}

		[global::Kampai.Util.DebugCommand(Name = "set config")]
		public void SetConfig(string[] args)
		{
			if (args[2] != null)
			{
				global::Kampai.Util.ABTestCommand.GameMetaData gameMetaData = new global::Kampai.Util.ABTestCommand.GameMetaData();
				gameMetaData.configurationVariant = args[2];
				gameMetaData.debugConsoleTest = true;
				ABTestSignal.Dispatch(gameMetaData);
			}
		}

		[global::Kampai.Util.DebugCommand(Name = "set definition", Args = new string[] { "[id id]|[variants variants]" })]
		public void SetDefinition(string[] args)
		{
			bool flag = false;
			global::Kampai.Util.ABTestCommand.GameMetaData gameMetaData = new global::Kampai.Util.ABTestCommand.GameMetaData();
			gameMetaData.debugConsoleTest = true;
			for (int i = 2; i < args.Length; i += 2)
			{
				if (i + 1 < args.Length && !string.IsNullOrEmpty(args[i]) && !string.IsNullOrEmpty(args[i + 1]))
				{
					string text = args[i];
					string text2 = args[i + 1];
					switch (text)
					{
					case "id":
						gameMetaData.definitionId = text2;
						flag = true;
						break;
					case "variants":
						gameMetaData.definitionVariants = text2;
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				ABTestSignal.Dispatch(gameMetaData);
			}
		}

		[global::Kampai.Util.DebugCommand(Name = "set pref")]
		public void SetPref(string[] args)
		{
			if (args.Length != 5)
			{
				outBuilder.AppendLine("set pref: Too little arguments.");
				return;
			}
			string text = args[2];
			string text2 = args[3];
			string text3 = args[4];
			string empty = string.Empty;
			if (text.Equals("int"))
			{
				global::UnityEngine.PlayerPrefs.SetInt(text2, int.Parse(text3));
				empty = global::UnityEngine.PlayerPrefs.GetInt(text2).ToString();
			}
			else if (text.Equals("float"))
			{
				global::UnityEngine.PlayerPrefs.SetFloat(text2, float.Parse(text3));
				empty = global::UnityEngine.PlayerPrefs.GetFloat(text2).ToString();
			}
			else
			{
				global::UnityEngine.PlayerPrefs.SetString(text2, text3);
				empty = global::UnityEngine.PlayerPrefs.GetString(text2);
			}
			global::UnityEngine.PlayerPrefs.Save();
			outBuilder.AppendLine(text2 + " = " + empty);
		}

		[global::Kampai.Util.DebugCommand(Name = "get config")]
		public void GetConfig(string[] args)
		{
			outBuilder.AppendLine("Config URL: " + configService.GetConfigURL());
			outBuilder.AppendLine("Definition URL: " + configService.GetConfigurations().definitions);
		}

		[global::Kampai.Util.DebugCommand(Name = "get pref")]
		public void GetPref(string[] args)
		{
			string key = args[2];
			if (global::UnityEngine.PlayerPrefs.HasKey(key))
			{
				string empty = string.Empty;
				float num = global::UnityEngine.PlayerPrefs.GetFloat(key, float.NaN);
				int num2 = global::UnityEngine.PlayerPrefs.GetInt(key, int.MaxValue);
				empty = ((!num.Equals(float.NaN)) ? num.ToString() : (num2.Equals(int.MaxValue) ? global::UnityEngine.PlayerPrefs.GetString(key) : num2.ToString()));
				outBuilder.AppendLine(empty);
			}
			else
			{
				outBuilder.AppendLine("Key not found");
			}
		}

		[global::Kampai.Util.DebugCommand(Name = "get shaders")]
		public void GetShaders(string[] args)
		{
			global::System.Collections.Generic.HashSet<string> hashSet = new global::System.Collections.Generic.HashSet<string>();
			global::UnityEngine.Renderer[] array = global::UnityEngine.Resources.FindObjectsOfTypeAll<global::UnityEngine.Renderer>();
			foreach (global::UnityEngine.Renderer renderer in array)
			{
				global::UnityEngine.Material[] materials = renderer.materials;
				foreach (global::UnityEngine.Material material in materials)
				{
					if (material.shader != null && !string.IsNullOrEmpty(material.shader.name))
					{
						hashSet.Add(material.shader.name);
					}
				}
			}
			outBuilder.AppendLine("Shaders:");
			foreach (string item in hashSet)
			{
				outBuilder.AppendLine("\t" + item);
			}
			outBuilder.AppendLine(string.Format("Found {0} shaders", hashSet.Count));
		}

		[global::Kampai.Util.DebugCommand]
		public void Enable(string[] args)
		{
			outBuilder.AppendLine(string.Join(" ", args) + " TO DO ??? not implemented");
		}

		[global::Kampai.Util.DebugCommand(Name = "load local")]
		public void LoadLocal(string[] args)
		{
			string data = args[2];
			localPersistService.PutData("LoadMode", "local");
			localPersistService.PutData("LocalID", data);
			global::UnityEngine.Application.LoadLevel("Main");
		}

		[global::Kampai.Util.DebugCommand(Name = "load file")]
		public void LoadFile(string[] args)
		{
			string data = args[2];
			localPersistService.PutData("LoadMode", "file");
			localPersistService.PutData("LocalFileName", data);
			global::UnityEngine.Application.LoadLevel("Main");
		}

		[global::Kampai.Util.DebugCommand(Name = "load remote")]
		public void LoadRemote(string[] args)
		{
			localPersistService.PutData("LoadMode", "remote");
			global::UnityEngine.Application.LoadLevel("Main");
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "local/remote" })]
		public void Save(string[] args)
		{
			string text = args[1];
			string second = string.Empty;
			if (text == "local")
			{
				second = args[2];
			}
			savePlayerSignal.Dispatch(new global::Kampai.Util.Tuple<string, string, bool>(text, second, false));
		}

		[global::Kampai.Util.DebugCommand(Name = "delete local")]
		public void DeleteLocal(string[] args)
		{
			localPersistService.DeleteAll();
		}

		[global::Kampai.Util.DebugCommand(Name = "fb init")]
		public void FBInit(string[] args)
		{
			socialInitSignal.Dispatch(facebookService);
		}

		[global::Kampai.Util.DebugCommand(Name = "fb login")]
		public void FBLogin(string[] args)
		{
			facebookService.LoginSource = "Debug Console";
			socialLoginSignal.Dispatch(facebookService);
		}

		[global::Kampai.Util.DebugCommand(Name = "fb logout")]
		public void FBLogout(string[] args)
		{
			socialLogoutSignal.Dispatch(facebookService);
		}

		[global::Kampai.Util.DebugCommand(Name = "camera position")]
		public void CameraPosition(string[] args)
		{
			outBuilder.AppendLine(cameraGO.transform.position.ToString());
		}

		[global::Kampai.Util.DebugCommand(Name = "camera tilt")]
		public void CameraTilt(string[] args)
		{
			outBuilder.AppendLine(cameraGO.transform.eulerAngles.x.ToString());
		}

		[global::Kampai.Util.DebugCommand(Name = "camera fov")]
		public void CameraFOV(string[] args)
		{
			outBuilder.AppendLine(cameraComponent.fieldOfView.ToString());
		}

		[global::Kampai.Util.DebugCommand(Name = "camera initial zoom")]
		public void CameraInitialZoom(string[] args)
		{
			outBuilder.AppendLine(zoomView.InitialFraction.ToString());
		}

		[global::Kampai.Util.DebugCommand(Name = "synergy login")]
		public void SynergyLogin(string[] args)
		{
			string userSynergyId = args[2];
			logger.Debug("Logging into synergy");
			NimbleBridge_SynergyIdManager.GetComponent().Login(userSynergyId, "test");
		}

		[global::Kampai.Util.DebugCommand(Name = "purchase a")]
		public void PurchaseApprove(string[] args)
		{
			PurchaseApproval(true);
		}

		[global::Kampai.Util.DebugCommand(Name = "purchase d")]
		public void PurchaseDeny(string[] args)
		{
			PurchaseApproval(false);
		}

		[global::Kampai.Util.DebugCommand(Name = "map view enable")]
		public void MapEnable(string[] args)
		{
			SetMap(true);
		}

		[global::Kampai.Util.DebugCommand(Name = "map view disable")]
		public void MapDisable(string[] args)
		{
			SetMap(false);
		}

		[global::Kampai.Util.DebugCommand(Name = "grid enable")]
		public void GridEnable(string[] args)
		{
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.PopulateEnvironmentSignal>().Dispatch(true);
		}

		[global::Kampai.Util.DebugCommand(Name = "grid disable")]
		public void GridDisable(string[] args)
		{
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.PopulateEnvironmentSignal>().Dispatch(false);
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "id" })]
		public void StartQuest(string[] args)
		{
			global::Kampai.Game.QuestDefinition def = definitionService.Get<global::Kampai.Game.QuestDefinition>(int.Parse(args[1]));
			global::Kampai.Game.Quest quest = new global::Kampai.Game.Quest(def);
			quest.Initialize();
			questService.RemoveQuest(quest);
			questService.AddQuest(quest);
			questService.StartQuestScript(quest, true);
		}

		[global::Kampai.Util.DebugCommand]
		public void Lua(string[] args)
		{
			string scriptText = string.Join(" ", args, 1, args.Length - 1);
			luaRunner.Stop();
			luaRunner.Start(consoleQuestScript, scriptText, "THE ALMIGHTY DEBUG CONSOLE", null);
		}

		[global::Kampai.Util.DebugCommand]
		public void LuaFile(string[] args)
		{
			luaRunner.Stop();
			global::UnityEngine.TextAsset textAsset = global::UnityEngine.Resources.Load<global::UnityEngine.TextAsset>(args[1]);
			luaRunner.Start(consoleQuestScript, textAsset.text, args[1], null);
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "id" })]
		public void ShowQuest(string[] args)
		{
			global::Kampai.Game.QuestDefinition questDefinition = definitionService.Get<global::Kampai.Game.QuestDefinition>(int.Parse(args[1]));
			global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.Quest> questMap = questService.GetQuestMap();
			global::Kampai.Game.Quest quest;
			if (questMap.ContainsKey(questDefinition.ID))
			{
				quest = questMap[questDefinition.ID];
			}
			else
			{
				quest = new global::Kampai.Game.Quest(questDefinition);
				quest.Initialize();
				questService.AddQuest(quest);
			}
			if (quest.state == global::Kampai.Game.QuestState.Notstarted)
			{
				quest.state = global::Kampai.Game.QuestState.RunningTasks;
			}
			foreach (global::Kampai.Game.QuestStep step in quest.Steps)
			{
				step.state = ((step.state != global::Kampai.Game.QuestStepState.Notstarted) ? step.state : global::Kampai.Game.QuestStepState.Inprogress);
			}
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowQuestPanelSignal>().Dispatch(quest.ID);
		}

		[global::Kampai.Util.DebugCommand(Name = "dlc list")]
		public void DLCList(string[] args)
		{
			outBuilder.AppendLine("DLC Packs:");
			global::System.IO.DirectoryInfo directoryInfo = new global::System.IO.DirectoryInfo(global::Kampai.Util.GameConstants.DLC_PATH);
			int num = 0;
			global::System.IO.FileInfo[] files = directoryInfo.GetFiles();
			foreach (global::System.IO.FileInfo fileInfo in files)
			{
				outBuilder.AppendLine("\t" + fileInfo.Name);
				num++;
			}
			outBuilder.AppendLine(string.Format("Found {0} bundles", num));
		}

		[global::Kampai.Util.DebugCommand(Name = "dlc clear")]
		public void DLCClear(string[] args)
		{
			global::System.IO.DirectoryInfo directoryInfo = new global::System.IO.DirectoryInfo(global::Kampai.Util.GameConstants.DLC_PATH);
			global::System.IO.FileInfo[] files = directoryInfo.GetFiles();
			foreach (global::System.IO.FileInfo fileInfo in files)
			{
				fileInfo.Delete();
			}
			global::UnityEngine.Application.LoadLevel("Initialize");
		}

		[global::Kampai.Util.DebugCommand(Name = "dlc quality")]
		public void DLCQuality(string[] args)
		{
			outBuilder.AppendLine("DLC Download Quality = " + dlcService.GetDownloadQualityLevel());
			outBuilder.AppendLine("DLC Display Quality = " + dlcService.GetDisplayQualityLevel());
			outBuilder.AppendLine("DLC Tier = " + playerService.GetQuantity(global::Kampai.Game.StaticItem.TIER_ID));
		}

		[global::Kampai.Util.DebugCommand(Name = "dlc setquality")]
		public void DLCSetQuality(string[] args)
		{
			string value = args[2];
			global::Kampai.Util.TargetPerformance targetPerformance = (global::Kampai.Util.TargetPerformance)(int)global::System.Enum.Parse(typeof(global::Kampai.Util.TargetPerformance), value, true);
			if (targetPerformance != global::Kampai.Util.TargetPerformance.UNKNOWN && targetPerformance != global::Kampai.Util.TargetPerformance.UNSUPPORTED)
			{
				localPersistService.PutData("FORCE_LOD", targetPerformance.ToString());
				mainContext.injectionBinder.GetInstance<global::Kampai.Main.ReloadGameSignal>().Dispatch();
			}
		}

		[global::Kampai.Util.DebugCommand(Name = "dlc tier")]
		public void DLCTier(string[] args)
		{
			outBuilder.AppendLine("Player's DLC Tier = " + playerService.GetQuantity(global::Kampai.Game.StaticItem.TIER_ID));
			outBuilder.AppendLine("Player's Tier Gate = " + playerService.GetQuantity(global::Kampai.Game.StaticItem.TIER_GATE_ID));
			outBuilder.AppendLine("Highest Tier Downloaded  = " + gameContext.injectionBinder.GetInstance<global::Kampai.Download.DLCModel>().HighestTierDownloaded);
		}

		[global::Kampai.Util.DebugCommand(Name = "dlc preinstall")]
		public void DLCPreInstall(string[] args)
		{
			string value = global::System.IO.File.ReadAllText(global::Kampai.Util.GameConstants.PREINSTALL_JSON_PATH);
			global::Kampai.Util.PreinstallBundles preinstallBundles = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Util.PreinstallBundles>(value);
			foreach (string bundle in preinstallBundles.Bundles)
			{
				outBuilder.AppendLine(bundle);
			}
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "fb/gc/gp" })]
		public void Unlink(string[] args)
		{
			global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.IdentityType> dictionary = new global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.IdentityType>();
			dictionary.Add("fb", global::Kampai.Game.IdentityType.facebook);
			dictionary.Add("gc", global::Kampai.Game.IdentityType.gamecenter);
			dictionary.Add("gp", global::Kampai.Game.IdentityType.googleplay);
			global::System.Collections.Generic.Dictionary<string, global::Kampai.Game.IdentityType> dictionary2 = dictionary;
			global::Kampai.Game.IdentityType value;
			if (dictionary2.TryGetValue(args[1], out value))
			{
				unlinkAccountSignal.Dispatch(value);
			}
			else
			{
				outBuilder.AppendLine("Invalid identity type!");
			}
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "placement id" })]
		public void Upsight(string[] args)
		{
			string text = args[1];
			global::Kampai.Game.UpsightPromoTrigger.Placement placement = global::Kampai.Game.UpsightPlacementUtils.GetPlacement(text);
			if (placement != global::Kampai.Game.UpsightPromoTrigger.Placement.Unknown)
			{
				upsightService.SendContentRequest(placement);
				return;
			}
			logger.Info("Debug console: send Upsight content request for placement: {0}", text);
			//Upsight.sendContentRequest(text, true);
			global::Upsight.sendContentRequest(text, true); // not sure it's the correct upsight? 

        }

        [global::Kampai.Util.DebugCommand(Args = new string[] { "def id" }, RequiresAllArgs = true)]
		public void CreatePrestige(string[] args)
		{
			int result = 0;
			int.TryParse(args[1], out result);
			global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(result, false);
			if (prestige != null)
			{
				outBuilder.AppendFormat("Prestige already exists for def id {0}: {1}\n", result, prestige);
				return;
			}
			global::Kampai.Game.PrestigeDefinition prestigeDefinition = definitionService.Get<global::Kampai.Game.PrestigeDefinition>(result);
			if (prestigeDefinition == null)
			{
				outBuilder.AppendLine(string.Join(" ", args) + " -> DEFINITION NOT FOUND");
				return;
			}
			outBuilder.AppendLine("Creating new prestige from def id " + result);
			prestige = new global::Kampai.Game.Prestige(prestigeDefinition);
			prestigeService.AddPrestige(prestige);
			prestigeService.ChangeToPrestigeState(prestige, global::Kampai.Game.PrestigeState.Prestige);
			outBuilder.AppendFormat("Prestige created: {0}\n", prestige);
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "instance id", "target state" }, RequiresAllArgs = true)]
		[global::Kampai.Util.DebugCommand(Name = "pc", Args = new string[] { "instance id", "target state" }, RequiresAllArgs = true)]
		public void PrestigeCharacter(string[] args)
		{
			int result = 0;
			int.TryParse(args[1], out result);
			global::Kampai.Game.Prestige byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Prestige>(result);
			if (byInstanceId == null)
			{
				outBuilder.AppendFormat("Prestige with id {0} not found!\n", result);
				return;
			}
			global::Kampai.Game.PrestigeState prestigeState;
			try
			{
				prestigeState = (global::Kampai.Game.PrestigeState)(int)global::System.Enum.Parse(typeof(global::Kampai.Game.PrestigeState), args[2], true);
			}
			catch (global::System.ArgumentException)
			{
				outBuilder.AppendFormat("{0} is not a quest state.\n", args[2]);
				return;
			}
			if (byInstanceId.state == global::Kampai.Game.PrestigeState.Prestige && prestigeState == global::Kampai.Game.PrestigeState.Prestige)
			{
				int num = byInstanceId.CurrentPrestigeLevel + 1;
				outBuilder.AppendFormat("Adding 1 to prestige level -> CurrentPrestigeLevel is now {0}\n", num);
				prestigeService.ChangeToPrestigeState(byInstanceId, global::Kampai.Game.PrestigeState.Prestige, num);
			}
			else
			{
				outBuilder.AppendFormat("Changing prestige from {0} state to {1} state.\n", byInstanceId.state, prestigeState);
				prestigeService.ChangeToPrestigeState(byInstanceId, prestigeState);
			}
		}

		[global::Kampai.Util.DebugCommand(Name = "lsp")]
		public void ListPrestige(string[] args)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.Prestige> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Prestige>();
			int i = 0;
			for (int count = instancesByType.Count; i < count; i++)
			{
				global::Kampai.Game.Prestige prestige = instancesByType[i];
				int num = 0;
				int num2 = 0;
				if (prestige.Definition.PrestigeLevelSettings != null)
				{
					num = prestige.CurrentPrestigeLevel;
					num2 = prestige.NeededPrestigePoints;
				}
				outBuilder.AppendFormat("{0}: ID:{1} State:{2} Level:{3} Points:{4} Needed:{5}\n", prestige.Definition.LocalizedKey, prestige.ID, num, global::System.Enum.GetName(typeof(global::Kampai.Game.PrestigeState), prestige.state), prestige.CurrentPrestigePoints, num2);
			}
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "character id" })]
		public void UnlockCharacter(string[] args)
		{
			int result = 0;
			int.TryParse(args[1], out result);
			if (result != 0)
			{
				global::Kampai.Game.Prestige prestige = prestigeService.GetPrestige(result);
				if (prestige == null)
				{
					outBuilder.AppendLine(string.Join(" ", args) + " -> CHARACTER NOT FOUND");
				}
				else if (prestige.state != global::Kampai.Game.PrestigeState.Taskable && prestige.state == global::Kampai.Game.PrestigeState.Questing)
				{
					prestigeService.ChangeToPrestigeState(prestige, global::Kampai.Game.PrestigeState.Taskable);
				}
			}
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "fatal id", "fatal code" })]
		public void Fatal(string[] args)
		{
			global::Kampai.Util.FatalCode code = (global::Kampai.Util.FatalCode)0;
			int result = 0;
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			if (args.Length > 1)
			{
				int result2 = 0;
				int.TryParse(args[1], out result2);
				code = (global::Kampai.Util.FatalCode)result2;
				if (args.Length > 2)
				{
					int.TryParse(args[2], out result);
				}
				for (int i = 4; i <= args.Length; i++)
				{
					stringBuilder.Append(args[i - 1]).Append(" ");
				}
			}
			string text = stringBuilder.ToString();
			if (string.IsNullOrEmpty(text))
			{
				logger.Fatal(code, result);
			}
			else
			{
				logger.Fatal(code, result, text);
			}
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "points" })]
		public void AddMignetteScore(string[] args)
		{
			int result;
			if (!mignetteGameModel.IsMignetteActive)
			{
				outBuilder.AppendLine(args[0] + "-> Can only be called while mignette is active");
			}
			else if (args.Length > 1 && int.TryParse(args[1], out result))
			{
				changeScoreSignal.Dispatch(result);
				spawnDooberSignal.Dispatch(glassCanvas.transform.GetComponentInChildren<global::Kampai.UI.View.MignetteHUDView>(), new global::UnityEngine.Vector3(250f, 250f, 0f), result, true);
			}
			else
			{
				outBuilder.AppendLine("Usage: addmignettescore 1234");
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void AwardNextPrize(string[] args)
		{
			if (!mignetteGameModel.IsMignetteActive)
			{
				outBuilder.AppendLine(args[0] + "-> Can only be called while mignette is active");
				return;
			}
			global::Kampai.Game.RewardCollection activeCollectionForMignette = mignetteCollectionService.GetActiveCollectionForMignette(mignetteGameModel.BuildingId, false);
			int pointTotalForNextReward = activeCollectionForMignette.GetPointTotalForNextReward();
			changeScoreSignal.Dispatch(pointTotalForNextReward);
			spawnDooberSignal.Dispatch(glassCanvas.transform.GetComponentInChildren<global::Kampai.UI.View.MignetteHUDView>(), new global::UnityEngine.Vector3(250f, 250f, 0f), pointTotalForNextReward, true);
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "version" })]
		public void Version(string[] args)
		{
			int result;
			if (args.Length > 1 && int.TryParse(args[1], out result))
			{
				localPersistService.PutDataInt("OverrideVersion", result);
				localPersistService.PutData("OverrideVersionPersistState", "keep");
				outBuilder.AppendLine("New version will be active on app restart.");
			}
			else
			{
				outBuilder.AppendLine("Usage: version 1234");
			}
		}

		[global::Kampai.Util.DebugCommand(Name = "upgrade", Args = new string[] { "f/o" })]
		public void UpgradeClient(string[] args)
		{
			switch (args[1])
			{
			case "f":
				showForcedClientUpgradeScreenSignal.Dispatch();
				break;
			case "o":
				showClientUpgradeDialogSignal.Dispatch();
				break;
			default:
				outBuilder.Append("Usage: update f / update o");
				break;
			}
		}

		[global::Kampai.Util.DebugCommand(Name = "idof", Args = new string[] { "item_name" }, RequiresAllArgs = true)]
		public void IdOfCommand(string[] args)
		{
			int id;
			if (!IdOf(args[1], out id))
			{
				outBuilder.AppendLine(string.Join(" ", args) + " -> DEFINITION NOT FOUND");
			}
			else
			{
				outBuilder.AppendLine(id.ToString());
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void UnlockSticker(string[] args)
		{
			int result;
			if (int.TryParse(args[1], out result))
			{
				playerService.CreateAndRunCustomTransaction(result, 1, global::Kampai.Game.TransactionTarget.NO_VISUAL);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void AddToTotemPole(string[] args)
		{
			foreach (global::Kampai.Game.CompositeBuildingPieceDefinition item in definitionService.GetAll<global::Kampai.Game.CompositeBuildingPieceDefinition>())
			{
				if (playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.CompositeBuildingPiece>(item.ID) != null)
				{
					continue;
				}
				playerService.CreateAndRunCustomTransaction(item.ID, 1, global::Kampai.Game.TransactionTarget.NO_VISUAL);
				global::Kampai.Game.CompositeBuilding firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.CompositeBuilding>(item.BuildingDefinitionID);
				compositeBuildingPieceAddedSignal.Dispatch(firstInstanceByDefinitionId);
				break;
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void UM(string[] args)
		{
			int num = 1;
			global::System.Collections.Generic.List<int> list = new global::System.Collections.Generic.List<int>();
			foreach (global::Kampai.Game.LandExpansionBuilding allExpansionBuilding in landExpansionService.GetAllExpansionBuildings())
			{
				global::Kampai.Game.LandExpansionConfig expansionConfig = landExpansionConfigService.GetExpansionConfig(allExpansionBuilding.ExpansionID);
				foreach (int containedAspirationalBuilding in expansionConfig.containedAspirationalBuildings)
				{
					global::Kampai.Game.Building aspirationalBuilding = landExpansionService.GetAspirationalBuilding(containedAspirationalBuilding);
					if (playerService.GetByInstanceId<global::Kampai.Game.Instance>(aspirationalBuilding.ID) == null)
					{
						playerService.Add(aspirationalBuilding);
						list.Add(containedAspirationalBuilding);
					}
				}
			}
			global::UnityEngine.GameObject instance = gameContext.injectionBinder.GetInstance<global::UnityEngine.GameObject>(global::Kampai.Game.GameElement.BUILDING_MANAGER);
			global::Kampai.Game.View.BuildingManagerView component = instance.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			foreach (int item in list)
			{
				global::Kampai.Game.Building aspirationalBuilding2 = landExpansionService.GetAspirationalBuilding(item);
				global::Kampai.Game.AspirationalBuildingDefinition aspirationalBuildingDefinition = definitionService.Get<global::Kampai.Game.AspirationalBuildingDefinition>(item);
				component.CleanupBuilding(-aspirationalBuildingDefinition.ID);
				aspirationalBuilding2.SetState(global::Kampai.Game.BuildingState.Idle);
				global::Kampai.Game.CreateInventoryBuildingSignal instance2 = gameContext.injectionBinder.GetInstance<global::Kampai.Game.CreateInventoryBuildingSignal>();
				instance2.Dispatch(aspirationalBuilding2, aspirationalBuilding2.Location);
			}
			global::System.Collections.Generic.ICollection<int> animatingBuildingIDs = playerService.GetAnimatingBuildingIDs();
			foreach (int item2 in animatingBuildingIDs)
			{
				global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(item2);
				global::Kampai.Game.MignetteBuilding mignetteBuilding = byInstanceId as global::Kampai.Game.MignetteBuilding;
				if (mignetteBuilding != null)
				{
					byInstanceId.SetState(global::Kampai.Game.BuildingState.Idle);
					if (mignetteBuilding.GetMinionSlotsOwned() > num)
					{
						num = mignetteBuilding.GetMinionSlotsOwned();
					}
					outBuilder.Append(mignetteBuilding.Definition.LocalizedKey + "(" + item2 + ") set to Idle\n");
				}
			}
			global::System.Collections.Generic.List<global::Kampai.Game.Minion> idleMinions = playerService.GetIdleMinions();
			int num2 = num - idleMinions.Count;
			outBuilder.Append(num2 + " more idle minions added\n");
			AddMinions(num2);
		}

		[global::Kampai.Util.DebugCommand]
		public void SetCooldown(string[] args)
		{
			int result;
			if (!int.TryParse(args[1], out result))
			{
				return;
			}
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(mignetteGameModel.BuildingId);
			global::Kampai.Game.MignetteBuilding mignetteBuilding = byInstanceId as global::Kampai.Game.MignetteBuilding;
			if (mignetteBuilding != null)
			{
				int cooldown = mignetteBuilding.GetCooldown();
				int num = cooldown - result;
				if ((float)num < 0f)
				{
					num = 0;
					result = cooldown;
				}
				timeEventService.RushEvent(byInstanceId.ID);
				byInstanceId.StateStartTime = timeService.GameTimeSeconds() - num;
				byInstanceId.SetState(global::Kampai.Game.BuildingState.Cooldown);
				int num2 = mignetteBuilding.GetCooldown() / 10;
				for (int i = 0; i < 10; i++)
				{
					timeEventService.AddEvent(byInstanceId.ID, byInstanceId.StateStartTime, i * num2, cooldownUpdateViewSignal);
				}
				timeEventService.AddEvent(byInstanceId.ID, byInstanceId.StateStartTime, mignetteBuilding.GetCooldown(), cooldownCompleteSignal);
				outBuilder.Append("Altered building:" + byInstanceId.Definition.ID + " cooldown to " + result);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void TriggerSale(string[] args)
		{
			int result;
			if (int.TryParse(args[1], out result))
			{
				global::Kampai.Game.SaleDefinition type = definitionService.Get<global::Kampai.Game.SaleDefinition>(result);
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.DisplaySaleSignal>().Dispatch(type);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void TriggerConfirmation(string[] args)
		{
			global::strange.extensions.signal.impl.Signal<bool> signal = new global::strange.extensions.signal.impl.Signal<bool>();
			signal.AddListener(delegate(bool result)
			{
				outBuilder.Append("User Selected: " + result);
				FlushSignal.Dispatch();
			});
			global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>> type = new global::Kampai.Util.Tuple<string, string, string, global::strange.extensions.signal.impl.Signal<bool>>("popupConfirmationDefaultTitle", "popupConfirmationDefaultTitle", "img_char_Min_FeedbackChecklist01", signal);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.QueueConfirmationSignal>().Dispatch(type);
		}

		[global::Kampai.Util.DebugCommand]
		public void SMTM(string[] args)
		{
			playerService.CreateAndRunCustomTransaction(0, 90000, global::Kampai.Game.TransactionTarget.CURRENCY);
			playerService.CreateAndRunCustomTransaction(1, 90000, global::Kampai.Game.TransactionTarget.CURRENCY);
		}

		[global::Kampai.Util.DebugCommand(Name = "dcn", Args = new string[] { "featured" })]
		public void DCN(string[] args)
		{
			switch (args[1])
			{
			case "featured":
				break;
			}
		}

		[global::Kampai.Util.DebugCommand(Name = "inventory", Args = new string[] { "typename" }, RequiresAllArgs = true)]
		public void Inventory(string[] args)
		{
			global::System.Type type = null;
			global::System.Type[] types = global::System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
			foreach (global::System.Type type2 in types)
			{
				if (type2.Name.ToLower() == args[1].ToLower())
				{
					type = type2;
					break;
				}
			}
			if (type == null)
			{
				outBuilder.AppendFormat("Type {0} not found", args[1]);
				return;
			}
			global::System.Reflection.MethodInfo method = playerService.GetType().GetMethod("GetInstancesByType");
			global::System.Reflection.MethodInfo methodInfo = method.MakeGenericMethod(type);
			global::System.Collections.IList list = methodInfo.Invoke(playerService, null) as global::System.Collections.IList;
			foreach (object item in list)
			{
				global::Kampai.Game.Instance instance = item as global::Kampai.Game.Instance;
				outBuilder.AppendFormat("{0}: {1}\n", instance.ID, instance.ToString());
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void StuartShow(string[] args)
		{
			socialOrderBoardCompleteSignal.Dispatch();
		}

		[global::Kampai.Util.DebugCommand]
		public void AddPhilToTikibar(string[] args)
		{
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.PhilSitAtBarSignal>().Dispatch(true);
		}

		[global::Kampai.Util.DebugCommand]
		public void EnableBuildMenu(string[] args)
		{
			bool type = args.Length <= 1 || !(args[1] == "off");
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.SetBuildMenuEnabledSignal>().Dispatch(type);
		}

		[global::Kampai.Util.DebugCommand]
		public void InitPN(string[] args)
		{
			localPersistService.PutDataPlayer("AllowPushNotifications", "true");
			setupPushNotificationsSignal.Dispatch();
		}

		[global::Kampai.Util.DebugCommand(Name = "pgtcooldown", Args = new string[] { "seconds" }, RequiresAllArgs = true)]
		public void pgtcooldown(string[] args)
		{
			int result;
			if (int.TryParse(args[1], out result) && result > 0)
			{
				global::Kampai.Game.TSMCharacter firstInstanceByDefinitionId = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.TSMCharacter>(70008);
				firstInstanceByDefinitionId.Definition.CooldownInSeconds = result;
				outBuilder.Append("Altered travelling sales minion's cooldown to " + result);
			}
			else
			{
				outBuilder.Append("Bad cooldown: " + args[1]);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void CompleteQuest(string[] args)
		{
			int result;
			if (int.TryParse(args[1], out result))
			{
				global::Kampai.Game.QuestDefinition def = definitionService.Get<global::Kampai.Game.QuestDefinition>(result);
				global::Kampai.Game.Quest quest = new global::Kampai.Game.Quest(def);
				quest.Initialize();
				quest.state = global::Kampai.Game.QuestState.Complete;
				questService.AddQuest(quest);
				gameContext.injectionBinder.GetInstance<global::Kampai.Game.GetNewQuestSignal>().Dispatch();
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void FinishQuest(string[] args)
		{
			int result;
			if (int.TryParse(args[1], out result))
			{
				global::Kampai.Game.Quest quest = questService.GetQuestMap()[result];
				questService.GoToQuestState(quest, global::Kampai.Game.QuestState.Harvestable);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void FinishQuests(string[] args)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.Quest> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Quest>();
			foreach (global::Kampai.Game.Quest item in instancesByType)
			{
				if (item.state == global::Kampai.Game.QuestState.RunningTasks)
				{
					questService.GoToQuestState(item, global::Kampai.Game.QuestState.Harvestable);
				}
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void BackButton(string[] args)
		{
			global::Kampai.UI.View.UIModel instance = uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.UIModel>();
			if (instance.UIOpen)
			{
				global::System.Action action = instance.RemoveTopUI();
				if (action != null)
				{
					action();
				}
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void ShowQuests(string[] args)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.Quest> instancesByType = playerService.GetInstancesByType<global::Kampai.Game.Quest>();
			foreach (global::Kampai.Game.Quest item in instancesByType)
			{
				outBuilder.AppendFormat("Quest ID {0}: State - {1} DefID - {2} LineID - {3} SurfaceType - {4} SurfaceID - {5}\n", item.ID, item.state.ToString(), item.GetActiveDefinition().ID, item.GetActiveDefinition().QuestLineID, item.GetActiveDefinition().SurfaceType.ToString(), item.GetActiveDefinition().SurfaceID);
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void ShowQuestLines(string[] args)
		{
			global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.QuestLine> questLines = questService.GetQuestLines();
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Kampai.Game.QuestLine> item in questLines)
			{
				global::Kampai.Game.QuestLine value = item.Value;
				outBuilder.AppendFormat("First Quest Id - {0} Last QuestId - {1} State - {2} \n", value.Quests[value.Quests.Count - 1].ID, value.Quests[0].ID, value.state.ToString());
			}
		}

		[global::Kampai.Util.DebugCommand]
		public void ShowQuestDebug(string[] args)
		{
			EnableQuestDebugSignal.Dispatch();
		}

		[global::Kampai.Util.DebugCommand(Args = new string[] { "delay" })]
		public void Disconnect(string[] args)
		{
			float result = 0f;
			if (args.Length > 1 && !float.TryParse(args[1], out result))
			{
				result = 0f;
			}
			routineRunner.StartTimer("DebugDisconnectID", result, networkConnectionLostSignal.Dispatch);
		}

		[global::Kampai.Util.DebugCommand]
		public void ResetMignetteScore(string[] args)
		{
			mignetteCollectionService.ResetMignetteProgress();
		}

		[global::Kampai.Util.DebugCommand(Name = "showhud", Args = new string[] { "on/off" }, RequiresAllArgs = true)]
		public void ShowHUD(string[] args)
		{
			bool type = args[1] == "on";
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowHUDSignal>().Dispatch(type);
		}

		[global::Kampai.Util.DebugCommand]
		public void HideWayfinders(string[] args)
		{
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.HideAllWayFindersSignal>().Dispatch();
		}

		[global::Kampai.Util.DebugCommand]
		public void RandomFlyOver(string[] args)
		{
			int result = 0;
			if (args.Length > 1)
			{
				int.TryParse(args[1], out result);
			}
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.RandomFlyOverSignal>().Dispatch(result);
		}

		[global::Kampai.Util.DebugCommand]
		public void ClearVideo(string[] args)
		{
			global::UnityEngine.PlayerPrefs.SetString("VideoCache", "INVALID");
			global::UnityEngine.PlayerPrefs.SetInt("intro_video_played", 0);
		}

		[global::Kampai.Util.DebugCommand]
		public void ToggleSellTimers(string[] args)
		{
			marketplaceService.IsDebugMode = !marketplaceService.IsDebugMode;
		}

		[global::Kampai.Util.DebugCommand(Name = "killswitch", Args = new string[] { "clear", "[type]", "[type] on/off" }, RequiresAllArgs = false)]
		public void Killswitch(string[] args)
		{
			if (args.Length > 1)
			{
				if (args[1].ToUpper() == "CLEAR")
				{
					configurationsService.ClearAllKillswitchOverrides();
					outBuilder.Append("CLEARED All killswitch overrides.");
					return;
				}
				try
				{
					global::Kampai.Game.KillSwitch killSwitch = (global::Kampai.Game.KillSwitch)(int)global::System.Enum.Parse(typeof(global::Kampai.Game.KillSwitch), args[1].ToUpper());
					if (args.Length > 2)
					{
						switch (args[2].ToLower())
						{
						case "on":
							configurationsService.OverrideKillswitch(killSwitch, true);
							break;
						case "off":
							configurationsService.OverrideKillswitch(killSwitch, false);
							break;
						}
					}
					outBuilder.Append(killSwitch.ToString() + " killswitch state:" + configurationsService.isKillSwitchOn(killSwitch));
					return;
				}
				catch (global::System.Exception ex)
				{
					logger.Info(ex.ToString());
				}
			}
			outBuilder.Append("Invalid killswitch type in second param. Valid types: ");
			foreach (int value in global::System.Enum.GetValues(typeof(global::Kampai.Game.KillSwitch)))
			{
				outBuilder.Append(((global::Kampai.Game.KillSwitch)value).ToString() + ", ");
			}
			outBuilder.Append(" Enter 'killswitch type on/off' to set an override, or 'killswitch clear' to disable all overrides.");
		}

		[global::Kampai.Util.DebugCommand(Name = "clone", Args = new string[] { "User ID" }, RequiresAllArgs = true)]
		public void Clone(string[] args)
		{
			try
			{
				long userId = global::System.Convert.ToInt64(args[1]);
				savePlayerSignal.Dispatch(new global::Kampai.Util.Tuple<string, string, bool>("remote", string.Empty, true));
				routineRunner.StartCoroutine(ClonePlayer(userId));
			}
			catch (global::System.Exception ex)
			{
				logger.Error(ex.Message);
				outBuilder.Append("Error: [" + args[0] + "]: " + ex.Message);
			}
		}

		private global::System.Collections.IEnumerator ClonePlayer(long userId)
		{
			yield return new global::UnityEngine.WaitForSeconds(1f);
			gameContext.injectionBinder.GetInstance<global::Kampai.Game.CloneUserSignal>().Dispatch(userId);
		}

		private void DebugKeyHit(global::Kampai.Util.DebugArgument arg)
		{
			switch (arg)
			{
			case global::Kampai.Util.DebugArgument.RUSH_ALL_QUESTS:
				FinishQuests(new string[0]);
				break;
			case global::Kampai.Util.DebugArgument.BACK_BUTTON:
				BackButton(new string[0]);
				break;
			}
		}

		private void SplitCommandKey(string key, string argString, out string commandName, out string subcommandName)
		{
			int num = -1;
			if (argString.Length > 0)
			{
				if (key.Length > argString.Length)
				{
					num = key.IndexOf(' ', argString.Length + 1);
				}
			}
			else
			{
				num = key.IndexOf(" ");
			}
			if (num != -1)
			{
				commandName = key.Substring(0, num);
				int num2 = key.IndexOf(' ', num + 1);
				if (num2 == -1)
				{
					num2 = key.Length;
				}
				subcommandName = key.Substring(num + 1, num2 - num - 1);
			}
			else
			{
				commandName = key;
				subcommandName = null;
			}
		}

		private void OutputSubcommands(global::System.Collections.Generic.List<string> subcommands)
		{
			if (subcommands == null)
			{
				return;
			}
			outBuilder.Append(" <");
			int i = 0;
			for (int count = subcommands.Count; i < count; i++)
			{
				outBuilder.Append(subcommands[i]);
				if (i < count - 1)
				{
					outBuilder.Append(",");
				}
			}
			outBuilder.Append("> ");
		}

		private void OutputArguments(global::System.Collections.IEnumerable arguments)
		{
			if (arguments == null)
			{
				return;
			}
			foreach (string argument in arguments)
			{
				outBuilder.Append(" {");
				outBuilder.Append(argument);
				outBuilder.Append("} ");
			}
		}

		private void ProcessTransaction(string input, int id, bool add)
		{
			uint result = 0u;
			if (uint.TryParse(input, out result))
			{
				global::Kampai.Util.QuantityItem item = new global::Kampai.Util.QuantityItem(id, result);
				global::Kampai.Game.Transaction.TransactionDefinition transactionDefinition = new global::Kampai.Game.Transaction.TransactionDefinition();
				transactionDefinition.ID = uniqueID++;
				if (add)
				{
					transactionDefinition.Outputs = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
					transactionDefinition.Outputs.Add(item);
				}
				else
				{
					transactionDefinition.Inputs = new global::System.Collections.Generic.List<global::Kampai.Util.QuantityItem>();
					transactionDefinition.Inputs.Add(item);
				}
				playerService.RunEntireTransaction(transactionDefinition, global::Kampai.Game.TransactionTarget.HARVEST, TransactionCallback);
			}
		}

		private void ProcessTransaction(string[] input)
		{
			int result = 0;
			if (int.TryParse(input[1], out result))
			{
				playerService.RunEntireTransaction(result, global::Kampai.Game.TransactionTarget.HARVEST, TransactionCallback);
			}
		}

		private void TransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pending)
		{
			if (pending.Success)
			{
				outBuilder.AppendLine("TRANSACTION COMPLETE");
			}
			else
			{
				outBuilder.AppendLine("TRANSACTION FAILED");
			}
			FlushSignal.Dispatch();
		}

		private void PurchaseApproval(bool approve)
		{
			global::System.Collections.Generic.IList<global::Kampai.Game.KampaiPendingTransaction> pendingTransactions = playerService.GetPendingTransactions();
			if (pendingTransactions.Count > 0)
			{
				string externalIdentifier = pendingTransactions[0].ExternalIdentifier;
				global::Kampai.Game.StoreItemDefinition storeItemDefinition = definitionService.Get<global::Kampai.Game.StoreItemDefinition>(pendingTransactions[0].StoreItemDefinitionId);
				if (approve)
				{
					outBuilder.AppendLine(string.Format("Approving {0} - {1}", externalIdentifier, storeItemDefinition.LocalizedKey));
					currencyService.PurchaseSucceededAndValidatedCallback(externalIdentifier);
				}
				else
				{
					outBuilder.AppendLine(string.Format("Denying {0} - {1}", externalIdentifier, storeItemDefinition.LocalizedKey));
					currencyService.PurchaseCanceledCallback(externalIdentifier, uint.MaxValue);
				}
			}
			else
			{
				outBuilder.AppendLine("No pending transactions.");
			}
		}

		private void SetMap(bool enabled)
		{
			global::Kampai.Game.EnableBuildingAnimatorsSignal instance = gameContext.injectionBinder.GetInstance<global::Kampai.Game.EnableBuildingAnimatorsSignal>();
			global::Kampai.Game.EnableAllMinionRenderersSignal instance2 = gameContext.injectionBinder.GetInstance<global::Kampai.Game.EnableAllMinionRenderersSignal>();
			instance.Dispatch(!enabled);
			instance2.Dispatch(!enabled);
		}

		private bool IdOf(string itemName, out int id)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::Kampai.Game.Definition> allDefinition in definitionService.GetAllDefinitions())
			{
				if (allDefinition.Value.LocalizedKey != null && itemName.Equals(allDefinition.Value.LocalizedKey.Replace(' ', '_').ToLower()))
				{
					id = allDefinition.Key;
					return true;
				}
			}
			id = 0;
			return false;
		}

		private bool parseSocialEventDefinitions()
		{
			int num = timeService.GameTimeSeconds();
			foreach (global::Kampai.Game.TimedSocialEventDefinition localSocialEvent in localSocialEvents)
			{
				int startTime = localSocialEvent.StartTime;
				int finishTime = localSocialEvent.FinishTime;
				if (num >= startTime && num < finishTime)
				{
					return true;
				}
			}
			return false;
		}

		private bool parseSocialEventInvitiations()
		{
			if (localInvitations != null)
			{
				localPersistService.GetData("UserID");
				foreach (global::Kampai.Game.SocialEventInvitation localInvitation in localInvitations)
				{
					if (localInvitation.EventID == timedSocialEventService.GetCurrentSocialEvent().ID)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
