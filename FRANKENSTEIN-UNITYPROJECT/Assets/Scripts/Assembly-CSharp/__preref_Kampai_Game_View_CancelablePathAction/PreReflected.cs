namespace __preref_Kampai_Game_View_CancelablePathAction
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.View.CancelablePathAction((global::strange.extensions.signal.impl.Signal)p[0], (float)p[1], (global::Kampai.Game.View.ActionableObject)p[2], (global::System.Collections.Generic.IList<global::UnityEngine.Vector3>)p[3], (float)p[4], (global::Kampai.Util.ILogger)p[5]);
			ConstructorParameters = new global::System.Type[6]
			{
				typeof(global::strange.extensions.signal.impl.Signal),
				typeof(float),
				typeof(global::Kampai.Game.View.ActionableObject),
				typeof(global::System.Collections.Generic.IList<global::UnityEngine.Vector3>),
				typeof(float),
				typeof(global::Kampai.Util.ILogger)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
