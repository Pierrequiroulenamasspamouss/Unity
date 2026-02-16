public sealed class FBAppRequestsFilterGroup : global::System.Collections.Generic.Dictionary<string, object>
{
	public FBAppRequestsFilterGroup(string name, global::System.Collections.Generic.List<string> user_ids)
	{
		this["name"] = name;
		this["user_ids"] = user_ids;
	}
}
