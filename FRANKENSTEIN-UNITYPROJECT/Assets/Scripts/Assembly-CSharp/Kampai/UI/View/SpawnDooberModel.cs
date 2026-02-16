namespace Kampai.UI.View
{
	public class SpawnDooberModel
	{
		public global::UnityEngine.Vector3 TikiBarWorldPosition { get; set; }

		public global::UnityEngine.Vector3 XPGlassPosition { get; set; }

		public global::UnityEngine.Vector3 PremiumGlassPosition { get; set; }

		public global::UnityEngine.Vector3 GrindGlassPosition { get; set; }

		public global::UnityEngine.Vector3 StorageGlassPosition { get; set; }

		public int DooberCounter { get; set; }

		public global::UnityEngine.Vector3 expScreenPosition { get; set; }

		public global::UnityEngine.Vector3 premiumScreenPosition { get; set; }

		public global::UnityEngine.Vector3 grindScreenPosition { get; set; }

		public global::UnityEngine.Vector3 itemScreenPosition { get; set; }

		public global::UnityEngine.Vector3 defaultDooberSpawnLocation { get; set; }

		public SpawnDooberModel()
		{
			expScreenPosition = new global::UnityEngine.Vector3(0.4f, 0.6f, 0f);
			premiumScreenPosition = new global::UnityEngine.Vector3(0.6f, 0.6f, 0f);
			grindScreenPosition = new global::UnityEngine.Vector3(0.4f, 0.4f, 0f);
			itemScreenPosition = new global::UnityEngine.Vector3(0.6f, 0.4f, 0f);
			defaultDooberSpawnLocation = new global::UnityEngine.Vector3(0.5f, 0.3f, 0f);
			TikiBarWorldPosition = global::UnityEngine.Vector3.zero;
			XPGlassPosition = global::UnityEngine.Vector3.zero;
			PremiumGlassPosition = global::UnityEngine.Vector3.zero;
			GrindGlassPosition = global::UnityEngine.Vector3.zero;
			StorageGlassPosition = global::UnityEngine.Vector3.zero;
		}
	}
}
