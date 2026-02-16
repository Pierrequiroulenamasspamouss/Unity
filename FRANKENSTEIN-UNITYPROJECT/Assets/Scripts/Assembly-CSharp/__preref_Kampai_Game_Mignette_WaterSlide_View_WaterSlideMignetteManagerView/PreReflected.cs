namespace __preref_Kampai_Game_Mignette_WaterSlide_View_WaterSlideMignetteManagerView
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[21]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StopMignetteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).stopMignette = (global::Kampai.Game.StopMignetteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SpawnMignetteDooberSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).spawnMignetteDooberSignal = (global::Kampai.UI.View.SpawnMignetteDooberSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.ChangeMignetteScoreSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).changeScoreSignal = (global::Kampai.Game.Mignette.ChangeMignetteScoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.Service.Audio.IFMODService), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).fmodService = (global::Kampai.Common.Service.Audio.IFMODService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MinionStateChangeSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).minionStateChangeSignal = (global::Kampai.Game.MinionStateChangeSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.WaterSlide.WaterSlideMignettePathCompletedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).pathCompletedSignal = (global::Kampai.Game.Mignette.WaterSlide.WaterSlideMignettePathCompletedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.WaterSlide.WaterslideMignetteOnDiveTriggerSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).diveTriggerSignal = (global::Kampai.Game.Mignette.WaterSlide.WaterslideMignetteOnDiveTriggerSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.WaterSlide.WaterslideMignetteOnPlayDiveTriggerSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).diveAnimationSignal = (global::Kampai.Game.Mignette.WaterSlide.WaterslideMignetteOnPlayDiveTriggerSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).playGlobalAudioSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayLocalAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).localAudioSignal = (global::Kampai.Main.PlayLocalAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).ShowAllWayFindersSignal = (global::Kampai.UI.View.ShowAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).ShowAllResourceIconsSignal = (global::Kampai.UI.View.ShowAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).HideAllResourceIconsSignal = (global::Kampai.UI.View.HideAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).mignetteCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MignetteEndedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).mignetteEndedSignal = (global::Kampai.Game.MignetteEndedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RequestStopMignetteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).requestStopMignetteSignal = (global::Kampai.Game.RequestStopMignetteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.StartMignetteHUDCountdownSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).startMignetteHUDCountdownSignal = (global::Kampai.Game.Mignette.StartMignetteHUDCountdownSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.WaterSlide.View.WaterSlideMignetteManagerView)target).glassCanvas = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[21]
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
