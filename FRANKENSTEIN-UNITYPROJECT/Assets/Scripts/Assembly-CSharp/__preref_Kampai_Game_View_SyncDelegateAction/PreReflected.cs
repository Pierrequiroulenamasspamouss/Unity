namespace __preref_Kampai_Game_View_SyncDelegateAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.SyncDelegateAction((global::System.Collections.Generic.IList<global::Kampai.Game.View.ActionableObject>)p[0], (global::System.Action)p[1], (global::Kampai.Util.ILogger)p[2]);
			ConstructorParameters = new global::System.Type[3]
			{
				typeof(global::System.Collections.Generic.IList<global::Kampai.Game.View.ActionableObject>),
				typeof(global::System.Action),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
