namespace __preref_Kampai_Game_Mignette_AlligatorSkiing_View_AlligatorSkiingMignetteManagerView
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[19]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.AlligatorSkiing.AlligatorMignettePathCompletedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).pathCompleteSignal = (global::Kampai.Game.Mignette.AlligatorSkiing.AlligatorMignettePathCompletedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.AlligatorSkiing.AlligatorMignetteMinionHitObstacleSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).hitObstacleSignal = (global::Kampai.Game.Mignette.AlligatorSkiing.AlligatorMignetteMinionHitObstacleSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.AlligatorSkiing.AlligatorMignetteMinionHitCollectableSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).hitCollectableSignal = (global::Kampai.Game.Mignette.AlligatorSkiing.AlligatorMignetteMinionHitCollectableSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.AlligatorSkiing.AlligatorMignetteJumpLandedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).jumpLandedSignal = (global::Kampai.Game.Mignette.AlligatorSkiing.AlligatorMignetteJumpLandedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SpawnMignetteDooberSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).spawnMignetteDooberSignal = (global::Kampai.UI.View.SpawnMignetteDooberSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.ChangeMignetteScoreSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).changeScoreSignal = (global::Kampai.Game.Mignette.ChangeMignetteScoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).globalAudioSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.Service.Audio.IFMODService), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).fmodService = (global::Kampai.Common.Service.Audio.IFMODService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionStateChangeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).minionStateChangeSignal = (global::Kampai.Game.MinionStateChangeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).ShowAllWayFindersSignal = (global::Kampai.UI.View.ShowAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).ShowAllResourceIconsSignal = (global::Kampai.UI.View.ShowAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).HideAllResourceIconsSignal = (global::Kampai.UI.View.HideAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).mignetteCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MignetteEndedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).mignetteEndedSignal = (global::Kampai.Game.MignetteEndedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RequestStopMignetteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).requestStopMignetteSignal = (global::Kampai.Game.RequestStopMignetteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.StartMignetteHUDCountdownSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).startMignetteHUDCountdownSignal = (global::Kampai.Game.Mignette.StartMignetteHUDCountdownSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)target).glassCanvas = (global::UnityEngine.GameObject)val;
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
				global::Kampai.Game.GameElement.CONTEXT,
				null,
				null,
				null,
				null,
				null,
				global::Kampai.Main.MainElement.CAMERA,
				null,
				null,
				null,
				null,
				global::Kampai.Main.MainElement.UI_GLASSCANVAS
			};
		}
	}
}
