namespace __preref_Kampai_Wrappers_NativeLibContext
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Wrappers.NativeLibContext((global::System.Action<string>)p[0], (global::System.Action<string>)p[1]);
			ConstructorParameters = new global::System.Type[2]
			{
				typeof(global::System.Action<string>),
				typeof(global::System.Action<string>)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
