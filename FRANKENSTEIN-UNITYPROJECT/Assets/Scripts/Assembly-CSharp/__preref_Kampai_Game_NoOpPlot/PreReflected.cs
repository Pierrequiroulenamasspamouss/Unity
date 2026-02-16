namespace __preref_Kampai_Game_NoOpPlot
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.NoOpPlot((global::Kampai.Game.NoOpPlotDefinition)p[0]);
			ConstructorParameters = new global::System.Type[1] { typeof(global::Kampai.Game.NoOpPlotDefinition) };
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
