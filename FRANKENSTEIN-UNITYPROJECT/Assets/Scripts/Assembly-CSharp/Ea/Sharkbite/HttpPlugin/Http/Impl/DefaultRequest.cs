namespace Ea.Sharkbite.HttpPlugin.Http.Impl
{
	public class DefaultRequest : global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest
	{
		private const int BLOCKSIZE = 8192;

		public const int TIMEOUT = 30000;

		private static readonly string SCHEME_HTTP = "http";

		private static readonly string SCHEME_HTTPS = "https";

		public static readonly string METHOD_GET = "GET";

		public static readonly string METHOD_HEAD = "GET";

		public static readonly string METHOD_OPTIONS = "GET";

		public static readonly string METHOD_POST = "POST";

		public static readonly string METHOD_PUT = "PUT";

		public static readonly string METHOD_DELETE = "DELETE";

		protected bool abort;

		public string Uri { get; set; }

		public string Method { get; set; }

		public byte[] Body { get; set; }

		public string Accept { get; set; }

		public string ContentType { get; set; }

		public string Username { get; set; }

		public string Password { private get; set; }

		public global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, string>> QueryParams { get; set; }

		public global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, string>> Headers { get; set; }

		public global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, string>> FormParams { get; set; }

		protected int Range { get; set; }

		public bool CanRetry { get; set; }

		public int RetryCount { get; set; }

		public global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> ResponseSignal { get; set; }

		public global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress, global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> NotifyProgress { get; set; }

		public DefaultRequest(string uri)
		{
			if (string.IsNullOrEmpty(uri))
			{
				throw new global::System.ArgumentNullException();
			}
			global::System.Uri uri2 = new global::System.Uri(uri);
			if (!uri2.Scheme.ToLower().Equals(SCHEME_HTTP) && !uri2.Scheme.ToLower().Equals(SCHEME_HTTPS))
			{
				throw new global::System.ArgumentException("Only HTTP and HTTPS schemes supported");
			}
			if (!string.IsNullOrEmpty(uri2.Query))
			{
				throw new global::System.ArgumentException("Query parameters should be set using the WithQueryParam method, rather than set directly in the Uri");
			}
			Uri = uri;
			Method = METHOD_GET;
			Body = null;
			Accept = string.Empty;
			ContentType = string.Empty;
			QueryParams = new global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, string>>();
			Headers = new global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, string>>();
			FormParams = new global::System.Collections.Generic.List<global::System.Collections.Generic.KeyValuePair<string, string>>();
			Range = 0;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Get()
		{
			Method = METHOD_GET;
			return GetResponse();
		}

		public void Get(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback)
		{
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				callback(Get());
			});
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Head()
		{
			Method = METHOD_HEAD;
			return GetResponse();
		}

		public void Head(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback)
		{
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				callback(Head());
			});
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Options()
		{
			Method = METHOD_OPTIONS;
			return GetResponse();
		}

		public void Options(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback)
		{
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				callback(Options());
			});
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Post()
		{
			Method = METHOD_POST;
			return GetResponse();
		}

		public void Post(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback)
		{
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				callback(Post());
			});
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Put()
		{
			Method = METHOD_PUT;
			return GetResponse();
		}

		public void Put(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback)
		{
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				callback(Put());
			});
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Delete()
		{
			Method = METHOD_DELETE;
			return GetResponse();
		}

		public void Delete(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback)
		{
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				callback(Delete());
			});
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse Execute()
		{
			return GetResponse();
		}

		public void Execute(global::System.Action<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> callback)
		{
			global::System.Threading.ThreadPool.QueueUserWorkItem(delegate
			{
				callback(Execute());
			});
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithContentType(string contentType)
		{
			ContentType = contentType;
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithAccept(string accept)
		{
			Accept = accept;
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithQueryParam(string key, string value)
		{
			QueryParams.Add(new global::System.Collections.Generic.KeyValuePair<string, string>(key, value));
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithHeaderParam(string key, string value)
		{
			Headers.Add(new global::System.Collections.Generic.KeyValuePair<string, string>(key, value));
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithFormParam(string key, string value)
		{
			FormParams.Add(new global::System.Collections.Generic.KeyValuePair<string, string>(key, value));
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithBasicAuth(string username, string password)
		{
			Username = username;
			Password = password;
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithBody(byte[] body)
		{
			Body = body;
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithPreprocessor(global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequestPreprocessor preprocessor)
		{
			preprocessor.preprocess(this);
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithMethod(string method)
		{
			Method = method;
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithResponseSignal(global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse> responseSignal)
		{
			ResponseSignal = responseSignal;
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithNotifyProgress(global::strange.extensions.signal.impl.Signal<global::Ea.Sharkbite.HttpPlugin.Http.Api.DownloadProgress, global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest> notify)
		{
			NotifyProgress = notify;
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithRetry(bool retry = true, int times = 3)
		{
			CanRetry = retry;
			RetryCount = times;
			return this;
		}

		public global::Ea.Sharkbite.HttpPlugin.Http.Api.IRequest WithEntity(object entity)
		{
			Body = global::Kampai.Util.FastJSONSerializer.SerializeUTF8(entity);
			return this;
		}

		public virtual void Abort()
		{
			abort = true;
		}

		public virtual bool IsAborted()
		{
			return abort;
		}

		protected virtual global::System.Net.HttpWebRequest CreateRequest()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			if (QueryParams.Count > 0)
			{
				stringBuilder.Append("?");
				global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> queryParam in QueryParams)
				{
					list.Add(string.Format("{0}={1}", global::UnityEngine.WWW.EscapeURL(queryParam.Key), global::UnityEngine.WWW.EscapeURL(queryParam.Value)));
				}
				stringBuilder.Append(string.Join("&", list.ToArray()));
			}
			string uriString = Uri + stringBuilder.ToString();
			global::System.Net.HttpWebRequest httpWebRequest = null;
			try
			{
				httpWebRequest = global::System.Net.WebRequest.Create(new global::System.Uri(uriString)) as global::System.Net.HttpWebRequest;
				httpWebRequest.Timeout = 30000;
				httpWebRequest.ReadWriteTimeout = 30000;
				if (global::Ea.Sharkbite.HttpPlugin.Http.Api.ConnectionSettings.ConnectionLimit != 0)
				{
					httpWebRequest.ServicePoint.ConnectionLimit = global::Ea.Sharkbite.HttpPlugin.Http.Api.ConnectionSettings.ConnectionLimit;
				}
				if (string.IsNullOrEmpty(Method))
				{
					throw new global::System.InvalidOperationException("A request Method (GET, POST, PUT, DELETE) must be provided.");
				}
				httpWebRequest.Method = Method;
				if (!string.IsNullOrEmpty(Accept))
				{
					httpWebRequest.Accept = Accept;
				}
				if (!string.IsNullOrEmpty(ContentType))
				{
					httpWebRequest.ContentType = ContentType;
				}
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> header in Headers)
				{
					httpWebRequest.Headers.Add(header.Key, header.Value);
				}
				if (Range != 0)
				{
					httpWebRequest.AddRange(Range);
				}
				if (!string.IsNullOrEmpty(Username))
				{
					httpWebRequest.Headers.Add(global::System.Net.HttpRequestHeader.Authorization, string.Format("Basic {0}", global::System.Convert.ToBase64String(global::System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", Username, Password)))));
				}
				int num = 0;
				if (Body != null)
				{
					num++;
				}
				if (FormParams.Count > 0)
				{
					num++;
				}
				if (num > 1)
				{
					throw new global::System.InvalidOperationException("Request must contain only form params, or a body, or an entity, without combination.");
				}
				if (FormParams.Count > 0)
				{
					if (string.IsNullOrEmpty(ContentType))
					{
						ContentType = "application/x-www-form-urlencoded";
						httpWebRequest.MediaType = "application/x-www-form-urlencoded";
					}
					global::System.Collections.Generic.List<string> list2 = new global::System.Collections.Generic.List<string>();
					foreach (global::System.Collections.Generic.KeyValuePair<string, string> formParam in FormParams)
					{
						list2.Add(string.Format("{0}={1}", global::UnityEngine.WWW.EscapeURL(formParam.Key), global::UnityEngine.WWW.EscapeURL(formParam.Value)));
					}
					Body = global::System.Text.Encoding.UTF8.GetBytes(string.Join("&", list2.ToArray()));
				}
				global::System.Net.ServicePointManager.ServerCertificateValidationCallback = (global::System.Net.Security.RemoteCertificateValidationCallback)global::System.Delegate.Combine(global::System.Net.ServicePointManager.ServerCertificateValidationCallback, new global::System.Net.Security.RemoteCertificateValidationCallback(CertificateValidationCallback));
				byte[] body = Body;
				if (body != null)
				{
					httpWebRequest.ContentLength = body.Length;
					global::System.IO.Stream requestStream = httpWebRequest.GetRequestStream();
					requestStream.Write(body, 0, body.Length);
					requestStream.Close();
				}
				if ((Method.Equals("DELETE") || Method.Equals("GET")) && Body != null)
				{
					throw new global::System.Net.ProtocolViolationException();
				}
			}
			catch (global::System.Net.WebException ex)
			{
				global::System.Net.ServicePointManager.ServerCertificateValidationCallback = (global::System.Net.Security.RemoteCertificateValidationCallback)global::System.Delegate.Remove(global::System.Net.ServicePointManager.ServerCertificateValidationCallback, new global::System.Net.Security.RemoteCertificateValidationCallback(CertificateValidationCallback));
				global::Kampai.Util.Native.LogError(ex.Message);
			}
			catch (global::System.Exception ex2)
			{
				global::Kampai.Util.Native.LogError(ex2.Message);
			}
			return httpWebRequest;
		}

		protected virtual global::System.Net.HttpWebResponse ExecuteRequest()
		{
			global::System.Net.HttpWebResponse result = null;
			global::System.Net.HttpWebRequest httpWebRequest = CreateRequest();
			if (httpWebRequest == null)
			{
				global::Kampai.Util.Native.LogError(string.Format("Null request for Uri {0}", Uri));
				return null;
			}
			try
			{
				result = httpWebRequest.GetResponse() as global::System.Net.HttpWebResponse;
			}
			catch (global::System.Net.WebException ex)
			{
				global::Kampai.Util.Native.LogWarning(string.Format("WebException Downloading {0}: {1}", Uri, ex.Message));
				if (ex.Response != null)
				{
					result = ex.Response as global::System.Net.HttpWebResponse;
				}
				else
				{
					global::System.Net.ServicePointManager.ServerCertificateValidationCallback = (global::System.Net.Security.RemoteCertificateValidationCallback)global::System.Delegate.Remove(global::System.Net.ServicePointManager.ServerCertificateValidationCallback, new global::System.Net.Security.RemoteCertificateValidationCallback(CertificateValidationCallback));
					global::Kampai.Util.Native.LogError(string.Format("WebException Response is NULL: {0}", ex.Message));
				}
			}
			catch (global::System.Exception ex2)
			{
				global::Kampai.Util.Native.LogError(string.Format("Exception Downloading {0}: {1}", Uri, ex2.Message));
			}
			return result;
		}

		protected virtual global::System.Collections.Generic.Dictionary<string, string> ProcessResponse(global::System.Net.HttpWebResponse response)
		{
			return ProcessResponse(response, string.Empty);
		}

		protected virtual global::System.Collections.Generic.Dictionary<string, string> ProcessResponse(global::System.Net.HttpWebResponse response, string body)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (response != null && response.Headers != null)
			{
				string[] allKeys = response.Headers.AllKeys;
				foreach (string text in allKeys)
				{
					dictionary.Add(text, response.Headers.Get(text));
				}
			}
			global::System.Net.ServicePointManager.ServerCertificateValidationCallback = (global::System.Net.Security.RemoteCertificateValidationCallback)global::System.Delegate.Remove(global::System.Net.ServicePointManager.ServerCertificateValidationCallback, new global::System.Net.Security.RemoteCertificateValidationCallback(CertificateValidationCallback));
			return dictionary;
		}

		protected virtual string ReadResponse(global::System.Net.HttpWebResponse response)
		{
			string result = string.Empty;
			try
			{
				if (response.GetResponseStream() != null)
				{
					global::System.Text.Encoding encoding;
					try
					{
						encoding = global::System.Text.Encoding.GetEncoding(response.CharacterSet);
					}
					catch (global::System.ArgumentException)
					{
						encoding = global::System.Text.Encoding.UTF8;
					}
					byte[] array = new byte[8192];
					int num = 0;
					using (global::System.IO.Stream stream = response.GetResponseStream())
					{
						for (int num2 = stream.Read(array, 0, array.Length); num2 > 0; num2 = stream.Read(array, num, array.Length - num))
						{
							num += num2;
							if (num == array.Length)
							{
								global::System.Array.Resize(ref array, array.Length + 8192);
							}
						}
					}
					if (num > 0)
					{
						result = encoding.GetString(array, 0, num);
					}
				}
			}
			catch (global::System.Exception ex2)
			{
				global::Kampai.Util.Native.LogError(ex2.Message);
			}
			return result;
		}

		protected virtual global::Ea.Sharkbite.HttpPlugin.Http.Api.IResponse GetResponse()
		{
			if (abort)
			{
				return null;
			}
			global::System.DateTime now = global::System.DateTime.Now;
			using (global::System.Net.HttpWebResponse httpWebResponse = ExecuteRequest())
			{
				if (httpWebResponse != null)
				{
					string body = ReadResponse(httpWebResponse);
					global::System.Collections.Generic.Dictionary<string, string> headers = ProcessResponse(httpWebResponse, body);
					int downloadTime = global::UnityEngine.Mathf.RoundToInt((float)(global::System.DateTime.Now - now).TotalMilliseconds);
					return new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultResponse().WithBody(body).WithCode((int)httpWebResponse.StatusCode).WithRequest(this)
						.WithContentLength(httpWebResponse.ContentLength)
						.WithDownloadTime(downloadTime)
						.WithContentType(httpWebResponse.ContentType)
						.WithHeaders(headers);
				}
				global::Kampai.Util.Native.LogError("Null response for " + Uri);
				return new global::Ea.Sharkbite.HttpPlugin.Http.Impl.DefaultResponse().WithCode(500).WithRequest(this).WithConnectionLoss(true);
			}
		}

		protected static bool CertificateValidationCallback(object sender, global::System.Security.Cryptography.X509Certificates.X509Certificate certificate, global::System.Security.Cryptography.X509Certificates.X509Chain chain, global::System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}
	}
}
