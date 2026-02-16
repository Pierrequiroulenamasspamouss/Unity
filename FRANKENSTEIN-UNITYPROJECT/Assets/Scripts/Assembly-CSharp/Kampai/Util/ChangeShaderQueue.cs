namespace Kampai.Util
{
	public class ChangeShaderQueue : global::UnityEngine.MonoBehaviour
	{
		public global::Kampai.Util.Graphics.RenderQueue Queue = global::Kampai.Util.Graphics.RenderQueue.Background;

		public int Offset;

		public int MaterialIndex;

		private void Awake()
		{
			if (base.renderer != null && base.renderer.materials != null && base.renderer.materials[MaterialIndex] != null)
			{
				base.renderer.materials[MaterialIndex].renderQueue = (int)(Queue + Offset);
			}
		}
	}
}
