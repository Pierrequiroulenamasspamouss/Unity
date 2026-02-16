public class FMOD_StudioEventEmitter : global::UnityEngine.MonoBehaviour
{
	[global::System.Serializable]
	public class Parameter
	{
		public string name;

		public float value;
	}

	public FMODAsset asset;

	public string path = string.Empty;

	public bool startEventOnAwake = true;

	private global::FMOD.Studio.EventInstance evt;

	private bool hasStarted;

	private global::UnityEngine.Rigidbody cachedRigidBody;

	private static bool isShuttingDown;

	public void Play()
	{
		if (evt != null)
		{
			ERRCHECK(evt.start());
		}
		else
		{
			global::FMOD.Studio.UnityUtil.Log("Tried to play event without a valid instance: " + path);
		}
	}

	public void Stop()
	{
		if (evt != null)
		{
			ERRCHECK(evt.stop(global::FMOD.Studio.STOP_MODE.IMMEDIATE));
		}
	}

	public global::FMOD.Studio.ParameterInstance getParameter(string name)
	{
		global::FMOD.Studio.ParameterInstance instance = null;
		ERRCHECK(evt.getParameter(name, out instance));
		return instance;
	}

	public global::FMOD.Studio.PLAYBACK_STATE getPlaybackState()
	{
		if (evt == null || !evt.isValid())
		{
			return global::FMOD.Studio.PLAYBACK_STATE.STOPPED;
		}
		global::FMOD.Studio.PLAYBACK_STATE state = global::FMOD.Studio.PLAYBACK_STATE.STOPPED;
		if (ERRCHECK(evt.getPlaybackState(out state)) == global::FMOD.RESULT.OK)
		{
			return state;
		}
		return global::FMOD.Studio.PLAYBACK_STATE.STOPPED;
	}

	private void Start()
	{
		if (evt == null || !evt.isValid())
		{
			CacheEventInstance();
		}
		cachedRigidBody = GetComponent<global::UnityEngine.Rigidbody>();
		if (startEventOnAwake)
		{
			StartEvent();
		}
	}

	private void CacheEventInstance()
	{
		if (asset != null)
		{
			evt = FMOD_StudioSystem.instance.GetEvent(asset.id);
		}
		else if (!string.IsNullOrEmpty(path))
		{
			evt = FMOD_StudioSystem.instance.GetEvent(path);
		}
		else
		{
			global::FMOD.Studio.UnityUtil.LogError("No asset or path specified for Event Emitter");
		}
	}

	private void OnApplicationQuit()
	{
		isShuttingDown = true;
	}

	private void OnDestroy()
	{
		if (isShuttingDown)
		{
			return;
		}
		global::FMOD.Studio.UnityUtil.Log("Destroy called");
		if (evt != null && evt.isValid())
		{
			if (getPlaybackState() != global::FMOD.Studio.PLAYBACK_STATE.STOPPED)
			{
				global::FMOD.Studio.UnityUtil.Log("Release evt: " + path);
				ERRCHECK(evt.stop(global::FMOD.Studio.STOP_MODE.IMMEDIATE));
			}
			ERRCHECK(evt.release());
			evt = null;
		}
	}

	public void StartEvent()
	{
		if (evt == null || !evt.isValid())
		{
			CacheEventInstance();
		}
		if (evt != null && evt.isValid())
		{
			Update3DAttributes();
			ERRCHECK(evt.start());
		}
		else
		{
			global::FMOD.Studio.UnityUtil.LogError("Event retrieval failed: " + path);
		}
		hasStarted = true;
	}

	public bool HasFinished()
	{
		if (!hasStarted)
		{
			return false;
		}
		if (evt == null || !evt.isValid())
		{
			return true;
		}
		return getPlaybackState() == global::FMOD.Studio.PLAYBACK_STATE.STOPPED;
	}

	private void Update()
	{
		if (evt != null && evt.isValid())
		{
			Update3DAttributes();
		}
		else
		{
			evt = null;
		}
	}

	private void Update3DAttributes()
	{
		if (evt != null && evt.isValid())
		{
			global::FMOD.Studio.ATTRIBUTES_3D attributes = global::FMOD.Studio.UnityUtil.to3DAttributes(base.gameObject, cachedRigidBody);
			ERRCHECK(evt.set3DAttributes(attributes));
		}
	}

	private global::FMOD.RESULT ERRCHECK(global::FMOD.RESULT result)
	{
		global::FMOD.Studio.UnityUtil.ERRCHECK(result);
		return result;
	}
}
