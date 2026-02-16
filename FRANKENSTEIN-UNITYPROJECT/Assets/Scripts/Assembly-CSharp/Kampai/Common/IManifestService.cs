namespace Kampai.Common
{
	public interface IManifestService
	{
		void GenerateMasterManifest();

		void AddBundle(global::Kampai.Util.BundleInfo bundle);

		string GetAssetLocation(string asset);

		string GetBundleLocation(string bundle);

		string GetBundleOriginalName(string bundle);

		global::System.Collections.Generic.HashSet<global::Kampai.Util.BundleInfo> GetBundles();

		string GetDLCURL();

		global::System.Collections.Generic.IList<string> GetSharedBundles();

		global::System.Collections.Generic.IList<string> GetShaderBundles();

		global::System.Collections.Generic.IList<string> GetAudioBundles();

		global::System.Collections.Generic.IList<string> GetAssetsInBundle(string bundle);

		bool ContainsBundle(string name);
	}
}
