namespace Kampai.UI.View
{
	public class ResourceIconView : global::Kampai.UI.View.WorldToGlassView
	{
		internal global::strange.extensions.signal.impl.Signal RemoveResourceIconSignal = new global::strange.extensions.signal.impl.Signal();

		internal int ItemDefID;

		protected global::Kampai.UI.View.KampaiImage m_image;

		protected global::UnityEngine.UI.Text m_text;

		protected global::Kampai.UI.View.KampaiImage m_textBackground;

		private global::UnityEngine.Vector3 localScale;

		private bool hasText;

		private global::UnityEngine.Vector3 iconIndexOffset;

		private global::Kampai.Game.IDefinitionService definitionService;

		public global::strange.extensions.signal.impl.Signal ClickedSignal { get; private set; }

		public float IconIndex { get; private set; }

		protected override string UIName
		{
			get
			{
				return "ResourceIcon";
			}
		}

		internal void Init(global::strange.extensions.context.api.ICrossContextCapable gameContext, global::Kampai.Util.ILogger logger, global::Kampai.Game.IPlayerService playerService, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.UI.IPositionService positionService, global::Kampai.Main.ILocalizationService localizationService)
		{
			this.definitionService = definitionService;
			Init(positionService, gameContext, logger, playerService, localizationService);
			localScale = m_transform.localScale;
		}

		protected override void LoadModalData(global::Kampai.UI.View.WorldToGlassUIModal modal)
		{
			global::Kampai.UI.View.ResourceIconModal resourceIconModal = modal as global::Kampai.UI.View.ResourceIconModal;
			if (resourceIconModal == null)
			{
				logger.Error("Resource modal doesn't exist!");
				return;
			}
			global::Kampai.UI.View.ResourceIconSettings resourceIconSettings = resourceIconModal.Settings as global::Kampai.UI.View.ResourceIconSettings;
			ClickedSignal = resourceIconModal.ClickedSignal;
			m_image = resourceIconModal.Image;
			m_text = resourceIconModal.Text;
			m_textBackground = resourceIconModal.TextBackground;
			UpdateIconCount(resourceIconSettings.Count);
			ItemDefID = resourceIconSettings.ItemDefId;
			global::Kampai.Game.Building byInstanceId = playerService.GetByInstanceId<global::Kampai.Game.Building>(m_trackedId);
			global::Kampai.Game.ItemDefinition itemDefinition = definitionService.Get<global::Kampai.Game.ItemDefinition>(ItemDefID);
			if (byInstanceId != null && itemDefinition != null)
			{
				UpdateView(byInstanceId.Definition, itemDefinition);
				return;
			}
			logger.Warning("Unable to find building with id: {0} or itemDef with id: {1} ", m_trackedId, ItemDefID);
		}

		private void UpdateView(global::Kampai.Game.BuildingDefinition buildingDef, global::Kampai.Game.ItemDefinition itemDef)
		{
			if (buildingDef is global::Kampai.Game.DebrisBuildingDefinition)
			{
				HideText();
				hasText = false;
			}
			else
			{
				m_image.color = global::UnityEngine.Color.white;
				m_image.sprite = UIUtils.LoadSpriteFromPath(itemDef.Image);
				m_image.maskSprite = UIUtils.LoadSpriteFromPath(itemDef.Mask);
				hasText = true;
			}
			global::Kampai.Game.TaskableBuildingDefinition taskableBuildingDefinition = buildingDef as global::Kampai.Game.TaskableBuildingDefinition;
			global::Kampai.Game.CraftingBuildingDefinition craftingBuildingDefinition = buildingDef as global::Kampai.Game.CraftingBuildingDefinition;
			if (taskableBuildingDefinition != null)
			{
				UIOffset = taskableBuildingDefinition.HarvestableIconOffset;
			}
			else if (craftingBuildingDefinition != null)
			{
				UIOffset = craftingBuildingDefinition.HarvestableIconOffset;
			}
		}

		public void UpdateIconIndex(float iconIndex)
		{
			IconIndex = iconIndex;
			iconIndexOffset = new global::UnityEngine.Vector3(1f * IconIndex, 0f, 0f);
		}

		internal void UpdateIconCount(int count)
		{
			string text;
			switch (count)
			{
			case 0:
				RemoveResourceIconSignal.Dispatch();
				return;
			case 1:
				text = "x 1";
				break;
			default:
				text = string.Format("x {0}", count);
				break;
			}
			m_text.text = text;
		}

		internal void HighlightHarvest(bool isHighlighted)
		{
			if (isHighlighted)
			{
				global::Kampai.Util.TweenUtil.Throb(m_image.transform, 0.85f, 0.5f, out localScale);
				return;
			}
			Go.killAllTweensWithTarget(m_image.transform);
			m_image.transform.localScale = localScale;
		}

		internal override void TargetObjectNullResponse()
		{
			logger.Warning("Removing Resource Icon with id: {0} since the target object does not exist anymore!", m_trackedId);
			RemoveResourceIconSignal.Dispatch();
		}

		public override global::UnityEngine.Vector3 GetIndicatorPosition()
		{
			global::Kampai.Game.View.BuildingObject buildingObject = targetObject as global::Kampai.Game.View.BuildingObject;
			if (buildingObject != null)
			{
				return buildingObject.ResourceIconPosition;
			}
			global::Kampai.Game.View.CharacterObject characterObject = targetObject as global::Kampai.Game.View.CharacterObject;
			if (characterObject != null)
			{
				return characterObject.GetIndicatorPosition();
			}
			return global::UnityEngine.Vector3.zero;
		}

		internal override void OnUpdatePosition(global::Kampai.UI.PositionData positionData)
		{
			m_transform.position = positionData.WorldPositionInUI + iconIndexOffset;
			m_transform.localPosition = VectorUtils.ZeroZ(m_transform.localPosition);
		}

		protected override void OnHide()
		{
			base.OnHide();
			HideText();
		}

		protected override void OnShow()
		{
			base.OnShow();
			ShowText();
		}

		private void ShowText()
		{
			if (hasText)
			{
				m_text.enabled = true;
				m_image.enabled = true;
				m_textBackground.enabled = true;
			}
		}

		private void HideText()
		{
			if (hasText)
			{
				m_text.enabled = false;
				m_image.enabled = false;
				m_textBackground.enabled = false;
			}
		}
	}
}
