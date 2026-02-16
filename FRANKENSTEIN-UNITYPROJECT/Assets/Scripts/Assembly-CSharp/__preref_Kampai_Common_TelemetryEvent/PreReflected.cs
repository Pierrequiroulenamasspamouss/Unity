namespace __preref_Kampai_Common_TelemetryEvent
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Common.TelemetryEvent((SynergyTrackingEventType)(int)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(SynergyTrackingEventType) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
