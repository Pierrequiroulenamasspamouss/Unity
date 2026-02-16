namespace Kampai.Game
{
	public class UpdateVillainPrestigeCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.Prestige prestige { get; set; }

		[Inject]
		public global::Kampai.Util.Tuple<global::Kampai.Game.PrestigeState, global::Kampai.Game.PrestigeState> states { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable uiContext { get; set; }

		[Inject]
		public global::Kampai.Game.QueueCabanaSignal updateCabanaSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MoveInCabanaSignal moveInSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MoveOutCabanaSignal moveOutSignal { get; set; }

		public override void Execute()
		{
			switch (states.Item2)
			{
			case global::Kampai.Game.PrestigeState.Prestige:
			case global::Kampai.Game.PrestigeState.Taskable:
				SwitchToPrestige();
				break;
			case global::Kampai.Game.PrestigeState.InQueue:
				SwitchToInQueue();
				break;
			case global::Kampai.Game.PrestigeState.Questing:
				SwitchToQuesting();
				break;
			}
		}

		private void SwitchToInQueue()
		{
			global::Kampai.Game.Villain villain;
			if (prestige.trackedInstanceId == 0)
			{
				global::Kampai.Game.VillainDefinition villainDefinition = definitionService.Get<global::Kampai.Game.VillainDefinition>(prestige.Definition.TrackedDefinitionID);
				if (villainDefinition == null)
				{
					logger.Error("Trying to create a villain instance, but this prestige ({0}) doesn't have a villain definition!", prestige);
					return;
				}
				villain = new global::Kampai.Game.Villain(villainDefinition);
				playerService.Add(villain);
				prestige.trackedInstanceId = villain.ID;
			}
			else
			{
				villain = playerService.GetByInstanceId<global::Kampai.Game.Villain>(prestige.trackedInstanceId);
			}
			uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowBuddyWelcomePanelUISignal>().Dispatch(new global::Kampai.Util.Boxed<global::Kampai.Game.Prestige>(prestige), global::Kampai.UI.View.CharacterWelcomeState.Welcome, 0);
			updateCabanaSignal.Dispatch(prestige, villain);
		}

		private void SwitchToQuesting()
		{
			moveInSignal.Dispatch(prestige);
		}

		private void SwitchToPrestige()
		{
			if (states.Item1 == global::Kampai.Game.PrestigeState.Questing)
			{
				uiContext.injectionBinder.GetInstance<global::Kampai.UI.View.ShowBuddyWelcomePanelUISignal>().Dispatch(new global::Kampai.Util.Boxed<global::Kampai.Game.Prestige>(prestige), global::Kampai.UI.View.CharacterWelcomeState.Farewell, 0);
				moveOutSignal.Dispatch(prestige);
			}
		}
	}
}
