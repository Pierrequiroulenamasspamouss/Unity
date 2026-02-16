namespace __preref_Kampai_Util_FatalException
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.FatalException((global::Kampai.Util.FatalCode)(int)p[0], (int)p[1]);
			ConstructorParameters = new global::System.Type[2]
			{
				typeof(global::Kampai.Util.FatalCode),
				typeof(int)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
