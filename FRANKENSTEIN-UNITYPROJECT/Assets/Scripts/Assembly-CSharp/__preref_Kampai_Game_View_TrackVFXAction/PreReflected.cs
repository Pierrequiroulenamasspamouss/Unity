namespace __preref_Kampai_Game_View_TrackVFXAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.TrackVFXAction((global::Kampai.Game.View.MinionObject)p[0], (global::Kampai.Util.VFXScript)p[1], (global::Kampai.Util.ILogger)p[2]);
			ConstructorParameters = new global::System.Type[3]
			{
				typeof(global::Kampai.Game.View.MinionObject),
				typeof(global::Kampai.Util.VFXScript),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
