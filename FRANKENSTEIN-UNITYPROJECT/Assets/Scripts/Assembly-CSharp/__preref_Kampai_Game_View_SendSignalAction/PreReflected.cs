namespace __preref_Kampai_Game_View_SendSignalAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.SendSignalAction((global::strange.extensions.signal.impl.Signal)p[0], (global::Kampai.Util.ILogger)p[1]);
			ConstructorParameters = new global::System.Type[2]
			{
				typeof(global::strange.extensions.signal.impl.Signal),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
