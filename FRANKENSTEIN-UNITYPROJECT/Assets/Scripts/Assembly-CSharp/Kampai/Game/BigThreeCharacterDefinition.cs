namespace Kampai.Game
{
	public class BigThreeCharacterDefinition : global::Kampai.Game.NamedCharacterDefinition
	{
		public override global::Kampai.Game.Instance Build()
		{
			return new global::Kampai.Game.BigThreeCharacter(this);
		}
	}
}
