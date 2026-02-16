namespace __preref_Kampai_Util_ActionSignal
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.ActionSignal((global::System.Action)p[0], (bool)p[1]);
			ConstructorParameters = new global::System.Type[2]
			{
				typeof(global::System.Action),
				typeof(bool)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
