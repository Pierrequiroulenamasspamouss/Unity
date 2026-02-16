public class RefreshSaleItemsCommand : global::strange.extensions.command.impl.Command
{
	private global::Kampai.Game.MarketplaceRefreshTimer refreshTimer;

	[Inject]
	public global::Kampai.Game.StartMarketplaceRefreshTimerSignal startRefreshTimerSignal { get; set; }

	[Inject]
	public global::Kampai.Game.GenerateBuyItemsSignal generateBuyItemsSignal { get; set; }

	[Inject]
	public global::Kampai.Game.IPlayerService playerService { get; set; }

	[Inject]
	public global::Kampai.Game.IDefinitionService definitionService { get; set; }

	[Inject]
	public global::Kampai.Game.ITimeService timeService { get; set; }

	[Inject]
	public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

	[Inject]
	public global::Kampai.Game.RefreshSaleItemsSuccessSignal refreshSuccessSignal { get; set; }

	[Inject]
	public global::Kampai.Game.RushRefreshTimerSuccessSignal rushSuccessSignal { get; set; }

	[Inject]
	public global::Kampai.Main.PlayGlobalSoundFXSignal playSFXSignal { get; set; }

	[Inject]
	public global::Kampai.Common.ITelemetryService telemetryService { get; set; }

	[Inject]
	public global::Kampai.Util.Tuple<bool, bool> refreshButtonStates { get; set; }

	public override void Execute()
	{
		refreshTimer = playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.MarketplaceRefreshTimer>(1000008093);
		int rushCost = refreshTimer.Definition.RushCost;
		if (refreshButtonStates.Item1)
		{
			playSFXSignal.Dispatch("Play_marketplace_slotStart_01");
		}
		if (refreshButtonStates.Item1)
		{
			RefreshMarketplace();
		}
		else if (!refreshButtonStates.Item2)
		{
			playerService.ProcessRefreshMarket(rushCost, true, RushTransactionCallback);
		}
	}

	private void RushTransactionCallback(global::Kampai.Game.PendingCurrencyTransaction pct)
	{
		if (pct.Success)
		{
			refreshTimer.UTCStartTime = timeService.GameTimeSeconds() - refreshTimer.Definition.RefreshTimeSeconds;
			rushSuccessSignal.Dispatch();
			setPremiumCurrencySignal.Dispatch();
		}
	}

	private void RefreshMarketplace()
	{
		generateBuyItemsSignal.Dispatch();
		refreshSuccessSignal.Dispatch();
		telemetryService.Send_Telemtry_EVT_MARKETPLACE_VIEWED("REFRESH");
	}
}
