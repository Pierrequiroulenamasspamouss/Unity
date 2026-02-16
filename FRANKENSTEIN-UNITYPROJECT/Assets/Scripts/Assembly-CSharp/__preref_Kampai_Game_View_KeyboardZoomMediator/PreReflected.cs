namespace __preref_Kampai_Game_View_KeyboardZoomMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.KeyboardZoomMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[14]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.KeyboardZoomView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).view = (global::Kampai.Game.View.KeyboardZoomView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.RequestZoomPercentageSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).requestSignal = (global::Kampai.Common.RequestZoomPercentageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ICameraControlsService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).cameraControlsService = (global::Kampai.Game.ICameraControlsService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DisableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).disableCameraSignal = (global::Kampai.Game.DisableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EnableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).enableCameraSignal = (global::Kampai.Game.EnableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ZoomPercentageSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).zoomSignal = (global::Kampai.Common.ZoomPercentageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoZoomSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).autoZoomSignal = (global::Kampai.Game.CameraAutoZoomSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.PickControllerModel), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).pickModel = (global::Kampai.Common.PickControllerModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraCinematicZoomSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).cinematicZoomSignal = (global::Kampai.Game.CameraCinematicZoomSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoZoomCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).cameraAutoZoomCompleteSignal = (global::Kampai.Game.CameraAutoZoomCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.CameraUtils), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).cameraUtils = (global::Kampai.Game.View.CameraUtils)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraModel), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).model = (global::Kampai.Game.CameraModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.KeyboardZoomMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[14]
			{
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
