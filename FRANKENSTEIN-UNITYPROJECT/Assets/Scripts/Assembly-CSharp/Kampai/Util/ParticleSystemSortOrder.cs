namespace Kampai.Util
{
	public class ParticleSystemSortOrder : global::UnityEngine.MonoBehaviour
	{
		public int SortingLayer = 1;

		private void Start()
		{
			if (base.particleSystem != null && base.particleSystem.renderer != null)
			{
				base.particleSystem.renderer.sortingOrder = SortingLayer;
			}
		}
	}
}
