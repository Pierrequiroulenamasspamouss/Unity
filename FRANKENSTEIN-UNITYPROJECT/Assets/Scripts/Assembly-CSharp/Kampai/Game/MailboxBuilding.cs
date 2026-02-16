namespace Kampai.Game
{
	public class MailboxBuilding : global::Kampai.Game.Building<global::Kampai.Game.MailboxBuildingDefinition>
	{
		public MailboxBuilding(global::Kampai.Game.MailboxBuildingDefinition def)
			: base(def)
		{
		}

		public override global::Kampai.Game.View.BuildingObject AddBuildingObject(global::UnityEngine.GameObject gameObject)
		{
			return gameObject.AddComponent<global::Kampai.Game.View.MailboxBuildingObject>();
		}
	}
}
