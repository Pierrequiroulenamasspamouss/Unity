namespace __preref_Kampai_Util_FastJSONWriter
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.FastJSONWriter((global::System.IO.TextWriter)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::System.IO.TextWriter) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
