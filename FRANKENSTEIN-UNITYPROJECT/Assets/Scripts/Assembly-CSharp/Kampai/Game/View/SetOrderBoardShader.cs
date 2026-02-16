namespace Kampai.Game.View
{
	public class SetOrderBoardShader : global::UnityEngine.MonoBehaviour
	{
		private void Start()
		{
			base.renderer.material.shader = global::UnityEngine.Shader.Find("Kampai/Background/(+4) Platform");
		}
	}
}
