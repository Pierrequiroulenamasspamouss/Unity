namespace __preref_Kampai_Util_PathFinder
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.PathFinder((global::Kampai.Game.Environment)p[0], (global::Kampai.Common.IRandomService)p[1]);
			ConstructorParameters = new global::System.Type[2]
			{
				typeof(global::Kampai.Game.Environment),
				typeof(global::Kampai.Common.IRandomService)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
