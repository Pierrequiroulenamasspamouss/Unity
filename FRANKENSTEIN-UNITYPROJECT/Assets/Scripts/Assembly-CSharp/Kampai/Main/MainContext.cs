namespace Kampai.Main
{
	public class MainContext : global::Kampai.Util.BaseContext
	{
		public MainContext()
		{
		}

		public MainContext(global::UnityEngine.MonoBehaviour view, bool autoStartup)
			: base(view, autoStartup)
		{
		}

        protected override void MapBindings()
        {
            // ========================================================================
            // FIX: FORCE UNBIND PARENT SERVICES
            // The Splash Screen (Initialize Scene) has already bound these services.
            // We must Unbind them first to clear the "Conflicted State".
            // ========================================================================



            // 2. MOCK AUDIO (FMOD)
            injectionBinder.Unbind<global::Kampai.Common.Service.Audio.IFMODService>(); // <--- IMPORTANT
            injectionBinder.Bind<global::Kampai.Common.Service.Audio.IFMODService>()
                           .To<global::Kampai.Game.MockFMODService>()
                           .ToSingleton()
                           .CrossContext();

            // 3. MOCK SWRVE
            injectionBinder.Unbind<global::Kampai.Common.ISwrveService>(); // <--- IMPORTANT
            injectionBinder.Bind<global::Kampai.Common.ISwrveService>()
                           .To<global::Kampai.Game.MockSwrveService>()
                           .ToSingleton()
                           .CrossContext();

            // 4. MOCK UPSIGHT
            injectionBinder.Unbind<global::Kampai.Main.IUpsightService>(); // <--- IMPORTANT
            injectionBinder.Bind<global::Kampai.Main.IUpsightService>()
                           .To<global::Kampai.Game.MockUpsightService>()
                           .ToSingleton()
                           .CrossContext();
            injectionBinder.Bind<global::Kampai.Main.InitLocalizationServiceSignal>()
    .ToSingleton()
    .CrossContext();

            // ========================================================================
            // FIX: BIND APP LIFECYCLE SIGNALS FOR AppTrackerView
            // These signals are required by AppTrackerView for dependency injection
            // ========================================================================
            injectionBinder.Bind<global::Kampai.Common.AppPauseSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Common.AppResumeSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Common.AppQuitSignal>().ToSingleton().CrossContext();
            injectionBinder.Bind<global::Kampai.Common.AppFocusGainedSignal>().ToSingleton().CrossContext();
            // ========================================================================
            // END FIX - CONTINUE WITH ORIGINAL CODE
            // ========================================================================

            injectionBinder.Bind<global::strange.extensions.context.api.ICrossContextCapable>().ToValue(this).ToName(global::Kampai.Main.MainElement.CONTEXT)
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Main.AppStartCompleteSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Common.IPickService>().To<global::Kampai.Common.PickService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Game.ICameraControlsService>().To<global::Kampai.Game.CameraControlsService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Game.Mignette.IMignetteService>().To<global::Kampai.Game.Mignette.MignetteService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Common.PickControllerModel>().ToSingleton().CrossContext();

            injectionBinder.Bind<global::Kampai.Main.MoveAudioListenerSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Main.PlayLocalAudioOneShotSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Main.PlayLocalAudioSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Main.StartLoopingAudioSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Main.StopLocalAudioSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Main.PlayMinionStateAudioSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Main.PlayGlobalSoundFXSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Main.QueueLocalAudioCommandSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Main.PlayGlobalSoundFXOneShotSignal>().ToSingleton().CrossContext()
				.Weak();

            // ...

			injectionBinder.Bind<global::Kampai.Main.PlayGlobalMusicSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Main.PlayMignetteMusicSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.UI.View.LoadUICompleteSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.UI.View.UpdateUIButtonsSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.UI.View.DisplayWorldProgressSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Game.ConstructionCompleteSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.ABTestSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.UI.View.ShowClientUpgradeDialogSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.UI.View.ShowForcedClientUpgradeScreenSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.UI.View.DisplayFloatingTextSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.UI.View.RemoveFloatingTextSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.ScheduleNotificationSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Game.SavePlayerSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Game.AutoSavePlayerStateSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Game.SocialLoginSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.SocialLoginSuccessSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.SocialLoginFailureSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.SocialLogoutSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.SocialInitAllServicesSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.SocialInitSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.SocialEventAvailableSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.SocialEventResponseSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Game.SocialEventWayfinderSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.ReLinkAccountSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.LinkAccountSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.UnlinkAccountSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.SocialInitSuccessSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.SocialInitFailureSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.GameCenterAuthTokenCompleteSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.LoadAnimatorStateInfoSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Game.UnloadAnimatorStateInfoSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Game.ITimeService>().To<global::Kampai.Game.TimeService>().ToSingleton()
				.CrossContext()
				.Weak();
			injectionBinder.Bind<string>().ToValue(PlayerDataSource()).ToName("devPlayerPath");
			injectionBinder.Bind<global::Kampai.Game.IUserSessionService>().To<global::Kampai.Game.UserSessionService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Game.IPlayerService>().To<global::Kampai.Game.PurchaseAwarePlayerService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Game.IPlayerDurationService>().To<global::Kampai.Game.PlayerDurationService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Game.ILandExpansionService>().To<global::Kampai.Game.LandExpansionService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Game.ILandExpansionConfigService>().To<global::Kampai.Game.LandExpansionConfigService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Game.IConfigurationsService>().To<global::Kampai.Game.ConfigurationsService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Game.IDLCService>().To<global::Kampai.Game.DLCService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<IResourceService>().To<ResourceService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Game.IDevicePrefsService>().To<global::Kampai.Game.DevicePrefsService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Game.UserSessionGrantedSignal>().ToSingleton().CrossContext()
				.Weak();
			injectionBinder.Bind<global::Kampai.Game.IMarketplaceService>().To<global::Kampai.Game.MarketplaceService>().ToSingleton()
				.CrossContext();
            injectionBinder.Bind<global::Kampai.Game.Mtx.IMtxReceiptValidationService>().To<global::Kampai.Game.Mtx.MtxReceiptValidationService>().ToSingleton()
                .CrossContext();
			injectionBinder.Bind<global::Kampai.Game.IPushNotificationService>().To<global::Kampai.Game.PushNotificationService>().ToSingleton()
				.CrossContext();
			injectionBinder.Bind<global::Kampai.Game.ISocialService>().To<global::Kampai.Game.FacebookService>().ToName(global::Kampai.Game.SocialServices.FACEBOOK)
				.CrossContext()
				.ToSingleton();
			injectionBinder.Bind<global::Kampai.Game.ISocialService>().To<global::Kampai.Game.GameCenterService>().ToName(global::Kampai.Game.SocialServices.GAMECENTER)
				.CrossContext()
				.ToSingleton();
			injectionBinder.Bind<global::Kampai.Game.ISocialService>().To<global::Kampai.Game.GooglePlayService>().ToName(global::Kampai.Game.SocialServices.GOOGLEPLAY)
				.CrossContext()
				.ToSingleton();
			injectionBinder.Bind<global::Kampai.Game.PurchaseMarketplaceSlotSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.UI.View.UpdateStorageItemsSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Game.CollectMarketplaceSaleSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Game.ISynergyService>().To<global::Kampai.Game.SynergyService>().CrossContext()
				.ToSingleton();
			injectionBinder.Bind<global::Kampai.Game.LoadPlayerSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Main.UpsightContentPreloadedSignal>().CrossContext().ToSingleton();

            injectionBinder.Bind<global::Kampai.Main.ReloadGameSignal>().CrossContext().ToSingleton();
			injectionBinder.Bind<global::Kampai.Common.NimbleTelemetryEventsPostedSignal>().CrossContext().ToSingleton();
			injectionBinder.Bind<global::Kampai.Game.EnvironmentState>().ToSingleton().CrossContext();
			BindPlayerCommand();
			base.commandBinder.Bind<global::Kampai.Main.MainCompleteSignal>().To<global::Kampai.Main.MainCompleteCommand>();
			base.commandBinder.Bind<global::Kampai.Game.LoadDefinitionsSignal>().To<LoadDefinitionsCommand>();
			base.commandBinder.Bind<global::Kampai.Game.FetchDefinitionsSignal>().To<FetchDefinitionsCommand>();
			base.commandBinder.Bind<global::Kampai.Game.DefinitionsChangedSignal>().To<global::Kampai.Game.DefinitionsChangedCommand>();
			base.commandBinder.Bind<global::Kampai.Game.LoadConfigurationSignal>().To<LoadConfigurationsCommand>();
			base.commandBinder.Bind<global::Kampai.Game.RegisterUserSignal>().To<RegisterUserCommand>();
			base.commandBinder.Bind<global::Kampai.Game.UserRegisteredSignal>().To<UserRegisteredCommand>();
			base.commandBinder.Bind<global::Kampai.Game.UpdateUserSignal>().To<UpdateUserCommand>();
			base.commandBinder.Bind<global::Kampai.Main.SetupDeepLinkSignal>().To<global::Kampai.Main.SetupDeepLinkCommand>();
			base.commandBinder.Bind<global::Kampai.Main.SetupHockeyAppUserSignal>().To<global::Kampai.Main.SetupHockeyAppUserCommand>();
			base.commandBinder.Bind<global::Kampai.Main.SetupHockeyAppSignal>().To<global::Kampai.Main.SetupHockeyAppCommand>();
			base.commandBinder.Bind<global::Kampai.Main.AppStartCompleteSignal>().To<global::Kampai.Main.AppStartCompleteCommand>();
			base.commandBinder.Bind<global::Kampai.Common.StartSignal>().To<global::Kampai.Main.MainStartCommand>();
			base.commandBinder.Bind<global::Kampai.Main.SetupEventSystemSignal>().To<global::Kampai.Main.SetupEventSystemCommand>();
			base.commandBinder.Bind<global::Kampai.Main.ReloadGameSignal>().To<global::Kampai.Main.ReloadGameCommand>();
			base.commandBinder.Bind<global::Kampai.Game.ConfigurationsLoadedSignal>().To<global::Kampai.Game.ConfigurationsLoadedCommand>();
			base.commandBinder.Bind<global::Kampai.UI.View.OpenRateAppPageSignal>().To<global::Kampai.Common.OpenRateAppPageCommand>();
			base.commandBinder.Bind<global::Kampai.Main.LoadSharedBundlesSignal>().To<global::Kampai.Main.LoadSharedBundlesCommand>();
			base.commandBinder.Bind<global::Kampai.Game.ABTestSignal>().To<global::Kampai.Util.ABTestCommand>();
			base.commandBinder.Bind<global::Kampai.Main.SetupSwrveSignal>().To<global::Kampai.Common.SetupSwrveCommand>();
			base.commandBinder.Bind<global::Kampai.Common.ABTestResourcesUpdatedSignal>().To<global::Kampai.Common.ABTestResourcesUpdatedCommand>();
			base.commandBinder.Bind<global::Kampai.Common.SetupLogglyServiceSignal>().To<global::Kampai.Common.SetupLogglyServiceCommand>();
			base.commandBinder.Bind<global::Kampai.Game.LoadMarketplaceOverridesSignal>().To<global::Kampai.Game.LoadMarketplaceOverridesCommand>();
			base.commandBinder.Bind<global::Kampai.Main.OpenHelpSignal>().To<global::Kampai.Main.OpenHelpCommand>();
			base.commandBinder.Bind<global::Kampai.Main.SetupUpsightSignal>().To<global::Kampai.Common.SetupUpsightCommand>();
			base.commandBinder.Bind<global::Kampai.Main.TriggerUpsightPromoSignal>().To<global::Kampai.Common.TriggerUpsightPromoCommand>();
			base.commandBinder.Bind<global::Kampai.Main.TriggerUpsightPreloadPromoSignal>().To<global::Kampai.Common.TriggerUpsightPreloadPromoCommand>();
			base.commandBinder.Bind<global::Kampai.Game.CheckUpgradeSignal>().To<global::Kampai.Game.CheckUpgradeCommand>();
			base.commandBinder.Bind<global::Kampai.Game.KillSwitchChangedSignal>().To<global::Kampai.Game.KillSwitchChangedCommand>();
			base.commandBinder.Bind<global::Kampai.Game.NimbleOTSignal>().To<global::Kampai.Game.NimbleOTCommand>();
			base.commandBinder.Bind<global::Kampai.Game.ReloadConfigurationsPeriodicSignal>().To<ReloadConfigurationsPeriodicCommand>();
			base.commandBinder.Bind<global::Kampai.Main.SetupNativeAlertManagerSignal>().To<global::Kampai.Common.Controller.SetupNativeAlertManagerCommand>();
			base.commandBinder.Bind<global::Kampai.UI.View.ShowClientUpgradeDialogSignal>().To<global::Kampai.UI.Controller.ShowClientUpgradeDialogCommand>();
			base.commandBinder.Bind<global::Kampai.UI.View.ShowForcedClientUpgradeScreenSignal>().To<global::Kampai.UI.Controller.ShowForcedClientUpdateScreenCommand>();
			base.commandBinder.Bind<global::Kampai.Main.LoadExternalScenesSignal>().To<global::Kampai.Game.LoadExternalScenesCommand>();
			base.commandBinder.Bind<global::Kampai.Main.LoadAudioSignal>().To<global::Kampai.Game.LoadAudioCommand>();
			base.commandBinder.Bind<global::Kampai.Common.DownloadManifestSignal>().To<global::Kampai.Main.DownloadManifestCommand>();
			base.commandBinder.Bind<global::Kampai.Common.PostDownloadManifestSignal>().To<global::Kampai.Main.PostDownloadManifestCommand>();
			base.commandBinder.Bind<global::Kampai.Game.SocialLoginSignal>().To<global::Kampai.Game.SocialLoginCommand>();
			base.commandBinder.Bind<global::Kampai.Game.SocialInitAllServicesSignal>().To<global::Kampai.Game.SocialInitAllServicesCommand>();
			base.commandBinder.Bind<global::Kampai.Game.SocialInitSignal>().To<global::Kampai.Game.SocialInitCommand>();
			base.commandBinder.Bind<global::Kampai.Game.SocialLogoutSignal>().To<global::Kampai.Game.SocialLogoutCommand>();
			base.commandBinder.Bind<global::Kampai.Game.SocialInitFailureSignal>().To<global::Kampai.Game.SocialInitFailureCommand>();
			base.commandBinder.Bind<global::Kampai.Game.SocialInitSuccessSignal>().To<global::Kampai.Game.SocialInitSuccessCommand>();
			base.commandBinder.Bind<global::Kampai.Game.SocialLoginFailureSignal>().To<global::Kampai.Game.SocialLoginFailureCommand>();
			base.commandBinder.Bind<global::Kampai.Game.SocialLoginSuccessSignal>().To<global::Kampai.Game.SocialLoginSuccessCommand>();
			base.commandBinder.Bind<global::Kampai.Game.SocialEventAvailableSignal>().To<global::Kampai.Common.SocialEventAvailableCommand>();
			base.commandBinder.Bind<global::Kampai.Game.SocialEventWayfinderSignal>().To<global::Kampai.Common.SocialEventWayfinderCommand>();
			base.commandBinder.Bind<global::Kampai.Game.LinkAccountSignal>().To<global::Kampai.Game.LinkAccountCommand>();
			base.commandBinder.Bind<global::Kampai.Game.ReLinkAccountSignal>().To<global::Kampai.Game.ReLinkAccountCommand>();
			base.commandBinder.Bind<global::Kampai.Game.UnlinkAccountSignal>().To<global::Kampai.Game.UnlinkAccountCommand>();
			base.commandBinder.Bind<global::Kampai.Main.LoadLocalizationServiceSignal>().To<global::Kampai.Main.LoadLocalizationServiceCommand>();
			base.commandBinder.Bind<global::Kampai.Main.LoadDevicePrefsSignal>().To<LoadDevicePrefsCommand>();
			base.commandBinder.Bind<global::Kampai.Common.SetupManifestSignal>().To<global::Kampai.Main.SetupManifestCommand>();
			base.commandBinder.Bind<global::Kampai.Common.LogClientMetricsSignal>().To<global::Kampai.Common.Controller.LogClientMetricsCommand>();
			base.commandBinder.Bind<global::Kampai.Common.LogTapEventMetricsSignal>().To<global::Kampai.Common.Controller.LogTapEventMetricsCommand>();
			base.commandBinder.Bind<global::Kampai.UI.View.LoadUICompleteSignal>().To<global::Kampai.Game.StartCOPPAFlowCommand>();
			base.commandBinder.Bind<global::Kampai.Game.SetupPushNotificationsSignal>().To<global::Kampai.Game.SetupPushNotificationsCommand>();
			base.commandBinder.Bind<global::Kampai.Common.LogTelemetrySignal>().To<global::Kampai.Common.LogTelemetryCommand>();
			base.commandBinder.Bind<global::Kampai.Main.SetupDataMitigationSignal>().To<global::Kampai.Common.SetupDataMitigationCommand>();
			base.commandBinder.Bind<global::Kampai.Common.NimbleTelemetryEventsPostedSignal>().To<global::Kampai.Common.Controller.HandleNimblePostedEventsCommand>();
			
			// [CRITICAL FIX] Bind DLC signals and model here so they are available for PostDownloadManifestCommand
			injectionBinder.Bind<global::Kampai.Common.ReconcileDLCSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Download.ShowDLCPanelSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Download.LaunchDownloadSignal>().ToSingleton().CrossContext();
			injectionBinder.Bind<global::Kampai.Download.DLCModel>().ToSingleton().CrossContext();
			base.commandBinder.Bind<global::Kampai.Common.ReconcileDLCSignal>().To<global::Kampai.Main.ReconcileDLCCommand>();
		}

		protected virtual void BindPlayerCommand()
		{
			base.commandBinder.Bind<global::Kampai.Game.LoadPlayerSignal>().To<LoadPlayerCommand>();
			base.commandBinder.Bind<global::Kampai.Game.LoadedPlayerDataSignal>().To<LoadedPlayerDataCommand>();
			base.commandBinder.Bind<global::Kampai.Game.LoginUserSignal>().To<global::Kampai.Game.LoginUserCommand>();
			base.commandBinder.Bind<global::Kampai.Game.AutoSavePlayerStateSignal>().To<AutoSavePlayerStateCommand>();

			var mcBinder = (injectionBinder as strange.extensions.injector.impl.CrossContextInjectionBinder);

		}

		protected virtual string PlayerDataSource()
		{
			return "test_player";
		}
	}
}
