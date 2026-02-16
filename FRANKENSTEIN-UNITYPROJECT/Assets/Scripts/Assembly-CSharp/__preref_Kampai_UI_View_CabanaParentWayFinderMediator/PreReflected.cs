namespace __preref_Kampai_UI_View_CabanaParentWayFinderMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.CabanaParentWayFinderMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[18]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CabanaParentWayFinderView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).CabanaParentWayFinderView = (global::Kampai.UI.View.CabanaParentWayFinderView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveToPositionSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).CameraAutoMoveToPositionSignal = (global::Kampai.Game.CameraAutoMoveToPositionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.IPositionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).PositionService = (global::Kampai.UI.IPositionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).GameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).RoutineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).PlayerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPrestigeService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).PrestigeService = (global::Kampai.Game.IPrestigeService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoMoveToInstanceSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).CameraAutoMoveToInstanceSignal = (global::Kampai.Game.CameraAutoMoveToInstanceSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).LocalizationService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CreateWayFinderSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).CreateWayFinderSignal = (global::Kampai.UI.View.CreateWayFinderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.RemoveWayFinderSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).RemoveWayFinderSignal = (global::Kampai.UI.View.RemoveWayFinderSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).GlassCanvas = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).Logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ITikiBarService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).TikiBarService = (global::Kampai.Game.ITikiBarService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IZoomCameraModel), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).ZoomCameraModel = (global::Kampai.Game.IZoomCameraModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateWayFinderPrioritySignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).UpdateWayFinderPrioritySignal = (global::Kampai.UI.View.UpdateWayFinderPrioritySignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).DefinitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.CabanaParentWayFinderMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[18]
			{
				null,
				null,
				null,
				global::Kampai.Game.GameElement.CONTEXT,
				null,
				null,
				null,
				null,
				null,
				null,
				null,
				global::Kampai.Main.MainElement.UI_GLASSCANVAS,
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
