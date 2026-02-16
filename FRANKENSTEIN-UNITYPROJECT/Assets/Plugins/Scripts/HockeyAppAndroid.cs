using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class HockeyAppAndroid : MonoBehaviour
{
    protected const string HOCKEYAPP_BASEURL = "https://rink.hockeyapp.net/api/2/apps/";
    protected const string HOCKEYAPP_CRASHESPATH = "/crashes/upload";
    protected const int MAX_CHARS = 199800;
    protected const string LOG_FILE_DIR = "/logs/";

    public string appID = string.Empty;
    public string packageID = string.Empty;
    public bool exceptionLogging;
    public bool autoUpload;
    public bool updateManager;
    public string userId = string.Empty;
    public Action crashReportCallback;

    private void Awake()
    {
        DontDestroyOnLoad(base.gameObject);

        // FIX: Sur Windows, on ne veut pas envoyer de logs réels, 
        // mais on garde la logique de fichier active si nécessaire pour le debug.
#if UNITY_ANDROID && !UNITY_EDITOR
        if (exceptionLogging)
        {
            List<string> logFiles = GetLogFiles();
            if (logFiles.Count > 0)
            {
                StartCoroutine(SendLogs(GetLogFiles()));
            }
        }
#endif
        // On appelle le Mock manager au lieu du vrai code Android
        StartCrashManager(appID, updateManager, autoUpload);
    }

    private void OnEnable()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (exceptionLogging)
        {
            AppDomain.CurrentDomain.UnhandledException += OnHandleUnresolvedException;
            Application.RegisterLogCallback(OnHandleLogCallback);
        }
#endif
    }

    private void OnDisable()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.RegisterLogCallback(null);
#endif
    }

    private void OnDestroy()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.RegisterLogCallback(null);
#endif
    }

    protected void StartCrashManager(string appID, bool updateManagerEnabled, bool autoSendEnabled)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // CODE ORIGINAL (Android seulement)
        using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("net.hockeyapp.unity.HockeyUnityPlugin"))
                {
                    androidJavaClass2.CallStatic("startHockeyAppManager", appID, androidJavaObject, updateManagerEnabled, autoSendEnabled);
                }
            }
        }
#else
        // FIX WINDOWS: On ne fait rien pour éviter le crash JNI.
        Debug.Log("MOCK HOCKEYAPP: StartCrashManager called (Safe Mode)");
#endif
    }

    protected string GetVersion()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.hockeyapp.unity.HockeyUnityPlugin"))
        {
            return androidJavaClass.CallStatic<string>("getAppVersion", new object[0]);
        }
#else
        return "1.0.0"; // Version factice pour Windows
#endif
    }

    protected virtual List<string> GetLogHeaders()
    {
        List<string> list = new List<string>();
        list.Add("Package: " + packageID);
        string version = GetVersion();
        list.Add("Version: " + version);

        string os = SystemInfo.operatingSystem;
        list.Add("OS: " + os); // Version simplifiée pour Windows

        list.Add("Model: " + SystemInfo.deviceModel);
        list.Add("GPU vendor: " + SystemInfo.graphicsDeviceVendor);
        list.Add("GPU name: " + SystemInfo.graphicsDeviceName);
        list.Add("VRAM: " + SystemInfo.graphicsMemorySize);
        list.Add("RAM: " + SystemInfo.systemMemorySize);
        list.Add("Date: " + DateTime.UtcNow.ToString("ddd MMM dd HH:mm:ss {}zzzz yyyy").Replace("{}", "GMT"));
        return list;
    }

    protected virtual WWWForm CreateForm(string log)
    {
        WWWForm wWWForm = new WWWForm();
        byte[] array = null;

        // Code de lecture de fichier standard C# (marche sur Windows)
        try
        {
            using (FileStream fileStream = File.OpenRead(log))
            {
                if (fileStream.Length > 199800)
                {
                    string text = null;
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        streamReader.BaseStream.Seek(fileStream.Length - 199800, SeekOrigin.Begin);
                        text = streamReader.ReadToEnd();
                    }
                    List<string> logHeaders = GetLogHeaders();
                    string text2 = string.Empty;
                    foreach (string item in logHeaders)
                    {
                        text2 = text2 + item + "\n";
                    }
                    text = text2 + "\n[...]" + text;
                    array = Encoding.Default.GetBytes(text);
                }
                else
                {
                    array = File.ReadAllBytes(log);
                }
            }
        }
        catch (Exception ex)
        {
            if (Debug.isDebugBuild) Debug.Log("Failed to read log file: " + ex);
        }

        if (array != null)
        {
            wWWForm.AddBinaryData("log", array, log, "text/plain");
        }
        wWWForm.AddField("userID", userId);
        return wWWForm;
    }

    protected virtual List<string> GetLogFiles()
    {
        List<string> list = new List<string>();
        string path = Application.persistentDataPath + "/logs/";
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] files = directoryInfo.GetFiles();
            if (files.Length > 0)
            {
                foreach (FileInfo fileInfo in files)
                {
                    if (fileInfo.Extension == ".log")
                    {
                        list.Add(fileInfo.FullName);
                    }
                    else
                    {
                        File.Delete(fileInfo.FullName);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log("Failed to write exception log to file: " + ex);
            }
        }
        return list;
    }

    protected virtual IEnumerator SendLogs(List<string> logs)
    {
        foreach (string log in logs)
        {
            string url = "https://rink.hockeyapp.net/api/2/apps/" + appID + "/crashes/upload";
            WWWForm postForm = CreateForm(log);
            try
            {
                File.Delete(log);
            }
            catch (Exception ex)
            {
                if (Debug.isDebugBuild)
                {
                    Debug.Log("Failed to delete exception log: " + ex);
                }
            }

            // Logique d'envoi uniquement pour Android ou si nécessaire
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                string lContent = postForm.headers["Content-Type"].ToString();
                lContent = lContent.Replace("\"", string.Empty);
                yield return new WWW(url, postForm.data, new Dictionary<string, string> { { "Content-Type", lContent } });
            }

            if (crashReportCallback != null)
            {
                crashReportCallback();
            }
        }
    }

    protected virtual void WriteLogToDisk(string logString, string stackTrace)
    {
        // Safe C# file writing (Windows OK)
        try
        {
            string text = DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss_fff");
            string text2 = logString.Replace("\n", " ");
            string[] array = stackTrace.Split('\n');
            text2 = "\n" + text2 + "\n";
            foreach (string text3 in array)
            {
                if (text3.Length > 0)
                {
                    text2 = text2 + "  at " + text3 + "\n";
                }
            }
            List<string> logHeaders = GetLogHeaders();
            string path = Application.persistentDataPath + "/logs/";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            using (StreamWriter streamWriter = new StreamWriter(path + "LogFile_" + text + ".log", true))
            {
                foreach (string item in logHeaders)
                {
                    streamWriter.WriteLine(item);
                }
                streamWriter.WriteLine(text2);
            }
        }
        catch { }
    }

    protected virtual void HandleException(string logString, string stackTrace)
    {
        WriteLogToDisk(logString, stackTrace);
    }

    public void OnHandleLogCallback(string logString, string stackTrace, LogType type)
    {
        if ((type == LogType.Assert || type == LogType.Exception) && (type != LogType.Exception || !logString.StartsWith("FatalInvocationException")))
        {
            HandleException(logString, stackTrace);
            // Application.Quit(); // Désactivé pour éviter de fermer l'éditeur Unity
        }
    }

    public void OnHandleUnresolvedException(object sender, UnhandledExceptionEventArgs args)
    {
        if (args != null && args.ExceptionObject != null && args.ExceptionObject.GetType() == typeof(Exception))
        {
            Exception ex = (Exception)args.ExceptionObject;
            HandleException(ex.Source, ex.StackTrace);
            // Application.Quit(); // Désactivé pour éviter de fermer l'éditeur Unity
        }
    }
}