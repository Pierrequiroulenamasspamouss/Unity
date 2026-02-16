namespace DeltaDNA
{
	public class Singleton<T> : global::UnityEngine.MonoBehaviour where T : global::UnityEngine.MonoBehaviour
	{
		private static T _instance;

		private static object _lock = new object();

		private static bool applicationIsQuitting = false;

		public static T Instance
		{
			get
			{
				if (applicationIsQuitting)
				{
					global::DeltaDNA.Logger.LogWarning(string.Concat("[Singleton] Instance '", typeof(T), "' already destroyed on application quit. Won't create again - returning null."));
					return (T)null;
				}
				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = (T)global::UnityEngine.Object.FindObjectOfType(typeof(T));
						if (global::UnityEngine.Object.FindObjectsOfType(typeof(T)).Length > 1)
						{
							global::DeltaDNA.Logger.LogError("[Singleton] Something went really wrong  - there should never be more than 1 singleton! Reopening the scene might fix it.");
							return _instance;
						}
						if (_instance == null)
						{
							global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject();
							_instance = gameObject.AddComponent<T>();
							gameObject.name = "(singleton) " + typeof(T).ToString();
							global::UnityEngine.Object.DontDestroyOnLoad(gameObject);
							global::DeltaDNA.Logger.LogDebug(string.Concat("[Singleton] An instance of ", typeof(T), " is needed in the scene, so '", gameObject, "' was created with DontDestroyOnLoad."));
						}
						else
						{
							global::DeltaDNA.Logger.LogDebug("[Singleton] Using instance already created: " + _instance.gameObject.name);
						}
					}
					return _instance;
				}
			}
		}

		public virtual void OnDestroy()
		{
			global::DeltaDNA.Logger.LogDebug("[Singleton] Destroying an instance of " + typeof(T));
			applicationIsQuitting = true;
		}
	}
}
