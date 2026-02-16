namespace Kampai.Game
{
	public class OrderBoardTicket
	{
		public global::Kampai.Game.Transaction.TransactionInstance TransactionInst { get; set; }

		public int BoardIndex { get; set; }

		public int OrderNameTableIndex { get; set; }

		public int StartTime { get; set; }

		public int CharacterDefinitionId { get; set; }

		public global::Kampai.Game.OrderBoardTicketDifficulty Difficulty { get; set; }
	}
}
