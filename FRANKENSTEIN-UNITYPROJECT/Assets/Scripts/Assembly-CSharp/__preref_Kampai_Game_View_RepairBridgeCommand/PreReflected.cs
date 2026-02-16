namespace __preref_Kampai_Game_View_RepairBridgeCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.RepairBridgeCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[15]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BridgeBuilding), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).building = (global::Kampai.Game.BridgeBuilding)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CreateInventoryBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).createInventroyBuildingSignal = (global::Kampai.Game.CreateInventoryBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingChangeStateSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).changeState = (global::Kampai.Game.BuildingChangeStateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.PurchaseLandExpansionSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).landExpansionSignal = (global::Kampai.Game.PurchaseLandExpansionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).buildingManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RemoveBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).removeBuildingSignal = (global::Kampai.Game.RemoveBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).autoMoveSignal = (global::Kampai.Game.CameraAutoMoveSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).globalAudioSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DebugUpdateGridSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).gridSignal = (global::Kampai.Game.DebugUpdateGridSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.AddFootprintSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).addFootprintSignal = (global::Kampai.Game.AddFootprintSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.View.RepairBridgeCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[15]
			{
				null,
				null,
				null,
				null,
				null,
				global::Kampai.Game.GameElement.BUILDING_MANAGER,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null
			};
		}
	}
}
