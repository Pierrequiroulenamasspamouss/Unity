namespace __preref_Kampai_Game_View_TouchDragPanMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.TouchDragPanMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[19]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.TouchDragPanView), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).view = (global::Kampai.Game.View.TouchDragPanView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ICameraControlsService), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).cameraControlsService = (global::Kampai.Game.ICameraControlsService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DisableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).disableCameraSignal = (global::Kampai.Game.DisableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EnableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).enableCameraSignal = (global::Kampai.Game.EnableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.UnitializeCameraSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).unitializeSignal = (global::Kampai.Game.UnitializeCameraSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.View.CameraUtils), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).cameraUtils = (global::Kampai.Game.View.CameraUtils)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoPanSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).autoPanSignal = (global::Kampai.Game.CameraAutoPanSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraCinematicPanSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).cinematicPanSignal = (global::Kampai.Game.CameraCinematicPanSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraModel), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).model = (global::Kampai.Game.CameraModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowBuildingDetailMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).showDetailMenuSignal = (global::Kampai.UI.View.ShowBuildingDetailMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowQuestPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).showQuestPanelSignal = (global::Kampai.UI.View.ShowQuestPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowQuestRewardSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).showQuestRewardSignal = (global::Kampai.UI.View.ShowQuestRewardSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowProceduralQuestPanelSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).showProceduralQuestSignal = (global::Kampai.UI.View.ShowProceduralQuestPanelSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.PickControllerModel), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).pickModel = (global::Kampai.Common.PickControllerModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoPanCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).cameraAutoPanCompleteSignal = (global::Kampai.Game.CameraAutoPanCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraResetPanVelocitySignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).cameraResetPanVelocitySignal = (global::Kampai.Game.CameraResetPanVelocitySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.StopAutopanSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).stopAutopanSignal = (global::Kampai.UI.View.StopAutopanSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.DeviceInformation), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).deviceInformation = (global::Kampai.Util.DeviceInformation)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.View.TouchDragPanMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[19]
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
