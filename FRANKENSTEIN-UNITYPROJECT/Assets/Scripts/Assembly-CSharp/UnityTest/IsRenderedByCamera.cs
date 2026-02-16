namespace UnityTest
{
	public class IsRenderedByCamera : global::UnityTest.ComparerBaseGeneric<global::UnityEngine.Renderer, global::UnityEngine.Camera>
	{
		public enum CompareType
		{
			IsVisible = 0,
			IsNotVisible = 1
		}

		public global::UnityTest.IsRenderedByCamera.CompareType compareType;

		protected override bool Compare(global::UnityEngine.Renderer renderer, global::UnityEngine.Camera camera)
		{
			global::UnityEngine.Plane[] planes = global::UnityEngine.GeometryUtility.CalculateFrustumPlanes(camera);
			bool flag = global::UnityEngine.GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
			switch (compareType)
			{
			case global::UnityTest.IsRenderedByCamera.CompareType.IsVisible:
				return flag;
			case global::UnityTest.IsRenderedByCamera.CompareType.IsNotVisible:
				return !flag;
			default:
				throw new global::System.Exception();
			}
		}
	}
}
