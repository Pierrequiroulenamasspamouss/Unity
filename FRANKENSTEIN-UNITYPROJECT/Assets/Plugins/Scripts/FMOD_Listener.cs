public class FMOD_Listener : global::UnityEngine.MonoBehaviour
{
	public string[] pluginPaths = new string[0];

	private static FMOD_Listener sListener;

	private global::UnityEngine.Rigidbody cachedRigidBody;

	private string pluginPath
	{
		get
		{
			if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.WindowsEditor)
			{
				return global::UnityEngine.Application.dataPath + "/Plugins/x86";
			}
			if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.WindowsPlayer || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.OSXEditor || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.OSXPlayer || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.OSXDashboardPlayer || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.LinuxPlayer)
			{
				return global::UnityEngine.Application.dataPath + "/Plugins";
			}
			if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.IPhonePlayer)
			{
				global::FMOD.Studio.UnityUtil.LogError("DSP Plugins not currently supported on iOS, contact support@fmod.org for more information");
				return string.Empty;
			}
			if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
			{
				global::System.IO.DirectoryInfo directoryInfo = new global::System.IO.DirectoryInfo(global::UnityEngine.Application.persistentDataPath);
				string text = directoryInfo.Parent.Name;
				return "/data/data/" + text + "/lib";
			}
			global::FMOD.Studio.UnityUtil.LogError("Unknown platform!");
			return string.Empty;
		}
	}

	private void OnEnable()
	{
		Initialize();
	}

	private void OnDisable()
	{
		if (sListener == this)
		{
			sListener = null;
		}
	}

	private void loadBank(string fileName)
	{
		string streamingAsset = getStreamingAsset(fileName);
		global::FMOD.Studio.Bank bank = null;
		global::FMOD.RESULT rESULT = FMOD_StudioSystem.instance.System.loadBankFile(streamingAsset, global::FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out bank);
		switch (rESULT)
		{
		case global::FMOD.RESULT.ERR_VERSION:
			global::FMOD.Studio.UnityUtil.LogError(string.Format("Bank {0} built with an incompatible version of FMOD Studio.", fileName));
			break;
		default:
			global::FMOD.Studio.UnityUtil.LogError(string.Format("Bank {0} encountered a loading error {1}", fileName, rESULT.ToString()));
			break;
		case global::FMOD.RESULT.OK:
			break;
		}
		global::FMOD.Studio.UnityUtil.Log("bank load: " + ((!(bank != null)) ? "failed!!" : "suceeded"));
	}

	private string getStreamingAsset(string fileName)
	{
		string empty = string.Empty;
		empty = ((global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android) ? ("jar:file://" + global::UnityEngine.Application.dataPath + "!/assets") : ((global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.MetroPlayerARM && global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.MetroPlayerX86 && global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.MetroPlayerX64) ? global::UnityEngine.Application.streamingAssetsPath : "ms-appx:///Data/StreamingAssets"));
		string url = empty + "/" + fileName;
		string text = global::UnityEngine.Application.persistentDataPath + "/" + fileName;
		global::FMOD.Studio.UnityUtil.Log("Unpacking bank from JAR file into:" + text);
		if (global::System.IO.File.Exists(text))
		{
			global::FMOD.Studio.UnityUtil.Log("File already unpacked!!");
			global::System.IO.File.Delete(text);
			if (global::System.IO.File.Exists(text))
			{
				global::FMOD.Studio.UnityUtil.Log("Could NOT delete!!");
			}
		}
		global::UnityEngine.WWW wWW = new global::UnityEngine.WWW(url);
		while (!wWW.isDone)
		{
		}
		if (!string.IsNullOrEmpty(wWW.error))
		{
			global::FMOD.Studio.UnityUtil.LogError("### WWW ERROR IN DATA STREAM:" + wWW.error);
		}
		global::FMOD.Studio.UnityUtil.Log("Android unpacked jar path: " + text);
		global::System.IO.File.WriteAllBytes(text, wWW.bytes);
		return text;
	}

	private void Initialize()
	{
		global::FMOD.Studio.UnityUtil.Log("Initialize Listener");
		if (sListener != null)
		{
			global::FMOD.Studio.UnityUtil.LogError("Too many listeners");
		}
		sListener = this;
		cachedRigidBody = GetComponent<global::UnityEngine.Rigidbody>();
		Update3DAttributes();
	}

	private void LoadBanks()
	{
		string streamingAsset = getStreamingAsset("FMOD_bank_list.txt");
		global::FMOD.Studio.UnityUtil.Log("Loading Banks");
		try
		{
			string[] array = global::System.IO.File.ReadAllLines(streamingAsset);
			string[] array2 = array;
			foreach (string text in array2)
			{
				global::FMOD.Studio.UnityUtil.Log("Load " + text);
				loadBank(text);
			}
		}
		catch (global::System.Exception ex)
		{
			global::FMOD.Studio.UnityUtil.LogError("Cannot read " + streamingAsset + ": " + ex.Message + " : No banks loaded.");
		}
		cachedRigidBody = GetComponent<global::UnityEngine.Rigidbody>();
		Update3DAttributes();
	}

	private void Start()
	{
	}

	private void Update()
	{
		Update3DAttributes();
	}

	private void Update3DAttributes()
	{
		global::FMOD.Studio.System system = FMOD_StudioSystem.instance.System;
		if (system != null && system.isValid())
		{
			global::FMOD.Studio.ATTRIBUTES_3D attributes = global::FMOD.Studio.UnityUtil.to3DAttributes(base.gameObject, cachedRigidBody);
			ERRCHECK(system.setListenerAttributes(0, attributes));
		}
	}

	private void LoadPlugins()
	{
		global::FMOD.System system = null;
		ERRCHECK(FMOD_StudioSystem.instance.System.getLowLevelSystem(out system));
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.IPhonePlayer && pluginPaths.Length != 0)
		{
			global::FMOD.Studio.UnityUtil.LogError("DSP Plugins not currently supported on iOS, contact support@fmod.org for more information");
			return;
		}
		string[] array = pluginPaths;
		foreach (string rawName in array)
		{
			string text = pluginPath + "/" + GetPluginFileName(rawName);
			global::FMOD.Studio.UnityUtil.Log("Loading plugin: " + text);
			if (!global::System.IO.File.Exists(text))
			{
				global::FMOD.Studio.UnityUtil.LogWarning("plugin not found: " + text);
			}
			uint handle;
			ERRCHECK(system.loadPlugin(text, out handle));
		}
	}

	private string GetPluginFileName(string rawName)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.WindowsEditor || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.WindowsPlayer)
		{
			return rawName + ".dll";
		}
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.OSXEditor || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.OSXPlayer || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.OSXDashboardPlayer)
		{
			return rawName + ".dylib";
		}
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android || global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.LinuxPlayer)
		{
			return "lib" + rawName + ".so";
		}
		global::FMOD.Studio.UnityUtil.LogError("Unknown platform!");
		return string.Empty;
	}

	private void ERRCHECK(global::FMOD.RESULT result)
	{
		global::FMOD.Studio.UnityUtil.ERRCHECK(result);
	}
}
