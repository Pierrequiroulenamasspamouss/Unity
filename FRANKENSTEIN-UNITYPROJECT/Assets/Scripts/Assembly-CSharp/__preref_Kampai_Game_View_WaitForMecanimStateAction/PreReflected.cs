namespace __preref_Kampai_Game_View_WaitForMecanimStateAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.WaitForMecanimStateAction((global::Kampai.Game.View.ActionableObject)p[0], (int)p[1], (global::Kampai.Util.ILogger)p[2], (int)p[3]);
			ConstructorParameters = new global::System.Type[4]
			{
				typeof(global::Kampai.Game.View.ActionableObject),
				typeof(int),
				typeof(global::Kampai.Util.ILogger),
				typeof(int)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
