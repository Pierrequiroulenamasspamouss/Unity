namespace __preref_Kampai_Game_OpenBuildingMenuCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.OpenBuildingMenuCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[26]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.BuildingObject), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).BuildingObject = (global::Kampai.Game.View.BuildingObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Building), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).Building = (global::Kampai.Game.Building)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).BuildingManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).PlayerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).Logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).AutoMoveSignal = (global::Kampai.Game.CameraAutoMoveSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraInstanceFocusSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).BuildingFocusSignal = (global::Kampai.Game.CameraInstanceFocusSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowBuildingDetailMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).ShowBuildingDetailmenuSignal = (global::Kampai.UI.View.ShowBuildingDetailMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OpenStageBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).openStageBuildingSignal = (global::Kampai.Game.OpenStageBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowBridgeUISignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).BridgeSignal = (global::Kampai.UI.View.ShowBridgeUISignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimeService), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).TimeService = (global::Kampai.Game.ITimeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowNeedXMinionsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).ShowNeedXMinionsSignal = (global::Kampai.UI.View.ShowNeedXMinionsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OpenStorageBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).OpenStorageBuildingSignal = (global::Kampai.Game.OpenStorageBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingZoomSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).BuildingZoomSignal = (global::Kampai.Game.BuildingZoomSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.MignetteGameModel), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).MignetteGameModel = (global::Kampai.Game.Mignette.MignetteGameModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SpawnDooberModel), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).SpawnDooberModel = (global::Kampai.UI.View.SpawnDooberModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MailboxSelectedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).MailboxSelectedSignal = (global::Kampai.Game.MailboxSelectedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.RepairBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).RepairBuildingSignal = (global::Kampai.Common.RepairBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.PickControllerModel), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).PickControllerModel = (global::Kampai.Common.PickControllerModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OpenOrderBoardSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).openOrderBoardSignal = (global::Kampai.Game.OpenOrderBoardSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IMarketplaceService), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).marketplaceService = (global::Kampai.Game.IMarketplaceService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.OrderBoardUpdateTicketOnBoardSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).updateTicketOnBoardSignal = (global::Kampai.Game.OrderBoardUpdateTicketOnBoardSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).uiContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.OpenBuildingMenuCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			object[] array = new object[26];
			array[2] = global::Kampai.Game.GameElement.BUILDING_MANAGER;
			array[22] = global::Kampai.UI.View.UIElement.CONTEXT;
			SetterNames = array;
		}
	}
}
