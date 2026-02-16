[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Camera))]
public class EnableCameraDepthInForward : global::UnityEngine.MonoBehaviour
{
	private void Start()
	{
		Set();
	}

	private void Set()
	{
		if (GetComponent<global::UnityEngine.Camera>().depthTextureMode == global::UnityEngine.DepthTextureMode.None)
		{
			GetComponent<global::UnityEngine.Camera>().depthTextureMode = global::UnityEngine.DepthTextureMode.Depth;
		}
	}
}
