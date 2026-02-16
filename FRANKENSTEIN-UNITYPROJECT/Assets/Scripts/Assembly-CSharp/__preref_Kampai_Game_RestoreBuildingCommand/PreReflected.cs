namespace __preref_Kampai_Game_RestoreBuildingCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.RestoreBuildingCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[20]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Building), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).building = (global::Kampai.Game.Building)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingChangeStateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).buildingChangeStateSignal = (global::Kampai.Game.BuildingChangeStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).timeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreScaffoldingViewSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).restoreScaffoldingViewSignal = (global::Kampai.Game.RestoreScaffoldingViewSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreRibbonViewSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).restoreRibbonViewSignal = (global::Kampai.Game.RestoreRibbonViewSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestorePlatformViewSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).restorePlatformViewSignal = (global::Kampai.Game.RestorePlatformViewSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreBuildingViewSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).restoreBuildingViewSignal = (global::Kampai.Game.RestoreBuildingViewSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeEventService), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).timeEventService = (global::Kampai.Game.ITimeEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardRefillTicketSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).refillTicketSignal = (global::Kampai.Game.OrderBoardRefillTicketSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardSetNewTicketSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).setNewTicketSignal = (global::Kampai.Game.OrderBoardSetNewTicketSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreTaskableBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).restoreTaskingSignal = (global::Kampai.Game.RestoreTaskableBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreCraftingBuildingsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).craftingRestoreSignal = (global::Kampai.Game.RestoreCraftingBuildingsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RestoreLeisureBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).restoreLeisureBuildingSignal = (global::Kampai.Game.RestoreLeisureBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CleanupDebrisSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).cleanupDebris = (global::Kampai.Game.CleanupDebrisSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ScheduleCooldownSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).scheduleCooldownSignal = (global::Kampai.Common.ScheduleCooldownSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDLCService), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).dlcService = (global::Kampai.Game.IDLCService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.RestoreBuildingCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[20];
		}
	}
}
