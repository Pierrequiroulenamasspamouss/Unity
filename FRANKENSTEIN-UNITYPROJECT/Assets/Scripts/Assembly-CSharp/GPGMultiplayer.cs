public class GPGMultiplayer
{
	private class RealTimeMessageReceivedListener : global::UnityEngine.AndroidJavaProxy
	{
		public RealTimeMessageReceivedListener()
			: base("com.prime31.IRealTimeMessageReceivedListener")
		{
		}

		public void onMessageReceived(string senderParticipantId, string messageData)
		{
			byte[] message = global::System.Convert.FromBase64String(messageData);
			GPGMultiplayerManager.onRealTimeMessageReceived(senderParticipantId, message);
		}

		public void onRawMessageReceived(global::UnityEngine.AndroidJavaObject senderParticipantId, global::UnityEngine.AndroidJavaObject messageData)
		{
			string senderParticipantId2 = senderParticipantId.Call<string>("toString", new object[0]);
			byte[] message = global::UnityEngine.AndroidJNI.FromByteArray(messageData.GetRawObject());
			GPGMultiplayerManager.onRealTimeMessageReceived(senderParticipantId2, message);
		}

		public override global::UnityEngine.AndroidJavaObject Invoke(string methodName, global::UnityEngine.AndroidJavaObject[] args)
		{
			if (methodName == "onRawMessageReceived")
			{
				onRawMessageReceived(args[0], args[1]);
				return null;
			}
			return base.Invoke(methodName, args);
		}

		public string toString()
		{
			return "RealTimeMessageReceivedListener class instance from Unity";
		}
	}

	private static global::UnityEngine.AndroidJavaObject _plugin;

	static GPGMultiplayer()
	{
		if (global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.Android)
		{
			return;
		}
		using (global::UnityEngine.AndroidJavaClass androidJavaClass = new global::UnityEngine.AndroidJavaClass("com.prime31.PlayGameServicesPlugin"))
		{
			_plugin = androidJavaClass.CallStatic<global::UnityEngine.AndroidJavaObject>("realtimeMultiplayerInstance", new object[0]);
			_plugin.Call("setRealtimeMessageListener", new GPGMultiplayer.RealTimeMessageReceivedListener());
		}
	}

	public static void registerDeviceToken(byte[] deviceToken, bool isProductionEnvironment)
	{
	}

	public static void showInvitationInbox()
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("showInvitationInbox");
		}
	}

	public static void startQuickMatch(int minAutoMatchPlayers, int maxAutoMatchPlayers, long exclusiveBitmask = 0)
	{
		createRoomProgrammatically(minAutoMatchPlayers, maxAutoMatchPlayers, exclusiveBitmask);
	}

	public static void createRoomProgrammatically(int minAutoMatchPlayers, int maxAutoMatchPlayers, long exclusiveBitmask = 0)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("createRoomProgrammatically", minAutoMatchPlayers, maxAutoMatchPlayers, exclusiveBitmask);
		}
	}

	public static void showPlayerSelector(int minPlayers, int maxPlayers)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("showPlayerSelector", minPlayers, maxPlayers);
		}
	}

	public static void joinRoomWithInvitation(string invitationId)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("joinRoomWithInvitation", invitationId);
		}
	}

	public static void showWaitingRoom(int minParticipantsToStart)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("showWaitingRoom", minParticipantsToStart);
		}
	}

	public static void leaveRoom()
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("leaveRoom");
		}
	}

	public static GPGRoom getRoom()
	{
		if (global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.Android)
		{
			return new GPGRoom();
		}
		string json = _plugin.Call<string>("getRoom", new object[0]);
		return global::Prime31.Json.decode<GPGRoom>(json);
	}

	public static global::System.Collections.Generic.List<GPGMultiplayerParticipant> getParticipants()
	{
		if (global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.Android)
		{
			return null;
		}
		string json = _plugin.Call<string>("getParticipants", new object[0]);
		return global::Prime31.Json.decode<global::System.Collections.Generic.List<GPGMultiplayerParticipant>>(json);
	}

	public static string getCurrentPlayerParticipantId()
	{
		if (global::UnityEngine.Application.platform != global::UnityEngine.RuntimePlatform.Android)
		{
			return null;
		}
		return _plugin.Call<string>("getCurrentPlayerParticipantId", new object[0]);
	}

	public static void sendReliableRealtimeMessage(string participantId, byte[] message)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("sendReliableRealtimeMessage", participantId, message);
		}
	}

	public static void sendReliableRealtimeMessageToAll(byte[] message)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("sendReliableRealtimeMessageToAll", message);
		}
	}

	public static void sendUnreliableRealtimeMessage(string participantId, byte[] message)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("sendUnreliableRealtimeMessage", participantId, message);
		}
	}

	public static void sendUnreliableRealtimeMessageToAll(byte[] message)
	{
		if (global::UnityEngine.Application.platform == global::UnityEngine.RuntimePlatform.Android)
		{
			_plugin.Call("sendUnreliableRealtimeMessageToAll", message);
		}
	}
}
