namespace Kampai.UI.View
{
	public abstract class AbstractGenericPopupMediator<T> : global::Kampai.UI.View.KampaiMediator where T : global::Kampai.UI.View.GenericPopupView
	{
		[Inject]
		public T view { get; set; }

		[Inject]
		public global::Kampai.Main.ILocalizationService localizationService { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject]
		public global::Kampai.UI.View.DisplayItemPopupSignal popupSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.HideItemPopupSignal closeSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayGlobalSoundFXSignal soundFXSignal { get; set; }

		public override void OnRegister()
		{
			popupSignal.AddListener(ForceClose);
			closeSignal.AddListener(Close);
			view.OnMenuClose.AddListener(OnMenuClose);
		}

		public override void OnRemove()
		{
			popupSignal.RemoveListener(ForceClose);
			closeSignal.RemoveListener(Close);
			view.OnMenuClose.RemoveListener(OnMenuClose);
		}

		public override void Initialize(global::Kampai.UI.View.GUIArguments args)
		{
			soundFXSignal.Dispatch("Play_menu_popUp_02");
			global::Kampai.Game.ItemDefinition itemDef = args.Get<global::Kampai.Game.ItemDefinition>();
			global::UnityEngine.Vector3 itemCenter = args.Get<global::UnityEngine.Vector3>();
			Register(itemDef, itemCenter);
		}

		public virtual void Register(global::Kampai.Game.ItemDefinition itemDef, global::UnityEngine.Vector3 itemCenter)
		{
			T val = view;
			val.Display(itemCenter);
			T val2 = view;
			val2.SetName(localizationService.GetString(itemDef.LocalizedKey));
			string itemOrigin = string.Empty;
			global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition = itemDef as global::Kampai.Game.IngredientsItemDefinition;
			if (ingredientsItemDefinition != null)
			{
				T val3 = view;
				val3.SetTime((int)ingredientsItemDefinition.TimeToHarvest);
				int buildingDefintionIDFromItemDefintionID = definitionService.GetBuildingDefintionIDFromItemDefintionID(itemDef.ID);
				itemOrigin = localizationService.GetString(definitionService.Get<global::Kampai.Game.BuildingDefinition>(buildingDefintionIDFromItemDefintionID).LocalizedKey);
			}
			else
			{
				global::Kampai.Game.DropItemDefinition dropItemDefinition = itemDef as global::Kampai.Game.DropItemDefinition;
				if (dropItemDefinition != null)
				{
					itemOrigin = localizationService.GetString("StorageBuildingTooltipRandomDrop");
					T val4 = view;
					val4.DisableDurationInfo();
				}
			}
			T val5 = view;
			val5.SetItemOrigin(itemOrigin);
		}

		private void ForceClose(int ignore, global::UnityEngine.RectTransform these, global::Kampai.UI.View.UIPopupType variables)
		{
			Close();
		}

		private void Close()
		{
			soundFXSignal.Dispatch("Play_menu_disappear_01");
			T val = view;
			val.Close();
		}

		private void OnMenuClose()
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
