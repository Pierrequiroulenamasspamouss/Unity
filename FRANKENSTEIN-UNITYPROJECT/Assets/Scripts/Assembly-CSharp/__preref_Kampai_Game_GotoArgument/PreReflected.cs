namespace __preref_Kampai_Game_GotoArgument
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.GotoArgument((int)p[0], (int)p[1], (int)p[2], (bool)p[3]);
			ConstructorParameters = new global::System.Type[4]
			{
				typeof(int),
				typeof(int),
				typeof(int),
				typeof(bool)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
