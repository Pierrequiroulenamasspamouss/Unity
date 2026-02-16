namespace __preref_Kampai_Util_NamedCharacterBuilder
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Util.NamedCharacterBuilder();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[7]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Util.NamedCharacterBuilder)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayLocalAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.NamedCharacterBuilder)target).audioSignal = (global::Kampai.Main.PlayLocalAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.StartLoopingAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.NamedCharacterBuilder)target).startLoopingAudioSignal = (global::Kampai.Main.StartLoopingAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.StopLocalAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.NamedCharacterBuilder)target).stopAudioSignal = (global::Kampai.Main.StopLocalAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayMinionStateAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Util.NamedCharacterBuilder)target).minionStateAudioSignal = (global::Kampai.Main.PlayMinionStateAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDLCService), delegate(object target, object val)
				{
					((global::Kampai.Util.NamedCharacterBuilder)target).dlcService = (global::Kampai.Game.IDLCService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.IMinionBuilder), delegate(object target, object val)
				{
					((global::Kampai.Util.NamedCharacterBuilder)target).minionBuilder = (global::Kampai.Util.IMinionBuilder)val;
				})
			};
			SetterNames = new object[7];
		}
	}
}
