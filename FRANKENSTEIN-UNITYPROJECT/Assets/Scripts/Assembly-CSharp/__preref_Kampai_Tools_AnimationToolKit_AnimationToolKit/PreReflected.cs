namespace __preref_Kampai_Tools_AnimationToolKit_AnimationToolKit
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Tools.AnimationToolKit.AnimationToolKit();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = new global::System.Action<object>[1]
			{
				delegate(object t)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)t).PostConstruct();
				}
			};
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[24]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).ContextView = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).Context = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).DefinitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).PlayerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IRandomService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).randomService = (global::Kampai.Common.IRandomService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.AnimationToolkitModel), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).model = (global::Kampai.Tools.AnimationToolKit.AnimationToolkitModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.LoadInterfaceSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).LoadInterfaceSignal = (global::Kampai.Tools.AnimationToolKit.LoadInterfaceSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.GenerateMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).generateMinionSignal = (global::Kampai.Tools.AnimationToolKit.GenerateMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.MinionCreatedSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).minionCreatedSignal = (global::Kampai.Tools.AnimationToolKit.MinionCreatedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.AddMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).AddMinionSignal = (global::Kampai.Tools.AnimationToolKit.AddMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.RemoveMinionSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).RemoveMinionSignal = (global::Kampai.Tools.AnimationToolKit.RemoveMinionSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.PlayMinionAnimationSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).playGachaSignal = (global::Kampai.Tools.AnimationToolKit.PlayMinionAnimationSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.GenerateVillainSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).generateVillainSignal = (global::Kampai.Tools.AnimationToolKit.GenerateVillainSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.VillainCreatedSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).villainCreatedSignal = (global::Kampai.Tools.AnimationToolKit.VillainCreatedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.GenerateCharacterSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).generateCharacterSignal = (global::Kampai.Tools.AnimationToolKit.GenerateCharacterSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.CharacterCreatedSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).characterCreatedSignal = (global::Kampai.Tools.AnimationToolKit.CharacterCreatedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.AddCharacterSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).addCharacterSignal = (global::Kampai.Tools.AnimationToolKit.AddCharacterSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.RemoveCharacterSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).removeCharacterSignal = (global::Kampai.Tools.AnimationToolKit.RemoveCharacterSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Tools.AnimationToolKit.EnableInterfaceSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).enableInterfaceSignal = (global::Kampai.Tools.AnimationToolKit.EnableInterfaceSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayLocalAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).audioSignal = (global::Kampai.Main.PlayLocalAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.StartLoopingAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).startLoopingAudioSignal = (global::Kampai.Main.StartLoopingAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.StopLocalAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).stopAudioSignal = (global::Kampai.Main.StopLocalAudioSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayMinionStateAudioSignal), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.AnimationToolKit)target).minionStateAudioSignal = (global::Kampai.Main.PlayMinionStateAudioSignal)val;
				})
			};
			object[] array = new object[24];
			array[0] = global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW;
			array[1] = global::Kampai.Tools.AnimationToolKit.AnimationToolKitElement.CONTEXT;
			SetterNames = array;
		}
	}
}
