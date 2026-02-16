namespace __preref_Kampai_Util_DummyCharacterBuilder
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.DummyCharacterBuilder();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[9]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Util.DummyCharacterBuilder)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayLocalAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DummyCharacterBuilder)target).audioSignal = (global::Kampai.Main.PlayLocalAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.StartLoopingAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DummyCharacterBuilder)target).startLoopingAudioSignal = (global::Kampai.Main.StartLoopingAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.StopLocalAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DummyCharacterBuilder)target).stopAudioSignal = (global::Kampai.Main.StopLocalAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayMinionStateAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.DummyCharacterBuilder)target).minionStateAudioSignal = (global::Kampai.Main.PlayMinionStateAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDLCService), delegate(object target, object val)
				{
					((global::Kampai.Util.DummyCharacterBuilder)target).dlcService = (global::Kampai.Game.IDLCService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IMinionBuilder), delegate(object target, object val)
				{
					((global::Kampai.Util.DummyCharacterBuilder)target).minionBuilder = (global::Kampai.Util.IMinionBuilder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IRoutineRunner), delegate(object target, object val)
				{
					((global::Kampai.Util.DummyCharacterBuilder)target).routineRunner = (global::Kampai.Util.IRoutineRunner)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IRandomService), delegate(object target, object val)
				{
					((global::Kampai.Util.DummyCharacterBuilder)target).randomService = (global::Kampai.Common.IRandomService)val;
				})
			};
			SetterNames = new object[9];
		}
	}
}
