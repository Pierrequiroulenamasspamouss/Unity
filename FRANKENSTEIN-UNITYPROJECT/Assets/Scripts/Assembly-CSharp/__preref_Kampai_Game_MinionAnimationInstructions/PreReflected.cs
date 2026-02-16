namespace __preref_Kampai_Game_MinionAnimationInstructions
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.MinionAnimationInstructions((global::System.Collections.Generic.HashSet<int>)p[0], (global::Kampai.Util.Boxed<global::UnityEngine.Vector3>)p[1], (bool)p[2]);
			ConstructorParameters = new global::System.Type[3]
			{
				typeof(global::System.Collections.Generic.HashSet<int>),
				typeof(global::Kampai.Util.Boxed<global::UnityEngine.Vector3>),
				typeof(bool)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
