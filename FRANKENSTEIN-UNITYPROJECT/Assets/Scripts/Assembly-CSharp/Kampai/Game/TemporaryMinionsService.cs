namespace Kampai.Game
{
	public class TemporaryMinionsService
	{
		private global::System.Collections.Generic.IDictionary<int, global::Kampai.Game.View.MinionObject> temporaryMinions = new global::System.Collections.Generic.Dictionary<int, global::Kampai.Game.View.MinionObject>();

		public void addTemporaryMinion(global::Kampai.Game.View.MinionObject mo)
		{
			if (mo != null)
			{
				mo.isTemporaryMinion = true;
				temporaryMinions.Add(mo.ID, mo);
			}
		}

		public global::System.Collections.Generic.IDictionary<int, global::Kampai.Game.View.MinionObject> getTemporaryMinions()
		{
			return temporaryMinions;
		}
	}
}
