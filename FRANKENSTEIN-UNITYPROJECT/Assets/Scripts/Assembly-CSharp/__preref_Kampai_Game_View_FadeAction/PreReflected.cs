namespace __preref_Kampai_Game_View_FadeAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.FadeAction((global::Kampai.Game.View.ActionableObject)p[0], (float)p[1], (bool)p[2], (global::Kampai.Util.ILogger)p[3]);
			ConstructorParameters = new global::System.Type[4]
			{
				typeof(global::Kampai.Game.View.ActionableObject),
				typeof(float),
				typeof(bool),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
