namespace __preref_Kampai_Game_Mignette_ChangeCurrentMignetteScore
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.Mignette.ChangeCurrentMignetteScore();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[5]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(int), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.ChangeCurrentMignetteScore)target).scoreDelta = (int)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.MignetteGameModel), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.ChangeCurrentMignetteScore)target).mignetteGameModel = (global::Kampai.Game.Mignette.MignetteGameModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.Mignette.MignetteScoreUpdatedSignal), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.ChangeCurrentMignetteScore)target).mignetteScoreUpdatedSignal = (global::Kampai.Game.Mignette.MignetteScoreUpdatedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.ChangeCurrentMignetteScore)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Game.Mignette.ChangeCurrentMignetteScore)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[5];
		}
	}
}
