namespace FMOD.Studio
{
	internal struct USER_PROPERTY_INTERNAL
	{
		private global::System.IntPtr name;

		private global::FMOD.Studio.USER_PROPERTY_TYPE type;

		private global::FMOD.Studio.Union_IntBoolFloatString value;

		public global::FMOD.Studio.USER_PROPERTY createPublic()
		{
			global::FMOD.Studio.USER_PROPERTY result = new global::FMOD.Studio.USER_PROPERTY
			{
				name = global::FMOD.Studio.MarshallingHelper.stringFromNativeUtf8(name),
				type = type
			};
			switch (type)
			{
			case global::FMOD.Studio.USER_PROPERTY_TYPE.INTEGER:
				result.intValue = value.intValue;
				break;
			case global::FMOD.Studio.USER_PROPERTY_TYPE.BOOLEAN:
				result.boolValue = value.boolValue;
				break;
			case global::FMOD.Studio.USER_PROPERTY_TYPE.FLOAT:
				result.floatValue = value.floatValue;
				break;
			case global::FMOD.Studio.USER_PROPERTY_TYPE.STRING:
				result.stringValue = global::FMOD.Studio.MarshallingHelper.stringFromNativeUtf8(value.stringValue);
				break;
			}
			return result;
		}
	}
}
