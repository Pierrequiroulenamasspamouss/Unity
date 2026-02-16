namespace __preref_Kampai_Game_CameraAutoMoveCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.CameraAutoMoveCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[13]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Vector3), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).position = (global::UnityEngine.Vector3)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(float), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).zoomPercentage = (float)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraMovementSettings), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).modalInfo = (global::Kampai.Game.CameraMovementSettings)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoZoomSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).autoZoomSignal = (global::Kampai.Game.CameraAutoZoomSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoPanSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).autoPanSignal = (global::Kampai.Game.CameraAutoPanSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraModel), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).model = (global::Kampai.Game.CameraModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).uiContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).playSoundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.PickControllerModel), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).pickModel = (global::Kampai.Common.PickControllerModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ShowHiddenBuildingsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).showHiddenBuildingsSignal = (global::Kampai.Game.ShowHiddenBuildingsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.CameraAutoMoveCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[13]
			{
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				global::Kampai.UI.View.UIElement.CONTEXT,
				null,
				null,
				null,
				null,
				null
			};
		}
	}
}
