public class GPGTurnBasedParticipant
{
	public GPGPlayerInfo player;

	public string participantId;

	public bool isAutoMatchedPlayer;

	public int statusInt;

	public GPGTurnBasedParticipantStatus status
	{
		get
		{
			return (GPGTurnBasedParticipantStatus)(int)global::System.Enum.ToObject(typeof(GPGTurnBasedParticipantStatus), statusInt);
		}
	}

	public string statusString
	{
		get
		{
			return status.ToString();
		}
	}

	public override string ToString()
	{
		return global::Prime31.JsonFormatter.prettyPrint(global::Prime31.Json.encode(this));
	}
}
