internal class NimbleBridge_CallbackHelper : global::UnityEngine.MonoBehaviour
{
	private global::System.Collections.Generic.Dictionary<int, NimbleBridge_Callback> m_callbacks = new global::System.Collections.Generic.Dictionary<int, NimbleBridge_Callback>();

	private int m_nextId = 1;

	private static NimbleBridge_CallbackHelper s_instance;

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimblePlugin")]
	private static extern void NimbleUnity_CallbackHelper_notifyCallbackComplete(global::System.IntPtr callback);
#endif

	private int GenerateId()
	{
		int nextId = m_nextId;
		if (++m_nextId > 100000000)
		{
			m_nextId = 1;
		}
		return nextId;
	}

	internal global::System.IntPtr AddCallback(NimbleBridge_Callback callback)
	{
		int num = GenerateId();
		m_callbacks.Add(num, callback);
		return new global::System.IntPtr(num);
	}

	internal NimbleBridge_Callback GetCallback(global::System.IntPtr callback)
	{
		return m_callbacks[callback.ToInt32()];
	}

	internal void NotifyCallbackComplete(global::System.IntPtr callback)
	{
		
		//NimbleUnity_CallbackHelper_notifyCallbackComplete(callback);
	}

	private void SendCallback(string callback)
	{
		int num = int.Parse(callback);
		m_callbacks[num].Callback(new global::System.IntPtr(num));
	}

	private void DisposeCallback(string callback)
	{
		int key = int.Parse(callback);
		m_callbacks.Remove(key);
	}

	private void Awake()
	{
		global::UnityEngine.Object.DontDestroyOnLoad(base.transform.gameObject);
	}

	internal static NimbleBridge_CallbackHelper Get()
	{
		if (object.ReferenceEquals(s_instance, null))
		{
			if (!global::UnityEngine.Application.isPlaying)
			{
				return null;
			}
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("NimbleCallbackHelper");
			s_instance = gameObject.AddComponent<NimbleBridge_CallbackHelper>();
		}
		return s_instance;
	}
}
