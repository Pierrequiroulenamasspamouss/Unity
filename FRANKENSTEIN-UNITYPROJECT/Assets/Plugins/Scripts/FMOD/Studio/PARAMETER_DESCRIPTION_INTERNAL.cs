namespace FMOD.Studio
{
	internal struct PARAMETER_DESCRIPTION_INTERNAL
	{
		public global::System.IntPtr name;

		public float minimum;

		public float maximum;

		public global::FMOD.Studio.PARAMETER_TYPE type;

		public void assign(out global::FMOD.Studio.PARAMETER_DESCRIPTION publicDesc)
		{
			publicDesc.name = global::FMOD.Studio.MarshallingHelper.stringFromNativeUtf8(name);
			publicDesc.minimum = minimum;
			publicDesc.maximum = maximum;
			publicDesc.type = type;
		}
	}
}
