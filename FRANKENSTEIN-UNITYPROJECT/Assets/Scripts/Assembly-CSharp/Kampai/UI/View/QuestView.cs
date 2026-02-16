namespace Kampai.UI.View
{
	public class QuestView : global::strange.extensions.mediation.impl.View
	{
		public ScrollableButtonView button;

		public global::Kampai.UI.View.MinionSlotModal MinionSlot;

		public global::Kampai.Game.Quest quest;

		public float PaddingInPixels;

		private global::UnityEngine.Animator animator;

		private global::Kampai.Util.IRoutineRunner routineRunner;

		private global::Kampai.Game.View.DummyCharacterObject characterObject;

		private global::Kampai.UI.IFancyUIService fancyUIService;

		private global::System.Collections.IEnumerator moveCompleteCoRoutine;

		internal void Init(global::Kampai.Util.IRoutineRunner routineRunner, global::Kampai.UI.IFancyUIService fancyUIService)
		{
			this.routineRunner = routineRunner;
			this.fancyUIService = fancyUIService;
			animator = GetComponent<global::UnityEngine.Animator>();
			global::Kampai.UI.DummyCharacterType characterType = fancyUIService.GetCharacterType(quest.GetActiveDefinition().SurfaceID);
			characterObject = fancyUIService.CreateCharacter(characterType, global::Kampai.UI.DummyCharacterAnimationState.Idle, MinionSlot.transform, MinionSlot.VillainScale, MinionSlot.VillainPositionOffset, quest.GetActiveDefinition().SurfaceID, true, true);
		}

		internal void UpdateQuest()
		{
			animator.Play("Close");
			moveCompleteCoRoutine = MoveComplete();
			routineRunner.StartCoroutine(moveCompleteCoRoutine);
		}

		public global::System.Collections.IEnumerator MoveComplete()
		{
			yield return new global::UnityEngine.WaitForSeconds(1f);
			button.GetComponent<global::UnityEngine.UI.Button>().interactable = true;
			RemoveCoroutine();
			global::Kampai.UI.DummyCharacterType characterType = fancyUIService.GetCharacterType(quest.GetActiveDefinition().SurfaceID);
			characterObject = fancyUIService.CreateCharacter(characterType, global::Kampai.UI.DummyCharacterAnimationState.Idle, MinionSlot.transform, MinionSlot.VillainScale, MinionSlot.VillainPositionOffset, quest.GetActiveDefinition().SurfaceID, true, true);
			animator.Play("Open");
			moveCompleteCoRoutine = null;
		}

		internal void RemoveCoroutine()
		{
			if (moveCompleteCoRoutine != null)
			{
				routineRunner.StopCoroutine(moveCompleteCoRoutine);
				moveCompleteCoRoutine = null;
			}
			if (characterObject != null)
			{
				characterObject.RemoveCoroutine();
				global::UnityEngine.Object.Destroy(characterObject.gameObject);
			}
		}
	}
}
