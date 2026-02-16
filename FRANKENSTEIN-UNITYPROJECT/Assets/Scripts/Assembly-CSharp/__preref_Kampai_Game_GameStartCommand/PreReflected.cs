namespace __preref_Kampai_Game_GameStartCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.GameStartCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[46]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IInput), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).input = (global::Kampai.Game.IInput)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DCNTokenSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).dcnTokenSignal = (global::Kampai.Game.DCNTokenSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PopulateEnvironmentSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).environmentSignal = (global::Kampai.Game.PopulateEnvironmentSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CancelAllNotificationSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).cancelAllNotificationSignal = (global::Kampai.Game.CancelAllNotificationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupObjectManagersSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).setupMinionsSignal = (global::Kampai.Game.SetupObjectManagersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupBuildingManagerSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).setupBuildingSignal = (global::Kampai.Game.SetupBuildingManagerSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.LoadCruiseShipSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).cruiseShipSignal = (global::Kampai.Game.LoadCruiseShipSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UpdateVolumeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).updateVolumeSignal = (global::Kampai.Game.UpdateVolumeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MuteVolumeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).muteVolumeSignal = (global::Kampai.Game.MuteVolumeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EnableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).enableCameraSignal = (global::Kampai.Game.EnableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DisableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).disableCameraSignal = (global::Kampai.Game.DisableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupTimeEventServiceSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).timeEventServiceSignal = (global::Kampai.Game.SetupTimeEventServiceSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AwardLevelSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).awardLevelSignal = (global::Kampai.Game.AwardLevelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).contextView = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IUserSessionService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).userSessionService = (global::Kampai.Game.IUserSessionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ITelemetryService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).telemetryService = (global::Kampai.Common.ITelemetryService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ILandExpansionService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).landExpansionService = (global::Kampai.Game.ILandExpansionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerDurationService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).playerDurationService = (global::Kampai.Game.IPlayerDurationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowInAppMessageSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).showInAppMessageSignal = (global::Kampai.UI.View.ShowInAppMessageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.FreezeTimeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).freezeTimeSignal = (global::Kampai.Game.FreezeTimeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupMailboxPromoSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).mailboxSetupSignal = (global::Kampai.Game.SetupMailboxPromoSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SocialInitAllServicesSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).socialInitSignal = (global::Kampai.Game.SocialInitAllServicesSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ISwrveService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).swrveService = (global::Kampai.Common.ISwrveService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.INotificationService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).notificationService = (global::Kampai.Game.INotificationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ICoroutineProgressMonitor), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).couroutineProgressMonitor = (global::Kampai.Util.ICoroutineProgressMonitor)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ICoppaService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).coppaService = (global::Kampai.Common.ICoppaService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).setupAudioSignal = (global::Kampai.Game.SetupAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.LoadMinionDataSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).loadMinionDataSignal = (global::Kampai.Game.LoadMinionDataSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.SetupNamedCharactersSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).setupNamedCharactersSignal = (global::Kampai.Game.SetupNamedCharactersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.VillainIslandMessageSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).villainIslandMessageSignal = (global::Kampai.Common.VillainIslandMessageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RandomizeMinionPositionsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).randomizeMinionPositionsSignal = (global::Kampai.Game.RandomizeMinionPositionsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(ILocalPersistanceService), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).localPersistence = (ILocalPersistanceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ToggleStickerbookGlowSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).stickerbookGlow = (global::Kampai.Game.ToggleStickerbookGlowSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RandomFlyOverSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).randomFlyOverSignal = (global::Kampai.Game.RandomFlyOverSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.InitializeMarketplaceSlotsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).initializeSlotsSignal = (global::Kampai.Game.InitializeMarketplaceSlotsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestorePlayersSalesSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).restoreSalesSignal = (global::Kampai.Game.RestorePlayersSalesSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StartMarketplaceOnboardingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).startMarketplaceOnboardingSignal = (global::Kampai.Game.StartMarketplaceOnboardingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MarketplaceUpdateSoldItemsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).updateSoldItemsSignal = (global::Kampai.Game.MarketplaceUpdateSoldItemsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.GameStartCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			object[] array = new object[46];
			array[13] = global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW;
			SetterNames = array;
		}
	}
}
