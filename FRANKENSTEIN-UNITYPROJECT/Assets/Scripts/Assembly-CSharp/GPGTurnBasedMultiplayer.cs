public class GPGTurnBasedMultiplayer
{
	private static global::UnityEngine.AndroidJavaObject _plugin;

	static GPGTurnBasedMultiplayer()
	{
		if (global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.Android)
		{
			return;
		}
		using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.prime31.PlayGameServicesPlugin"))
		{
			_plugin = androidJavaClass.CallStatic<global::UnityEngine.AndroidJavaObject>("turnBasedMultiplayerInstance", new object[0]);
		}
	}

	public static void checkForInvitesAndMatches()
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("checkForInvitesAndMatches");
		}
	}

	public static void showInbox()
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("showInbox");
		}
	}

	public static void showPlayerSelector(int minPlayersToPick, int maxPlayersToPick)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("showPlayerSelector", minPlayersToPick, maxPlayersToPick);
		}
	}

	public static void createMatchProgrammatically(int minAutoMatchPlayers, int maxAutoMatchPlayers, long exclusiveBitmask)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("createMatchProgrammatically", minAutoMatchPlayers, maxAutoMatchPlayers, exclusiveBitmask);
		}
	}

	public static void loadAllMatches()
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("loadAllMatches");
		}
	}

	public static void takeTurn(string matchId, byte[] matchData, string pendingParticipantId)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("takeTurn", matchId, matchData, pendingParticipantId);
		}
	}

	public static void leaveDuringTurn(string matchId, string pendingParticipantId)
	{
		if (pendingParticipantId == null)
		{
			global::UnityEngine.Debug.LogWarning("leaveDuringTurn called with a null pendingParticipantId which is invalid");
		}
		else if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("leaveDuringTurn", matchId, pendingParticipantId);
		}
	}

	public static void leaveOutOfTurn(string matchId)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("leaveOutOfTurn", matchId);
		}
	}

	public static void finishMatchWithData(string matchId, byte[] matchData, global::System.Collections.Generic.List<GPGTurnBasedParticipantResult> results)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("finishMatchWithData", matchId, matchData, global::Prime31.Json.encode(results));
		}
	}

	public static void finishMatchWithoutData(string matchId)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("finishMatchWithoutData", matchId);
		}
	}

	public static void dismissMatch(string matchId)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("dismissMatch", matchId);
		}
	}

	public static void rematch(string matchId)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("rematch", matchId);
		}
	}

	public static void joinMatchWithInvitation(string invitationId)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("joinMatchWithInvitation", invitationId);
		}
	}

	public static void declineMatchWithInvitation(string invitationId)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("declineMatchWithInvitation", invitationId);
		}
	}
}
