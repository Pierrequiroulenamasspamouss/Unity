namespace Kampai.Game
{
	public class StuartCharacter : global::Kampai.Game.NamedCharacter<global::Kampai.Game.StuartCharacterDefinition>
	{
		public StuartCharacter(global::Kampai.Game.StuartCharacterDefinition def)
			: base(def)
		{
			Name = "Stuart";
		}

		public override global::Kampai.Game.View.NamedCharacterObject Setup(global::UnityEngine.GameObject go)
		{
			return go.AddComponent<global::Kampai.Game.View.StuartView>();
		}
	}
}
