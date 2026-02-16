namespace __preref_Kampai_Util_AI_SteerMinionToWander
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.AI.SteerMinionToWander();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[1]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IncidentalAnimationSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.AI.SteerMinionToWander)target).animSignal = (global::Kampai.Game.IncidentalAnimationSignal)val;
				})
			};
			SetterNames = new object[1];
		}
	}
}
