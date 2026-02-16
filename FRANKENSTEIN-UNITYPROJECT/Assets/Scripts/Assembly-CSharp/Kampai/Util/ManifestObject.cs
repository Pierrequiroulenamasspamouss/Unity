namespace Kampai.Util
{
	public class ManifestObject
	{
		public string id { get; set; }

		public string baseURL { get; set; }

		public global::System.Collections.Generic.HashSet<global::Kampai.Util.BundleInfo> bundles { get; set; }

		public global::System.Collections.Generic.Dictionary<string, string> assets { get; set; }
	}
}
