public static class ItemType
{
	public enum ItemTypeIdentifier
	{
		UNKNOWN = 0,
		DEFAULT = 1,
		INGREDIENTS = 2,
		DYNAMIC_INGREDIENTS = 3,
		RESOURCE = 4,
		COSTUME = 5,
		UNLOCK = 6,
		BRIDGE = 7,
		DROP = 8
	}

	public static ItemType.ItemTypeIdentifier ParseIdentifier(string identifier)
	{
		if (identifier != null)
		{
			return (ItemType.ItemTypeIdentifier)(int)global::System.Enum.Parse(typeof(ItemType.ItemTypeIdentifier), identifier);
		}
		return ItemType.ItemTypeIdentifier.UNKNOWN;
	}
}
