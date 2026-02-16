public static class VectorUtils
{
	public static global::UnityEngine.Vector3 ZeroY(global::UnityEngine.Vector3 vector)
	{
		return new global::UnityEngine.Vector3(vector.x, 0f, vector.z);
	}

	public static global::UnityEngine.Vector3 ZeroXZ(global::UnityEngine.Vector3 vector)
	{
		return new global::UnityEngine.Vector3(0f, vector.y, 0f);
	}

	public static global::UnityEngine.Vector3 ZeroZ(global::UnityEngine.Vector3 vector)
	{
		return new global::UnityEngine.Vector3(vector.x, vector.y, 0f);
	}
}
