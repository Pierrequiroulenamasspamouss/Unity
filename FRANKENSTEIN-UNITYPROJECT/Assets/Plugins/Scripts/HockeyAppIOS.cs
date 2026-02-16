public class HockeyAppIOS : global::UnityEngine.MonoBehaviour
{
	protected const string HOCKEYAPP_BASEURL = "https://rink.hockeyapp.net/";

	protected const string HOCKEYAPP_CRASHESPATH = "api/2/apps/[APPID]/crashes/upload";

	protected const int MAX_CHARS = 199800;

	protected const string LOG_FILE_DIR = "/logs/";

	public string appID = string.Empty;

	public string secret = string.Empty;

	public string authenticationType = string.Empty;

	public string serverURL = string.Empty;

	public bool autoUpload;

	public bool exceptionLogging;

	public bool updateManager;

	public string userId = string.Empty;

	public global::System.Action crashReportCallback;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
		global::UnityEngine.Application.RegisterLogCallback(null);
	}

	private void OnDestroy()
	{
		global::UnityEngine.Application.RegisterLogCallback(null);
	}

	private void GameViewLoaded(string message)
	{
	}

	protected virtual global::System.Collections.Generic.List<string> GetLogHeaders()
	{
		return new global::System.Collections.Generic.List<string>();
	}

	protected virtual global::UnityEngine.WWWForm CreateForm(string log)
	{
		return new global::UnityEngine.WWWForm();
	}

	protected virtual global::System.Collections.Generic.List<string> GetLogFiles()
	{
		return new global::System.Collections.Generic.List<string>();
	}

	protected virtual global::System.Collections.IEnumerator SendLogs(global::System.Collections.Generic.List<string> logs)
	{
		foreach (string log in logs)
		{
			string crashPath = "api/2/apps/[APPID]/crashes/upload";
			string url = GetBaseURL() + crashPath.Replace("[APPID]", appID);
			global::UnityEngine.WWWForm postForm = CreateForm(log);
			string lContent = postForm.headers["Content-Type"].ToString();
			lContent = lContent.Replace("\"", string.Empty);
			global::UnityEngine.WWW www = new global::UnityEngine.WWW(headers: new global::System.Collections.Generic.Dictionary<string, string> { { "Content-Type", lContent } }, url: url, postData: postForm.data);
			yield return www;
			if (string.IsNullOrEmpty(www.error))
			{
				try
				{
					global::System.IO.File.Delete(log);
				}
				catch (global::System.Exception ex)
				{
					global::System.Exception e = ex;
					if (global::UnityEngine.Debug.isDebugBuild)
					{
						global::UnityEngine.Debug.Log("Failed to delete exception log: " + e);
					}
				}
			}
			if (crashReportCallback != null)
			{
				crashReportCallback();
			}
		}
	}

	protected virtual void WriteLogToDisk(string logString, string stackTrace)
	{
	}

	protected virtual string GetBaseURL()
	{
		return string.Empty;
	}

	protected virtual bool IsConnected()
	{
		return false;
	}

	protected virtual void HandleException(string logString, string stackTrace)
	{
	}

	public void OnHandleLogCallback(string logString, string stackTrace, global::UnityEngine.LogType type)
	{
	}

	public void OnHandleUnresolvedException(object sender, global::System.UnhandledExceptionEventArgs args)
	{
	}
}
