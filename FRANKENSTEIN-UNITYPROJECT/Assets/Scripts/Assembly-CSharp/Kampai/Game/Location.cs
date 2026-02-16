namespace Kampai.Game
{
	public class Location : global::System.IEquatable<global::Kampai.Game.Location>
	{
		public int x { get; set; }

		public int y { get; set; }

		[global::Newtonsoft.Json.JsonConstructor]
		public Location(int xPos = 0, int yPos = 0)
		{
			x = xPos;
			y = yPos;
		}

		public Location(global::UnityEngine.Vector3 pos)
		{
			x = global::UnityEngine.Mathf.RoundToInt(pos.x);
			y = global::UnityEngine.Mathf.RoundToInt(pos.z);
		}

		public static float Distance(global::Kampai.Game.Location l1, global::Kampai.Game.Location l2)
		{
			int num = l2.x - l1.x;
			int num2 = l2.y - l1.y;
			return global::UnityEngine.Mathf.Sqrt(num * num + num2 * num2);
		}

		public override bool Equals(object obj)
		{
			return obj is global::Kampai.Game.Location;
		}

		public bool Equals(global::Kampai.Game.Location obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != typeof(global::Kampai.Game.Location))
			{
				return false;
			}
			return x == obj.x && y == obj.y;
		}

		public override int GetHashCode()
		{
			return x.GetHashCode() ^ y.GetHashCode();
		}

		public static explicit operator global::UnityEngine.Vector3(global::Kampai.Game.Location l)
		{
			return new global::UnityEngine.Vector3(l.x, 0f, l.y);
		}
	}
}
