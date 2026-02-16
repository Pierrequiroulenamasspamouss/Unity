namespace Kampai.Common.Service.Audio
{
	public class FMODService : global::Kampai.Common.Service.Audio.IFMODService
	{
		private sealed class InitializeSystem_003Ec__Iterator23 : global::System.IDisposable, global::System.Collections.IEnumerator, global::System.Collections.Generic.IEnumerator<object>
		{
			internal int _003Cid_003E__0;

			internal int _0024PC;

			internal object _0024current;

			internal global::Kampai.Common.Service.Audio.FMODService _003C_003Ef__this;

			object global::System.Collections.Generic.IEnumerator<object>.Current
			{
				[global::System.Diagnostics.DebuggerHidden]
				get
				{
					return _0024current;
				}
			}

			object global::System.Collections.IEnumerator.Current
			{
				[global::System.Diagnostics.DebuggerHidden]
				get
				{
					return _0024current;
				}
			}

			public bool MoveNext()
			{
				uint num = (uint)_0024PC;
				_0024PC = -1;
				switch (num)
				{
				case 0u:
					_003C_003Ef__this.logger.EventStart("FMODService.InitializeSystem");
					global::Kampai.Util.TimeProfiler.StartSection("fmod");
					_003Cid_003E__0 = _003C_003Ef__this.couroutinProgressMonitor.StartTask("fmod");
					global::Kampai.Util.TimeProfiler.StartSection("bundles");
					_0024current = _003C_003Ef__this.routineRunner.StartCoroutine(_003C_003Ef__this.LoadAllFromAssetBundles());
					_0024PC = 1;
					break;
				case 1u:
					global::Kampai.Util.TimeProfiler.EndSection("bundles");
					global::Kampai.Util.TimeProfiler.StartSection("raw");
					_0024current = _003C_003Ef__this.routineRunner.StartCoroutine(_003C_003Ef__this.LoadRawAudioBanks());
					_0024PC = 2;
					break;
				case 2u:
					global::Kampai.Util.TimeProfiler.EndSection("raw");
					global::Kampai.Util.TimeProfiler.StartSection("streaming");
					_0024current = _003C_003Ef__this.routineRunner.StartCoroutine(_003C_003Ef__this.LoadStreamingAudioBanks());
					_0024PC = 3;
					break;
				case 3u:
					global::Kampai.Util.TimeProfiler.EndSection("streaming");
					_003C_003Ef__this.couroutinProgressMonitor.FinishTask(_003Cid_003E__0);
					global::Kampai.Util.TimeProfiler.EndSection("fmod");
					_003C_003Ef__this.logger.EventStop("FMODService.InitializeSystem");
					_0024PC = -1;
					goto default;
				default:
					return false;
				}
				return true;
			}

			[global::System.Diagnostics.DebuggerHidden]
			public void Dispose()
			{
				_0024PC = -1;
			}

			[global::System.Diagnostics.DebuggerHidden]
			public void Reset()
			{
				throw new global::System.NotSupportedException();
			}
		}

		private const string SHARED_AUDIO_CONTENT_LOCATION = "Content/Shared/";

		private const string LOCAL_AUDIO_CONTENT_LOCATION = "Content/Resources/Local/";

		private const string DLC_AUDIO_CONTENT_LOCATION = "Content/DLC/";

		private const string RESOURCES_AUDIO_CONTENT_LOCATION = "Content/Resources/";

		private readonly global::Kampai.Common.IManifestService _manifestService;

		private readonly global::System.Collections.Generic.Dictionary<string, string> _nameIdMap = new global::System.Collections.Generic.Dictionary<string, string>();

		public static readonly global::System.Collections.Generic.Dictionary<string, string> RawAudioBanks = new global::System.Collections.Generic.Dictionary<string, string>
		{
			{ "Shared_Audio_KampaiFMOD_Shared.bank", "KampaiFMOD_Shared.bank.bytes" },
			{ "Raw_Audio_Unique_TikiBar.bank", "Unique_TikiBar.bank.bytes" },
			{ "Raw_Audio_Mignette_Shared.bank", "Mignette_Shared.bank.bytes" }
		};

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalContentService localContentService { get; set; }

		[Inject]
		public global::Kampai.Main.IAssetBundlesService assetBundlesService { get; set; }

		[Inject]
		public global::Kampai.Util.ICoroutineProgressMonitor couroutinProgressMonitor { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		public FMODService(global::Kampai.Common.IManifestService manifestService)
		{
			_manifestService = manifestService;
		}

		[global::System.Diagnostics.DebuggerHidden]
		global::System.Collections.IEnumerator global::Kampai.Common.Service.Audio.IFMODService.InitializeSystem()
		{
			global::Kampai.Common.Service.Audio.FMODService.InitializeSystem_003Ec__Iterator23 initializeSystem_003Ec__Iterator = new global::Kampai.Common.Service.Audio.FMODService.InitializeSystem_003Ec__Iterator23();
			initializeSystem_003Ec__Iterator._003C_003Ef__this = this;
			return initializeSystem_003Ec__Iterator;
		}

		string global::Kampai.Common.Service.Audio.IFMODService.GetGuid(string eventName)
		{
			if (_nameIdMap.ContainsKey(eventName))
			{
				return _nameIdMap[eventName];
			}
			logger.Error("eventName '{0}' was not found in the dictionary.", eventName);
			return null;
		}

		private bool IsRawAudioBank(string originalBundleName)
		{
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> rawAudioBank in RawAudioBanks)
			{
				if (originalBundleName.StartsWith(rawAudioBank.Key))
				{
					return true;
				}
			}
			return false;
		}

		private global::System.Collections.IEnumerator LoadAllFromAssetBundles()
		{
			logger.Debug("Starting Load of Audio Assets from Bundles");
			foreach (string bundleName in _manifestService.GetAudioBundles())
			{
				string originalBundleName = _manifestService.GetBundleOriginalName(bundleName);
				logger.Verbose("Loading audio data from bundle {0} [{1}]", bundleName, originalBundleName);
				if (IsRawAudioBank(originalBundleName))
				{
					logger.Debug("Skip loading of {0} bundle, will load it from file system by LoadSharedAudioBank", bundleName);
				}
				else if (LoadFromAssetBundle(bundleName))
				{
					yield return null;
				}
			}
		}

		public bool LoadFromAssetBundle(string bundleName)
		{
			string bundleLocation = _manifestService.GetBundleLocation(bundleName);
			if (bundleLocation.Length == 0)
			{
				logger.Error("Unable to find bundle: {0}", bundleName);
				return false;
			}
			string path = global::System.IO.Path.Combine(bundleLocation, bundleName + ".unity3d");
			global::UnityEngine.AssetBundle assetBundle = global::UnityEngine.AssetBundle.CreateFromFile(path);
			if (null == assetBundle)
			{
				return false;
			}
			global::System.Collections.Generic.IList<string> assetsInBundle = _manifestService.GetAssetsInBundle(bundleName);
			string bankName = global::System.Linq.Enumerable.FirstOrDefault(global::System.Linq.Enumerable.Where(assetsInBundle, (string name) => name.EndsWith(".bank")));
			LoadBundledAudioBank(assetBundle, bankName, bundleName);
			string mapName = global::System.Linq.Enumerable.FirstOrDefault(global::System.Linq.Enumerable.Where(assetsInBundle, (string name) => name.Contains("_map")));
			LoadBundledEventsMap(assetBundle, mapName);
			assetBundle.Unload(true);
			return true;
		}

		private void LoadBundledEventsMap(global::UnityEngine.AssetBundle bundle, string mapName)
		{
			global::UnityEngine.Object obj = bundle.Load(mapName);
			if (obj != null)
			{
				string text = (obj as global::UnityEngine.TextAsset).text;
				LoadEventsMap(text);
			}
		}

		private void LoadEventsMap(string json, bool logErrors = false)
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = global::Newtonsoft.Json.JsonConvert.DeserializeObject<global::System.Collections.Generic.Dictionary<string, string>>(json);
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> item in dictionary)
			{
				if (_nameIdMap.ContainsKey(item.Key))
				{
					if (logErrors && _nameIdMap[item.Key] != item.Value)
					{
						logger.Error("key: {0}\tvalue: {1}", item.Key, item.Value);
					}
				}
				else
				{
					_nameIdMap.Add(item.Key, item.Value);
				}
			}
		}

		private void LoadBundledAudioBank(global::UnityEngine.AssetBundle bundle, string bankName, string bundleName)
		{
			global::UnityEngine.Object obj = bundle.Load(bankName);
			if (obj != null)
			{
				logger.Verbose("Trying to load audio bank: {0} from bundle: {2} [{1}]...", obj.name, _manifestService.GetBundleOriginalName(bundleName), bundleName);
				byte[] bytes = (obj as global::UnityEngine.TextAsset).bytes;
				global::FMOD.Studio.Bank bank = null;
				global::FMOD.RESULT rESULT = FMOD_StudioSystem.instance.System.loadBankMemory(bytes, global::FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out bank);
				if (rESULT == global::FMOD.RESULT.OK)
				{
					logger.Info("Loaded audio bank: {0} from bundle: {2} [{1}]", obj.name, _manifestService.GetBundleOriginalName(bundleName), bundleName);
				}
				else
				{
					logger.Error("Error: '{3}' Unable to load audio bank: {0} from bundle: {2} [{1}]", obj.name, _manifestService.GetBundleOriginalName(bundleName), bundleName, rESULT);
				}
			}
		}

		private global::System.Collections.IEnumerator LoadBanksFromFileSystem()
		{
			global::System.Collections.Generic.List<string> bankFiles = GetFiles(".bytes");
			if (bankFiles == null)
			{
				yield break;
			}
			foreach (string bankFile in bankFiles)
			{
				global::FMOD.Studio.Bank bank = null;
				global::FMOD.RESULT result = FMOD_StudioSystem.instance.System.loadBankFile(bankFile, global::FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out bank);
				if (result == global::FMOD.RESULT.ERR_VERSION)
				{
					global::FMOD.Studio.UnityUtil.LogError("These banks were built with an incompatible version of FMOD Studio.");
				}
				global::FMOD.Studio.UnityUtil.Log("bank load: " + ((!(bank != null)) ? "failed!!" : "succeeded"));
				yield return null;
			}
		}

		private global::System.Collections.IEnumerator LoadRawAudioBanks()
		{
			logger.Debug("Starting Load of Raw Audio Assets");
			foreach (global::System.Collections.Generic.KeyValuePair<string, string> rawAudioBank2 in RawAudioBanks)
			{
				string bankFile = GetRawAudioBankPathByOriginalName(rawAudioBank2.Key);
				if (!string.IsNullOrEmpty(bankFile))
				{
					LoadLocalBank(bankFile);
					yield return null;
				}
			}
		}

		private void LoadLocalBank(string bankFile)
		{
			logger.Verbose("LoadLocalBank: try to load audio bank {0} from file system", bankFile);
			global::FMOD.Studio.Bank bank = null;
			global::FMOD.RESULT rESULT = FMOD_StudioSystem.instance.System.loadBankFile(bankFile, global::FMOD.Studio.LOAD_BANK_FLAGS.NORMAL, out bank);
			if (rESULT == global::FMOD.RESULT.ERR_VERSION)
			{
				logger.Error("These banks were built with an incompatible version of FMOD Studio.");
			}
			if (bank == null)
			{
				logger.Error("LoadLocalBank: Bank {0} loading failed: {1}", bankFile, rESULT.ToString());
			}
			else
			{
				logger.Verbose("LoadLocalBank: Bank {0} successfully loaded", bankFile);
			}
		}

		private string GetStreamingBankPath(string bank)
		{
			return "file:///android_asset/" + bank + ".bytes";
		}

		private global::System.Collections.IEnumerator LoadStreamingAudioBanks()
		{
			logger.Debug("Start Loading Streaming Audio Banks");
			global::System.Collections.Generic.List<string> streamingBanks = localContentService.GetStreamingAudioBanks();
			foreach (string bankName in streamingBanks)
			{
				if (!string.IsNullOrEmpty(_manifestService.GetAssetLocation(bankName)))
				{
					continue;
				}
				if (FMOD_StudioSystem.instance.IsPaused())
				{
					yield return null;
				}
				string path = GetStreamingBankPath(bankName);
				LoadLocalBank(path);
				string mapName = bankName.Replace(".bank", "_map");
				string bundleName = _manifestService.GetAssetLocation(mapName);
				if (!string.IsNullOrEmpty(bundleName))
				{
					global::UnityEngine.AssetBundle bundle = ((!assetBundlesService.IsSharedBundle(bundleName)) ? assetBundlesService.GetDLCBundle(bundleName) : assetBundlesService.GetSharedBundle(bundleName));
					LoadBundledEventsMap(bundle, mapName);
				}
				else if (localContentService.IsLocalAsset(mapName))
				{
					string mapPath = localContentService.GetAssetPath(mapName);
					global::UnityEngine.TextAsset mapAsset = global::UnityEngine.Resources.Load<global::UnityEngine.TextAsset>(mapPath);
					if (null != mapAsset)
					{
						LoadEventsMap(mapAsset.text);
						global::UnityEngine.Resources.UnloadAsset(mapAsset);
					}
				}
				yield return null;
			}
		}

		private string GetRawAudioBankPathByOriginalName(string originalName)
		{
			global::System.Collections.Generic.IList<string> audioBundles = _manifestService.GetAudioBundles();
			string text = null;
			foreach (string item in audioBundles)
			{
				if (_manifestService.GetBundleOriginalName(item).StartsWith(originalName))
				{
					text = item;
					break;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				logger.Error("Failed to find {0} in manifest", originalName);
				return null;
			}
			string bundleLocation = _manifestService.GetBundleLocation(text);
			return global::System.IO.Path.Combine(bundleLocation, text + ".unity3d");
		}

		private global::System.Collections.IEnumerator LoadMapsFromFileSystem()
		{
			global::System.Collections.Generic.List<string> mapFiles = GetFiles("_map.json");
			if (mapFiles == null)
			{
				yield break;
			}
			foreach (string file in mapFiles)
			{
				using (global::System.IO.FileStream stream = global::System.IO.File.OpenRead(file))
				{
					using (global::System.IO.StreamReader reader = new global::System.IO.StreamReader(stream))
					{
						string json = reader.ReadToEnd();
						LoadEventsMap(json, true);
					}
				}
				yield return null;
			}
		}

		private void GetFilesAtPath(global::System.Collections.Generic.List<string> files, string path, string pattern, string fileEnding, bool recursive)
		{
			if (!global::System.IO.Directory.Exists(path))
			{
				global::FMOD.Studio.UnityUtil.LogError(path + " not found, no banks loaded.");
			}
			string[] directories = global::System.IO.Directory.GetDirectories(path, pattern, recursive ? global::System.IO.SearchOption.AllDirectories : global::System.IO.SearchOption.TopDirectoryOnly);
			string[] array = directories;
			foreach (string path2 in array)
			{
				global::System.Collections.Generic.IEnumerable<string> collection = global::System.Linq.Enumerable.Where(global::System.IO.Directory.GetFiles(path2, "*.*", global::System.IO.SearchOption.AllDirectories), (string file) => file.EndsWith(fileEnding));
				files.AddRange(collection);
			}
		}

		private global::System.Collections.Generic.List<string> GetFiles(string fileEnding)
		{
			global::System.Collections.Generic.List<string> list = new global::System.Collections.Generic.List<string>();
			string path = global::UnityEngine.Application.dataPath + global::System.IO.Path.DirectorySeparatorChar + "Content/Shared/";
			GetFilesAtPath(list, path, "Shared_Audio_*", fileEnding, false);
			string path2 = global::UnityEngine.Application.dataPath + global::System.IO.Path.DirectorySeparatorChar + "Content/DLC/";
			GetFilesAtPath(list, path2, "Audio", fileEnding, true);
			string path3 = global::UnityEngine.Application.dataPath + global::System.IO.Path.DirectorySeparatorChar + "Content/Resources/";
			GetFilesAtPath(list, path3, "Audio", fileEnding, true);
			return list;
		}
	}
}
