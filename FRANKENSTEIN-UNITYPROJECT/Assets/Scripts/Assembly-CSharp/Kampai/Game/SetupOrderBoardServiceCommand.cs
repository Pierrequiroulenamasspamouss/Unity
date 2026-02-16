namespace Kampai.Game
{
	public class SetupOrderBoardServiceCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.IOrderBoardService orderBoardService { get; set; }

		[Inject]
		public global::Kampai.Game.OrderBoard board { get; set; }

		public override void Execute()
		{
			orderBoardService.Initialize(board);
		}
	}
}
