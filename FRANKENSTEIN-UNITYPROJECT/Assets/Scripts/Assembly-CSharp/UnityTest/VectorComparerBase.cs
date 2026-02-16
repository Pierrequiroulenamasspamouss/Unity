namespace UnityTest
{
	public abstract class VectorComparerBase<T> : global::UnityTest.ComparerBaseGeneric<T>
	{
		protected bool AreVectorMagnitudeEqual(float a, float b, double floatingPointError)
		{
			if ((double)global::System.Math.Abs(a) < floatingPointError && (double)global::System.Math.Abs(b) < floatingPointError)
			{
				return true;
			}
			if ((double)global::System.Math.Abs(a - b) < floatingPointError)
			{
				return true;
			}
			return false;
		}
	}
}
