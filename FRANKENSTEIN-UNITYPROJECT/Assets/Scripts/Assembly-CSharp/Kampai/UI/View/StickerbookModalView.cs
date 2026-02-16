namespace Kampai.UI.View
{
	public class StickerbookModalView : global::Kampai.UI.View.PopupMenuView
	{
		public global::UnityEngine.RectTransform characterPanel;

		public global::UnityEngine.RectTransform stickerPanel;

		public global::UnityEngine.UI.Text stickerTitle;

		public float padding = 10f;

		private global::System.Collections.Generic.List<int> characterList = new global::System.Collections.Generic.List<int>();

		private global::System.Collections.Generic.List<int> displayableLimitedTimeEvents;

		private global::System.Collections.Generic.List<global::Kampai.UI.View.StickerbookCharacterView> characterViewList = new global::System.Collections.Generic.List<global::Kampai.UI.View.StickerbookCharacterView>();

		private global::System.Collections.Generic.List<global::UnityEngine.GameObject> stickerInstanceList = new global::System.Collections.Generic.List<global::UnityEngine.GameObject>();

		private global::UnityEngine.GameObject stickerPackPrefab;

		private global::UnityEngine.GameObject stickerPrefab;

		private float characterWidth;

		private float stickerWidth;

		private bool firstTime = true;

		internal int lastSelectedID;

		private int unlockedCharacterCount;

		public void Init(global::System.Collections.Generic.Dictionary<int, bool> characters, global::System.Collections.Generic.List<int> LTEs, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Game.IPlayerService playerService)
		{
			SetupCharacterList(characters, definitionService, playerService);
			displayableLimitedTimeEvents = LTEs;
			base.Init();
			CachePrefabs();
			PopulateCharacterScrollView();
			Open();
		}

		internal void Close()
		{
			foreach (global::Kampai.UI.View.StickerbookCharacterView characterView in characterViewList)
			{
				characterView.RemoveCoroutine();
			}
			Close(false);
		}

		private void SetupCharacterList(global::System.Collections.Generic.Dictionary<int, bool> characters, global::Kampai.Game.IDefinitionService definitionService, global::Kampai.Game.IPlayerService playerService)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.Prestige> list = new global::System.Collections.Generic.List<global::Kampai.Game.Prestige>();
			global::System.Collections.Generic.List<global::Kampai.Game.PrestigeDefinition> list2 = new global::System.Collections.Generic.List<global::Kampai.Game.PrestigeDefinition>();
			foreach (global::System.Collections.Generic.KeyValuePair<int, bool> character in characters)
			{
				if (character.Value)
				{
					list.Add(playerService.GetFirstInstanceByDefinitionId<global::Kampai.Game.Prestige>(character.Key));
					unlockedCharacterCount++;
				}
				else
				{
					list2.Add(definitionService.Get<global::Kampai.Game.PrestigeDefinition>(character.Key));
				}
			}
			list.Sort((global::Kampai.Game.Prestige x, global::Kampai.Game.Prestige y) => y.UTCTimeUnlocked.CompareTo(x.UTCTimeUnlocked));
			list2.Sort((global::Kampai.Game.PrestigeDefinition x, global::Kampai.Game.PrestigeDefinition y) => x.PreUnlockLevel.CompareTo(y.PreUnlockLevel));
			foreach (global::Kampai.Game.Prestige item in list)
			{
				characterList.Add(item.Definition.ID);
			}
			foreach (global::Kampai.Game.PrestigeDefinition item2 in list2)
			{
				characterList.Add(item2.ID);
			}
		}

		private void CachePrefabs()
		{
			stickerPackPrefab = global::Kampai.Util.KampaiResources.Load("cmp_StickerPackCharacters") as global::UnityEngine.GameObject;
			stickerPrefab = global::Kampai.Util.KampaiResources.Load("cmp_Sticker") as global::UnityEngine.GameObject;
			characterWidth = (stickerPackPrefab.transform as global::UnityEngine.RectTransform).sizeDelta.x;
			stickerWidth = (stickerPrefab.transform as global::UnityEngine.RectTransform).sizeDelta.x;
		}

		private void PopulateCharacterScrollView()
		{
			int num = 0;
			int num2 = characterList.Count + displayableLimitedTimeEvents.Count;
			for (int i = 0; i < num2; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(stickerPackPrefab) as global::UnityEngine.GameObject;
				global::Kampai.UI.View.StickerbookCharacterView component = gameObject.GetComponent<global::Kampai.UI.View.StickerbookCharacterView>();
				global::UnityEngine.RectTransform rectTransform = gameObject.transform as global::UnityEngine.RectTransform;
				gameObject.transform.SetParent(characterPanel, false);
				if (i < characterList.Count)
				{
					component.prestigeID = characterList[i];
					component.isLimited = false;
					if (i < unlockedCharacterCount)
					{
						component.isLocked = false;
					}
					else
					{
						component.isLocked = true;
					}
				}
				else
				{
					component.character.gameObject.SetActive(false);
					component.limitedEvent.gameObject.SetActive(true);
					component.isLimited = true;
					component.limitedID = displayableLimitedTimeEvents[num];
					num++;
				}
				rectTransform.offsetMin = new global::UnityEngine.Vector2(characterWidth * (float)i + padding * (float)i, 0f);
				rectTransform.offsetMax = new global::UnityEngine.Vector2(characterWidth * (float)(i + 1) + padding * (float)i, 0f);
				characterViewList.Add(component);
			}
			characterPanel.sizeDelta = new global::UnityEngine.Vector2(characterWidth * (float)num2 + padding * (float)num2, 0f);
			characterPanel.localPosition = new global::UnityEngine.Vector2(0f, characterPanel.localPosition.y);
		}

		internal void PopulateStickersForCurrentCharacter(int unlockedStickerCount, global::System.Collections.Generic.List<int> stickerList)
		{
			CleanupExistingStickers();
			for (int i = 0; i < stickerList.Count; i++)
			{
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(stickerPrefab) as global::UnityEngine.GameObject;
				global::Kampai.UI.View.StickerbookStickerView component = gameObject.GetComponent<global::Kampai.UI.View.StickerbookStickerView>();
				global::UnityEngine.RectTransform rectTransform = gameObject.transform as global::UnityEngine.RectTransform;
				gameObject.transform.SetParent(stickerPanel, false);
				if (i < unlockedStickerCount)
				{
					component.locked = false;
				}
				else
				{
					component.locked = true;
				}
				component.stickerDefinitionID = stickerList[i];
				rectTransform.offsetMin = new global::UnityEngine.Vector2(stickerWidth * (float)i, 0f);
				rectTransform.offsetMax = new global::UnityEngine.Vector2(stickerWidth * (float)(i + 1), 0f);
				stickerInstanceList.Add(gameObject);
			}
			stickerPanel.sizeDelta = new global::UnityEngine.Vector2(stickerWidth * (float)stickerList.Count, 0f);
			stickerPanel.localPosition = new global::UnityEngine.Vector2(0f, stickerPanel.localPosition.y);
		}

		private void CleanupExistingStickers()
		{
			foreach (global::UnityEngine.GameObject stickerInstance in stickerInstanceList)
			{
				global::UnityEngine.Object.Destroy(stickerInstance);
			}
		}

		internal void SetCharacterStrings(string characterCollection)
		{
			if (firstTime)
			{
				firstTime = false;
				stickerTitle.gameObject.SetActive(true);
			}
			stickerTitle.text = characterCollection;
		}
	}
}
