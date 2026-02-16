namespace __preref_Kampai_Game_View_MinimumSpeedPathAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.MinimumSpeedPathAction((global::Kampai.Game.View.MinionObject)p[0], (global::System.Collections.Generic.IList<global::UnityEngine.Vector3>)p[1], (float)p[2], (float)p[3], (global::Kampai.Util.ILogger)p[4]);
			ConstructorParameters = new global::System.Type[5]
			{
				typeof(global::Kampai.Game.View.MinionObject),
				typeof(global::System.Collections.Generic.IList<global::UnityEngine.Vector3>),
				typeof(float),
				typeof(float),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
