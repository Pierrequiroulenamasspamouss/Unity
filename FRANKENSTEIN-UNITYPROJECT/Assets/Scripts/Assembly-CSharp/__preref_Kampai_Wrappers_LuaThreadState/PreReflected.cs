namespace __preref_Kampai_Wrappers_LuaThreadState
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Wrappers.LuaThreadState((global::Kampai.Wrappers.LuaState)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::Kampai.Wrappers.LuaState) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
