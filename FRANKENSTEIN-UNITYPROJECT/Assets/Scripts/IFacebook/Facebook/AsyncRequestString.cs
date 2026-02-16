namespace Facebook
{
	public class AsyncRequestString : global::UnityEngine.MonoBehaviour
	{
		protected string url;

		protected global::Facebook.HttpMethod method;

		protected global::System.Collections.Generic.Dictionary<string, string> formData;

		protected global::UnityEngine.WWWForm query;

		protected global::Facebook.FacebookDelegate callback;

		internal static void Post(string url, global::System.Collections.Generic.Dictionary<string, string> formData = null, global::Facebook.FacebookDelegate callback = null)
		{
			Request(url, global::Facebook.HttpMethod.POST, formData, callback);
		}

		internal static void Get(string url, global::System.Collections.Generic.Dictionary<string, string> formData = null, global::Facebook.FacebookDelegate callback = null)
		{
			Request(url, global::Facebook.HttpMethod.GET, formData, callback);
		}

		internal static void Request(string url, global::Facebook.HttpMethod method, global::UnityEngine.WWWForm query = null, global::Facebook.FacebookDelegate callback = null)
		{
			global::Facebook.FBComponentFactory.AddComponent<global::Facebook.AsyncRequestString>().SetUrl(url).SetMethod(method)
				.SetQuery(query)
				.SetCallback(callback);
		}

		internal static void Request(string url, global::Facebook.HttpMethod method, global::System.Collections.Generic.Dictionary<string, string> formData = null, global::Facebook.FacebookDelegate callback = null)
		{
			global::Facebook.FBComponentFactory.AddComponent<global::Facebook.AsyncRequestString>().SetUrl(url).SetMethod(method)
				.SetFormData(formData)
				.SetCallback(callback);
		}

		private global::System.Collections.IEnumerator Start()
		{
			global::UnityEngine.WWW www;
			if (method == global::Facebook.HttpMethod.GET)
			{
				string urlParams = ((!url.Contains("?")) ? "?" : "&");
				if (formData != null)
				{
					foreach (global::System.Collections.Generic.KeyValuePair<string, string> pair in formData)
					{
						urlParams += string.Format("{0}={1}&", global::System.Uri.EscapeDataString(pair.Key), global::System.Uri.EscapeDataString(pair.Value));
					}
				}
				www = new global::UnityEngine.WWW(url + urlParams);
			}
			else
			{
				if (query == null)
				{
					query = new global::UnityEngine.WWWForm();
				}
				if (method == global::Facebook.HttpMethod.DELETE)
				{
					query.AddField("method", "delete");
				}
				if (formData != null)
				{
					foreach (global::System.Collections.Generic.KeyValuePair<string, string> pair2 in formData)
					{
						query.AddField(pair2.Key, pair2.Value);
					}
				}
				www = new global::UnityEngine.WWW(url, query);
			}
			yield return www;
			if (callback != null)
			{
				callback(new FBResult(www));
			}
			www.Dispose();
			global::UnityEngine.Object.Destroy(this);
		}

		internal global::Facebook.AsyncRequestString SetUrl(string url)
		{
			this.url = url;
			return this;
		}

		internal global::Facebook.AsyncRequestString SetMethod(global::Facebook.HttpMethod method)
		{
			this.method = method;
			return this;
		}

		internal global::Facebook.AsyncRequestString SetFormData(global::System.Collections.Generic.Dictionary<string, string> formData)
		{
			this.formData = formData;
			return this;
		}

		internal global::Facebook.AsyncRequestString SetQuery(global::UnityEngine.WWWForm query)
		{
			this.query = query;
			return this;
		}

		internal global::Facebook.AsyncRequestString SetCallback(global::Facebook.FacebookDelegate callback)
		{
			this.callback = callback;
			return this;
		}
	}
}
