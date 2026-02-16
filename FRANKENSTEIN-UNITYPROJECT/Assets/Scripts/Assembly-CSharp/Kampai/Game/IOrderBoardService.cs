namespace Kampai.Game
{
	public interface IOrderBoardService
	{
		void Initialize(global::Kampai.Game.OrderBoard orderBoard);

		void ReplaceCharacterTickets(int characterDefinitionID);

		void AddPriorityPrestigeCharacter(int prestigeDefinitionID);

		void GetNewTicket(int orderBoardIndex);

		void UpdateLevelBand();

		global::Kampai.Game.OrderBoard GetBoard();

		void SetEnabled(bool b);
	}
}
