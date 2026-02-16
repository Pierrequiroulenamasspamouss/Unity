namespace __preref_Kampai_Game_Mignette_EdwardMinionHands_View_EdwardMinionHandsMignetteManagerMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[15]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.MinionReactInRadiusSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).minionReactInRadiusSignal = (global::Kampai.Common.MinionReactInRadiusSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).view = (global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.IMignetteService), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).mignetteService = (global::Kampai.Game.Mignette.IMignetteService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RequestStopMignetteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).requestStopMignetteSignal = (global::Kampai.Game.RequestStopMignetteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MignetteEndedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).mignetteEndedSignal = (global::Kampai.Game.MignetteEndedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StopMignetteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).stopMignetteSignal = (global::Kampai.Game.StopMignetteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalMusicSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).musicSignal = (global::Kampai.Main.PlayGlobalMusicSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayMignetteMusicSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).mignetteMusicSignal = (global::Kampai.Main.PlayMignetteMusicSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ShowAndIncreaseMignetteScoreSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).showResultsSignal = (global::Kampai.Game.ShowAndIncreaseMignetteScoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EjectAllMinionsFromBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).ejectAllMinionsFromBuildingSignal = (global::Kampai.Game.EjectAllMinionsFromBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.MignetteGameModel), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).mignetteGameModel = (global::Kampai.Game.Mignette.MignetteGameModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ScheduleCooldownSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).scheduleCooldownSignal = (global::Kampai.Common.ScheduleCooldownSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.NetworkModel), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).networkModel = (global::Kampai.Common.NetworkModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.EdwardMinionHands.View.EdwardMinionHandsMignetteManagerMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			SetterNames = new object[15]
			{
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
				null,
				null,
				global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW
			};
		}
	}
}
