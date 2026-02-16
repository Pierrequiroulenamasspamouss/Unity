namespace __preref_Kampai_Game_View_SelectedAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.SelectedAction((int)p[0], (global::Kampai.Util.Boxed<global::UnityEngine.Vector3>)p[1], (global::Kampai.Game.MinionMoveToSignal)p[2], (global::Kampai.Util.ILogger)p[3]);
			ConstructorParameters = new global::System.Type[4]
			{
				typeof(int),
				typeof(global::Kampai.Util.Boxed<global::UnityEngine.Vector3>),
				typeof(global::Kampai.Game.MinionMoveToSignal),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
