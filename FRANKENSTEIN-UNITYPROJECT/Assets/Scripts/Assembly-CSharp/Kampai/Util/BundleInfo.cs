namespace Kampai.Util
{
	public class BundleInfo
	{
		public string name { get; set; }

		public string originalName { get; set; }

		public int tier { get; set; }

		public string sum { get; set; }

		public ulong size { get; set; }

		public bool shared { get; set; }

		public bool shaders { get; set; }

		public bool audio { get; set; }

		public bool isZipped { get; set; }

		public ulong zipsize { get; set; }

		public string zipsum { get; set; }
	}
}
