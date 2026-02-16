public abstract class AbstractGoSplineSolver
{
	protected global::System.Collections.Generic.List<global::UnityEngine.Vector3> _nodes;

	protected float _pathLength;

	protected int totalSubdivisionsPerNodeForLookupTable = 5;

	protected global::System.Collections.Generic.Dictionary<float, float> _segmentTimeForDistance;

	public global::System.Collections.Generic.List<global::UnityEngine.Vector3> nodes
	{
		get
		{
			return _nodes;
		}
	}

	public float pathLength
	{
		get
		{
			return _pathLength;
		}
	}

	public virtual void buildPath()
	{
		int num = _nodes.Count * totalSubdivisionsPerNodeForLookupTable;
		_pathLength = 0f;
		float num2 = 1f / (float)num;
		_segmentTimeForDistance = new global::System.Collections.Generic.Dictionary<float, float>(num);
		global::UnityEngine.Vector3 b = getPoint(0f);
		for (int i = 1; i < num + 1; i++)
		{
			float num3 = num2 * (float)i;
			global::UnityEngine.Vector3 point = getPoint(num3);
			_pathLength += global::UnityEngine.Vector3.Distance(point, b);
			b = point;
			_segmentTimeForDistance.Add(num3, _pathLength);
		}
	}

	public abstract void closePath();

	public abstract global::UnityEngine.Vector3 getPoint(float t);

	public virtual global::UnityEngine.Vector3 getPointOnPath(float t)
	{
		float num = _pathLength * t;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		global::System.Collections.Generic.Dictionary<float, float>.Enumerator enumerator = _segmentTimeForDistance.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				global::System.Collections.Generic.KeyValuePair<float, float> current = enumerator.Current;
				float key = current.Key;
				float value = current.Value;
				if (value >= num)
				{
					num4 = key;
					num5 = value;
					if (num2 > 0f)
					{
						num3 = _segmentTimeForDistance[num2];
					}
					break;
				}
				num2 = key;
			}
		}
		finally
		{
			enumerator.Dispose();
		}
		float num6 = num4 - num2;
		float num7 = num5 - num3;
		float num8 = num - num3;
		t = num2 + num8 / num7 * num6;
		return getPoint(t);
	}

	public void reverseNodes()
	{
		_nodes.Reverse();
	}

	public virtual void drawGizmos()
	{
	}
}
