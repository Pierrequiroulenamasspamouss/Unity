namespace Kampai.Common
{
	public class ManifestService : global::Kampai.Common.IManifestService
	{
		private global::System.Collections.Generic.Dictionary<string, string> assetManifest = new global::System.Collections.Generic.Dictionary<string, string>();

		private global::System.Collections.Generic.Dictionary<string, string> bundleManifest = new global::System.Collections.Generic.Dictionary<string, string>();

		private global::System.Collections.Generic.Dictionary<string, string> bundleOriginalNameMap = new global::System.Collections.Generic.Dictionary<string, string>();

		private global::System.Collections.Generic.List<string> sharedBundles = new global::System.Collections.Generic.List<string>();

		private global::System.Collections.Generic.List<string> shaderBundles = new global::System.Collections.Generic.List<string>();

		private global::System.Collections.Generic.List<string> audioBundles = new global::System.Collections.Generic.List<string>();

		private global::System.Collections.Generic.HashSet<global::Kampai.Util.BundleInfo> bundles = new global::System.Collections.Generic.HashSet<global::Kampai.Util.BundleInfo>();

		private global::System.Collections.Generic.HashSet<string> bundleNames = new global::System.Collections.Generic.HashSet<string>();

		private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> bundleAssetsMap = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>>();

		private string dlcURL;

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

        //public void GenerateMasterManifest()
        //{
        //	Clear();
        //	string value = global::System.IO.File.ReadAllText(global::Kampai.Util.GameConstants.RESOURCE_MANIFEST_PATH);
        //	global::Kampai.Util.ManifestObject manifestObject = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Util.ManifestObject>(value);
        //	assetManifest = manifestObject.assets;
        //	bundles = manifestObject.bundles;
        //	foreach (global::Kampai.Util.BundleInfo bundle in bundles)
        //	{
        //		bundleNames.Add(bundle.name);
        //		if (bundle.shaders)
        //		{
        //			shaderBundles.Add(bundle.name);
        //		}
        //		else if (bundle.audio)
        //		{
        //			audioBundles.Add(bundle.name);
        //		}
        //		else if (bundle.shared)
        //		{
        //			sharedBundles.Add(bundle.name);
        //		}
        //		AddBundle(bundle);
        //	}
        //	dlcURL = manifestObject.baseURL;
        //	buildBundledAssetsLookup();
        //}
        public void GenerateMasterManifest()
        {
            // 1. Nettoyer les anciennes données pour éviter les doublons
            Clear();

            try
            {
                string text = global::System.IO.File.ReadAllText(global::Kampai.Util.GameConstants.RESOURCE_MANIFEST_PATH);
                global::Kampai.Util.ManifestObject manifestObject = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::Kampai.Util.ManifestObject>(text);

                if (manifestObject != null)
                {
                    // 2. Récupérer les assets (Dictionnaire)
                    if (manifestObject.assets != null)
                    {
                        assetManifest = manifestObject.assets;
                    }

                    // 3. Récupérer l'URL de base
                    dlcURL = manifestObject.baseURL; // Peut être null, pas grave

                    // 4. Traiter les Bundles
                    if (manifestObject.bundles != null)
                    {
                        bundles = manifestObject.bundles;

                        foreach (global::Kampai.Util.BundleInfo bundle in bundles)
                        {
                            // Logique originale restaurée
                            if (bundle != null)
                            {
                                bundleNames.Add(bundle.name);

                                // Vérifications safe des booléens
                                if (bundle.shaders)
                                {
                                    shaderBundles.Add(bundle.name);
                                }
                                else if (bundle.audio)
                                {
                                    audioBundles.Add(bundle.name);
                                }
                                else if (bundle.shared)
                                {
                                    sharedBundles.Add(bundle.name);
                                }

                                AddBundle(bundle);
                            }
                        }
                    }
                    else
                    {
                        global::UnityEngine.Debug.LogWarning("MOCK: Manifest 'bundles' list is null. Using local assets only.");
                    }

                    // 5. Reconstruire l'index
                    buildBundledAssetsLookup();
                }
            }
            catch (global::System.Exception ex)
            {
                global::UnityEngine.Debug.LogWarning("MOCK: Error in GenerateMasterManifest (safe to ignore): " + ex.Message);
            }
        }

        public void AddBundle(global::Kampai.Util.BundleInfo bundle)
		{
			bundleManifest.Add(bundle.name, global::Kampai.Util.GameConstants.DLC_PATH);
			logger.Info("ManifestService added bundle: '{0}'", bundle.originalName);
			bundleOriginalNameMap.Add(bundle.name, bundle.originalName);
		}

		public string GetAssetLocation(string asset)
		{
			if (!assetManifest.ContainsKey(asset))
			{
				return string.Empty;
			}
			return assetManifest[asset];
		}

		public string GetBundleLocation(string bundle)
		{
			if (!bundleManifest.ContainsKey(bundle))
			{
				logger.Error("Unable to find bundle: {0}", bundle);
				return string.Empty;
			}
			return bundleManifest[bundle];
		}

		public string GetBundleOriginalName(string bundle)
		{
			if (!bundleOriginalNameMap.ContainsKey(bundle))
			{
				logger.Error("Unable to find bundle: {0}", bundle);
				return string.Empty;
			}
			return bundleOriginalNameMap[bundle];
		}

		public global::System.Collections.Generic.HashSet<global::Kampai.Util.BundleInfo> GetBundles()
		{
			return bundles;
		}

		public string GetDLCURL()
		{
			return dlcURL;
		}

		public global::System.Collections.Generic.IList<string> GetSharedBundles()
		{
			return sharedBundles;
		}

		public global::System.Collections.Generic.IList<string> GetShaderBundles()
		{
			return shaderBundles;
		}

		public global::System.Collections.Generic.IList<string> GetAudioBundles()
		{
			return audioBundles;
		}

		public bool ContainsBundle(string name)
		{
			return bundleNames.Contains(name);
		}

		public global::System.Collections.Generic.IList<string> GetAssetsInBundle(string bundle)
		{
			if (!bundleAssetsMap.ContainsKey(bundle))
			{
				return new global::System.Collections.Generic.List<string>();
			}
			return bundleAssetsMap[bundle];
		}

		private void buildBundledAssetsLookup()
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> item in assetManifest)
			{
				string key = item.Key;
				string value = item.Value;
				if (!bundleAssetsMap.ContainsKey(value))
				{
					bundleAssetsMap.Add(value, new global::System.Collections.Generic.List<string>());
				}
				bundleAssetsMap[value].Add(key);
			}
		}

		private void Clear()
		{
			bundleManifest.Clear();
			bundleOriginalNameMap.Clear();
			sharedBundles.Clear();
			shaderBundles.Clear();
			audioBundles.Clear();
			bundleNames.Clear();
		}
	}
}
