namespace Facebook
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Assembly)]
	public class FBBuildVersionAttribute : global::System.Attribute
	{
		private global::System.DateTime buildDate;

		private string buildHash;

		private string buildVersion;

		private string sdkVersion;

		public global::System.DateTime Date
		{
			get
			{
				return buildDate;
			}
		}

		public string Hash
		{
			get
			{
				return buildHash;
			}
		}

		public string SdkVersion
		{
			get
			{
				return sdkVersion;
			}
		}

		public string BuildVersion
		{
			get
			{
				return buildVersion;
			}
		}

		public FBBuildVersionAttribute(string sdkVersion, string buildVersion)
		{
			this.buildVersion = buildVersion;
			string[] array = buildVersion.Split('.');
			buildDate = global::System.DateTime.ParseExact(array[0], "yyMMdd", global::System.Globalization.CultureInfo.InvariantCulture);
			buildHash = array[1];
			this.sdkVersion = sdkVersion;
		}

		public override string ToString()
		{
			return buildVersion;
		}

		public static global::Facebook.FBBuildVersionAttribute GetVersionAttributeOfType(global::System.Type type)
		{
			global::Facebook.FBBuildVersionAttribute[] attributes = getAttributes(type);
			int num = 0;
			if (num < attributes.Length)
			{
				return attributes[num];
			}
			return null;
		}

		private static global::Facebook.FBBuildVersionAttribute[] getAttributes(global::System.Type type)
		{
			if (type == null)
			{
				throw new global::System.ArgumentNullException("type");
			}
			global::System.Reflection.Assembly assembly = type.Assembly;
			return (global::Facebook.FBBuildVersionAttribute[])assembly.GetCustomAttributes(typeof(global::Facebook.FBBuildVersionAttribute), false);
		}
	}
}
