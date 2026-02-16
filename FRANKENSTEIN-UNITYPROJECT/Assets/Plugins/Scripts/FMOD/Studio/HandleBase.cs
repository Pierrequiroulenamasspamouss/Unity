namespace FMOD.Studio
{
	public abstract class HandleBase
	{
		protected global::System.IntPtr rawPtr;

		public HandleBase(global::System.IntPtr newPtr)
		{
			rawPtr = newPtr;
		}

		public bool isValid()
		{
			return rawPtr != global::System.IntPtr.Zero && isValidInternal();
		}

		protected abstract bool isValidInternal();

		public global::System.IntPtr getRaw()
		{
			return rawPtr;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as global::FMOD.Studio.HandleBase);
		}

		public bool Equals(global::FMOD.Studio.HandleBase p)
		{
			return (object)p != null && rawPtr == p.rawPtr;
		}

		public override int GetHashCode()
		{
			return rawPtr.ToInt32();
		}

		public static bool operator ==(global::FMOD.Studio.HandleBase a, global::FMOD.Studio.HandleBase b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.rawPtr == b.rawPtr;
		}

		public static bool operator !=(global::FMOD.Studio.HandleBase a, global::FMOD.Studio.HandleBase b)
		{
			return !(a == b);
		}
	}
}
