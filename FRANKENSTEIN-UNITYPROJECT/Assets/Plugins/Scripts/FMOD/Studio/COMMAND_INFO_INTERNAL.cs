namespace FMOD.Studio
{
	internal struct COMMAND_INFO_INTERNAL
	{
		public global::System.IntPtr commandName;

		public int parentCommandIndex;

		public int frameNumber;

		public float frameTime;

		public global::FMOD.Studio.INSTANCETYPE instanceType;

		public global::FMOD.Studio.INSTANCETYPE outputType;

		public uint instanceHandle;

		public uint outputHandle;

		public global::FMOD.Studio.COMMAND_INFO createPublic()
		{
			return new global::FMOD.Studio.COMMAND_INFO
			{
				commandName = global::FMOD.Studio.MarshallingHelper.stringFromNativeUtf8(commandName),
				parentCommandIndex = parentCommandIndex,
				frameNumber = frameNumber,
				frameTime = frameTime,
				instanceType = instanceType,
				outputType = outputType,
				instanceHandle = instanceHandle,
				outputHandle = outputHandle
			};
		}
	}
}
