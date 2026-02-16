namespace Kampai.UI
{
	public class MailboxIconService : global::Kampai.UI.IMailboxIconService
	{
		private global::UnityEngine.Transform worldCanvasTransform;

		private global::UnityEngine.GameObject currentMailboxIcon;

		private global::Kampai.Game.View.BuildingObject mailboxBuildingGO;

		private global::Kampai.Game.MailboxBuildingDefinition mailboxBuildingDef;

		[Inject(global::Kampai.Main.MainElement.CONTEXT)]
		public global::strange.extensions.context.api.ICrossContextCapable MainContext { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService PlayerService { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject BuildingManager { get; set; }

		[PostConstruct]
		public void PostConstruct()
		{
			worldCanvasTransform = MainContext.injectionBinder.GetInstance<global::UnityEngine.GameObject>(global::Kampai.Main.MainElement.UI_WORLDCANVAS).transform;
			global::Kampai.Game.Building firstInstanceByDefinitionId = PlayerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.Building>(3089);
			mailboxBuildingDef = firstInstanceByDefinitionId.Definition as global::Kampai.Game.MailboxBuildingDefinition;
			if (firstInstanceByDefinitionId != null)
			{
				mailboxBuildingGO = BuildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>().GetBuildingObject(firstInstanceByDefinitionId.ID);
			}
		}

		public void CreateMailboxIcon()
		{
			if (mailboxBuildingGO != null && !MailboxIconExists())
			{
				currentMailboxIcon = global::UnityEngine.Object.Instantiate(global::Kampai.Util.KampaiResources.Load("cmp_MailboxIcon")) as global::UnityEngine.GameObject;
				currentMailboxIcon.transform.SetParent(worldCanvasTransform, false);
				currentMailboxIcon.transform.position = mailboxBuildingGO.IndicatorPosition;
			}
		}

		public bool MailboxIconExists()
		{
			return currentMailboxIcon != null;
		}

		public int GetRefreshFrequencyInSeconds()
		{
			return mailboxBuildingDef.RefreshFrequencyInSeconds;
		}

		public void RemoveMailboxIcon()
		{
			if (mailboxBuildingGO != null && MailboxIconExists())
			{
				global::UnityEngine.Object.Destroy(currentMailboxIcon);
				currentMailboxIcon = null;
			}
		}
	}
}
