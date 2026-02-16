namespace Kampai.UI.View
{
	public class LoadDefinitionForUICommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.IDefinitionService service { get; set; }

		[Inject]
		public global::Kampai.UI.View.BuildMenuDefinitionLoadedSignal buildMenuLoadedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.CurrencyMenuDefinitionLoadedSignal currenctMenuLoadedSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.AddStoreTabSignal addTabSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetLevelSignal setLevelSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetXPSignal setXPSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetGrindCurrencySignal setGrindCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetPremiumCurrencySignal setPremiumCurrencySignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.SetStorageCapacitySignal setStorageSignal { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Util.ICoroutineProgressMonitor coroutineProgressMonitor { get; set; }

		public override void Execute()
		{
			logger.EventStart("LoadDefinitionForUICommand.Execute");
			global::Kampai.Util.TimeProfiler.StartSection("load ui defs");
			global::Kampai.Util.TimeProfiler.StartSection("parse");
			global::Kampai.Util.TimeProfiler.StartSection("load");
			global::System.Collections.Generic.IList<global::Kampai.Game.StoreItemDefinition> all = service.GetAll<global::Kampai.Game.StoreItemDefinition>();
			global::Kampai.Util.TimeProfiler.EndSection("load");
			global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<global::Kampai.Game.Definition>> dictionary = new global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<global::Kampai.Game.Definition>>();
			global::System.Collections.Generic.List<global::Kampai.Game.StoreItemDefinition> list = new global::System.Collections.Generic.List<global::Kampai.Game.StoreItemDefinition>();
			foreach (global::Kampai.Game.StoreItemDefinition item in all)
			{
				switch (item.Type)
				{
				case global::Kampai.Game.StoreItemType.BaseResource:
				case global::Kampai.Game.StoreItemType.Crafting:
				case global::Kampai.Game.StoreItemType.Decoration:
				case global::Kampai.Game.StoreItemType.Leisure:
				case global::Kampai.Game.StoreItemType.Special:
					if (!dictionary.ContainsKey(item.Type))
					{
						dictionary[item.Type] = new global::System.Collections.Generic.List<global::Kampai.Game.Definition>();
					}
					if (item.OnlyShowIfInInventory)
					{
						dictionary[item.Type].Insert(0, item);
					}
					else
					{
						dictionary[item.Type].Add(item);
					}
					break;
				case global::Kampai.Game.StoreItemType.PremiumCurrency:
				case global::Kampai.Game.StoreItemType.GrindCurrency:
					list.Add(item);
					break;
				}
			}
			global::Kampai.Util.TimeProfiler.EndSection("parse");
			logger.EventStart("LoadDefinitionForUICommand.BuildMenu");
			routineRunner.StartCoroutine(WaitAFrame(dictionary, list));
			logger.EventStop("LoadDefinitionForUICommand.Execute");
		}

		private global::System.Collections.IEnumerator WaitAFrame(global::System.Collections.Generic.Dictionary<global::Kampai.Game.StoreItemType, global::System.Collections.Generic.List<global::Kampai.Game.Definition>> buildMenuItems, global::System.Collections.Generic.List<global::Kampai.Game.StoreItemDefinition> storeItems)
		{
			yield return null;
			while (coroutineProgressMonitor.HasRunningTasks())
			{
				yield return null;
			}
			global::Kampai.Util.TimeProfiler.StartSection("signals");
			if (buildMenuItems.ContainsKey(global::Kampai.Game.StoreItemType.BaseResource))
			{
				addTabSignal.Dispatch(new global::Kampai.UI.View.StoreTab(localService.GetString("MakeStuff"), global::Kampai.Game.StoreItemType.BaseResource, 0, false));
			}
			if (buildMenuItems.ContainsKey(global::Kampai.Game.StoreItemType.Crafting))
			{
				addTabSignal.Dispatch(new global::Kampai.UI.View.StoreTab(localService.GetString("MixStuff"), global::Kampai.Game.StoreItemType.Crafting, 0, false));
			}
			if (buildMenuItems.ContainsKey(global::Kampai.Game.StoreItemType.Decoration))
			{
				addTabSignal.Dispatch(new global::Kampai.UI.View.StoreTab(localService.GetString("DecorateStuff"), global::Kampai.Game.StoreItemType.Decoration, 0, false));
			}
			if (buildMenuItems.ContainsKey(global::Kampai.Game.StoreItemType.Leisure))
			{
				addTabSignal.Dispatch(new global::Kampai.UI.View.StoreTab(localService.GetString("LeisureStuff"), global::Kampai.Game.StoreItemType.Leisure, 0, false));
			}
			if (buildMenuItems.ContainsKey(global::Kampai.Game.StoreItemType.Special))
			{
				addTabSignal.Dispatch(new global::Kampai.UI.View.StoreTab(localService.GetString("OtherStuff"), global::Kampai.Game.StoreItemType.Special, 0, false));
			}
			buildMenuLoadedSignal.Dispatch(buildMenuItems);
			currenctMenuLoadedSignal.Dispatch(storeItems);
			setLevelSignal.Dispatch();
			setXPSignal.Dispatch();
			setGrindCurrencySignal.Dispatch();
			setPremiumCurrencySignal.Dispatch();
			setStorageSignal.Dispatch();
			global::Kampai.Util.TimeProfiler.EndSection("signals");
			global::Kampai.Util.TimeProfiler.EndSection("load ui defs");
			logger.EventStop("LoadDefinitionForUICommand.BuildMenu");
		}
	}
}
