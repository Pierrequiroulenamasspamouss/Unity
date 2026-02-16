namespace __preref_Kampai_Util_TimerUnit
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.TimerUnit((float)p[0], (global::System.Action)p[1]);
			ConstructorParameters = new global::System.Type[2]
			{
				typeof(float),
				typeof(global::System.Action)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
