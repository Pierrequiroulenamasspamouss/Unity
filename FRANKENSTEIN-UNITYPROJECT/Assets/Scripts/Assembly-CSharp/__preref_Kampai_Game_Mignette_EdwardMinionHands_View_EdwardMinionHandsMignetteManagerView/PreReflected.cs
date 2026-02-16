namespace __preref_Kampai_Game_Mignette_EdwardMinionHands_View_EdwardMinionHandsMignetteManagerView
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[14]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).globalAudioSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.ChangeMignetteScoreSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).changeScoreSignal = (global::Kampai.Game.Mignette.ChangeMignetteScoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.Service.Audio.IFMODService), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).fmodService = (global::Kampai.Common.Service.Audio.IFMODService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SpawnMignetteDooberSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).spawnMignetteDooberSignal = (global::Kampai.UI.View.SpawnMignetteDooberSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllWayFindersSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).ShowAllWayFindersSignal = (global::Kampai.UI.View.ShowAllWayFindersSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ShowAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).ShowAllResourceIconsSignal = (global::Kampai.UI.View.ShowAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideAllResourceIconsSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).HideAllResourceIconsSignal = (global::Kampai.UI.View.HideAllResourceIconsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).mignetteCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MignetteEndedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).mignetteEndedSignal = (global::Kampai.Game.MignetteEndedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RequestStopMignetteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).requestStopMignetteSignal = (global::Kampai.Game.RequestStopMignetteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.StartMignetteHUDCountdownSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).startMignetteHUDCountdownSignal = (global::Kampai.Game.Mignette.StartMignetteHUDCountdownSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)target).glassCanvas = (global::UnityEngine.GameObject)val;
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
