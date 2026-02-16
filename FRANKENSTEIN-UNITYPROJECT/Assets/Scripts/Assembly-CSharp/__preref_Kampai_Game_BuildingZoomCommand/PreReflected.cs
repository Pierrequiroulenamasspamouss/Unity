namespace __preref_Kampai_Game_BuildingZoomCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.BuildingZoomCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[22]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingZoomSettings), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).zoomBuildingSettings = (global::Kampai.Game.BuildingZoomSettings)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IZoomCameraModel), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).zoomCameraModel = (global::Kampai.Game.IZoomCameraModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).closeSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowHUDSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).showHUDSignal = (global::Kampai.UI.View.ShowHUDSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowStoreSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).showStoreSignal = (global::Kampai.UI.View.ShowStoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ToggleHitboxSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).toggleHitboxSignal = (global::Kampai.Game.ToggleHitboxSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).playSoundFXSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DisableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).disableCameraSignal = (global::Kampai.Game.DisableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EnableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).enableCameraSignal = (global::Kampai.Game.EnableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraModel), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).model = (global::Kampai.Game.CameraModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraResetPanVelocitySignal), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).cameraResetPanVelocitySignal = (global::Kampai.Game.CameraResetPanVelocitySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).mainCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StageService), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).stageService = (global::Kampai.Game.StageService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.FTUETikiOpened), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).ftueSignal = (global::Kampai.UI.View.FTUETikiOpened)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoPanCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).cameraAutoPanCompleteSignal = (global::Kampai.Game.CameraAutoPanCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveToBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).cameraAutoMoveToBuildingSignal = (global::Kampai.Game.CameraAutoMoveToBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.FadeBackgroundAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).fadeBackgroundAudioSignal = (global::Kampai.Main.FadeBackgroundAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPrestigeService), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).prestigeService = (global::Kampai.Game.IPrestigeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.BuildingZoomCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			object[] array = new object[22];
			array[13] = global::Kampai.Main.MainElement.CAMERA;
			SetterNames = array;
		}
	}
}
