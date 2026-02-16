namespace Kampai.Game
{
	public class VillainIslandLocation : global::UnityEngine.MonoBehaviour
	{
		public global::System.Collections.Generic.List<global::UnityEngine.GameObject> colliders;

		public void removeAllColliders()
		{
			foreach (global::UnityEngine.GameObject collider in colliders)
			{
				collider.collider.enabled = false;
			}
		}
	}
}
