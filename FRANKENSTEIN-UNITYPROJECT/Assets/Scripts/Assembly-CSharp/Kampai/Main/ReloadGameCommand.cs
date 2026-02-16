namespace Kampai.Main
{
	public class ReloadGameCommand : global::strange.extensions.command.impl.Command
	{
		private static readonly global::System.Collections.Generic.IList<string> DO_NOT_DESTROY_GOS = new global::System.Collections.Generic.List<string> { "NimbleCallbackHelper", "(singleton) DeltaDNA.SDK", "UnityFacebookSDKPlugin" };

		[Inject]
		public global::Kampai.Main.IAssetBundlesService assetBundlesService { get; set; }

		[Inject]
		public global::Kampai.Main.IUpsightService upsightService { get; set; }

		[Inject]
		public global::Kampai.Game.EnvironmentState state { get; set; }

		public override void Execute()
		{
			upsightService.OnGameReloadCallback();
			state.DisplayOn = false;
			state.EnvironmentBuilt = false;
			state.GridConstructed = false;
			global::UnityEngine.Object[] array = global::UnityEngine.Object.FindObjectsOfType(typeof(global::UnityEngine.GameObject));
			for (int i = 0; i < array.Length; i++)
			{
				global::UnityEngine.GameObject gameObject = (global::UnityEngine.GameObject)array[i];
				string name = gameObject.name;
				if (name == null || !DO_NOT_DESTROY_GOS.Contains(name))
				{
					global::UnityEngine.Object.Destroy(gameObject);
				}
			}
			assetBundlesService.UnloadDLCBundles();
			assetBundlesService.UnloadSharedBundles();
			Go.killAllTweens();
			global::Kampai.Util.KampaiResources.ClearCache();
			global::UnityEngine.Application.LoadLevel("Initialize");
		}
	}
}
