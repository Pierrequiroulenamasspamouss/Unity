public class RestoreNamedCharacterCommand : global::strange.extensions.command.impl.Command
{
	[Inject]
	public global::Kampai.Game.NamedCharacter character { get; set; }

	[Inject]
	public global::Kampai.Game.Prestige prestige { get; set; }

	[Inject]
	public global::Kampai.Util.ILogger logger { get; set; }

	[Inject]
	public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

	[Inject]
	public global::Kampai.Game.CreateNamedCharacterViewSignal createSignal { get; set; }

	[Inject]
	public global::Kampai.Game.PhilSitAtBarSignal sitAtBarSignal { get; set; }

	[Inject]
	public global::Kampai.Game.RestoreMinionAtTikiBarSignal restoreMinionAtTikiBarSignal { get; set; }

	[Inject]
	public global::Kampai.Game.SocialEventAvailableSignal socialEventAvailableSignal { get; set; }

	[Inject]
	public global::Kampai.Game.IQuestService questService { get; set; }

	public override void Execute()
	{
		if (character.Definition.Type != global::Kampai.Game.NamedCharacterType.TSM)
		{
			logger.Info("Restoring Character: {0} with prestige state: {1}", character.Name, prestige.state);
			createSignal.Dispatch(character);
			routineRunner.StartCoroutine(WaitAFrame());
		}
	}

	private global::System.Collections.IEnumerator WaitAFrame()
	{
		yield return null;
		global::Kampai.Game.PrestigeState state = prestige.state;
		if (state == global::Kampai.Game.PrestigeState.Questing)
		{
			if (character.Definition.Type == global::Kampai.Game.NamedCharacterType.PHIL)
			{
				sitAtBarSignal.Dispatch(true);
			}
			else
			{
				restoreMinionAtTikiBarSignal.Dispatch(character);
			}
		}
		if (character.Definition.Type == global::Kampai.Game.NamedCharacterType.STUART && (state == global::Kampai.Game.PrestigeState.Taskable || prestige.CurrentPrestigeLevel > 0) && questService.IsQuestCompleted(31075))
		{
			socialEventAvailableSignal.Dispatch(true);
		}
	}
}
