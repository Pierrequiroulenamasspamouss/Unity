namespace Facebook
{
	public class HttpMethod
	{
		private static global::Facebook.HttpMethod getMethod = new global::Facebook.HttpMethod("GET");

		private static global::Facebook.HttpMethod postMethod = new global::Facebook.HttpMethod("POST");

		private static global::Facebook.HttpMethod deleteMethod = new global::Facebook.HttpMethod("DELETE");

		private string methodValue;

		public static global::Facebook.HttpMethod GET
		{
			get
			{
				return getMethod;
			}
		}

		public static global::Facebook.HttpMethod POST
		{
			get
			{
				return postMethod;
			}
		}

		public static global::Facebook.HttpMethod DELETE
		{
			get
			{
				return deleteMethod;
			}
		}

		private HttpMethod(string value)
		{
			methodValue = value;
		}

		public override string ToString()
		{
			return methodValue;
		}
	}
}
