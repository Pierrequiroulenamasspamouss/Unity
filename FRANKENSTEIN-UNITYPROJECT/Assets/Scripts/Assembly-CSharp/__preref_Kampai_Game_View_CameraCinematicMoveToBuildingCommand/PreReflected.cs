namespace __preref_Kampai_Game_View_CameraCinematicMoveToBuildingCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.CameraCinematicMoveToBuildingCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[8]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(int), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CameraCinematicMoveToBuildingCommand)target).buildingID = (int)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(float), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CameraCinematicMoveToBuildingCommand)target).moveTime = (float)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CameraCinematicMoveToBuildingCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CameraCinematicMoveToBuildingCommand)target).buildingManager = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraCinematicZoomSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CameraCinematicMoveToBuildingCommand)target).autoZoomSignal = (global::Kampai.Game.CameraCinematicZoomSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraCinematicPanSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CameraCinematicMoveToBuildingCommand)target).autoPanSignal = (global::Kampai.Game.CameraCinematicPanSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CameraCinematicMoveToBuildingCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.View.CameraCinematicMoveToBuildingCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[8]
			{
				null,
				null,
				null,
				global::Kampai.Game.GameElement.BUILDING_MANAGER,
				null,
				null,
				null,
				null
			};
		}
	}
}
