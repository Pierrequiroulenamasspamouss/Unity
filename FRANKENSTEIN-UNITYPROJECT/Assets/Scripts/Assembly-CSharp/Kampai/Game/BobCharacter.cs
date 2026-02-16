namespace Kampai.Game
{
	public class BobCharacter : global::Kampai.Game.NamedCharacter<global::Kampai.Game.BobCharacterDefinition>
	{
		[global::Newtonsoft.Json.JsonIgnore]
		public global::Kampai.Game.FloatLocation CurrentFrolicLocation;

		public BobCharacter(global::Kampai.Game.BobCharacterDefinition def)
			: base(def)
		{
			Name = "Bob";
		}

		public override global::Kampai.Game.View.NamedCharacterObject Setup(global::UnityEngine.GameObject go)
		{
			return go.AddComponent<global::Kampai.Game.View.BobView>();
		}
	}
}
