#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// Extracts ALL assets from specific .unity3d DLC bundles into Assets/Resources/DLC/
/// Unity 4.7 AssetBundle API: CreateFromFile / Load / LoadAll
/// Run: Tools > Extract DLC Assets to Resources
/// </summary>
public class ExtractDLCAssets : MonoBehaviour
{
    private const string BUNDLES_DIR = @"C:\Users\Pierr\Documents\Projects\Decompiles\Minions_Paradise\DLCs\IPA server's downloadable files dump\bundles";

    // Bundles to fully extract (all assets inside them)
    private static readonly string[] BundlesToExtract = new string[]
    {
        "508efd1b-411f-4a5c-ba1a-8d5b2ee9ce58.unity3d",  // Unique_CruiseShip_Prefab
        "8d7b84fd-2bbb-4812-911d-1039c15f5a7e.unity3d",  // AnimBuildingReaction
        "efd01f0f-73d1-4061-8148-dbd682b29483.unity3d",  // Decor_HorzBridgeFixed_Prefab
    };

    [MenuItem("Tools/Extract DLC Assets to Resources")]
    static void Extract()
    {
        int totalSaved = 0;

        foreach (string bundleFileName in BundlesToExtract)
        {
            string bundlePath = Path.Combine(BUNDLES_DIR, bundleFileName);
            if (!File.Exists(bundlePath))
            {
                Debug.LogError("[ExtractDLC] Bundle not found: " + bundlePath);
                continue;
            }

            AssetBundle bundle = AssetBundle.CreateFromFile(bundlePath);
            if (bundle == null)
            {
                Debug.LogError("[ExtractDLC] Failed to load bundle: " + bundlePath);
                continue;
            }

            Object[] all = bundle.LoadAll();
            Debug.Log(string.Format("[ExtractDLC] Bundle '{0}' — {1} assets:", bundleFileName, all.Length));

            foreach (Object asset in all)
            {
                if (asset == null) continue;
                string assetName = asset.name;
                if (string.IsNullOrEmpty(assetName)) continue;

                string ext = GetExtension(asset);
                string destPath = "Assets/Resources/" + assetName + ext;

                // Skip if already exists
                if (File.Exists(Application.dataPath + "/Resources/" + assetName + ext))
                {
                    Debug.Log("[ExtractDLC] Skipping (already exists): " + destPath);
                    continue;
                }

                Debug.Log(string.Format("[ExtractDLC]   -> {0} ({1})", assetName, asset.GetType().Name));

                try
                {
                    if (asset is GameObject)
                    {
                        GameObject instance = Object.Instantiate(asset) as GameObject;
                        instance.name = assetName;
                        PrefabUtility.CreatePrefab(destPath, instance);
                        Object.DestroyImmediate(instance);
                    }
                    else if (asset is Mesh || asset is Material || asset is Texture || 
                             asset is AnimationClip || asset is AudioClip || asset is Shader)
                    {
                        // These can be saved directly as assets
                        Object copy = Object.Instantiate(asset);
                        copy.name = assetName;
                        AssetDatabase.CreateAsset(copy, destPath);
                    }
                    else
                    {
                        // Generic fallback
                        Object copy = Object.Instantiate(asset);
                        copy.name = assetName;
                        AssetDatabase.CreateAsset(copy, destPath);
                    }
                    totalSaved++;
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning("[ExtractDLC] Could not save '" + assetName + "': " + ex.Message);
                }
            }

            bundle.Unload(false);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("Extract DLC Assets",
            "Done! Saved " + totalSaved + " assets to Assets/Resources/", "OK");
    }

    private static string GetExtension(Object asset)
    {
        if (asset is GameObject)          return ".prefab";
        if (asset is Mesh)                return ".asset";
        if (asset is Material)            return ".mat";
        if (asset is Texture2D)           return ".asset";
        if (asset is AnimationClip)       return ".anim";
        if (asset is AudioClip)           return ".asset";
        if (asset is Shader)              return ".shader";
        if (asset is RuntimeAnimatorController) return ".controller";
        return ".asset";
    }
}
#endif
