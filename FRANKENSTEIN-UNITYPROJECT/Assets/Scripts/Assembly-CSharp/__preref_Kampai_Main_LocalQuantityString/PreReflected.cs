namespace __preref_Kampai_Main_LocalQuantityString
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Main.LocalQuantityString((string)p[0], (string)p[1]);
			ConstructorParameters = new global::System.Type[2]
			{
				typeof(string),
				typeof(string)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
