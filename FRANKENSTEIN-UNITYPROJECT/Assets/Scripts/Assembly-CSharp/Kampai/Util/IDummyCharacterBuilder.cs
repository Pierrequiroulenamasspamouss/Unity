namespace Kampai.Util
{
	public interface IDummyCharacterBuilder
	{
		global::Kampai.Game.View.DummyCharacterObject BuildMinion(global::Kampai.Game.Minion minion, string baseModelPath, global::Kampai.Game.CharacterUIAnimationDefinition characterUIAnimationDefinition, global::UnityEngine.Transform parent, bool isHigh, global::UnityEngine.Vector3 villainScale, global::UnityEngine.Vector3 villainPositionOffset);

		global::Kampai.Game.View.DummyCharacterObject BuildNamedChacter(global::Kampai.Game.NamedCharacter namedCharacter, global::UnityEngine.Transform parent, bool isHigh, global::UnityEngine.Vector3 villainScale, global::UnityEngine.Vector3 villainPositionOffset);
	}
}
