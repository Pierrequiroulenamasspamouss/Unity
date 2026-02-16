namespace Kampai.Game.View
{
	public class TSMCharacterMediator : global::strange.extensions.mediation.impl.Mediator
	{
		private global::Kampai.Game.View.NamedCharacterManagerView namedCharacterManagerView;

		[Inject]
		public global::Kampai.Game.View.TSMCharacterView View { get; set; }

		[Inject]
		public global::Kampai.Game.HideTSMCharacterSignal HideTSMCharacterSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.NAMED_CHARACTER_MANAGER)]
		public global::UnityEngine.GameObject NamedCharacterManager { get; set; }

		public override void OnRegister()
		{
			namedCharacterManagerView = NamedCharacterManager.GetComponent<global::Kampai.Game.View.NamedCharacterManagerView>();
			HideTSMCharacterSignal.AddListener(HideTSMCharacter);
			View.RemoveCharacterSignal.AddListener(RemoveTSMCharacter);
			Init();
		}

		public override void OnRemove()
		{
			HideTSMCharacterSignal.RemoveListener(HideTSMCharacter);
			View.RemoveCharacterSignal.RemoveListener(RemoveTSMCharacter);
		}

		private void Init()
		{
			View.ShowTSMCharacter();
		}

		private void HideTSMCharacter(bool completedQuest)
		{
			View.HideTSMCharacter(completedQuest);
		}

		private void RemoveTSMCharacter()
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			namedCharacterManagerView.Remove(301);
		}
	}
}
