namespace Kampai.Util
{
	public abstract class BaseContext : global::strange.extensions.context.impl.MVCSContext
	{
		public BaseContext()
		{
		}

		public BaseContext(global::UnityEngine.MonoBehaviour view, bool autoStartup)
			: base(view, autoStartup)
		{
			if (global::strange.extensions.context.impl.Context.firstContext == this)
			{
				global::UnityEngine.Debug.Log(string.Format("Persistent path is {0}", global::Kampai.Util.GameConstants.PERSISTENT_DATA_PATH));
			}
		}

		protected override void addCoreComponents()
		{
			base.addCoreComponents();
			injectionBinder.Unbind<global::strange.extensions.command.api.ICommandBinder>();
			injectionBinder.Bind<global::strange.extensions.command.api.ICommandBinder>().To<global::strange.extensions.command.impl.SignalCommandBinder>().ToSingleton();
		}

		public override void Launch()
		{
			base.Launch();
			if (global::strange.extensions.context.impl.Context.firstContext == this)
			{
				injectionBinder.GetInstance<global::Kampai.Common.StartAndroidBackButtonSignal>().Dispatch();
			}
			injectionBinder.GetInstance<global::Kampai.Common.StartSignal>().Dispatch();
		}

		protected abstract void MapBindings();

		protected override void mapBindings()
		{
			base.mapBindings();
			// [FIX] Original: if (firstContext == this)
			// Problem: UIContext is firstContext, but its mapBindings runs AFTER GameContext
			// (UIRoot waits for GameContext in a coroutine). So when GameContext runs, the
			// CrossContextBinder is empty. Fix: populate on first context to reach this point,
			// using a sentinel binding to prevent duplicate execution.
			var _ccib = (injectionBinder as global::strange.extensions.injector.impl.CrossContextInjectionBinder);
			bool ccbAlreadyPopulated = _ccib != null && _ccib.CrossContextBinder != null 
				&& _ccib.CrossContextBinder.GetBinding<global::Kampai.Util.ILogger>() != null;
			if (!ccbAlreadyPopulated)
			{
				injectionBinder.Bind<global::strange.extensions.context.api.ICrossContextCapable>().ToValue(this).ToName(global::Kampai.Util.BaseElement.CONTEXT)
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.FastCommandPool>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Util.IRoutineRunner>().To<global::Kampai.Util.RoutineRunner>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.IUpdateRunner>().To<global::Kampai.Util.UpdateRunner>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.FPSUtil>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Util.IInvokerService>().To<global::Kampai.Util.InvokerService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.ICoroutineProgressMonitor>().To<global::Kampai.Util.CoroutineProgressMonitor>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.DeviceInformation>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Util.ILogger>().To<global::Kampai.Util.Logging.Hosted.LogglyLogger>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.Logging.Hosted.ILogBuffer>().To<global::Kampai.Util.Logging.Hosted.LogBuffer>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.Logging.Hosted.ILogglyService>().To<global::Kampai.Util.Logging.Hosted.LogglyService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.Logging.Hosted.LogglyDtoCache>().ToSingleton().CrossContext();
                injectionBinder.Bind<global::Kampai.Main.ILocalizationService>()
    .To<global::Kampai.Main.MockHALService>() 
    .ToSingleton()
    .CrossContext();

                injectionBinder.Bind<global::Kampai.Main.IAssetBundlesService>().To<global::Kampai.Main.AssetBundlesService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Main.ILocalContentService>().To<global::Kampai.Main.LocalContentService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.ClientVersion>().ToSingleton().CrossContext();
				injectionBinder.Bind<string>().ToValue(global::Kampai.Util.GameConstants.Server.SERVER_URL).ToName("game.server.host")
					.CrossContext();
				injectionBinder.Bind<string>().ToValue(global::Kampai.Util.GameConstants.Server.SERVER_ENVIRONMENT).ToName("game.server.environment")
					.CrossContext();
				injectionBinder.Bind<string>().ToValue(global::Kampai.Util.GameConstants.Server.CDN_METADATA_URL).ToName("cdn.server.host")
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Game.IDefinitionService>().To<global::Kampai.Game.DefinitionService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Common.AppResumeSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.BuildingUtilities>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.OrderBoardTransactionFailedSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.LoadRushDialogSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.LoadCurrencyWarningSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.OpenStorageBuildingSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.ShowSocialPartyStartSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.ShowSocialPartyRewardSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.ShowSocialPartyInviteAlertSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.ShowSocialPartyFillOrderSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.ShowSocialPartyFBConnectSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.OpenStageBuildingSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.UIModel>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.DestructibleHighlightSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.MagnetFingerIndicatorSelectSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.MagnetFingerIndicatorDeselectSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.SetupPushNotificationsSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.CoppaCompletedSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.LogToScreenSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.MinionPickSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.MinionReactInRadiusSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.TikiBarViewPickSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.UnlockMinionsSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.DisplayLevelUpRewardSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.EnvironmentalMignetteTappedSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.DeselectPickSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.BuildingPickSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.MagnetFingerPickSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.DoubleClickPickSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.DragAndDropPickSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.LandExpansionPickSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.DefinitionsChangedSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.FinishCallMinionSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.CallMinionSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.RushTaskSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.StartMinionTaskSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.AdjustBuildingTimerSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.RushOneMinionInBuildingSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.BuildingChangeStateSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.BuildingCooldownCompleteSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.BuildingCooldownUpdateViewSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.UpdateQueueIcon>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.ShowPremiumStoreSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.ShowGrindStoreSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.MinionStateChangeSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.RepairBridgeSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.SaveDevicePrefsSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.UpdateVolumeSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.ICoppaService>().To<global::Kampai.Common.CoppaService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Common.ITelemetryService>().To<global::Kampai.Common.TelemetryService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Common.NimbleTelemetrySender>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.INimbleTelemetryPostListener>().To<global::Kampai.Common.NimbleTelemetryPostListener>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Download.IDownloadService>().To<global::Kampai.Download.DownloadService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Download.IBackgroundDownloadDlcService>().To<global::Kampai.Download.BackgroundDownloadDlcService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Common.DeltaDNATelemetryService>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.DeltaDNADataMitigationSettings>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.EnableBuildingIndicatorSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.UpdateBuildingIndicatorSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.DisablePickControllerSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.SetLevelSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.CloseLevelUpRewardSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.SetXPSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.SetGrindCurrencySignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.SetPremiumCurrencySignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.SetStorageCapacitySignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.SelectBuildingSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.DeselectBuildingSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.ZoomPercentageSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.RequestZoomPercentageSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.SpawnDooberSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.SpawnMignetteDooberSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.MignetteDooberSpawnedSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.Mignette.ChangeMignetteScoreSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.PostTransactionSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.MoveMinionFinishedSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.BuildingHarvestSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.ShowBridgeUISignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.RefreshQueueSlotSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.CraftingCompleteSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.RemoveCraftingQueueSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.OrderBoardFillOrderSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.OrderBoardDeleteOrderSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.DisplayRandomDropIconSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.CameraAutoMoveToBuildingSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.PanAndOpenModalSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.PanAndOpenQuestSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.InsufficientInputsSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.ConfigurationsLoadedSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Main.MainCompleteSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.MoveBuildMenuSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.StopAutopanSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.RefreshSaleItemsSuccessSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.RushRefreshTimerSuccessSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.HaltSlotMachine>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.MarketplaceUpdateSoldItemsSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.UpdateMarketplaceSaleOrderSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.UpdateMarketplaceSlotStateSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.GenerateBuyItemsSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.OpenStoreHighlightItemSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.QuestStepRushSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.QuestDetailIDSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.DeliverTaskItemSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.CancelTSMQuestTaskSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.StartQuestTaskSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.CollectTSMQuestTaskRewardSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.TimedQuestStartSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.QuestTimeoutSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.UpdateQuestBookBadgeSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.QuestTaskReadySignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.UpdateQuestPanelSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.TimedQuestNotificationSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.ShowQuestRewardSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.HarvestReadySignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.OrderBoardStartRefillTicketSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.OrderBoardRefillTicketSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.OrderBoardFillOrderCompleteSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.ShowBuildingDetailMenuSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.ShowQuestPanelSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.ShowProceduralQuestPanelSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.UpdateProceduralQuestPanelSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.TryHarvestBuildingSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.RepairBuildingSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.RecreateBuildingSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.UITryHarvestSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.HighlightHarvestButtonSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.UI.View.SetFTUETextSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.MinionTaskCompleteSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.AddMinionToEventServiceSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.PurchaseLandExpansionSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.BurnLandExpansionSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.RequestStopMignetteSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.StopMignetteSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.StartMignetteSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.ShowAndIncreaseMignetteScoreSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.MignetteEndedSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.Mignette.DestroyMignetteContextSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.ShowNeedXMinionsSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.Mignette.MignetteScoreUpdatedSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.Mignette.StartMignetteHUDCountdownSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.CreditCollectionRewardSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.CompositeBuildingPieceAddedSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.ShuffleCompositeBuildingPiecesSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.ShowTipsSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.PopupMessageSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.PopupMessageAtLocationSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.HideSkrimSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.EnableSkrimSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.IRandomService>().ToValue(new global::Kampai.Common.RandomService(global::System.DateTime.Now.Ticks)).CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.StoreButtonClickSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.MoveBuildingSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.RotateBuildingSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.ScheduleCooldownSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.UpdateMovementValidity>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.OpenBuildingMenuSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.SelectMinionSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.DeselectMinionSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.DeselectAllMinionsSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.DeselectTaskedMinionsSignal>().ToSingleton().CrossContext()
					.Weak();
				// FIX: Keep lazy instantiation - QuestService created on-demand after all bindings ready
				injectionBinder.Bind<global::Kampai.Game.IQuestService>().To<global::Kampai.Game.QuestService>()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Game.QuestScriptKernel>().ToSingleton().CrossContext();
				// DIAGNOSTIC
				var _ccb = (injectionBinder as strange.extensions.injector.impl.CrossContextInjectionBinder);
				
			injectionBinder.Bind<global::Kampai.Game.QuestScriptController>().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.IQuestScriptRunner>().To<global::Kampai.Game.LuaScriptRunner>().ToName(global::Kampai.Game.QuestRunnerLanguage.Lua)
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Game.LuaKernel>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.IOrderBoardService>().To<global::Kampai.Game.OrderBoardService>().ToSingleton()
					.CrossContext();
				// FIX: Remove ToSingleton to prevent premature instantiation before NAMED_CHARACTER_MANAGER is created
				injectionBinder.Bind<global::Kampai.Game.IPrestigeService>().To<global::Kampai.Game.PrestigeService>()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Game.IZoomCameraModel>().To<global::Kampai.Game.ZoomCameraModel>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.IMinionBuilder>().To<global::Kampai.Util.MinionBuilder>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.IDummyCharacterBuilder>().To<global::Kampai.Util.DummyCharacterBuilder>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Util.INamedCharacterBuilder>().To<global::Kampai.Util.NamedCharacterBuilder>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Common.IVideoService>().To<global::Kampai.Common.VideoService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<ILocalPersistanceService>().To<LocalPersistanceService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<IEncryptionService>().To<EncryptionService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Game.MignetteCollectionService>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.IManifestService>().To<global::Kampai.Common.ManifestService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Common.Service.HealthMetrics.IClientHealthService>().To<global::Kampai.Common.Service.HealthMetrics.ClientHealthService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Common.Service.HealthMetrics.ITapEventMetricsService>().To<global::Kampai.Common.Service.HealthMetrics.TapEventMetricsService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Common.LogClientMetricsSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Common.LogTapEventMetricsSignal>().ToSingleton().CrossContext()
					.Weak();
				injectionBinder.Bind<global::Kampai.Game.ITimedSocialEventService>().To<global::Kampai.Game.TimedSocialEventService>().ToSingleton()
					.CrossContext();

				injectionBinder.Bind<global::Kampai.Game.SocialTeam>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.MinimizeAppSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.CheckAvailableStorageSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.NetworkModel>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Common.NetworkConnectionLostSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.ShowOfflinePopupSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.SetupCanvasSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.UIAddedSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.UI.View.UIRemovedSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.ToggleHitboxSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.CameraModel>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Game.StageService>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Main.FadeBackgroundAudioSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Splash.ILoadInService>().To<global::Kampai.Splash.LoadInService>().ToSingleton()
					.CrossContext();
				injectionBinder.Bind<global::Kampai.Main.LocalizationServiceInitializedSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Splash.SplashProgressUpdateSignal>().ToSingleton().CrossContext();
				injectionBinder.Bind<global::Kampai.Splash.SplashProgressResetSignal>().ToSingleton().CrossContext();
				
				// [FIX] Bind LoadNewGameLevelSignal as injectable signal - required by FatalView
				// [FIX] Bind LoadNewGameLevelSignal - required by FatalView.
                // Restoring explicit binding as commandBinder implicit binding was not sufficient for injection.
				injectionBinder.Bind<global::Kampai.Main.LoadNewGameLevelSignal>().ToSingleton().CrossContext();
				
				// [FIX] Removed duplicate OfflineView binding. UIContext binds this.
				// base.mediationBinder.Bind<global::Kampai.UI.View.OfflineView>().To<global::Kampai.UI.View.OfflineMediator>();
				base.commandBinder.Bind<global::Kampai.Common.StartAndroidBackButtonSignal>().To<global::Kampai.Common.StartAndroidBackButtonCommand>();
				base.commandBinder.Bind<global::Kampai.Common.AndroidBackButtonSignal>().To<global::Kampai.Game.View.AndroidBackButtonCommand>();
				base.commandBinder.Bind<global::Kampai.Game.BuildingZoomSignal>().To<global::Kampai.Game.BuildingZoomCommand>();
				base.commandBinder.Bind<global::Kampai.Common.MinimizeAppSignal>().To<global::Kampai.Main.MinimizeAppCommand>();
				base.commandBinder.Bind<global::Kampai.Common.CheckAvailableStorageSignal>().To<global::Kampai.Common.CheckAvailableStorageCommand>();
				base.commandBinder.Bind<global::Kampai.Common.NetworkConnectionLostSignal>().To<global::Kampai.Common.NetworkConnectionLostCommand>();
				base.commandBinder.Bind<global::Kampai.UI.View.ShowOfflinePopupSignal>().To<global::Kampai.UI.Controller.ShowOfflinePopupCommand>();
				base.commandBinder.Bind<global::Kampai.UI.View.SetupCanvasSignal>().To<global::Kampai.UI.View.SetupCanvasCommand>();
				base.commandBinder.Bind<global::Kampai.UI.View.UIAddedSignal>().To<global::Kampai.UI.View.UIAddedCommand>();
				base.commandBinder.Bind<global::Kampai.UI.View.UIRemovedSignal>().To<global::Kampai.UI.View.UIRemovedCommand>();
				base.commandBinder.Bind<global::Kampai.Main.LoadNewGameLevelSignal>().To<global::Kampai.Main.LoadNewGameLevelCommand>();
			}
			MapBindings();
		}

		protected virtual void PostBindings()
		{
		}

		protected override void postBindings()
		{
			PostBindings();
			base.postBindings();
		}
	}
}
