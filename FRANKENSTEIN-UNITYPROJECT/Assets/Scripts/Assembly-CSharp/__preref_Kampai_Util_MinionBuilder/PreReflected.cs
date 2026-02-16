namespace __preref_Kampai_Util_MinionBuilder
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.MinionBuilder();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[5]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Util.MinionBuilder)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayLocalAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.MinionBuilder)target).audioSignal = (global::Kampai.Main.PlayLocalAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.StartLoopingAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.MinionBuilder)target).startLoopingAudioSignal = (global::Kampai.Main.StartLoopingAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.StopLocalAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.MinionBuilder)target).stopAudioSignal = (global::Kampai.Main.StopLocalAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayMinionStateAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.MinionBuilder)target).minionStateAudioSignal = (global::Kampai.Main.PlayMinionStateAudioSignal)val;
				})
			};
			SetterNames = new object[5];
		}
	}
}
