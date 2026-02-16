namespace Kampai.Game
{
	public class KevinCharacter : global::Kampai.Game.NamedCharacter<global::Kampai.Game.KevinCharacterDefinition>
	{
		[global::Newtonsoft.Json.JsonIgnore]
		public global::Kampai.Game.FloatLocation CurrentFrolicLocation;

		public KevinCharacter(global::Kampai.Game.KevinCharacterDefinition def)
			: base(def)
		{
			Name = "Kevin";
		}

		public override global::Kampai.Game.View.NamedCharacterObject Setup(global::UnityEngine.GameObject go)
		{
			return go.AddComponent<global::Kampai.Game.View.KevinView>();
		}
	}
}
