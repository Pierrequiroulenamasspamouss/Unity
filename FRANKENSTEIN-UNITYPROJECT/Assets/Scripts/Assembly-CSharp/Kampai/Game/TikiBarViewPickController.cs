namespace Kampai.Game
{
	public class TikiBarViewPickController : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::UnityEngine.GameObject EndHitObject { get; set; }

		[Inject]
		public global::Kampai.Game.ITikiBarService TikiBarService { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger Logger { get; set; }

		[Inject]
		public global::Kampai.Game.DisplayStickerbookSignal DisplayStickerbookSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IPlayerService PlayerService { get; set; }

		[Inject]
		public global::Kampai.Game.IZoomCameraModel ZoomCameraModel { get; set; }

		[Inject]
		public global::Kampai.UI.View.GetWayFinderSignal GetWayFinderSignal { get; set; }

		[Inject]
		public global::Kampai.UI.View.ShowQuestPanelSignal ShowQuestPanelSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IQuestService QuestService { get; set; }

		[Inject(global::Kampai.Game.GameElement.NAMED_CHARACTER_MANAGER)]
		public global::UnityEngine.GameObject NamedCharacterManagerGO { get; set; }

		public override void Execute()
		{
			if (!ZoomCameraModel.ZoomedIn || ZoomCameraModel.LastZoomBuildingType != global::Kampai.Game.BuildingZoomType.TIKIBAR)
			{
				Logger.Warning("Camera is not zoomed in to tikibar building");
				return;
			}
			if (EndHitObject.name == "StampAlbum")
			{
				Logger.Debug("StampAlbum was clicked in Tiki bar view!");
				DisplayStickerbookSignal.Dispatch();
				return;
			}
			if (EndHitObject.name == "Shelve")
			{
				Logger.Debug("Shelve was clicked in Tiki bar view!");
				HandlePhil(NamedCharacterManagerGO.GetComponent<global::Kampai.Game.View.NamedCharacterManagerView>().Get(78));
				return;
			}
			global::Kampai.Game.View.CharacterObject characterObject = EndHitObject.GetComponentInParent<global::Kampai.Game.View.CharacterObject>();
			if (characterObject != null)
			{
				Logger.Debug("{0} was clicked in Tiki bar view!", characterObject.name);
				if (characterObject.ID == 78)
				{
					HandlePhil(characterObject);
					return;
				}
				GetWayFinderSignal.Dispatch(characterObject.ID, delegate(int trackedId, global::Kampai.UI.View.IWayFinderView wayFinderView)
				{
					if (wayFinderView != null)
					{
						ClickedOnCharacter(characterObject, wayFinderView);
					}
					else
					{
						ShowQuestBook();
					}
				});
			}
			else
			{
				Logger.Debug("{0} clicked event was ignored since they are not a named minion!", EndHitObject.name);
			}
		}

		private void HandlePhil(global::Kampai.Game.View.CharacterObject characterObject)
		{
			GetWayFinderSignal.Dispatch(78, delegate(int trackedId, global::Kampai.UI.View.IWayFinderView wayFinderView)
			{
				if (wayFinderView != null)
				{
					ClickedOnCharacter(characterObject, wayFinderView);
				}
				else
				{
					ShowQuestBook();
				}
			});
		}

		private void ShowQuestBook()
		{
			foreach (global::Kampai.Game.Quest value in QuestService.GetQuestMap().Values)
			{
				global::Kampai.Game.QuestDefinition activeDefinition = value.GetActiveDefinition();
				if (activeDefinition.SurfaceType != global::Kampai.Game.QuestSurfaceType.Automatic && activeDefinition.SurfaceType != global::Kampai.Game.QuestSurfaceType.ProcedurallyGenerated && value.state == global::Kampai.Game.QuestState.RunningTasks)
				{
					ShowQuestPanelSignal.Dispatch(value.ID);
					break;
				}
			}
		}

		private void ClickedOnCharacter(global::Kampai.Game.View.CharacterObject characterObject, global::Kampai.UI.View.IWayFinderView wayFinder)
		{
			int iD = characterObject.ID;
			global::Kampai.Game.Character byInstanceId = PlayerService.GetByInstanceId<global::Kampai.Game.Character>(iD);
			if (byInstanceId == null)
			{
				Logger.Warning("Could not find named character for instance id:{0} ", iD);
			}
			else if (TikiBarService.IsCharacterSitting(byInstanceId))
			{
				wayFinder.SimulateClick();
			}
			else
			{
				Logger.Warning("Ignoring clicks to {0} since they are not sitting", characterObject.name);
			}
		}
	}
}
