namespace Kampai.UI.View
{
	public class CurrentQuestView : global::strange.extensions.mediation.impl.View
	{
		public global::Kampai.UI.View.KampaiImage icon;

		public global::Kampai.UI.View.MinionSlotModal MinionSlot;

		private global::UnityEngine.Animator animator;

		private global::Kampai.UI.IFancyUIService fancyUIService;

		private global::Kampai.Game.View.DummyCharacterObject characterObject;

		private global::System.Collections.IEnumerator moveCompleteCoRoutine;

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		internal void Init(global::Kampai.Game.Quest currentQuest, global::Kampai.UI.IFancyUIService fancyUIService, global::Kampai.UI.DummyCharacterType characterType)
		{
			this.fancyUIService = fancyUIService;
			animator = base.gameObject.GetComponent<global::UnityEngine.Animator>();
			global::UnityEngine.RectTransform rectTransform = MinionSlot.transform as global::UnityEngine.RectTransform;
			rectTransform.anchoredPosition3D = new global::UnityEngine.Vector3(rectTransform.anchoredPosition3D.x, rectTransform.anchoredPosition3D.y, rectTransform.anchoredPosition3D.z + -900f);
			characterObject = fancyUIService.CreateCharacter(characterType, global::Kampai.UI.DummyCharacterAnimationState.SelectedIdle, MinionSlot.transform, MinionSlot.VillainScale, MinionSlot.VillainPositionOffset, currentQuest.GetActiveDefinition().SurfaceID);
		}

		internal void UpdateQuest(global::Kampai.Game.Quest currentQuest, global::Kampai.UI.DummyCharacterType characterType)
		{
			animator.Play("Close");
			moveCompleteCoRoutine = MoveComplete(currentQuest, characterType);
			routineRunner.StartCoroutine(moveCompleteCoRoutine);
		}

		public global::System.Collections.IEnumerator MoveComplete(global::Kampai.Game.Quest currentQuest, global::Kampai.UI.DummyCharacterType characterType)
		{
			yield return new global::UnityEngine.WaitForSeconds(1f);
			RemoveCoroutine();
			if (MinionSlot != null)
			{
				characterObject = fancyUIService.CreateCharacter(characterType, global::Kampai.UI.DummyCharacterAnimationState.SelectedIdle, MinionSlot.transform, MinionSlot.VillainScale, MinionSlot.VillainPositionOffset, currentQuest.GetActiveDefinition().SurfaceID);
				animator.Play("Open");
			}
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
