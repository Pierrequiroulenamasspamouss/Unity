namespace Kampai.UI.View
{
	public class MoveBuildingMenuView : global::Kampai.UI.View.WorldToGlassView
	{
		internal global::Kampai.UI.View.ButtonView InventoryButton;

		internal global::Kampai.UI.View.ButtonView AcceptButton;

		internal global::Kampai.UI.View.ButtonView CloseButton;

		private global::UnityEngine.UI.Button inventoryButton;

		private global::UnityEngine.UI.Button acceptButton;

		private global::UnityEngine.UI.Button closeButton;

		protected override string UIName
		{
			get
			{
				return "MoveBuildingMenu";
			}
		}

		protected override void LoadModalData(global::Kampai.UI.View.WorldToGlassUIModal modal)
		{
			global::Kampai.UI.View.MoveBuildingModal moveBuildingModal = modal as global::Kampai.UI.View.MoveBuildingModal;
			global::Kampai.UI.View.MoveBuildingSetting moveBuildingSetting = moveBuildingModal.Settings as global::Kampai.UI.View.MoveBuildingSetting;
			InventoryButton = moveBuildingModal.InventoryButton;
			AcceptButton = moveBuildingModal.AcceptButton;
			CloseButton = moveBuildingModal.CloseButton;
			inventoryButton = InventoryButton.GetComponent<global::UnityEngine.UI.Button>();
			acceptButton = AcceptButton.GetComponent<global::UnityEngine.UI.Button>();
			closeButton = CloseButton.GetComponent<global::UnityEngine.UI.Button>();
			if (moveBuildingSetting.Mask == 1)
			{
				inventoryButton.interactable = false;
			}
		}

		public override global::UnityEngine.Vector3 GetIndicatorPosition()
		{
			global::Kampai.Game.View.BuildingObject buildingObject = targetObject as global::Kampai.Game.View.BuildingObject;
			if (buildingObject != null)
			{
				return buildingObject.ResourceIconPosition + global::Kampai.Util.GameConstants.UI.MOVE_MENU_OFFSET;
			}
			return global::UnityEngine.Vector3.zero;
		}

		internal override void OnUpdatePosition(global::Kampai.UI.PositionData positionData)
		{
			global::Kampai.UI.SnappablePositionData snappablePositionData = positionService.GetSnappablePositionData(positionData, global::Kampai.UI.ViewportBoundary.FULLSCREEN);
			m_transform.position = snappablePositionData.ClampedWorldPositionInUI;
			m_transform.localPosition = VectorUtils.ZeroZ(m_transform.localPosition);
		}

		internal void DisableInventory()
		{
			inventoryButton.interactable = false;
		}

		internal void SetButtonState(int mask)
		{
			if ((mask & 1) > 0)
			{
				inventoryButton.interactable = false;
			}
			else
			{
				inventoryButton.interactable = true;
			}
			if ((mask & 4) > 0)
			{
				acceptButton.interactable = false;
			}
			else
			{
				acceptButton.interactable = true;
			}
			if ((mask & 8) > 0)
			{
				closeButton.interactable = false;
			}
			else
			{
				closeButton.interactable = true;
			}
		}

		internal void UpdateValidity(bool enable)
		{
			acceptButton.interactable = enable;
		}
	}
}
