namespace Kampai.Game
{
	internal sealed class PlayerSerializerV3 : global::Kampai.Game.PlayerSerializerV2
	{
		public override int Version
		{
			get
			{
				return 3;
			}
		}

		public override global::Kampai.Game.Player Deserialize(string json, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Util.ILogger logger)
		{
			global::Kampai.Game.Player player = base.Deserialize(json, definitionService, logger);
			if (player.Version < 3)
			{
				foreach (global::Kampai.Game.Villain item in player.GetInstancesByType<global::Kampai.Game.Villain>())
				{
					global::Kampai.Game.CabanaBuilding cabana = item.Cabana;
					if (cabana != null)
					{
						item.CabanaBuildingId = cabana.ID;
						item.Cabana = null;
					}
				}
				player.Version = 3;
			}
			return player;
		}
	}
}
