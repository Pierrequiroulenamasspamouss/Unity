namespace __preref_Kampai_Wrappers_WeakLuaState
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Wrappers.WeakLuaState((global::System.IntPtr)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::System.IntPtr) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
