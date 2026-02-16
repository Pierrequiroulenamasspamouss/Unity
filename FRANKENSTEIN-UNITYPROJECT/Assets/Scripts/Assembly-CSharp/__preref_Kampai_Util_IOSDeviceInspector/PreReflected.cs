namespace __preref_Kampai_Util_IOSDeviceInspector
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.IOSDeviceInspector((global::Kampai.Util.ILogger)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::Kampai.Util.ILogger) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
