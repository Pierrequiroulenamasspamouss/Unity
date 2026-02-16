namespace __preref_Kampai_Tools_AnimationToolKit_LoadAnimationToolKitManifestCommand
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitManifestCommand();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[5]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.IManifestService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitManifestCommand)target).manifestService = (global::Kampai.Common.IManifestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.IAssetBundlesService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitManifestCommand)target).assetBundlesService = (global::Kampai.Main.IAssetBundlesService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalContentService), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitManifestCommand)target).localContentService = (global::Kampai.Main.ILocalContentService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.command.api.ICommandBinder), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitManifestCommand)target).commandBinder = (global::strange.extensions.command.api.ICommandBinder)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.injector.api.IInjectionBinder), delegate(object target, object val)
				{
					((global::Kampai.Tools.AnimationToolKit.LoadAnimationToolKitManifestCommand)target).injectionBinder = (global::strange.extensions.injector.api.IInjectionBinder)val;
				})
			};
			SetterNames = new object[5];
		}
	}
}
