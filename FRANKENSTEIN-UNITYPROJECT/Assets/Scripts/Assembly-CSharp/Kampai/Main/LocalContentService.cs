namespace Kampai.Main
{
	public class LocalContentService : global::Kampai.Main.ILocalContentService
	{
		private global::System.Collections.Generic.Dictionary<string, string> resourceNamesMap;

		private global::System.Collections.Generic.HashSet<string> streamingAssets;

		private global::System.Collections.Generic.List<string> audioBanks;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[PostConstruct]
		public void PostConstruct()
		{
			global::UnityEngine.TextAsset textAsset = global::UnityEngine.Resources.Load<global::UnityEngine.TextAsset>("Manifest");
			if (null == textAsset)
			{
				logger.Error("Failed to load bundle resources manifest");
				return;
			}
			global::Kampai.Main.LocalResourcesManifest localResourcesManifest = global::Kampai.Util.FastJsonParser.Deserialize<global::Kampai.Main.LocalResourcesManifest>(textAsset.text);
			global::UnityEngine.Resources.UnloadAsset(textAsset);
			resourceNamesMap = new global::System.Collections.Generic.Dictionary<string, string>(localResourcesManifest.bundledAssets.Count);
			foreach (string bundledAsset in localResourcesManifest.bundledAssets)
			{
				string fileName = global::System.IO.Path.GetFileName(bundledAsset);
				if (resourceNamesMap.ContainsKey(fileName))
				{
					logger.Error("Resource {0} is already mapped", bundledAsset);
				}
				else
				{
					resourceNamesMap.Add(fileName, bundledAsset);
				}
			}
			streamingAssets = new global::System.Collections.Generic.HashSet<string>(localResourcesManifest.streamingAssets);
			audioBanks = localResourcesManifest.audioBanks;
		}

		public bool IsLocalAsset(string name)
		{
			return resourceNamesMap.ContainsKey(name);
		}

		public string GetAssetPath(string name)
		{
			if (!IsLocalAsset(name))
			{
				return string.Empty;
			}
			return resourceNamesMap[name];
		}

		public bool IsStreamingAsset(string name)
		{
			return streamingAssets.Contains(name);
		}

		public global::System.Collections.Generic.List<string> GetStreamingAudioBanks()
		{
			return audioBanks;
		}
	}
}
