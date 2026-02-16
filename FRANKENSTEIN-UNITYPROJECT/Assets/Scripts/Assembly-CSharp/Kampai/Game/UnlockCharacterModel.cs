namespace Kampai.Game
{
	public class UnlockCharacterModel
	{
		public int routeIndex { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.Character> minionUnlocks { get; set; }

		public global::System.Collections.Generic.IList<global::Kampai.Game.Character> characterUnlocks { get; set; }

		public UnlockCharacterModel()
		{
			routeIndex = -1;
			minionUnlocks = new global::System.Collections.Generic.List<global::Kampai.Game.Character>();
			characterUnlocks = new global::System.Collections.Generic.List<global::Kampai.Game.Character>();
		}
	}
}
