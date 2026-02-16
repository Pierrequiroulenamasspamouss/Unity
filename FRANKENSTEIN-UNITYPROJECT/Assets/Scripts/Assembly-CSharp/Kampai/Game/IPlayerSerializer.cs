namespace Kampai.Game
{
	public interface IPlayerSerializer
	{
		int Version { get; }

		global::Kampai.Game.Player Deserialize(string json, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Util.ILogger logger);

		byte[] Serialize(global::Kampai.Game.Player player, global::Kampai.Game.IDefinitionService defintitionService, global::Kampai.Util.ILogger logger);
	}
}
