namespace __preref_Kampai_Common_PickService
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Common.PickService();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[26]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.PickControllerModel), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).model = (global::Kampai.Common.PickControllerModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).camera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ICameraControlsService), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).cameraControlsService = (global::Kampai.Game.ICameraControlsService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ShowHiddenBuildingsSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).showHiddenBuildingsSignal = (global::Kampai.Game.ShowHiddenBuildingsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.MinionPickSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).minionSignal = (global::Kampai.Common.MinionPickSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.EnvironmentalMignetteTappedSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).environmentalMignetteTappedSignal = (global::Kampai.Common.EnvironmentalMignetteTappedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.BuildingPickSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).buildingSignal = (global::Kampai.Common.BuildingPickSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.DeselectPickSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).deselectSignal = (global::Kampai.Common.DeselectPickSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.MagnetFingerPickSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).magnetFingerSignal = (global::Kampai.Common.MagnetFingerPickSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.DragAndDropPickSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).dragAndDropSignal = (global::Kampai.Common.DragAndDropPickSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.LandExpansionPickSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).landExpansionSignal = (global::Kampai.Common.LandExpansionPickSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.DoubleClickPickSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).doubleClickSignal = (global::Kampai.Common.DoubleClickPickSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.Service.HealthMetrics.ITapEventMetricsService), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).tapEventMetricsService = (global::Kampai.Common.Service.HealthMetrics.ITapEventMetricsService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.TikiBarViewPickSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).tikiBarViewPickSignal = (global::Kampai.Common.TikiBarViewPickSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.DeselectAllMinionsSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).deselectAllMinionsSignal = (global::Kampai.Common.DeselectAllMinionsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IZoomCameraModel), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).zoomCameraModel = (global::Kampai.Game.IZoomCameraModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StuartShowCompleteSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).showCompleteSignal = (global::Kampai.Game.StuartShowCompleteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StuartGetOnStageImmediateSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).stuartGetOnStageImmediateSignal = (global::Kampai.Game.StuartGetOnStageImmediateSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MoveBuildMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).moveBuildMenuSignal = (global::Kampai.UI.View.MoveBuildMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.VillainIslandMessageSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).villainIslandMessageSignal = (global::Kampai.Common.VillainIslandMessageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITimedSocialEventService), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).timedSocialEventService = (global::Kampai.Game.ITimedSocialEventService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingZoomSignal), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).buildingZoomSignal = (global::Kampai.Game.BuildingZoomSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.MignetteGameModel), delegate(object target, object val)
				{
					((global::Kampai.Common.PickService)target).mignetteGameModel = (global::Kampai.Game.Mignette.MignetteGameModel)val;
				})
			};
			object[] array = new object[26];
			array[1] = global::Kampai.Main.MainElement.CAMERA;
			array[12] = global::Kampai.Game.GameElement.CONTEXT;
			SetterNames = array;
		}
	}
}
