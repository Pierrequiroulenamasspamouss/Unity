namespace __preref_Kampai_Game_Mignette_AlligatorSkiing_View_AlligatorSkiingMignetteManagerMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[13]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).view = (global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.IMignetteService), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).mignetteService = (global::Kampai.Game.Mignette.IMignetteService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.RequestStopMignetteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).requestStopMignetteSignal = (global::Kampai.Game.RequestStopMignetteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.MignetteEndedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).mignetteEndedSignal = (global::Kampai.Game.MignetteEndedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.StopMignetteSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).stopMignetteSignal = (global::Kampai.Game.StopMignetteSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalMusicSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).musicSignal = (global::Kampai.Main.PlayGlobalMusicSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayMignetteMusicSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).mignetteMusicSignal = (global::Kampai.Main.PlayMignetteMusicSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.ShowAndIncreaseMignetteScoreSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).showResultsSignal = (global::Kampai.Game.ShowAndIncreaseMignetteScoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EjectAllMinionsFromBuildingSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).ejectAllMinionsFromBuildingSignal = (global::Kampai.Game.EjectAllMinionsFromBuildingSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.MignetteGameModel), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).mignetteGameModel = (global::Kampai.Game.Mignette.MignetteGameModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.ScheduleCooldownSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).scheduleCooldownSignal = (global::Kampai.Common.ScheduleCooldownSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.NetworkModel), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).networkModel = (global::Kampai.Common.NetworkModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.AlligatorSkiing.View.AlligatorSkiingMignetteManagerMediator)target).contextView = (global::UnityEngine.GameObject)val;
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
