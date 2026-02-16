namespace Kampai.Util
{
	public static class KampaiResources
	{
		private sealed class AssetsCache
		{
			private global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::UnityEngine.Object>> cache = new global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<global::UnityEngine.Object>>();

			public global::UnityEngine.Object Get(string name, global::System.Type type)
			{
				global::System.Collections.Generic.List<global::UnityEngine.Object> value;
				if (name == null || !cache.TryGetValue(name, out value))
				{
					return null;
				}
				global::System.Collections.Generic.List<global::UnityEngine.Object>.Enumerator enumerator = value.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						global::UnityEngine.Object current = enumerator.Current;
						if (current != null && type.IsAssignableFrom(current.GetType()))
						{
							return current;
						}
					}
				}
				finally
				{
					enumerator.Dispose();
				}
				return null;
			}

			public void Clear()
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Collections.Generic.List<global::UnityEngine.Object>> item in cache)
				{
					foreach (global::UnityEngine.Object item2 in item.Value)
					{
						if (!(item2 is global::UnityEngine.GameObject))
						{
							global::UnityEngine.Resources.UnloadAsset(item2);
						}
					}
				}
				cache.Clear();
				global::System.GC.Collect();
				global::System.GC.WaitForPendingFinalizers();
			}

			public void Add(global::UnityEngine.Object t)
			{
				string name = t.name;
				global::System.Collections.Generic.List<global::UnityEngine.Object> value;
				if (!cache.TryGetValue(name, out value))
				{
					value = new global::System.Collections.Generic.List<global::UnityEngine.Object>();
					cache.Add(name, value);
				}
				value.Add(t);
			}
		}

		private static global::Kampai.Common.IManifestService manifestService;

		private static global::Kampai.Main.IAssetBundlesService assetBundlesService;

		private static global::Kampai.Main.ILocalContentService localContentService;

		private static global::Kampai.Util.KampaiResources.AssetsCache cachedObjects = new global::Kampai.Util.KampaiResources.AssetsCache();

		public static void SetManifestService(global::Kampai.Common.IManifestService service)
		{
			manifestService = service;
		}

		public static void SetAssetBundlesService(global::Kampai.Main.IAssetBundlesService service)
		{
			assetBundlesService = service;
		}

		public static void SetLocalContentService(global::Kampai.Main.ILocalContentService service)
		{
			localContentService = service;
		}

		public static void ClearCache()
		{
			cachedObjects.Clear();
		}

		public static bool FileExists(string path)
		{
			return manifestService.GetAssetLocation(path).Length > 0 || localContentService.IsLocalAsset(path);
		}

		public static T Load<T>(string path)
		{
			object obj = Load(path, typeof(T));
			return (T)obj;
		}

		public static global::UnityEngine.Object Load(string path)
		{
			return Load(path, typeof(global::UnityEngine.Object));
		}

        public static global::UnityEngine.Object Load(string path, global::System.Type type)
        {
            if (cachedObjects == null) cachedObjects = new AssetsCache();

            // 1. Check Cache
            global::UnityEngine.Object obj = cachedObjects.Get(path, type);
            if (obj != null) return obj;

            TimeProfiler.StartAssetLoadSection(path);
            global::UnityEngine.Object result = null;

            // 2. Try Local Resources (Blind Fallback)
            // This is essential for the UI to load from Resources/
            if (localContentService != null && localContentService.IsLocalAsset(path))
            {
                result = global::UnityEngine.Resources.Load(localContentService.GetAssetPath(path), type);
            }
            if (result == null)
            {
                result = global::UnityEngine.Resources.Load(path, type);
            }

            // 3. Try Asset Bundles
            if (result == null && manifestService != null && assetBundlesService != null)
            {
                try
                {
                    string loc = manifestService.GetAssetLocation(path);
                    if (!string.IsNullOrEmpty(loc))
                    {
                        global::UnityEngine.AssetBundle bundle = assetBundlesService.GetDLCBundle(loc);
                        if (bundle != null) result = bundle.Load(path, type);
                    }
                }
                catch { }
            }

            // =================================================================
            // FIX: SHADER SWAP (Fixes White/Pink Textures)
            // =================================================================
            if (result != null && result is global::UnityEngine.Material)
            {
                global::UnityEngine.Material mat = result as global::UnityEngine.Material;
                if (mat.shader == null || !mat.shader.isSupported || mat.shader.name == "Hidden/InternalErrorShader")
                {
                    global::UnityEngine.Shader fallback = global::UnityEngine.Shader.Find("Mobile/Diffuse");
                    if (fallback != null) mat.shader = fallback;
                }
            }

            // For GameObjects (Prefabs), we need to drill down
            if (result != null && result is global::UnityEngine.GameObject)
            {
                // Note: We can't modify assets on disk, but we can catch them at instantiation time.
                // This block is just a placeholder; the real fix for prefabs happens when they are instantiated.
            }

            // 4. Cache & Return
            if (result != null)
            {
                cachedObjects.Add(result);
            }
            else
            {
                // FIX: Log Warning only. This keeps the game running even if bees are missing.
                global::UnityEngine.Debug.LogWarning("[FIX] KampaiResources: Could not load '" + path + "'. Returning null.");
            }

            TimeProfiler.EndAssetLoadSection();
            return result;
        }

    }
}
