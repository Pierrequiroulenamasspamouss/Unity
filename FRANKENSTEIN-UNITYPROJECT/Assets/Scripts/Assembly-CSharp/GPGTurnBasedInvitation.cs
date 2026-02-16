public class GPGTurnBasedInvitation
{
	public GPGTurnBasedParticipant invitingParticipant;

	public GPGTurnBasedMatch match;

	public override string ToString()
	{
		return global::Prime31.JsonFormatter.prettyPrint(global::Prime31.Json.encode(this));
	}
}
