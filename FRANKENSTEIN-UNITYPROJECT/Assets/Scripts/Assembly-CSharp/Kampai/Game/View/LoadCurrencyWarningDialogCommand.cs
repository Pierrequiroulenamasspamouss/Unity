namespace Kampai.Game.View
{
	public class LoadCurrencyWarningDialogCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.UI.View.CurrencyWarningModel model { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowPremiumStoreSignal showPremiumStoreSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.UI.View.IGUICommand iGUICommand;
			if (model.Type == global::Kampai.Game.StoreItemType.PremiumCurrency)
			{
				if (!model.GrindFromPremium)
				{
					showPremiumStoreSignal.Dispatch();
					return;
				}
				iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Load, "PremiumCurrencyWarning");
			}
			else
			{
				iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.Load, "GrindCurrencyWarning");
			}
			iGUICommand.skrimScreen = "CurrencySkrim";
			iGUICommand.darkSkrim = true;
			iGUICommand.singleSkrimClose = true;
			iGUICommand.Args.Add(model);
			guiService.Execute(iGUICommand);
		}
	}
}
