namespace Prime31
{
	public class P31RestKit
	{
		protected string _baseUrl;

		public bool debugRequests = false;

		protected bool forceJsonResponse;

		private global::UnityEngine.GameObject _surrogateGameObject;

		private global::UnityEngine.MonoBehaviour _surrogateMonobehaviour;

		protected virtual global::UnityEngine.GameObject surrogateGameObject
		{
			get
			{
				if (_surrogateGameObject == null)
				{
					_surrogateGameObject = global::UnityEngine.GameObject.Find("P31CoroutineSurrogate");
					if (_surrogateGameObject == null)
					{
						_surrogateGameObject = new global::UnityEngine.GameObject("P31CoroutineSurrogate");
						global::UnityEngine.Object.DontDestroyOnLoad(_surrogateGameObject);
					}
				}
				return _surrogateGameObject;
			}
			set
			{
				_surrogateGameObject = value;
			}
		}

		protected global::UnityEngine.MonoBehaviour surrogateMonobehaviour
		{
			get
			{
				if (_surrogateMonobehaviour == null)
				{
					_surrogateMonobehaviour = surrogateGameObject.AddComponent<global::UnityEngine.MonoBehaviour>();
				}
				return _surrogateMonobehaviour;
			}
			set
			{
				_surrogateMonobehaviour = value;
			}
		}

		protected virtual global::System.Collections.IEnumerator send(string path, global::Prime31.HTTPVerb httpVerb, global::System.Collections.Generic.Dictionary<string, object> parameters, global::System.Action<string, object> onComplete)
		{
			if (path.StartsWith("/"))
			{
				path = path.Substring(1);
			}
			global::UnityEngine.WWW www = processRequest(path, httpVerb, parameters);
			yield return www;
			if (debugRequests)
			{
				global::UnityEngine.Debug.Log("response error: " + www.error);
				global::UnityEngine.Debug.Log("response text: " + www.text);
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
				stringBuilder.Append("Response Headers:\n");
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> responseHeader in www.responseHeaders)
				{
					stringBuilder.AppendFormat("{0}: {1}\n", responseHeader.Key, responseHeader.Value);
				}
				global::UnityEngine.Debug.Log(stringBuilder.ToString());
			}
			if (onComplete != null)
			{
				processResponse(www, onComplete);
			}
			www.Dispose();
		}

		protected virtual global::UnityEngine.WWW processRequest(string path, global::Prime31.HTTPVerb httpVerb, global::System.Collections.Generic.Dictionary<string, object> parameters)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			if (!path.StartsWith("http"))
			{
				stringBuilder.Append(_baseUrl).Append(path);
			}
			else
			{
				stringBuilder.Append(path);
			}
			bool flag = httpVerb != global::Prime31.HTTPVerb.GET;
			global::UnityEngine.WWWForm wWWForm = ((!flag) ? null : new global::UnityEngine.WWWForm());
			if (parameters != null && parameters.Count > 0)
			{
				if (flag)
				{
					foreach (global::System.Collections.Generic.KeyValuePair<string, object> parameter in parameters)
					{
						if (parameter.Value is string)
						{
							wWWForm.AddField(parameter.Key, parameter.Value as string);
						}
						else if (parameter.Value is byte[])
						{
							wWWForm.AddBinaryData(parameter.Key, parameter.Value as byte[]);
						}
					}
				}
				else
				{
					bool flag2 = true;
					if (path.Contains("?"))
					{
						flag2 = false;
					}
					foreach (global::System.Collections.Generic.KeyValuePair<string, object> parameter2 in parameters)
					{
						if (parameter2.Value is string)
						{
							stringBuilder.AppendFormat("{0}{1}={2}", (!flag2) ? "&" : "?", global::UnityEngine.WWW.EscapeURL(parameter2.Key), global::UnityEngine.WWW.EscapeURL(parameter2.Value as string));
							flag2 = false;
						}
					}
				}
			}
			if (debugRequests)
			{
				global::UnityEngine.Debug.Log("url: " + stringBuilder.ToString());
			}
			return (!flag) ? new global::UnityEngine.WWW(stringBuilder.ToString()) : new global::UnityEngine.WWW(stringBuilder.ToString(), wWWForm);
		}

		protected virtual global::System.Collections.Hashtable headersForRequest(global::Prime31.HTTPVerb httpVerb, global::System.Collections.Generic.Dictionary<string, object> parameters)
		{
			if (httpVerb == global::Prime31.HTTPVerb.PUT)
			{
				global::System.Collections.Hashtable hashtable = new global::System.Collections.Hashtable();
				hashtable.Add("X-HTTP-Method-Override", "PUT");
				return hashtable;
			}
			return null;
		}

		protected virtual void processResponse(global::UnityEngine.WWW www, global::System.Action<string, object> onComplete)
		{
			if (!string.IsNullOrEmpty(www.error))
			{
				onComplete(www.error, null);
			}
			else if (isResponseJson(www))
			{
				object obj = global::Prime31.Json.decode(www.text);
				if (obj == null)
				{
					obj = www.text;
				}
				onComplete(null, obj);
			}
			else
			{
				onComplete(null, www.text);
			}
		}

		protected bool isResponseJson(global::UnityEngine.WWW www)
		{
			bool flag = false;
			if (forceJsonResponse)
			{
				flag = true;
			}
			if (!flag)
			{
				foreach (global::System.Collections.Generic.KeyValuePair<string, string> responseHeader in www.responseHeaders)
				{
					if (responseHeader.Key.ToLower() == "content-type" && (responseHeader.Value.ToLower().Contains("/json") || responseHeader.Value.ToLower().Contains("/javascript")))
					{
						flag = true;
					}
				}
			}
			if (flag && !www.text.StartsWith("[") && !www.text.StartsWith("{"))
			{
				return false;
			}
			return flag;
		}

		public void get(string path, global::System.Action<string, object> completionHandler)
		{
			get(path, null, completionHandler);
		}

		public void get(string path, global::System.Collections.Generic.Dictionary<string, object> parameters, global::System.Action<string, object> completionHandler)
		{
			surrogateMonobehaviour.StartCoroutine(send(path, global::Prime31.HTTPVerb.GET, parameters, completionHandler));
		}

		public void post(string path, global::System.Action<string, object> completionHandler)
		{
			post(path, null, completionHandler);
		}

		public void post(string path, global::System.Collections.Generic.Dictionary<string, object> parameters, global::System.Action<string, object> completionHandler)
		{
			surrogateMonobehaviour.StartCoroutine(send(path, global::Prime31.HTTPVerb.POST, parameters, completionHandler));
		}

		public void put(string path, global::System.Action<string, object> completionHandler)
		{
			put(path, null, completionHandler);
		}

		public void put(string path, global::System.Collections.Generic.Dictionary<string, object> parameters, global::System.Action<string, object> completionHandler)
		{
			surrogateMonobehaviour.StartCoroutine(send(path, global::Prime31.HTTPVerb.PUT, parameters, completionHandler));
		}
	}
}
