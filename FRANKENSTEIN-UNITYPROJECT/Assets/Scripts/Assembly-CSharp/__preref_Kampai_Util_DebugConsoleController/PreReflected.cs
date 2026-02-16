namespace __preref_Kampai_Util_DebugConsoleController
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.DebugConsoleController((global::Kampai.Util.ILogger)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::Kampai.Util.ILogger) };
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.Util.DebugConsoleController)t).Init();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[59]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IQuestService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).questService = (global::Kampai.Game.IQuestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IVideoService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).videoService = (global::Kampai.Common.IVideoService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IMinionBuilder), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).minionBuilder = (global::Kampai.Util.IMinionBuilder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.LogClientMetricsSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).metricsSignal = (global::Kampai.Common.LogClientMetricsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ScheduleNotificationSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).notificationSignal = (global::Kampai.Game.ScheduleNotificationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ITelemetryService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).telemetryService = (global::Kampai.Common.ITelemetryService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).uiContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).mainContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(ILocalPersistanceService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).localPersistService = (ILocalPersistanceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(IEncryptionService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).encryptionService = (IEncryptionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IConfigurationsService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).configurationsService = (global::Kampai.Game.IConfigurationsService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ISocialService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).facebookService = (global::Kampai.Game.ISocialService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ISocialService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).gamecenterService = (global::Kampai.Game.ISocialService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ISocialService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).googlePlayService = (global::Kampai.Game.ISocialService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).cameraGO = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IMarketplaceService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).marketplaceService = (global::Kampai.Game.IMarketplaceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PurchaseLandExpansionSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).purchaseLandExpansionSignal = (global::Kampai.Game.PurchaseLandExpansionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ILandExpansionService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).landExpansionService = (global::Kampai.Game.ILandExpansionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ILandExpansionConfigService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).landExpansionConfigService = (global::Kampai.Game.ILandExpansionConfigService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPrestigeService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).prestigeService = (global::Kampai.Game.IPrestigeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.LoadAnimatorStateInfoSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).loadAnimatorStateInfoSignal = (global::Kampai.Game.LoadAnimatorStateInfoSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UnloadAnimatorStateInfoSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).unloadAnimatorStateInfoSignal = (global::Kampai.Game.UnloadAnimatorStateInfoSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ABTestSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).ABTestSignal = (global::Kampai.Game.ABTestSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IConfigurationsService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).configService = (global::Kampai.Game.IConfigurationsService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SavePlayerSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).savePlayerSignal = (global::Kampai.Game.SavePlayerSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SocialLoginSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).socialLoginSignal = (global::Kampai.Game.SocialLoginSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SocialInitSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).socialInitSignal = (global::Kampai.Game.SocialInitSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SocialLogoutSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).socialLogoutSignal = (global::Kampai.Game.SocialLogoutSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IQuestScriptRunner), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).luaRunner = (global::Kampai.Game.IQuestScriptRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDLCService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).dlcService = (global::Kampai.Game.IDLCService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UnlinkAccountSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).unlinkAccountSignal = (global::Kampai.Game.UnlinkAccountSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.IUpsightService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).upsightService = (global::Kampai.Main.IUpsightService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.MignetteGameModel), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).mignetteGameModel = (global::Kampai.Game.Mignette.MignetteGameModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MignetteCollectionService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).mignetteCollectionService = (global::Kampai.Game.MignetteCollectionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingCooldownCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).cooldownCompleteSignal = (global::Kampai.Game.BuildingCooldownCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingCooldownUpdateViewSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).cooldownUpdateViewSignal = (global::Kampai.Game.BuildingCooldownUpdateViewSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SpawnMignetteDooberSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).spawnDooberSignal = (global::Kampai.UI.View.SpawnMignetteDooberSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.ChangeMignetteScoreSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).changeScoreSignal = (global::Kampai.Game.Mignette.ChangeMignetteScoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ICurrencyService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).currencyService = (global::Kampai.Game.ICurrencyService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowClientUpgradeDialogSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).showClientUpgradeDialogSignal = (global::Kampai.UI.View.ShowClientUpgradeDialogSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowForcedClientUpgradeScreenSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).showForcedClientUpgradeScreenSignal = (global::Kampai.UI.View.ShowForcedClientUpgradeScreenSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CompositeBuildingPieceAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).compositeBuildingPieceAddedSignal = (global::Kampai.Game.CompositeBuildingPieceAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimedSocialEventService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).timedSocialEventService = (global::Kampai.Game.ITimedSocialEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeEventService), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).timeEventService = (global::Kampai.Game.ITimeEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SocialOrderBoardCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).socialOrderBoardCompleteSignal = (global::Kampai.Game.SocialOrderBoardCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CreateWayFinderSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).createWayFinderSignal = (global::Kampai.UI.View.CreateWayFinderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RemoveWayFinderSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).removeWayFinderSignal = (global::Kampai.UI.View.RemoveWayFinderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupPushNotificationsSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).setupPushNotificationsSignal = (global::Kampai.Game.SetupPushNotificationsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DebugKeyHitSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).debugKeyHitSignal = (global::Kampai.Game.DebugKeyHitSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).glassCanvas = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.NetworkModel), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).networkModel = (global::Kampai.Common.NetworkModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.NetworkConnectionLostSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).networkConnectionLostSignal = (global::Kampai.Common.NetworkConnectionLostSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RushDialogConfirmationSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).dialogConfirmedSignal = (global::Kampai.UI.View.RushDialogConfirmationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OpenBuildingMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DebugConsoleController)target).openBuildingMenuSignal = (global::Kampai.Game.OpenBuildingMenuSignal)val;
				})
			};
			object[] array = new object[59];
			array[6] = global::Kampai.UI.View.UIElement.CONTEXT;
			array[7] = global::Kampai.Game.GameElement.CONTEXT;
			array[8] = global::Kampai.Main.MainElement.CONTEXT;
			array[13] = global::Kampai.Game.SocialServices.FACEBOOK;
			array[14] = global::Kampai.Game.SocialServices.GAMECENTER;
			array[15] = global::Kampai.Game.SocialServices.GOOGLEPLAY;
			array[16] = global::Kampai.Main.MainElement.CAMERA;
			array[32] = global::Kampai.Game.QuestRunnerLanguage.Lua;
			array[54] = global::Kampai.Main.MainElement.UI_GLASSCANVAS;
			SetterNames = array;
		}
	}
}
