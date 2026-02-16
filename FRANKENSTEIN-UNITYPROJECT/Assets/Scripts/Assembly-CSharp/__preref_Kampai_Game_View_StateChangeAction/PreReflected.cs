namespace __preref_Kampai_Game_View_StateChangeAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.StateChangeAction((int)p[0], (global::Kampai.Game.MinionStateChangeSignal)p[1], (global::Kampai.Game.MinionState)(int)p[2], (global::Kampai.Util.ILogger)p[3]);
			ConstructorParameters = new global::System.Type[4]
			{
				typeof(int),
				typeof(global::Kampai.Game.MinionStateChangeSignal),
				typeof(global::Kampai.Game.MinionState),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
