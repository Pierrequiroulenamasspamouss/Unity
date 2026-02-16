namespace __preref_Kampai_Game_View_IncidentalFinishedAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.IncidentalFinishedAction((int)p[0], (global::Kampai.Game.MinionStateChangeSignal)p[1], (global::Kampai.Util.ILogger)p[2]);
			ConstructorParameters = new global::System.Type[3]
			{
				typeof(int),
				typeof(global::Kampai.Game.MinionStateChangeSignal),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
