namespace Kampai.Util
{
	public class TimeProfiler : global::System.IDisposable
	{
		private sealed class FrameCounter : global::UnityEngine.MonoBehaviour
		{
			public int Frame;

			public global::System.Action<int, float> LongFrameOccured;

			private global::UnityEngine.Coroutine updateCoroutine;

			private bool isRunning = true;

			public void Start()
			{
				updateCoroutine = StartCoroutine(UpdateFrameStats());
			}

			public void StopUpdateCoroutine()
			{
				isRunning = false;
				StopCoroutine(updateCoroutine);
			}

			public void Update()
			{
				float unscaledDeltaTime = global::UnityEngine.Time.unscaledDeltaTime;
				if ((double)unscaledDeltaTime > 0.1)
				{
					LongFrameOccured(global::UnityEngine.Time.frameCount - 1, unscaledDeltaTime);
				}
			}

			public global::System.Collections.IEnumerator UpdateFrameStats()
			{
				while (isRunning)
				{
					yield return new global::UnityEngine.WaitForEndOfFrame();
					Frame = global::UnityEngine.Time.frameCount + 1;
				}
			}
		}

		private sealed class Section
		{
			private string name;

			private global::System.Diagnostics.Stopwatch timer;

			private global::System.TimeSpan assetsLoadTime;

			private global::System.TimeSpan subsectionLoadTime;

			private int numberOfAssetsLoaded;

			private bool wasReleased;

			public Section(string name)
			{
				this.name = name;
				timer = global::System.Diagnostics.Stopwatch.StartNew();
			}

			public string FormatSectionTime()
			{
				return GetSectionTime().ToString();
			}

			public global::System.TimeSpan GetSectionTime()
			{
				return timer.Elapsed;
			}

			public string GetSectionName()
			{
				return name;
			}

			public void mergeSubSection(global::System.TimeSpan time)
			{
				assetsLoadTime += time;
				numberOfAssetsLoaded++;
			}

			public void mergeSubSection(global::Kampai.Util.TimeProfiler.Section section)
			{
				assetsLoadTime += section.assetsLoadTime;
				numberOfAssetsLoaded += section.numberOfAssetsLoaded;
				subsectionLoadTime += section.timer.Elapsed;
			}

			public string FormatAssetsLoadTime()
			{
				return GetAssetsLoadTime().ToString();
			}

			public global::System.TimeSpan GetAssetsLoadTime()
			{
				return assetsLoadTime;
			}

			public int GetNumberOfAssetsLoaded()
			{
				return numberOfAssetsLoaded;
			}

			public string FormatTotalSubSectionsTime()
			{
				return GetTotalSubSectionsTime().ToString();
			}

			public global::System.TimeSpan GetTotalSubSectionsTime()
			{
				return subsectionLoadTime;
			}

			public void ReleaseSection()
			{
				wasReleased = true;
			}

			public bool IsSectionReleased()
			{
				return wasReleased;
			}
		}

		private static global::Kampai.Util.TimeProfiler currentInstance;

		private global::System.IO.StreamWriter stream;

		private global::System.TimeSpan totalAssetsLoadTime;

		private global::UnityEngine.GameObject frameCounterGO;

		private global::Kampai.Util.TimeProfiler.FrameCounter frameCounter;

		private static readonly string PROFILER_LOG_PATH = global::UnityEngine.Application.persistentDataPath + "/profiler/";

		private global::System.Collections.Generic.List<global::Kampai.Util.TimeProfiler.Section> sections = new global::System.Collections.Generic.List<global::Kampai.Util.TimeProfiler.Section>();

		protected bool Disposed { get; private set; }

		private TimeProfiler()
		{
			string path = "timelog_" + global::System.DateTime.Now.ToString("yyyyMMddHHmm") + ".txt";
			string path2 = global::System.IO.Path.Combine(global::UnityEngine.Application.persistentDataPath, path);
			stream = new global::System.IO.StreamWriter(path2);
			stream.AutoFlush = true;
			frameCounterGO = new global::UnityEngine.GameObject("FrameCounter");
			frameCounter = frameCounterGO.AddComponent<global::Kampai.Util.TimeProfiler.FrameCounter>();
			global::Kampai.Util.TimeProfiler.FrameCounter obj = frameCounter;
			obj.LongFrameOccured = (global::System.Action<int, float>)global::System.Delegate.Combine(obj.LongFrameOccured, new global::System.Action<int, float>(LogLongFrame));
		}

		private global::Kampai.Util.TimeProfiler.Section GetLastSection(bool pop)
		{
			int count = sections.Count;
			if (count == 0)
			{
				return null;
			}
			global::Kampai.Util.TimeProfiler.Section section = sections[count - 1];
			if (pop)
			{
				sections.Remove(section);
			}
			return section;
		}

		private void LogLongFrame(int frameNumber, float time)
		{
			stream.Write("!!!! Frame {0} took {1} s.\n", frameNumber, time);
		}

		private static string GetTimeStamp()
		{
			return global::System.DateTime.Now.ToString("HH:mm:ss.ffff");
		}

		public static void Reset(bool enabled)
		{
			if (currentInstance != null)
			{
				Flush();
				if (currentInstance.frameCounter != null)
				{
					currentInstance.frameCounter.StopUpdateCoroutine();
					global::UnityEngine.Object.Destroy(currentInstance.frameCounterGO);
				}
				currentInstance.Dispose();
				currentInstance = null;
			}
			if (enabled)
			{
				currentInstance = new global::Kampai.Util.TimeProfiler();
			}
		}

		public static void Flush()
		{
			if (currentInstance != null)
			{
				currentInstance.stream.Flush();
			}
		}

		public static void StartSection(string sectionName)
		{
			if (currentInstance != null)
			{
				global::Kampai.Util.TimeProfiler.Section section = new global::Kampai.Util.TimeProfiler.Section(sectionName);
				currentInstance.sections.Add(section);
				currentInstance.stream.Write("{0}>{1}{2} (frame {3})\n", GetTimeStamp(), new string('\t', currentInstance.sections.Count - 1), section.GetSectionName(), currentInstance.frameCounter.Frame);
			}
		}

		public static void EndSection(string sectionName)
		{
			if (currentInstance == null)
			{
				return;
			}
			global::Kampai.Util.TimeProfiler.Section lastSection = currentInstance.GetLastSection(false);
			if (lastSection.GetSectionName() == sectionName)
			{
				lastSection.ReleaseSection();
				while (lastSection != null && lastSection.IsSectionReleased())
				{
					PopLatestSection();
					lastSection = currentInstance.GetLastSection(false);
				}
				return;
			}
			foreach (global::Kampai.Util.TimeProfiler.Section section in currentInstance.sections)
			{
				if (section.GetSectionName() == sectionName)
				{
					section.ReleaseSection();
					break;
				}
			}
		}

		private static void PopLatestSection()
		{
			global::Kampai.Util.TimeProfiler.Section lastSection = currentInstance.GetLastSection(true);
			global::Kampai.Util.TimeProfiler.Section lastSection2 = currentInstance.GetLastSection(false);
			if (lastSection2 != null)
			{
				lastSection2.mergeSubSection(lastSection);
			}
			currentInstance.stream.Write("{0}<{1}{2} in {3}. ({4} assets loaded in {5}). {6} in section. (frame {7})\n", GetTimeStamp(), new string('\t', currentInstance.sections.Count), lastSection.GetSectionName(), lastSection.FormatSectionTime(), lastSection.GetNumberOfAssetsLoaded(), lastSection.FormatAssetsLoadTime(), (lastSection2 == null) ? string.Empty : lastSection2.FormatTotalSubSectionsTime(), currentInstance.frameCounter.Frame);
		}

		public static void StartAssetLoadSection(string name)
		{
			if (currentInstance != null)
			{
				global::Kampai.Util.TimeProfiler.Section item = new global::Kampai.Util.TimeProfiler.Section(new string('\t', currentInstance.sections.Count) + "asset " + name);
				currentInstance.sections.Add(item);
			}
		}

		public static void EndAssetLoadSection()
		{
			if (currentInstance != null)
			{
				global::Kampai.Util.TimeProfiler.Section lastSection = currentInstance.GetLastSection(true);
				global::System.TimeSpan sectionTime = lastSection.GetSectionTime();
				currentInstance.totalAssetsLoadTime += sectionTime;
				if (currentInstance.sections.Count > 0)
				{
					currentInstance.GetLastSection(false).mergeSubSection(sectionTime);
				}
				currentInstance.stream.Write("{0}<{1} in {2}. total {3} (frame {4})\n", GetTimeStamp(), lastSection.GetSectionName(), sectionTime.ToString(), currentInstance.totalAssetsLoadTime.ToString(), currentInstance.frameCounter.Frame);
			}
		}

		public static void startUnityProfiler(string sectionName)
		{
			if (global::UnityEngine.Debug.isDebugBuild && false) // never start the profiler
			{
				if (!global::System.IO.Directory.Exists(PROFILER_LOG_PATH))
				{
					global::System.IO.Directory.CreateDirectory(PROFILER_LOG_PATH);
				}
				global::UnityEngine.Profiler.logFile = PROFILER_LOG_PATH + sectionName;
				global::UnityEngine.Profiler.enableBinaryLog = true;
				global::UnityEngine.Profiler.enabled = true;
			}
		}

		public static void stopUnityProfiler()
		{
			if (global::UnityEngine.Debug.isDebugBuild && false) // never stop the profiler
            {
				global::UnityEngine.Profiler.enabled = false;
				global::UnityEngine.Profiler.enableBinaryLog = false;
				global::UnityEngine.Profiler.logFile = string.Empty;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			global::System.GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!Disposed && disposing)
			{
				stream.Dispose();
				stream = null;
			}
			Disposed = true;
		}

		~TimeProfiler()
		{
			Dispose(false);
		}
	}
}
