namespace Kampai.UI.View
{
	public class SellPanelView : global::Kampai.UI.View.PopupMenuView
	{
		public global::Kampai.UI.View.ButtonView ArrowButtonView;

		public global::Kampai.UI.View.KampaiScrollView ScrollView;

		public global::Kampai.UI.View.CreateNewSellView CreateSaleView;

		internal bool isOpen;

		internal global::strange.extensions.signal.impl.Signal OnOpenPanelSignal = new global::strange.extensions.signal.impl.Signal();

		internal bool isCreateNewSalePanelOpened;

		internal void FadeAnimation(bool fade)
		{
			isCreateNewSalePanelOpened = fade;
			if (animator != null)
			{
				animator.Play((!fade) ? "CloseSidePanel" : "OpenSidePanel");
			}
		}

		internal void SetOpen(bool show, bool isInstant = false)
		{
			if (show)
			{
				if (isInstant)
				{
					float normalizedTime = 1f;
					int layer = -1;
					animator.Play("Open", layer, normalizedTime);
					isOpened = true;
				}
				else
				{
					Open();
				}
				OnOpenPanelSignal.Dispatch();
			}
			else
			{
				Close();
			}
			isOpen = show;
		}

		public void FadeOutItems()
		{
			FadeItems(false);
		}

		public void FadeInItems()
		{
			FadeItems(true);
		}

		private void FadeItems(bool fadeIn)
		{
			foreach (global::strange.extensions.mediation.impl.View itemView in ScrollView.ItemViewList)
			{
				global::Kampai.UI.View.StorageBuildingSaleSlotView storageBuildingSaleSlotView = itemView as global::Kampai.UI.View.StorageBuildingSaleSlotView;
				if (!(storageBuildingSaleSlotView == null))
				{
					if (fadeIn)
					{
						storageBuildingSaleSlotView.FadeIn();
					}
					else
					{
						storageBuildingSaleSlotView.FadeOut();
					}
				}
			}
		}

		internal bool HasSlot(global::Kampai.Game.MarketplaceSaleSlot slot)
		{
			foreach (global::strange.extensions.mediation.impl.View item in ScrollView)
			{
				global::Kampai.UI.View.StorageBuildingSaleSlotView storageBuildingSaleSlotView = item as global::Kampai.UI.View.StorageBuildingSaleSlotView;
				if (storageBuildingSaleSlotView == null || slot.ID != storageBuildingSaleSlotView.slotId)
				{
					continue;
				}
				return true;
			}
			return false;
		}
	}
}
