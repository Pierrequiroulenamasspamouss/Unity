namespace UnityTest
{
	public interface AssertionComponentConfigurator
	{
		int UpdateCheckStartOnFrame { set; }

		int UpdateCheckRepeatFrequency { set; }

		bool UpdateCheckRepeat { set; }

		float TimeCheckStartAfter { set; }

		float TimeCheckRepeatFrequency { set; }

		bool TimeCheckRepeat { set; }

		global::UnityTest.AssertionComponent Component { get; }
	}
}
