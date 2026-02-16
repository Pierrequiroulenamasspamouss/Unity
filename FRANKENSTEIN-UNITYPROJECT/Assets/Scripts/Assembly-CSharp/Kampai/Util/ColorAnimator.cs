namespace Kampai.Util
{
	public class ColorAnimator : global::UnityEngine.MonoBehaviour
	{
		public global::UnityEngine.Color color = global::UnityEngine.Color.white;

		private void Update()
		{
			base.renderer.material.color = color;
		}
	}
}
