namespace Kampai.UI
{
	public interface IFancyUIService
	{
		global::Kampai.Game.View.DummyCharacterObject CreateCharacter(global::Kampai.UI.DummyCharacterType type, global::Kampai.UI.DummyCharacterAnimationState startingState, global::UnityEngine.Transform parent, global::UnityEngine.Vector3 villainScale, global::UnityEngine.Vector3 villainPositionOffset, int prestigeDefinitionID = 0, bool isHighLOD = true, bool adjustMaterial = false);

		global::Kampai.UI.DummyCharacterType GetCharacterType(int prestigeDefinitionID);
	}
}
