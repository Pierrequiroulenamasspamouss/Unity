namespace Kampai.Game
{
	public class UnleashCharacterAtShoreCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Character minionCharacter { get; set; }

		[Inject]
		public int openSlot { get; set; }

		[Inject(global::Kampai.Game.GameElement.NAMED_CHARACTER_MANAGER)]
		public global::UnityEngine.GameObject namedCharacterManager { get; set; }

		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject]
		public global::Kampai.Game.PhilPlayIntroSignal playIntroSignal { get; set; }

		[Inject]
		public global::Kampai.Game.PopUnleashedCharacterToTikiBarSignal popCustomerToTikiBarSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.View.CharacterObject characterObject = null;
			if (minionCharacter is global::Kampai.Game.Minion)
			{
				global::Kampai.Game.View.MinionManagerView component = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
				characterObject = component.Get(minionCharacter.ID);
			}
			else if (minionCharacter is global::Kampai.Game.NamedCharacter)
			{
				global::Kampai.Game.View.NamedCharacterManagerView component2 = namedCharacterManager.GetComponent<global::Kampai.Game.View.NamedCharacterManagerView>();
				characterObject = component2.Get(minionCharacter.ID);
			}
			if (characterObject == null)
			{
				logger.Error("AddMinionToTikiBarSlot: ao as MinionObject and NamedCharacterObject == null");
				return;
			}
			popCustomerToTikiBarSignal.Dispatch(characterObject, openSlot);
			playIntroSignal.Dispatch();
		}
	}
}
