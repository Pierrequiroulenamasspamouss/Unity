namespace __preref_Kampai_Game_View_MinionTaskAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.MinionTaskAction((global::Kampai.Game.Minion)p[0], (global::Kampai.Game.Building)p[1], (global::strange.extensions.signal.impl.Signal<global::Kampai.Game.Minion, global::Kampai.Game.Building>)p[2], (global::Kampai.Util.ILogger)p[3]);
			ConstructorParameters = new global::System.Type[4]
			{
				typeof(global::Kampai.Game.Minion),
				typeof(global::Kampai.Game.Building),
				typeof(global::strange.extensions.signal.impl.Signal<global::Kampai.Game.Minion, global::Kampai.Game.Building>),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
