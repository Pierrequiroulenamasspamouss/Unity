public static class BuildingType
{
	public enum BuildingTypeIdentifier
	{
		UNKNOWN = 0,
		CRAFTING = 1,
		DECORATION = 2,
		LEISURE = 3,
		RESOURCE = 4,
		SPECIAL = 5,
		BLACKMARKETBOARD = 6,
		STORAGE = 7,
		LANDEXPANSION = 8,
		DEBRIS = 9,
		MIGNETTE = 10,
		TIKIBAR = 11,
		CABANA = 12,
		BRIDGE = 13,
		COMPOSITE = 14,
		STAGE = 15,
		WELCOMEHUT = 16,
		FOUNTAIN = 17,
		MAILBOX = 18
	}

	public static BuildingType.BuildingTypeIdentifier ParseIdentifier(string identifier)
	{
		if (identifier != null)
		{
			return (BuildingType.BuildingTypeIdentifier)(int)global::System.Enum.Parse(typeof(BuildingType.BuildingTypeIdentifier), identifier);
		}
		return BuildingType.BuildingTypeIdentifier.UNKNOWN;
	}
}
