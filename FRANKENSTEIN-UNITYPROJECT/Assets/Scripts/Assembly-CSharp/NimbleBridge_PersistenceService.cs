public class NimbleBridge_PersistenceService
{
	private NimbleBridge_PersistenceService()
	{
	}

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern NimbleBridge_Persistence NimbleBridge_PersistenceService_getPersistence(string identifier, int storage);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern void NimbleBridge_PersistenceService_migratePersistence(string sourcePersistenceId, int storage, string targetPersistenceId, int mergePolicy);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern NimbleBridge_Persistence NimbleBridge_PersistenceService_getAppPersistence(int storage);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
	[global::System.Runtime.InteropServices.DllImport("NimbleCInterface")]
	private static extern NimbleBridge_Persistence NimbleBridge_PersistenceService_getPersistenceForNimbleComponent(string componentId, int storage);
#endif

	public static NimbleBridge_PersistenceService GetComponent()
	{
		return new NimbleBridge_PersistenceService();
	}

	public NimbleBridge_Persistence GetPersistence(string identifier, NimbleBridge_Persistence.Storage storage)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_PersistenceService_getPersistence(identifier, (int)storage);
#else
        // Dummy persistence
        return new NimbleBridge_Persistence();
#endif
	}

	public void MigratePersistence(string sourcePersistenceId, NimbleBridge_Persistence.Storage storage, string targetPersistenceId, NimbleBridge_Persistence.MergePolicy mergePolicy)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		NimbleBridge_PersistenceService_migratePersistence(sourcePersistenceId, (int)storage, targetPersistenceId, (int)mergePolicy);
#endif
	}

	public static NimbleBridge_Persistence GetAppPersistence(NimbleBridge_Persistence.Storage storage)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_PersistenceService_getAppPersistence((int)storage);
#else
        return new NimbleBridge_Persistence();
#endif
	}

	public static NimbleBridge_Persistence GetPersistenceForNimbleComponent(string componentId, NimbleBridge_Persistence.Storage storage)
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		return NimbleBridge_PersistenceService_getPersistenceForNimbleComponent(componentId, (int)storage);
#else
        return new NimbleBridge_Persistence();
#endif
	}
}
