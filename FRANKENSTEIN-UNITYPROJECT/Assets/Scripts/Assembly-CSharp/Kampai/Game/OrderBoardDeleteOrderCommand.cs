namespace Kampai.Game
{
	public class OrderBoardDeleteOrderCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.OrderBoard building { get; set; }

		[Inject]
		public int TicketIndex { get; set; }

		[Inject]
		public global::Kampai.Game.Transaction.TransactionDefinition def { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeEventService timeEventService { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardStartRefillTicketSignal startRefillTicketSignal { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoardRefillTicketSignal refillTicketSignal { get; set; }

		[Inject]
		public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

		public override void Execute()
		{
			int startTime = timeService.GameTimeSeconds();
			DeleteTicket(startTime);
		}

		private void DeleteTicket(int startTime)
		{
			foreach (global::Kampai.Game.OrderBoardTicket ticket in building.tickets)
			{
				if (ticket.BoardIndex == TicketIndex)
				{
					ticket.StartTime = startTime;
					break;
				}
			}
			int resurfaceTime = building.Definition.LevelBands[building.CurrentLevelBandIndex].ResurfaceTime;
			timeEventService.AddEvent(-TicketIndex, timeService.GameTimeSeconds(), resurfaceTime, refillTicketSignal);
			startRefillTicketSignal.Dispatch(new global::Kampai.Util.Tuple<int, int, float>(TicketIndex, resurfaceTime, building.Definition.TicketRepopTime));
			global::Kampai.Game.OrderBoardTicket orderBoardTicket = building.tickets[TicketIndex];
			telemetryService.Send_TelemetryOrderBoard(false, def, orderBoardTicket.CharacterDefinitionId, global::System.Enum.GetName(typeof(global::Kampai.Game.OrderBoardTicketDifficulty), orderBoardTicket.Difficulty));
		}
	}
}
