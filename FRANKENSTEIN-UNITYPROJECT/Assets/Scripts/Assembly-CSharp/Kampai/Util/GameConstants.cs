namespace Kampai.Util
{
	public static class GameConstants
	{
		public static class Animation
		{
			public const int DEFAULT_BUILDING_COSTUME_ID = -1;

			public const string TRIGGER_LOOP = "OnLoop";

			public const string TRIGGER_WAIT = "OnWait";

			public const string TRIGGER_STOP = "OnStop";

			public const string TRIGGER_GAG = "OnGag";

			public const string TRIGGER_OPEN = "OnOpen";

			public const string TRIGGER_CLOSE = "OnClose";

			public const string TRIGGER_DEPART = "OnDepart";

			public const string TRIGGER_PICKUP = "OnPickUp";

			public const string TRIGGER_ARRIVE = "OnArrive";

			public const string BOOL_IS_READY = "isReady";

			public const string BOOL_IS_AWARE = "isAware";

			public const string BOOL_IS_MOVING = "isMoving";

			public const string BOOL_IS_CELEBRATING = "isCelebrating";

			public const string INT_IDLE_RANDOM = "IdleRandom";

			public const string INT_COORDINATED_GACHA_POSITION = "actor";

			public const string MINION_POSITION = "minionPosition";

			public const string MINION_SPEED = "speed";

			public const string MINION_IS_MOVING = "isMoving";

			public const string FX = "fx";

			public static readonly global::System.Collections.Generic.IEnumerable<string> ALL_TRIGGERS = new global::System.Collections.Generic.List<string> { "OnStop", "OnLoop", "OnWait", "OnGag" };
		}

		public static class AnimationStates
		{
			public const string Base_Layer_Exit = "Base Layer.Exit";

			public const string Base_Layer_Gacha = "Base Layer.gacha";

			public const string Base_Layer_Init = "Base Layer.Init";

			public const string Base_Layer_Loop = "Base Layer.Loop";

			public const string Base_Layer_Loop_Pos = "Base Layer.Loop_Pos";

			public const string Base_Layer_Gag = "Base Layer.Gag";

			public const string Base_Layer_Gag_Pos = "Base Layer.Gag_Pos";

			public const string Base_Layer_Idle = "Base Layer.Idle";

			public const string Base_Layer_Wait = "Base Layer.Wait";

			public const string Base_Layer_Wait_Pos = "Base Layer.Wait_Pos";

			public const string Base_Layer_Opening = "Base Layer.Opening";

			public const string Base_Layer_Open = "Base Layer.Open";

			public const string Base_Layer_Closing = "Base Layer.Closing";

			public const string Base_Layer_Wiggle = "Base Layer.Wiggle";

			public const string Base_Layer_Actions = "Base Layer.Actions";

			public const string Base_Layer_Intro = "Base Layer.NewMinionIntro";

			public const string Base_Layer_Occupied = "Base Layer.Occupied";

			public const string Base_Layer_OnStage_Idle = "Base Layer.OnStage_Idle";
		}

		public static class Movement
		{
			public const float WALK_SPEED = 2f;

			public const float RUN_SPEED = 4.5f;

			public const float PANIC_SPEED = 5.5f;

			public const float RUSH_SPEED = 20f;

			public const float ROTATE_SPEED = 720f;
		}

		public static class AI
		{
			public const float MAX_SPEED = 1f;

			public const float MAX_FORCE = 8f;

			public const float DEFAULT_RADIUS = 0.5f;

			public const float DEFAULT_MASS = 1f;
		}

		public static class Audio
		{
			public const string AUDIO_MAIN_BACKGROUND_MUSIC = "Play_backGroundMusic_01";

			public const string AUDIO_FADE_STAGE_BACKGROUND_MUSIC = "Play_stageStuart_snapshotDuck_01";

			public const string AUDIO_FADE_TIKIBAR_BACKGROUND_MUSIC = "Play_tikiBar_snapshotDuck_01";

			public const string AUDIO_MIGNETTE_BACKGROUND_MUSIC_PROGRESS_PARAM = "Progress";

			public const string AUDIO_MIGNETTE_SCORE_SUMMARY_BGM = "Play_mignetteTally_loop_01";

			public const string AUDIO_MIGNETTE_SCORE_SUMMARY_BGM_PARAM = "Cue";

			public const string AUDIO_MENU_POPUP = "Play_menu_popUp_01";

			public const string AUDIO_MENU_POPUP2 = "Play_menu_popUp_02";

			public const string AUDIO_MENU_DISAPPEAR = "Play_menu_disappear_01";

			public const string AUDIO_BUTTON_CLICK = "Play_button_click_01";

			public const string AUDIO_MENU_CELEBRATION = "Play_celebration_confetti_01";

			public const string AUDIO_PREMIUM_PURCHASE_BUTTON_SOUND = "Play_button_premium_01";

			public const string AUDIO_ENVIRONMENTAL_MIGNETTE_TREE = "Play_tree_shake_01";

			public const string AUDIO_MIGNETTE_SHARED_CHEER = "Play_mignette_group_cheer";

			public const string AUDIO_MIGNETTE_SHARED_COLLECT = "Play_mignette_collect";

			public const string AUDIO_MIGNETTE_SHARED_RECEIVE_AWARD = "Play_Mign_receivedAward_01";

			public const string AUDIO_MIGNETTE_SHARED_SMALL_SCORE = "Play_Mign_smallScoreEvent_01";

			public const string AUDIO_MIGNETTE_SHARED_SCORE_FLY_DOWN = "Play_mignette_scoreFlyDown_01";

			public const string AUDIO_MIGNETTE_SHARED_REWARD_FLY_UP = "Play_mignette_rewardFlyUp_01";

			public const string AUDIO_MIGNETTE_SHARED_INTRO_COUNTDOWN = "Play_mignette_countDown_01";

			public const string AUDIO_MIGNETTE_MANGO_HIT_GROUND = "Play_mango_splat_01";

			public const string AUDIO_MIGNETTE_MANGO_CAUGHT = "Play_crate_land_01";

			public const string AUDIO_MIGNETTE_EDWARDMINIONHANDS_MUSIC = "Play_MUS_topiary_01";

			public const string AUDIO_MIGNETTE_EDWARDMINIONHANDS_TRIM = "Play_minion_topiary_trim_01";

			public const string AUDIO_MIGNETTE_EDWARDMINIONHANDS_TOOL_OUT = "Play_minion_topiary_toolOut_01";

			public const string AUDIO_MIGNETTE_EDWARDMINIONHANDS_DOOBER_SPAWN = "Play_dooberSpawn_whistle_01";

			public const string AUDIO_MIGNETTE_EDWARDMINIONHANDS_CHAT_1 = "Play_minion_topiary_chatter_01";

			public const string AUDIO_MIGNETTE_EDWARDMINIONHANDS_NOD_1 = "Play_minion_topiary_nod_01";

			public const string AUDIO_MIGNETTE_BALLOONBARRAGE_MUSIC = "Play_MUS_balloonBarrage_01";

			public const string AUDIO_MIGNETTE_BALLOONBARRAGE_CATCH = "Play_minion_balloon_catchMango_01";

			public const string AUDIO_MIGNETTE_BALLOONBARRAGE_HIT_FACE = "Play_minion_balloon_hitFace_01";

			public const string AUDIO_MIGNETTE_BALLOONBARRAGE_POP_BALLOON = "Play_balloon_pop_01";

			public const string AUDIO_MIGNETTE_BUTTERFLYCATCH_MUSIC = "Play_MUS_butterflyCatch_01";

			public const string AUDIO_MIGNETTE_BUTTERFLYCATCH_BIG_CATCH = "Play_minon_butterfly_celebrate_01";

			public const string AUDIO_MIGNETTE_BUTTERFLYCATCH_RUN = "Play_minon_butterfly_run_01";

			public const string AUDIO_MIGNETTE_BUTTERFLYCATCH_SWING = "Play_minon_butterfly_swing_01";

			public const string AUDIO_MIGNETTE_BUTTERFLYCATCH_BEE_STING = "Play_balloon_pop_01";

			public const string AUDIO_MIGNETTE_BUTTERFLYCATCH_FALL_DOWN = "Play_balloon_pop_01";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_MUSIC = "Play_MUS_alligatorSki_01";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_WATER = "Play_minion_ski_01";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_JUMP = "Play_minion_ski_jump_01";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_GROWL = "Play_alligator_pull_growl_01";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_MINION_HIT_OBSTACLE = "Play_minion_crash_01";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_ALLIGATOR_HIT_OBSTACLE = "Play_alligator_react_01";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_COLLECTABLE = "Play_mignette_collect";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_CHECKPOINT = "Play_mignette_checkpoint";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_CHEER = "";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_GRUMBLE = "";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_NEON_SIGN_BUZZ = "Play_fish_neonBuzz_01";

			public const string AUDIO_MIGNETTE_ALLIGATORSKIING_NEON_SIGN_CRACKLE = "Play_fish_neonCrackle_01";

			public const string AUDIO_MIGNETTE_WATERSLIDE_MUSIC = "Play_MUS_waterslide_01";

			public const string AUDIO_MIGNETTE_WATERSLIDE_SPINNER_TICK = "Play_poseShuffle_01";

			public const string AUDIO_MIGNETTE_WATERSLIDE_SPINNER_SELECT = "Play_poseSelect_01";

			public const string AUDIO_MIGNETTE_WATERSLIDE_AMBIENT_LOOP = "Play_waterslide_active_loop_01";

			public const string AUDIO_EXAPAND_STORAGE = "Play_expand_storage_01";

			public const string AUDIO_NOT_ENOUGH_ITEMS = "Play_not_enough_items_01";

			public const string AUDIO_PLAY_FILL_ORDER = "Play_fill_order_01";

			public const string AUDIO_MENU_SLIDE_OPEN = "Play_main_menu_open_01";

			public const string AUDIO_MENU_SLIDE_CLOSED = "Play_main_menu_close_01";

			public const string AUDIO_MENU_BOOK_OPEN = "Play_book_open_01";

			public const string AUDIO_MENU_BOOK_CLOSE = "Play_book_close_01";

			public const string AUDIO_BUILDING_REPAIR = "Play_building_repair_01";

			public const string AUDIO_DEBRIS_CLEAR = "Play_building_rotate_01";

			public const string AUDIO_BUILDING_PLACEMENT = "Play_building_place_01";

			public const string AUDIO_PLACE_PROP = "Play_prop_land_01";

			public const string AUDIO_SCAFFOLDING_APPEAR = "Play_scaffold_construction_01";

			public const string AUDIO_SCAFFOLDING_DISAPPEAR = "Play_scaffold_disappear_01";

			public const string AUDIO_ITEM_SOLD = "Play_marketplace_bagDrop_01";

			public const string AUDIO_MARKETPLACE_REFRESH = "Play_marketplace_slotStart_01";

			public const string AUDIO_MARKETPLACE_SPINNER = "Play_marketplace_slotTick_01";

			public const string AUDIO_MARKETPLACE_SPINNER_STOP = "Play_marketplace_slotEnd_01";

			public const string AUDIO_MARKETPLACE_PUTONSALE = "Play_marketplace_putOnSale_01";

			public const string AUDIO_MARKETPLACE_PURCHASED = "Play_marketplace_purchased_01";

			public const string AUDIO_MARKETPLACE_BACK_FROM_CREATE_SALE = "Play_marketplace_backFromSale_01";

			public const string AUDIO_MARKETPLACE_CREATE_SALE_PANE = "Play_marketplace_newSale_01";

			public const string AUDIO_MARKETPLACE_SALE_CARD_FLIP = "Play_marketplace_sellCardFlip_01";

			public const string AUDIO_QUEST_STEP_COMPLETE = "Play_completePartQuest_01";

			public const string AUDIO_QUEST_COMPLETE = "Play_completeQuest_01";

			public const string AUDIO_QUEST_CHECKMARK = "Play_quest_checkMark_01";

			public const string AUDIO_LEVELUP = "Play_levelUp_01";

			public const string AUDIO_LEVELUP_FIRST = "Play_UI_levelUp_first_01";

			public const string AUDIO_LEVELUP_REWARDS = "Play_UI_levelUp_rewards_01";

			public const string AUDIO_LEVELUP_UNLOCKS = "Play_UI_levelUp_unlocks_01";

			public const string AUDIO_LEVELUP_LAST = "Play_UI_levelUp_last_01";

			public const string AUDIO_MINION_ARRIVES = "Play_minionArrives_01";

			public const string AUDIO_MINION_UNLOCK = "Play_minionUnlock_01";

			public const string AUDIO_STICKERBOOK_COMPLETE = "Play_stickerBookComplete_01";

			public const string AUDIO_STICKER_EARNED = "Play_stickerGet_01";

			public const string AUDIO_VILLAIN_LEAVES = "Play_villainLeaves_01";

			public const string AUDIO_VILLAIN_ARRIVES = "Play_villainArrives_01";

			public const string AUDIO_ENVIRONMENT_AMBIENT = "Play_environment_everglades_01";

			public const string AUDIO_WATER_AMBIENT = "Play_water_stream_light_01";

			public const string AUDIO_VOLCANO_AMBIENT = "Play_volcano_lava_01";

			public const string AUDIO_NOTIFICATION_DEFAULT = "bob_booya";

			public const string AUDIO_MINION_RUN = "Play_minion_run_01";

			public const string AUDIO_MINION_DESELECT = "Play_minion_deselect_01";

			public const string AUDIO_MINION_COUNTER_DOWN = "Play_minion_counter_down_01";

			public const string AUDIO_MINION_CONFIRM_BLDG_PATH = "Play_minion_confirm_pathToBldg_01";

			public const string AUDIO_GRASS_CLEAR = "Play_grass_clear_01";

			public const string AUDIO_WHOOSH = "Play_low_woosh_01";

			public const string AUDIO_LOOT_PICK_UP = "Play_loot_pick_up_01";

			public const string AUDIO_MINION_CONFIRM_SELECT = "Play_minion_confirm_select_01";

			public const string AUDIO_MINION_CONFIRM_SELECT_2 = "Play_minion_confirm_select_02";

			public const string AUDIO_MINION_CONFIRM_PATH = "Play_minion_confirm_path_01";

			public const string AUDIO_MINION_GROUP_ACTIVATE = "Play_minion_group_activate_01";

			public const string AUDIO_MINION_COUNTER_SELECT = "Play_minion_counter_select_02";

			public const string AUDIO_BUILDING_SELECT = "Play_building select_01";

			public const string AUDIO_CLICK_SNAP = "Play_click_snap_01";

			public const string AUDIO_ERROR_BUTTON = "Play_error_button_01";

			public const string AUDIO_DELETE_TICKET = "Play_delete_ticket_01";

			public const string AUDIO_UNLOCK_STINGER = "Play_unlock_stinger";

			public const string AUDIO_DROP_HARVEST = "Play_drop_harvest_01";

			public const string AUDIO_WHISTLE_CALL = "Play_whistle_call_01";

			public const string AUDIO_DECREASE_STORAGE = "Play_decrease_storage_01";

			public const string AUDIO_SHOP_PANE_OUT = "Play_shop_pane_out_01";

			public const string AUDIO_SHOP_PANE_IN = "Play_shop_pane_in_01";

			public const string AUDIO_ACTION_LOCKED = "Play_action_locked_01";

			public const string AUDIO_HUD_VFX = "Play_icon_sparkle_01";

			public const string AUDIO_BAR_SCALE = "Play_bar_scale_01";

			public const string AUDIO_PICK_ITEM = "Play_pick_item_01";

			public const string AUDIO_PLACE_ITEM = "Play_place_item_01";

			public const string NO_MEDIA = ".nomedia";
		}

		public static class Paths
		{
			public const string STATE_MACHINE_MINION = "StateMachines/Minions";

			public const string MINION_WALK_STATE_MACHINE = "asm_minion_movement";

			public const string MINION_BARTENER_STATE_MACHINE = "asm_unique_tikibar_bartender";

			public const string MINION_PATRON_STATE_MACHINE = "asm_unique_tikibar_patron";

			public const string MINION_ARRIVE_STATE_MACHINE = "asm_unique_tikibar_newMinion";

			public const string SEQUENCE_GACHA = "Sequences/Gacha";

			public const string ROUTE = "route";

			public const string MINION_TIMELINE = "Timelines for minion";

			public const string BUILDING_STATION_LAYER = "Pos";

			public const string UI_RESOURCES = "UI/";

			public const string UI_LOCK_IMAGE = "LockMain";

			public const string PELVIS_JOINT = "minion:ROOT/minion:pelvis_jnt";

			public const string PELVIS_JOIN_VILLAIN = "{0}:{0}/{0}:ROOT/{0}:Pelvis_M";

			public const string ROOT_JOINT = "minion:ROOT";

			public const string DEFINITIONS = "definitions.json";

			public const string PROCEDURAL_DELIVERY_ICON = "tempCharQuestIcon";

			public const string PROCEDURAL_ORDERBOARD_ICON = "tempCharQuestIcon";

			public const string PROCEDURAL_MINIONTASK_ICON = "tempCharQuestIcon";

			public const string PROCEDURAL_MIGNETTE_ICON = "tempCharQuestIcon";

			public const string DEFAULT_ICON_MASK = "btn_Circle01_mask";

			public const string DEFAULT_MASK_MATERIAL = "CircleIconAlphaMaskMat";

			public const string NOTFOUND_ICON_MASK = "btn_Main01_mask";

			public const string NOTFOUND_ICON_IMG = "btn_Main01_fill";

			public const string ORDERBOARD_CRATESEND_NORMAL = "Unique_JobBoard_CrateSend0_Prefab";

			public const string ORDERBOARD_CRATESEND_PRESTIGE = "Unique_JobBoard_CrateSend1_Prefab";

			public const string ORDERBOARD_CRATERECEIVE_NORMAL = "Unique_JobBoard_CrateReceive0_Prefab";

			public const string ORDERBOARD_CRATERECEIVE_PRESTIGE = "Unique_JobBoard_CrateReceive1_Prefab";

			public const string UI_DEFAULT_DIALOG_ICON = "icn_nav_story_mask";

			public const string QUEST_DIALOG_MINION = "icn_minionCall_mask";

			public const string QUEST_DIALOG_ORDER_BOARD = "icn_nav_orderboard_mask";

			public const string DEFAULT_OVERLAY_IMG = "btn_Main01_overlay_fill";

			public const string AUDIO_ASSETS = "Audio/";

			public const string MARKETPLACE_TICKET_MASK = "icn_nav_salesMinion_mask";
		}

		public static class GUIPrefabs
		{
			public const string TABLET = "_Tablet";

			public const string PHONE = "_Phone";

			public const string HUD = "screen_HUD";

			public const string SKRIM = "Skrim";

			public const string MOVE_BUILDING_PANEL = "screen_MoveBuilding";

			public const string QUEST_REWARD_PANEL = "popup_QuestReward";

			public const string LEVEL_UP_MODAL = "popup_LevelUp";

			public const string RATE_APP_PANEL = "RateAppPanel";

			public const string BILLING_NOT_AVAILABLE_PANEL = "BillingNotAvailablePanel";

			public const string BUDDY_WELCOME_PANEL = "popup_CharacterState";

			public const string COLLECTION_REWARD_DIALOG = "CollectionRewardDialog";

			public const string QUEST_DIALOG = "screen_Dialog";

			public const string MESSAGE_PANEL = "popup_MessageBox";

			public const string FILL_128 = "img_fill_128";

			public const string COPPA_AGE_PANEL = "COPPA_Age_Gate_Panel";

			public const string DLC_DIALOG = "DLC_Dialog";

			public const string SALE_POPUP = "popup_Sale";

			public const string TIPS_POPUP = "popup_Tip";

			public const string REWARD_SLIDER = "cmp_RewardSlider";

			public const string QUEST_SLIDER = "cmp_QuestSlider";

			public const string DLC_DOWNLOAD_DIALOG = "DLC_DownloadProgressDialog";

			public const string NOTIFICATIONS_DIALOG = "popup_Notification";

			public const string WAYFINDER = "cmp_WayFinder";

			public const string TICKET_PREFAB = "cmp_TicketPrefab";

			public const string QUEST_WAYFINDER = "cmp_QuestWayFinder";

			public const string QUEST_PANEL = "screen_QuestPanel";

			public const string QUEST_PROCEDURE_PANEL = "cmp_FlyOut_ProcedurallyGenQuest";

			public const string QUEST_BOOK_ICON = "cmp_QuestBookIcon";

			public const string QUEST_STEP_PANEL = "cmp_TaskPanel";

			public const string MIGNETTE_HUD = "MignetteHUD";

			public const string MIGNETTE_SCORE_SUMMARY = "MignetteScoreSummary";

			public const string MIGNETTE_COLLECTION_REWARD_INDICATOR = "cmp_MignetteReward";

			public const string MIGNETTE_PLAY_CONFIRM_MENU = "MignettePlayConfirmMenu";

			public const string MIGNETTE_COOLDOWN_MENU = "MignetteCooldownMenu";

			public const string MIGNETTE_CALL_MINIONS_MENU = "MignetteCallMinionsMenu";

			public const string MIGNETTE_DOOBER = "NumberedDoober";

			public const string RECIPE_MENU = "screen_CraftingMenu";

			public const string CRAFTING_RECIPE_CARD = "cmp_CraftingRecipe";

			public const string CRAFTING_QUEUE = "cmp_CraftQueue";

			public const string CRAFTING_DRAG = "cmp_DragIcon";

			public const string CURRENCY_WARNING_GRIND = "GrindCurrencyWarning";

			public const string CURRENCY_WARNING_PREMIUM = "PremiumCurrencyWarning";

			public const string BASE_RESOURCE_MENU = "screen_BaseResource";

			public const string BLACK_MARKET_MENU = "screen_OrderBoard";

			public const string RUSH_DIALOG = "popup_MissingResources";

			public const string LAND_EXPANSION_MODAL = "popup_Confirmation_Expansion";

			public const string DEBRIS_MODAL = "screen_ClearDebris";

			public const string STORAGE_BUILDING_MODAL = "screen_StorageBuilding";

			public const string STORAGE_BUILDING_ITEM_INFO_VIEW_POPUP = "cmp_StorageItemInfo";

			public const string REQUIRED_ITEMS_FOR_SHOW = "cmp_RequiredItem_ForShow";

			public const string CONFIRMATION_POPUP = "popup_Confirmation";

			public const string STICKERBOOK_MODAL = "screen_Stickerbook";

			public const string STICKERBOOK_STICKERPACK = "cmp_StickerPackCharacters";

			public const string STICKERBOOK_STICKER_DESCRIPTION = "cmp_StickerInfo";

			public const string NUDGE_UPGRADE_DIALOG = "popup_NudgeUpgrade";

			public const string STICKERBOOK_STICKER = "cmp_Sticker";

			public const string STICKERBOOK_CONTENT_PANEL = "cmp_LIST_StickerPacks";

			public const string TOWN_HALL_DIALOG = "screen_TownHall";

			public const string GENERIC_DOOBER_PREFAB = "TweeningDoober";

			public const string OFFLINE_POPUP = "popup_Error_LostConnectivity";

			public const string GENERIC_HOVER_DESCRIPTION = "cmp_GenericTooltip";

			public const string CRAFTING_HOVER_DESCRIPTION = "cmp_CraftingTooltip";

			public const string BUILD_STORE_MAKE = "icn_build_mask_cat_make";

			public const string BUILD_STORE_MIX = "icn_build_mask_cat_mix";

			public const string BUILD_STORE_DECOR = "icn_build_mask_cat_decor";

			public const string BUILD_STORE_LEISURE = "icn_build_mask_cat_leisure";

			public const string MARKET_PLACE_CREATE_NEW_SALE_PANEL = "cmp_createNewSalePanel";

			public const string MARKET_PLACE_BUY_PANEL = "cmp_marketPlaceBuyPanel";

			public const string MARKET_PLACE_SELL_PANEL = "cmp_marketPlaceSellPanel";

			public const string MARKET_PLACE_SELL_SLOT_PANEL = "cmp_StorageBuildingSaleSlot";

			public const string MARKET_PLACE_BUY_SLOT_PANEL = "cmp_StorageBuildingBuySlot";

			public const string SOCIAL_PARTY_START_SCREEN = "cmp_FlyOut_SocialParty";

			public const string SOCIAL_PARTY_FBCONNECT_SCREEN = "popup_SocialParty_FBConnect";

			public const string SOCIAL_PARTY_FILL_ORDER_SCREEN = "SocialPartyFillOrderScreen";

			public const string SOCIAL_PARTY_FILL_ORDER_BUTTON = "cmp_SocialFillOrder";

			public const string SOCIAL_PARTY_FILL_ORDER_AVATAR = "cmp_TeamPlayer";

			public const string SOCIAL_PARTY_INVITE_ALERT_SCREEN = "popup_SocialParty_InviteAlert";

			public const string SOCIAL_PARTY_REWARD_SCREEN = "popup_SocialPartyReward";

			public const string SOCIAL_PARTY_EVENT_END_SCREEN = "popup_SocialParty_End";

			public const string SOCIAL_PARTY_EVENT_COMPLETED_SCREEN = "popup_SocialParty_EventCompleted";

			public const string SOCIAL_PARTY_NO_EVENT_SCREEN = "popup_SocialParty_NoEvent";

			public const string MAILBOX_ICON = "cmp_MailboxIcon";

			public const string HARVEST_ICON = "cmp_HarvestIcon";

			public const string CONSTRUCTION_PROGRESSBAR = "cmp_BuildingProgress";

			public const string TEXTWAYFINDER_PREFAB = "cmp_FloatingText";

			public const string MOCK_STORE = "PurchaseAuthorizationWarning";

			public const string DEBUG_CONSOLE = "DebugConsole";

			public const string DEBUG_CONSOLE_BUTTON = "DebugConsoleButton";
		}

		public static class PrefabLabels
		{
			public const string RUSH_DIALOG_STORAGE_LABEL = "popup_OutOfResourceForStorage";
		}

		public static class SkrimLabels
		{
			public const string RUSH_SKRIM = "RushSkrim";

			public const string RUSH_STORAGE_SKRIM = "RushStorageSkrim";

			public const string QUEST_PANEL_SKRIM = "QuestPanelSkrim";

			public const string RATE_APP_SKRIM = "RateAppSkrim";

			public const string LAND_EXPANSION_SKRIM = "LandExpansionSkrim";

			public const string STAGE_SKRIM = "StageSkrim";

			public const string STAGE_SKRIM_FB = "StageSkrimFB";

			public const string STAGE_SKRIM_REWARD = "SocialReward";

			public const string SOCIAL_INVITE = "Social Invite";

			public const string BRIDGE_SKRIM = "BridgeSkrim";

			public const string BUILDING_DETAIL_SKRIM = "BuildingSkrim";

			public const string ORDER_BOARD_SKRIM = "OrderBoardSkrim";

			public const string CLIENT_UPGRADE_SKRIM = "ClientUpgradeSkrim";

			public const string PROCEDURAL_TASK_SKRIM = "ProceduralTaskSkrim";

			public const string BILLING_SKRIM = "BillingSkrim";

			public const string CRAFTING_SKRIM = "CraftingSkrim";

			public const string SOCIAL_SKRIM = "SocialSkrim";

			public const string SOCIAL_COMPLETE_SKRIM = "SocialCompleteSkrim";

			public const string DEBRIS_SKRIM = "DebrisSkrim";

			public const string MIGNETTE_SKRIM = "MignetteSkrim";

			public const string NOTIFICATIONS_SKRIM = "NotificationsSkrim";

			public const string STORAGE_SKRIM = "StorageSkrim";

			public const string LEVEL_UP_REWARD = "LevelUpRewardSkrim";

			public const string CURRENCY_SKRIM = "CurrencySkrim";

			public const string BUDDY_WELCOME_SKRIM = "BuddySkrim";

			public const string QUEST_REWARD_SKRIM = "QuestRewardSkrim";

			public const string TOWN_HALL_SKIM = "TownHallSkrim";

			public const string STICKER_BOOK_SKRIM = "StickerBookSkrim";

			public const string CONFIRMATION_SKRIM = "ConfirmationSkrim";

			public const string DID_YOU_KNOW_SKRIM = "DidYouKnowSkrim";
		}

		public static class Layers
		{
			public const int DEFAULT = 0;

			public const int TRANSPARENT_FX = 1;

			public const int IGNORE_RAYCAST = 2;

			public const int WATER = 4;

			public const int UI = 5;

			public const int MINION = 8;

			public const int BUILDING = 9;

			public const int INVISIBLE = 10;

			public const int ENVIRONMENTAL_MIGNETTE = 11;

			public const int LAND_EXPANSION = 12;

			public const int MIGNETTE = 13;

			public const int MOVE = 14;

			public const int VILLAIN_ISLAND = 15;
		}

		public static class PickEvents
		{
			public const int NONE = 0;

			public const int START = 1;

			public const int HOLD = 2;

			public const int END = 3;
		}

		[global::System.Flags]
		public enum CameraBehaviours
		{
			None = 0,
			Pan = 1,
			Zoom = 2,
			Shake = 4,
			DragPan = 8
		}

		public static class CameraBoundaries
		{
			public const float PAN_MIN_X = 25f;

			public const float PAN_MAX_X = 233f;

			public const float PAN_MIN_Y = -9f;

			public const float PAN_MAX_Y = 205f;
		}

		public static class Building
		{
			public const int MOVE_HEIGHT = 0;

			public const float SELECT_TIME = 0.5f;

			public const float SCAFFOLDING_BUILD_TIME = 2f;

			public const float SCAFFOLDING_REMOVE_TIME = 1f;

			public const float BUILDING_PLACEMENT_Y_OFFSET = 0f;

			public const float SCAFFOLDING_Y_OFFSET = -5f;

			public const string SCAFFOLDING_REVEAL_EVENT = "CreateObject";

			public const string SCAFFOLDING_REMOVE_EVENT = "RemoveObject";

			public const string REPAIR_BUILDING_EVENT = "RepairBuilding";

			public const string RIBBON_VFX_PREFAB_NAME = "FX_Bow_Prefab";

			public const string REVEAL_VFX_PREFAB_NAME = "FX_Reveal_Prefab";

			public const string SCAFFOLDING_VFX_PREFAB_NAME = "FX_Drop_Prefab";

			public const string PENDING_SALE_DISAPPEAR = "PendingSale_Disappear";

			public const string SOLD_APPEAR = "GrindReward_Appear";

			public const string SOLD_DISAPPEAR = "GrindReward_Disappear";

			public static readonly global::UnityEngine.Color VALID_PLACEMENT_COLOR = new global::UnityEngine.Color(0.35686275f, 53f / 85f, 0.16862746f, 0.7f);

			public static readonly global::UnityEngine.Color INVALID_PLACEMENT_COLOR = new global::UnityEngine.Color(0.8784314f, 36f / 85f, 0.16078432f, 0.7f);
		}

		public static class Probabilities
		{
			public const int MagnetGachaSoundFactor = 200;
		}

		public static class Social
		{
			public const int MaxGoogleFail = 1;

			public const int MaxGoogleSuc = 1;
		}

		public static class PersistanceKeys
		{
			public const string EnvPrefix = "EnvPrefix";

			public const string PlayerPrefix = "Player_";

			public const string LoadMode = "LoadMode";

			public const string LocalID = "LocalID";

			public const string LocalFilename = "LocalFileName";

			public const string UserID = "UserID";

			public const string IsSpender = "IsSpender";

			public const string AnonymousSecret = "AnonymousSecret";

			public const string AnonymousID = "AnonymousID";

			public const string DevicePrefs = "DevicePrefs";

			public const string SplashSeen = "SplashSeen";

			public const string FirstLaunch = "FirstLaunch";

			public const string GoogleFailCount = "GoogleFailCount";

			public const string GoogleSuccessCount = "GoogleSuccessCount";

			public const string SocialInProgress = "SocialInProgress";

			public const string MtxPurchaseInProgress = "MtxPurchaseInProgress";

			public const string RateApp = "RateApp";

			public const string ClientConfigURL = "configURL";

			public const string DlcTier = "dlcTier";

			public const string DlcQuality = "dlcQuality";

			public const string DlcDisplayQuality = "DlcDisplayQuality";

			public const string HasUpToDateDlc = "HasUpToDateDlc";

			public const string DefinitionsUrl = "DefinitionsUrl";

			public const string MtxPendingReceipts = "MtxPendingReceipts";

			public const string COPPA_Age_Month = "COPPA_Age_Month";

			public const string COPPA_Age_Year = "COPPA_Age_Year";

			public const string SharingUsage = "SharingUsage";

			public const string DoubleConfirmPurchase = "DoublePurchaseConfirm";

			public const string DeepLinkPrefsKey = "DeepLink";

			public const string OverrideVersion = "OverrideVersion";

			public const string OverrideVersionPersistState = "OverrideVersionPersistState";

			public const string PushNotifications = "AllowPushNotifications";

			public const string DidYouKnow_MagnetFinger = "didyouknow_MagnetFinger";

			public const string DidYouKnow_MultiMinionTap = "didyouknow_MultiMinionTap";

			public const string DidYouKnow_PutBuildingInInventory = "didyouknow_PutBuildingInInventory";

			public const string DidYouKnow_MoveBuilding = "didyouknow_MoveBuilding";

			public const string FreezeTime = "freezeTime";

			public const string ExternalLinkOpened = "ExternalLinkOpened";

			public const string ENCRYPTION_SECRET = "Kampai!";

			public const string BuildMenuLocalSave = "BuildMenuLocalSave";

			public const string UpsightTriggeredAtGameLaunch = "UpsightTriggeredAtGameLaunch";

			public const string VideoPlayed = "intro_video_played";

			public const string ForceVideoMagicString = "ForceVideo";

			public const string StickerbookGlow = "StickerbookGlow";

			public const string VideoCache = "VideoCache";

			public const string SavedLocTipsWithTimes = "SavedLocTipsWithTimes";

			public const string NewUser = "NewUser";

			public const string MarketplaceSurfacing = "MarketSurfacing";

			public const string MarketplaceSurfacingButtonPulse = "MarketSurfacingButtonPulse";
		}

		public static class Facebook
		{
			public const bool COOKIE = true;

			public const bool LOGGING = true;

			public const bool STATUS = true;

			public const bool XFBML = false;

			public const bool FRICTIONLESS = true;

			public static string APP_ID = global::Kampai.Util.GameConstants.StaticConfig.FACEBOOK_APP_ID;
		}

		public static class DCN
		{
			public const string HEADER_TOKEN = "X-DCN-TOKEN";

			public const string END_POINT_TOKEN = "/token";

			public const string END_POINT_CONTENTS = "/contents";

			public const string END_POINT_FEATURED = "/contents/featured";

			public const string END_POINT_EVENT = "/contents/{0}/events";

			public const string KEY_HTML5 = "html5";

			public const string QUERY_TOKEN_KEY = "&token=";

			public const string QUERY_RETURN_NAME_KEY = "&return_name=";

			public const string QUERY_RETURN_NAME_VALUE = "Minions";

			public const string QUERY_RETURN_URL_KEY = "&return_url=";

			public const string QUERY_RETURN_URL_VALUE = "minions:\\dcn";

			public const string JSON_STRING = "{{ \"{0}\": \"{1}\" }}";

			public const string JSON_KEY_APP_TOKEN = "app_token";

			public const string JSON_KEY_EVENT = "type";

			public const string JSON_EVENT_DISPLAY = "display";

			public const int EVENT_REGISTERED = 204;

			public const string IEToken = "IEToken";

			public const string IETokenExpiration = "IETokenExpiration";

			public static string SERVER = global::Kampai.Util.GameConstants.StaticConfig.DCN_SERVER_URL;

			public static string APP_TOKEN = global::Kampai.Util.GameConstants.StaticConfig.DCN_APP_TOKEN;
		}

		public static class Marketplace
		{
			public static readonly string OVERRIDES_SERVER = global::Kampai.Util.GameConstants.StaticConfig.MARKETPLACE_SERVER_URL;

			public static readonly string STAT_REPORTING_SERVER = global::Kampai.Util.GameConstants.StaticConfig.SERVER_URL;
		}

		public static class Timers
		{
			public const float SELECT_TIME = 1f;

			public const float RUSH_TO_SELECT_TIME = 3.7f;

			public const float UI_FLY_OUT_TIME = 1f;

			public const float INVALID_CRAFT_LOCATION = 0.25f;

			public const string MINION_SELECT_TIMER_ID = "MinionSelectionComplete";

			public const float PRESTIGE_BAR_FILL_TIME = 0.75f;

			public const float PRESTIGE_FILL_WAIT_TIME = 0.25f;

			public const float STORAGE_BUILDING_OPEN_TIME = 0f;

			public const float HARVEST_METER_STAY_TIME = 3f;

			public const float BRIDGE_REPAIR_TIME = 2f;

			public const float BURN_GRASS_SPREAD_TIME = 0.25f;

			public const float MINION_IDLE_READY_ANIMATION_OFFSET_MAX = 0.4f;

			public const float MINION_IDLE_READY_ANIMATION_INTERVAL = 1.4f;

			public const float CHARACTER_UI_ANIMATION_INTERVAL = 2f;

			public const int RESTART_GAME_IF_PAUSED_LONGER_THAN = 10;

			public const float MAGNET_FINGER_FADE_TIMER = 0.5f;

			public const float POPUP_UI_CLOSE_WAIT = 0.5f;

			public const string PARTY_OVER = "StartingPartyOver";

			public const string RESOLVE_PARTY = "ResolveParty";

			public const int VERIFY_NETWORK_INTERVAL = 15;

			public const float BLUR_IMAGE_FADE_IN_DURATION = 1f;

			public const float BLUR_IMAGE_FADE_OUT_DURATION = 1f;

			public const float SPIN_SOUND_DELAY = 0.5f;

			public const float SPIN_SOUND_RATE = 0.12f;

			public const float SPIN_STARTUP_RATE = 0.2f;

			public const float SPINNER_BLUR_START = 0f;

			public const float SPINNER_BLUR_DURATION = 0.35f;

			public const float SPINNER_ANCHOR_DURATION = 1f;

			public const float SPINNER_BLUR_FADE_IN_START = 0.35f;

			public const float SPINNER_BLUR_FADE_OUT_START = 0.95f;

			public const float SPINNER_BLUR_FADE_OUT_DURATION = 1f;

			public const float SPINNER_ICON_FADE_IN_START = 1.7f;

			public const float SPINNER_ICON_FADE_IN_DURATION = 0.5f;

			public const float SPINNER_PRICE_DURATION = 0.25f;

			public const float SPINNER_PRICE_POP_DURATION = 0.5f;

			public const float SPINNER_PRICE_CHAIN_START = 1.95f;

			public const float SPINNER_PRICE_FADE_OUT_DURATION = 0.4f;

			public const float SLOT_MACHINE_TOTAL_TIME = 3.7f;

			public const float CRAFTING_QUEUE_SCALE_TIME = 0.2f;

			public static global::System.DateTime epochStart = new global::System.DateTime(1970, 1, 1, 0, 0, 0, global::System.DateTimeKind.Utc);
		}

		public static class UI
		{
			public const float PHONE_SCALE_MULTIPLIER = 1.5f;

			public const float UI_PULSE_SCALE = 0.85f;

			public const float UI_PULSE_RATE = 0.5f;

			public const float UI_RESOURCE_ICON_X_OFFSET = 1f;

			public const float UI_HUD_SPACE_FROM_TOP = 0.9f;

			public const float UI_HUD_PEEK_TIME = 3f;

			public const string UI_BUILD_BUTTON = "btn_OpenStore";

			public const string UI_SETTINGS_BUTTON = "btn_Settings";

			public const string UI_STORAGE_ICON_GROUP = "group_Storage";

			public const string UI_GRID_ICON_GROUP = "group_Currency_Grind";

			public const float CRAFTING_QUEUE_SECONDARY_SCALE = 0.8f;

			public const float CRAFTING_QUEUE_HOVER_SCALE_RATE = 1.15f;

			public const int SELECT_CHARACTER_Z_OFFSET = -900;

			public static global::UnityEngine.Color UI_GREEN = new global::UnityEngine.Color(0.18f, 0.8f, 0.443f, 1f);

			public static global::UnityEngine.Color UI_DARK_GREEN = new global::UnityEngine.Color(0.176f, 0.47f, 0.294f, 1f);

			public static global::UnityEngine.Color UI_ORANGE = new global::UnityEngine.Color(1f, 0.451f, 0.023f, 1f);

			public static global::UnityEngine.Color UI_TEXT_ORANGE = new global::UnityEngine.Color(1f, 0.474f, 0f, 1f);

			public static global::UnityEngine.Color UI_TEXT_LIGHT_BLUE = new global::UnityEngine.Color(0.133f, 0.678f, 0.807f, 1f);

			public static global::UnityEngine.Color UI_TEXT_LIGHT_GREY = new global::UnityEngine.Color(0.858f, 0.874f, 0.894f, 1f);

			public static global::UnityEngine.Color UI_TEXT_GREY = new global::UnityEngine.Color(0.196f, 0.196f, 0.196f, 1f);

			public static global::UnityEngine.Color UI_BLACK = new global::UnityEngine.Color(0.2f, 0.247f, 0.286f, 1f);

			public static global::UnityEngine.Color UI_RED = new global::UnityEngine.Color(0.843f, 0.357f, 0.357f, 1f);

			public static global::UnityEngine.Color UI_LOCKED_GRAY = new global::UnityEngine.Color(0.58f, 0.58f, 0.58f, 1f);

			public static global::UnityEngine.Color UI_TRANSPARENT = new global::UnityEngine.Color(1f, 1f, 1f, 0f);

			public static float UI_MODAL_BORDER_TOP_DISTANCE = 40f;

			public static global::UnityEngine.Sprite DUMMY_SCREEN_TWEEN_SPRITE = new global::UnityEngine.Sprite();

			public static global::UnityEngine.Vector3 MAGNET_FINGER_OFFSET = new global::UnityEngine.Vector3(-1.5f, 5f, 1.5f);

			public static global::UnityEngine.Vector3 VILLAIN_UI_OFFSET = new global::UnityEngine.Vector3(0f, 2f, 0f);

			public static global::UnityEngine.Vector3 MOVE_MENU_OFFSET = new global::UnityEngine.Vector3(4f, 0f, -4f);

			public static readonly global::UnityEngine.Color UI_PURCHASE_BUTTON_COLOR = new global::UnityEngine.Color(0.952941f, 0.439216f, 0.12549f, 1f);

			public static readonly global::UnityEngine.Color UI_ACTION_BUTTON_COLOR = new global::UnityEngine.Color(0.13333f, 0.678431f, 0.807843f, 1f);

			public static readonly global::UnityEngine.Color UI_MENU_BUTTON_COLOR = new global::UnityEngine.Color(1f, 0.501961f, 0f, 1f);
		}

		public static class LanguagePaths
		{
			public const string ENGLISH = "EN-US";

			public const string ENGLISH_SOURCE = "EN-US-SOURCE";

			public const string FRENCH = "FR-FR";

			public const string GERMAN = "DE-DE";

			public const string SPANISH = "ES-ES";

			public const string ITALIAN = "IT-IT";

			public const string BRAZILIAN_PORTUGUESE = "PT-BR";

			public const string DUTCH = "NL-NL";

			public const string RUSSIAN = "RU-RU";

			public const string JAPANESE = "JA";

			public const string KOREAN = "KO-KR";

			public const string SIMPLIFIED_CHINESE = "ZH-CN";

			public const string TRADITIONAL_CHINESE = "ZH-TW";

			public const string TURKISH = "TR";

			public const string BAHASA_INDONESIAN = "ID";
		}

		public static class LocalKeys
		{
			public const string MULTIPLIER_STRING = "x {0}";

			public const string SINGLE_MULTIPLIER_STRING = "x 1";

			public const string DESCRIPTION = "Description";

			public const string NO_INTERNET_CONNECTION = "NoInternetConnection";

			public const string FATAL_NETWORK_TITLE = "FatalNetworkTitle";

			public const string FATAL_NETWORK_MESSAGE = "FatalNetworkMessage";

			public const string FATAL_TITLE = "FatalTitle";

			public const string FATAL_MESSAGE = "FatalMessage";

			public const string INSUFFICIENT_STORAGE_KEY = "InsufficientStorage";

			public const string INSUFFICIENT_STORAGE_MESSAGE_KEY = "InsufficientStorageMessage";

			public const string NO_TICKET_KEY = "NoTicketSelected";

			public const string SLOT_EMPTY_KEY = "SlotEmpty";

			public const string SLOT_UNLOCK_KEY = "SlotUnlock";

			public const string CRAFTING_QUEUE_FULL = "CraftQueueFull";

			public const string IN_QUEUE_KEY = "InQueue";

			public const string SLOT_AVAILABLE_KEY = "SlotAvailable";

			public const string CALL_KEY = "Call";

			public const string BRB_SLOT_LOCKED = "BRBLockedSlot";

			public const string INVALID_LOCATION_KEY = "InvalidLocation";

			public const string LAND_LOCKED_BY_LEVEL = "LandLockedByLevel";

			public const string LAND_LOCKED_BY_OTHER_LAND = "LandLockedByOtherLand";

			public const string MARKETPLACE_UNLOCK = "MarketplaceUnlock";

			public const string MARKETPLACE_ONBOARDING = "MarketplaceOnboarding";

			public const string BRIDGE_NOT_AVAILABLE_KEY = "BridgeNotAvailable";

			public const string VILLAIN_ISLAND_ASPIRATION = "AspirationalMessageVillainIsland";

			public const string VILLAIN_ISLAND_MUST_PRESTIGE_KEVIN = "UnlockKevinForVillainIsland";

			public const string MUST_PRESTIGE_BOB_KEY = "MustPrestigeBobKey";

			public const string MUST_OWN_ADJACENT_KEY = "MustOwnAdjacent";

			public const string HARVEST_READY = "HarvestReady";

			public const string NOT_ENOUGH = "NotEnough";

			public const string NOT_ENOUGH_DESC = "NotEnoughResources";

			public const string YOU_NEED = "YouNeed";

			public const string YOU_NEED_DESC = "YouNeedResources";

			public const string SOCIAL_NOT_ENOUGH = "SocialPartyNotEnough";

			public const string SOCIAL_NOT_ENOUGH_DESC = "SocialPartyNotEnoughResources";

			public const string PURCHASE_LABEL = "PurchaseLabel";

			public const string FACEBOOK_LINK_WARNING_KEY = "FacebookLinkWarning";

			public const string UNLOCK_AT_KEY = "UnlockAt";

			public const string SERVER = "server";

			public const string BUILD_NUMBER = "buildNumber";

			public const string PENDING_TRANSACTION = "PendingTransaction";

			public const string CANCEL_TRANSACTION = "CancelTransaction";

			public const string QUEST_TIMEOUT_KEY = "QuestTimeout";

			public const string NEW_UNLOCK = "NewUnlock";

			public const string DLC_PROGRESS_TITLE = "DLCProgressTitle";

			public const string DLC_PROGRESS = "DLCProgress";

			public const string DLC_INDICATOR_PROGRESS = "DLCIndicatorProgress";

			public const string DLC_CONFIRMATION_DIALOG = "DLCConfirmationDialog";

			public const string DLC_MEDIUM_PERF_CONFIRMATION_DIALOG = "DLCMediumPerfConfirmationDialog";

			public const string DLC_HD_PACK = "DLCHDPack";

			public const string DLC_SD_PACK = "DLCSDPack";

			public const string DOUBLE_CONFIRM_PURCHASE = "DoubleConfirm";

			public const string QUEST_TASK_DELIVER_KEY = "Deliver";

			public const string PURCHASE_KEY = "Purchase";

			public const string OF_COMPLETE = "OfComplete";

			public const string GOTO_KEY = "GoTo";

			public const string IN_PROGRESS_KEY = "InProgress";

			public const string READY_KEY = "Ready";

			public const string COMPLETE_KEY = "Complete";

			public const string YES_KEY = "Yes";

			public const string NO_KEY = "No";

			public const string DONE_KEY = "Done";

			public const string DID_YOU_KNOW_KEY = "DidYouKnow";

			public const string MAYBE_LATER_KEY = "MaybeLater";

			public const string SELL_TITLE_KEY = "SellTitle";

			public const string MARKET_PLACE_REFRESH_TITLE_KEY = "BuyPanelRefreshTitle";

			public const string MARKET_PLACE_REFRESH_USER_PROMPT_KEY = "BuyPanelRefreshUserPrompt";

			public const string MARKET_PLACE_STOP_SPINNING_KEY = "BuyPanelStopSpinningUserPrompt";

			public const string MARKET_PLACE_REFRESH_FACEBOOK_CONNECT_KEY = "BuyPanelRefreshFacebookConnect";

			public const string MARKET_PLACE_SALE_SLOT_CREATE_NEW_SALE_KEY = "SellPanelCreateNewSale";

			public const string MARKET_PLACE_SALE_SLOT_SOLD_KEY = "SellPanelSold";

			public const string SOLD_MARKET_PLACE_SLOT_TITLE = "SoldMarketplaceSlotTitle";

			public const string BUY_MARKET_PLACE_SLOT_PURCHASED_TITLE = "BuyMarketplaceSlotPurchasedTitle";

			public const string MARKET_PLACE_SALE_SLOT_DELETE_SALE_KEY = "SellSaleDeleteSale";

			public const string MARKETPLACE_BUY_SLOT_FACEBOOK_CONNECT_KEY = "BuySlotFacebookConnnectTitle";

			public const string STORAGE_TOOLTIP_CRAFTING_BUILDING_KEY = "StorageBuildingTooltipCraftingBuilding";

			public const string STORAGE_TOOLTIP_HARVEST_BUILDING_KEY = "StorageBuildingTooltipHarvestBuilding";

			public const string STORAGE_TOOLTIP_RANDOM_DROP_KEY = "StorageBuildingTooltipRandomDrop";

			public const string EARN_KEY = "Earn";

			public const string GREAT_JOB_KEY = "GreatJob";

			public const string COLLECT_KEY = "Collect";

			public const string CLEAR_X_KEY_QUESTION = "ClearX?";

			public const string CLEAR_X_KEY = "ClearX";

			public const string EXPAND_LAND_PROMPT_KEY = "ExpandLandPrompt";

			public const string REPAIR_BRIDGE_PROMPT_KEY = "RepairBridge";

			public const string CRAFTING_LIMIT_REACHED = "CraftingLimit";

			public const string BUILDING_HELPER_DIALOG = "BuildingHelperDialog";

			public const string CURRENCY_PREMIUM_TITLE = "CurrencyPremiumTitle";

			public const string CURRENCY_GRIND_TITLE = "CurrencyGrindTitle";

			public const string CONFIRMATION_TITLE_DEFAULT = "popupConfirmationDefaultTitle";

			public const string CONFIRMATION_DEFAULT_CANCEL_TEXT = "CancelText";

			public const string CONFIRMATION_DEFAULT_PROCEED_TEXT = "ProceedText";

			public const string NOTIFICATIONS_BUTTON_LABEL = "NotificationsLabel";

			public const string NOTIFICATION_TITLE = "NotificationTitle";

			public const string NOTIFICATION_MESSAGE = "NotificationMessage";

			public const string NOTIFICATION_YES = "NotificationYes";

			public const string NOTIFICATION_NO = "NotificationNo";

			public const string RESOURCE_PROD = "ResourceProd";

			public const string YOU_NEED_X_UNTASKED_MINIONS = "YouNeedXUntaskedMinions";

			public const string RATE_US_MENU = "RateUsMenu";

			public const string PLAY_MOVIE = "PlayMovie";

			public const string MORE_HELP = "MoreHelp";

			public const string FAQ = "FAQ";

			public const string CAPACITY = "Capacity";

			public const string OKAY = "Okay";

			public const string SAVE_POPUP_TITLE = "SavePopupTitle";

			public const string SAVE_POPUP_BODY = "SavePopupBody";

			public const string NEW_SOCIAL_TITLE = "NewSocialTitle";

			public const string NEW_SOCIAL_BODY = "NewSocialBody";

			public const string PRESTIGE_PROGRESS = "PrestigeProgress";

			public const string PRESTIGE_TEXT = "PrestigeText";

			public const string REPRESTIGE_TEXT = "RePrestigeText";

			public const string BANNER_TITLE_WELCOME = "WelcomeTitle";

			public const string BANNER_TITLE_FAREWELL = "FarewellTitle";

			public const string BANNER_TITLE_REPRESTIGE = "RePrestigeTitle";

			public const string MINION_SINGULAR_UPPER = "MinionsSingularUpper";

			public const string MINION_PLURAL_UPPER = "MinionsPluralUpper";

			public const string RESOURCE_AVAILABLE = "ResourceAvailable";

			public const string STORAGE = "Storage";

			public const string STORAGE_MAX = "StorageMax";

			public const string NEXT_REWARD = "NextReward";

			public const string NO_UPSIGHT_CONTENT_AVAILABLE = "NoUpsightContentAvailable";

			public const string PUNCTUATION_DELIMITERS = "PunctuationDelimiters";

			public const string CLIENT_UPGRADE_TITLE_KEY = "ClientUpgradeTitle";

			public const string FORCED_CLIENT_UPGRADE_MESSAGE_KEY = "ForcedClientUpgradeMessage";

			public const string NUDGE_CLIENT_UPGRADE_MESSAGE_KEY = "NudgeClientUpgradeMessage";

			public const string UPDATE_KEY = "Update";

			public const string STICKERBOOK_CHARACTER_LOCKED = "StickerbookCharLocked";

			public const string STICKERBOOK_STICKER_LOCKED_TITLE = "StickerbookStickerLockedPart1";

			public const string STICKERBOOK_STICKER_LOCKED_DESC = "StickerbookStickerLockedPart2";

			public const string QUEST_ACTION_DELIVER = "deliveryAction";

			public const string QUEST_ACTION_CONSTRUCT = "constructAction";

			public const string QUEST_ACTION_MIGNETTE = "mignetteAction";

			public const string QUEST_ACTION_ORDERBOARD = "orderBoardAction";

			public const string QUEST_ACTION_HAVE = "haveAction";

			public const string QUEST_ACTION_REPAIR = "repairAction";

			public const string QUEST_ACTION_BUILD = "buildAction";

			public const string QUEST_REWARD = "questRewardDescription";

			public const string TASK_DESC_CONSTRUCT = "constructTaskDesc";

			public const string TASK_DESC_MIGNETTE_WRAP = "mignetteTaskDescWrap";

			public const string TASK_DESC_MIGNETTE = "mignetteTaskDesc*";

			public const string TASK_DESC_ORDERBOARD = "orderboardTaskDesc";

			public const string TASK_DESC_HARVEST = "harvestTaskDesc";

			public const string TASK_DESC_DELIVER = "deliverTaskDesc";

			public const string TASK_DESC_REPAIRBRIDGE = "repairBridgeDesc";

			public const string TASK_DESC_REPAIRSTAGE = "repairStageDesc";

			public const string TASK_DESC_REPAIRCABANA = "CabanaRepair";

			public const string TASK_DESC_REPAIRWELCOME = "repairWelcomeDesc";

			public const string TASK_DESC_REPAIRFOUNTAIN = "repairFountainDesc";

			public const string MIGNETTE_SCORE_TITLE = "MignetteScoreSummary_Score";

			public const string MIGNETTE_TEXT_TOTAL_SCORE = "MignetteTotalScore";

			public const string MIGNETTE_TEXT_AVAILABLE = "MignetteMinionsAvailable";

			public const string MIGNETTE_TEXT_REWARDS = "MignetteRewards";

			public const string MIGNETTE_TEXT_PTS = "MignettePoints";

			public const string MIGNETTESCORESUMMARY_CONFIRMBUTTON = "MignetteScoreSummary_ConfirmButton";

			public const string MIGNETTESCORESUMMARY_GOTOBUTTON = "MignetteScoreSummary_GoToButton";

			public const string MIGNETTESCORESUMMARY_COLLECT = "MignetteScoreSummary_CollectButton";

			public const string COMPOSITE_MENU_SHUFFLE = "CompositeMenu_Shuffle";

			public const string COMPOSITE_MENU_MIGNETTES = "CompositeMenu_Mignettes";

			public const string COMPOSITE_MENU_PIECESOWNED = "CompositeMenu_PiecesOwned";

			public const string TOWNHALLDIALOG_TITLE = "TownHallDialog_Title";

			public const string MINION_FANFARE_TITLE = "FanfareTitle";

			public const string MINION_FANFARE_DESCRIPTION = "FanfareDescription";

			public const string MINIONS = "Minions*";

			public const string ALLOW_PANEL_PRIVACY_POLICY = "PrivacyPolicyLabel";

			public const string ALLOW_PANEL_EULA = "EULALabel";

			public const string CREDIT_CONTENTS = "CreditContents";

			public const int CREDITS_CONTENTS_COUNT = 31;

			public const string ALLOW_PANEL_CREDITS = "CreditsLabel";

			public const string ALLOW_PANEL_ENABLE_SHARING = "EnableUsageSharing";

			public const string ALLOW_PANEL_DISABLE_SHARING = "DisableUsageSharing";

			public const string ALLOW_PANEL_TOS = "TermsOfServiceLabel";

			public const string ANNOUNCE_MINION_SET = "AnnounceMinionSet{0}";

			public const string ANNOUNCE_MINIONS_RANDOM = "AnnounceMinions{0}";

			public const string ANNOUNCE_NAMED_CHARACTERS = "AnnounceCharacter{0}";

			public const string ANNOUNCE_COSTUME_MINIONS = "AnnounceMinion{0}";

			public const string NEED_QUANTITY_KEY = "NeedQuantity*";

			public const string PROD_COIN_XP_KEY = "ProdCoinXp*";

			public const string IN_PRODUCTION = "InProduction*";

			public const string NEEDS_X_MINIONS_KEY = "NeedsXMinions*";

			public const string PUSH_NOTE_CONSTRUCTION_KEY = "PushNoteConstruction";

			public const string PUSH_NOTE_BLACK_MARKET_KEY = "PushNoteVillainWantAdsBoard";

			public const string PUSH_NOTE_MINIONS_KEY = "PushNoteMinions";

			public const string PUSH_NOTE_BASE_RESOURCE_KEY = "PushNoteBaseResource";

			public const string PUSH_NOTE_CRAFTING_KEY = "PushNoteCrafting";

			public const string PUSH_NOTE_EVENT_KEY = "PushNoteEvent";

			public const string PUSH_NOTE_MARKET_PLACE_KEY = "PushNoteMarketPlace";

			public const string PROCEDURAL_DELIVERY_KEY = "deliveryTaskName";

			public const string PROCEDURAL_ORDERBOARD_KEY = "orderBoardTaskName";

			public const string PROCEDURAL_MINIONTASK_KEY = "minionTaskName";

			public const string PROCEDURAL_MIGNETTE_KEY = "mignetteTaskName";

			public const string USAGE_SHARING_YES = "UsageSharingYes";

			public const string USAGE_SHARING_NO = "UsageSharingNo";

			public const string USAGE_SHARING_ENABLE_TITLE = "UsageSharingEnableTitle";

			public const string USAGE_SHARING_DISABLE_TITLE = "UsageSharingDisableTitle";

			public const string USAGE_SHARING_ENABLE_DESC = "UsageSharingEnableDesc";

			public const string USAGE_SHARING_DISABLE_DESC = "UsageSharingDisableDesc";

			public const string USAGE_SHARING_ENABLE_PROMPT = "UsageSharingEnablePrompt";

			public const string USAGE_SHARING_DISABLE_PROMPT = "UsageSharingDisablePrompt";

			public const string DUPLICATE_LOGIN = "DuplicateLogin";

			public const string DUPLICATE_LOGIN_DETAIL = "DuplicateLoginDetail";

			public const string OFFLINE_TITLE = "OfflineTitle";

			public const string OFFLINE_DESCRIPTION = "OfflineDescription";

			public const string OFFLINE_RETRY = "OfflineRetry";

			public const string MINION_SLOT_FULL = "MinionSlotFull";

			public const string VILLAIN_SLOT_FULL = "VillainSlotFull";

			public const string ORDERBOARD_FTUE_TEXT = "ftue_q6_order";

			public const string DEBRIS_KEY = "Debris";

			public const string EXPAND_STORAGE_KEY = "ExpandStorage";

			public const string MAX_STORAGE_CAPACITY_REACHED = "MaxStorageCapacityReached";

			public const string MAX_STORAGE_EXPANSION_REACHED = "MaxStorageExpansionReached";
		}

		public static class Quests
		{
			public const int MAX_NUM_QUESTS = 5;

			public const int DEFAULT_DURATION = 720;

			public const int DEFAULT_PUSH_NOTE_WARNING_TIME = 540;

			public const bool DEFAULT_REPEAT = false;

			public const string SERVER_START_TIME_KEY = "serverStartTimeUTC";

			public const string SERVER_STOP_TIME_KEY = "serverStopTimeUTC";
		}

		public static class CruiseShip
		{
			public const string PREFAB = "Unique_CruiseShip_Prefab";

			public const int ROUTING_SLOT_COUNT = 2;
		}

		public static class JSON
		{
			public const string TYPE = "type";
		}

		public static class Telemetry
		{
			public const string GAME_PROMPT = "Game Prompt";

			public const string TRUE = "TRUE";

			public const string FREE = "FREE";

			public const string STORE = "STORE";

			public const string UNKNOWN = "unknown";

			public const string RUSH = "Rush";

			public const string SLOT_PURCHASE = "SlotPurchase";

			public const string DELETE_SALE = "DeleteSale";

			public const string REFRESH_MARKET = "RefreshMarket";

			public const string ORDER_COMPLETION = "OrderCompletion";

			public const string ITEM_PURCHASE = "ItemPurchase";

			public const string NO_SWRVE_GROUP_ACTIVE = "anyVariant";
		}

		public static class PushNotes
		{
			public const int MINION_TASK_COMPLETE_ID = 10007;

			public const int BUILDING_CONSTRUCTION_COMPLETE_ID = 10008;

			public const int NEW_VILLAIN_ID = 10009;

			public const int VILLAIN_RESOURCES_ID = 10010;

			public const int CRAFTING_NOTIFICATION_ID = 10011;

			public const int QUEST_WARNING_ID = 10012;

			public const int MARKETPLACE_SOLD_ID = 10013;
		}

		public static class Server
		{
			public const string CLIENT_CONFIG_URL = "/rest/config/{0}/{1}/{2}/{3}/{4}";

			public const string CONTENT_TYPE_JSON = "application/json";

			public const string CONTENT_TYPE_TEXT = "text/plain";

			public const string CONTENT_TYPE_FORM = "application/x-www-form-urlencoded";

			public const string SESSION_HEADER_USER = "user_id";

			public const string SESSION_HEADER_KEY = "session_key";

			public const string DATE_HEADER = "Date";

			public const string COUNTRY_CODE = "X-Kampai-Country";

			public static string VIDEO_PATH = global::Kampai.Util.GameConstants.StaticConfig.CDN_URL + "/video.mp4";

			public const string PLATFORM_UNKNOWN = "unknown";

			public const string PLATFORM_IOS = "iOS";

			public const string PLATFORM_ANDROID = "android";

			public const string PLATFORM_OSX = "osx";

			public const string CLIENT_HEADER_PLATFORM = "K-Platform";

			public const string CLIENT_HEADER_DEVICE = "K-Device";

			public const string CLIENT_HEADER_VERSION = "K-Version";

			public static string SERVER_URL = global::Kampai.Util.GameConstants.StaticConfig.SERVER_URL;

			public static string SERVER_ENVIRONMENT = global::Kampai.Util.GameConstants.StaticConfig.ENVIRONMENT;

			public static string CDN_METADATA_URL = global::Kampai.Util.GameConstants.StaticConfig.CDN_METADATA_URL;
		}

		public static class StaticConfig
		{
			public static string SERVER_URL { get; private set; }

			public static string ENVIRONMENT { get; private set; }

			public static string CDN_METADATA_URL { get; private set; }

			public static string CDN_URL { get; private set; }

			public static string UPSIGHT_TOKEN { get; private set; }

			public static string UPSIGHT_SECRET { get; private set; }

			public static string HOCKEY_APP_ID { get; private set; }

			public static string SWRVE_APP_ID { get; private set; }

			public static string SWRVE_API_KEY { get; private set; }

			public static bool PUSHTNG_USE_PROD_APNS_CERT { get; private set; }

			public static bool DEBUG_ENABLED { get; private set; }

			public static string DELTADNA_ENVIRONMENT_KEY { get; private set; }

			public static string DELTADNA_COLLECT_URL { get; private set; }

			public static string DELTADNA_ENGAGE_URL { get; private set; }

			public static string WWCE_GAME_NAME { get; private set; }

			public static string WWCE_URL { get; private set; }

			public static string WWCE_SECRET { get; private set; }

			public static string DCN_SERVER_URL { get; private set; }

			public static string DCN_APP_TOKEN { get; private set; }

			public static string FACEBOOK_APP_ID { get; private set; }

			public static string MARKETPLACE_SERVER_URL { get; private set; }

			static StaticConfig()
			{
				string staticConfig = global::Kampai.Util.Native.StaticConfig;
				global::System.Collections.Generic.Dictionary<string, object> dictionary = global::Kampai.Util.MiniJSON.KampaiJson.Deserialize(staticConfig) as global::System.Collections.Generic.Dictionary<string, object>;
				SERVER_URL = dictionary["server"].ToString();
				ENVIRONMENT = dictionary["env"].ToString();
				CDN_METADATA_URL = dictionary["cdn_metadata_url"].ToString();
				CDN_URL = dictionary["cdn_url"].ToString();
				UPSIGHT_TOKEN = dictionary["upsight_t"].ToString();
				UPSIGHT_SECRET = dictionary["upsight_s"].ToString();
				HOCKEY_APP_ID = dictionary["hockey"].ToString();
				SWRVE_APP_ID = dictionary["swrve_app_id"].ToString();
				SWRVE_API_KEY = dictionary["swrve_api_key"].ToString();
				PUSHTNG_USE_PROD_APNS_CERT = false;
				initUseProdApnsCert(dictionary);
				initDebugEnabled(dictionary);
				DELTADNA_ENVIRONMENT_KEY = dictionary["ddna_env_key"].ToString();
				DELTADNA_COLLECT_URL = dictionary["ddna_col_url"].ToString();
				DELTADNA_ENGAGE_URL = dictionary["ddna_eng_url"].ToString();
				WWCE_URL = dictionary["wwce_url"].ToString();
				WWCE_SECRET = dictionary["wwce_secret"].ToString();
				WWCE_GAME_NAME = dictionary["wwce_game_name"].ToString();
				DCN_SERVER_URL = dictionary["dcn_server_url"].ToString();
				DCN_APP_TOKEN = dictionary["dcn_app_token"].ToString();
				FACEBOOK_APP_ID = dictionary["facebookAppId"].ToString();
				MARKETPLACE_SERVER_URL = dictionary["marketplace_server_url"].ToString();
			}

			private static void initDebugEnabled(global::System.Collections.Generic.Dictionary<string, object> dict)
			{
				DEBUG_ENABLED = false;
				if (dict.ContainsKey("debug"))
				{
					DEBUG_ENABLED = (bool)dict["debug"];
				}
			}

			private static void initUseProdApnsCert(global::System.Collections.Generic.Dictionary<string, object> dict)
			{
				object value;
				if (dict.TryGetValue("pushtng_use_prod_apns_cert", out value))
				{
					PUSHTNG_USE_PROD_APNS_CERT = (bool)value;
				}
			}
		}

		public static class ApplicationStorePage
		{
			public const string GOOGLE_PLAY_URL = "market://details?id=com.ea.gp.minions";

			public const string APPLE_APPSTORE_URL = "itms-apps://itunes.apple.com/app/id922318156";
		}

		public static class HealthMetrics
		{
			public static class Meters
			{
				public const string APPSTART = "AppFlow.AppStart";

				public const string PURCHASE = "AppFlow.Purchase";

				public const string CRASH = "AppFlow.Crash";

				public const string FATAL = "AppFlow.Fatal.{0}";

				public const string RESUME = "AppFlow.Resume";

				public const string FBLOGIN = "External.Facebook.Login";

				public const string GPLOGIN = "External.Google.Login";

				public const string GCLOGIN = "External.GameCenter.Login";

				public const string SYNERGYCALL = "External.Synergy";
			}

			public static class Timers
			{
				public const string PAUSE = "AppFlow.Pause";

				public const string QUIT = "AppFlow.Quit";

				public const string TAP = "AppFlow.Tap.Interval{0}";
			}
		}

		public static class Shaders
		{
			public const string DEFAULT = "Diffuse";

			public const string TERRAIN_GRID_SHADER = "Kampai/Standard/Texture";

			public const string STANDARD_TEXTURE = "Kampai/Standard/Texture";

			public const string STANDARD_MINION = "Kampai/Standard/Minion";

			public const string PLATFORM = "Kampai/Background/(+4) Platform";

			public const string HIDDEN = "Kampai/Standard/Hidden";
		}

		public static class SupportedShaderFeatureNames
		{
			public const string LIGHTMAP_ON = "LIGHTMAP_ON";

			public const string LIGHTMAP_OFF = "LIGHTMAP_OFF";

			public const string ALPHA_MASK = "ALPHA_MASK";

			public const string TEXTURE_ALPHA = "TEXTURE_ALPHA";

			public const string ALPHA_CLIP = "ALPHA_CLIP";
		}

		public static class ABTest
		{
			public const string CONFIG_ANY_VARIANT = "anyVariant";
		}

		public static class Mood
		{
			public const string MOOD_HAPPY = "HAPPY";

			public const string MOOD_SAD = "SAD";

			public const string MOOD_NEUTRAL = "NEUTRAL";

			public const string MOOD_ANGRY = "ANGRY";
		}

		public static class FirstLoadTipText
		{
			public const string FIRST_LOAD_TIP1 = "defaultTip001";

			public const string FIRST_LOAD_TIP2 = "defaultTip002";

			public const string FIRST_LOAD_TIP3 = "defaultTip003";

			public const string FIRST_LOAD_TIP4 = "defaultTip004";

			public const string FIRST_LOAD_TIP5 = "defaultTip005";

			public const string FIRST_LOAD_TIP6 = "defaultTip006";

			public const string FIRST_LOAD_TIP7 = "defaultTip007";

			public const string FIRST_LOAD_TIP8 = "defaultTip008";

			public const string FIRST_LOAD_TIP9 = "defaultTip009";

			public const string FIRST_LOAD_TIP10 = "defaultTip010";

			public const string FIRST_LOAD_TIP11 = "defaultTip011";
		}

		public static class LoadProgressGates
		{
			public const int SPLASH_START_COMMAND = 10;

			public const int INIT_LOCALIZATION_SERVERICE_COMMAND = 20;

			public const int LOAD_DEFINITIONS_COMMAND = 35;

			public const int LOAD_PLAYER_COMMAND = 35;

			public const int MAIN_COMPLETE_COMMAND = 100;
		}

		public const int SCAFFOLDING_ID = -1;

		public const int COSTUME_ID = -2;

		public const int LOD_LEVELS = 3;

		public const uint RATE_APP_LEVEL = 7u;

		public const uint RATE_APP_PER_LEVEL = 2u;

		public const float CAMERA_AUTO_MOVE_TIME = 0.8f;

		public const string TIKIBIR_STAMP_ALBUM = "StampAlbum";

		public const string TIKIBAR_SHELVE = "Shelve";

		public const string MINION_SELECT_ICON_PREFIX = "selectIcon";

		public const ulong MIN_REQUIRED_INTERNAL_STORAGE_BYTES = 2097152uL;

		public const string BUNDLED_RESOURCE_MANIFEST_PATH = "Manifest";

		public const string PREINSTALLED_LOC_TEXT_PATH = "Loc_Text_Preinstalled/";

		public const int PURCHASED_EXPANSIONS_ID = 354;

		public const int NUMBER_OF_MIGNETTE_COOLDOWN_EVENTS = 10;

		public const float TSM_MODAL_ZOOM_OFFSET = 0.8f;

		public const float CAMERA_ZOOM_WAYFINDER_CABANA = 0.3f;

		public static readonly string PERSISTENT_DATA_PATH = global::Kampai.Util.Native.GetPersistentDataPath();

		public static readonly string DLC_PATH = PERSISTENT_DATA_PATH + "/dlc/";

		public static readonly string VIDEO_PATH = PERSISTENT_DATA_PATH + "/video/intro.mp4";

		public static readonly string CONTENT_PATH = global::UnityEngine.Application.dataPath + "/Content/";

		public static readonly string RESOURCE_PATH = global::UnityEngine.Application.dataPath + "/Content/Resources/";

		public static readonly string RESOURCE_MANIFEST_PATH = PERSISTENT_DATA_PATH + "/DLC_Manifest.json";

		public static readonly string PREINSTALL_JSON_PATH = global::UnityEngine.Application.dataPath + "/Content/Resources/PreinstalledBundles.json";

		public static readonly string PRE_INSTALLED_DLC_PATH = global::UnityEngine.Application.streamingAssetsPath;

		private static readonly float[] DEFAULT_MINION_LOD_HEIGHTS = new float[3] { 0.2f, 0.12f, 0f };

		public static readonly int[] MINION_DEFINITION_IDS = new int[6] { 601, 602, 604, 605, 606, 607 };

		public static readonly global::UnityEngine.Vector3 CAMERA_OFFSET_ACTIONABLE_OBJECT = new global::UnityEngine.Vector3(19f, 0f, -19f);

		public static readonly global::UnityEngine.Vector3 CAMERA_OFFSET_LANDEXPANSION = new global::UnityEngine.Vector3(20.5f, 0f, -18f);

		public static readonly global::UnityEngine.Vector3 CAMERA_OFFSET_WAYFINDER_CABANA = new global::UnityEngine.Vector3(14f, 0f, -14f);

		public static readonly global::UnityEngine.Vector3 TSM_MODAL_OFFSET = new global::UnityEngine.Vector3(17.4f, 0f, -17.4f);

		public static float[] GetLODHeightsArray()
		{
			return (float[])DEFAULT_MINION_LOD_HEIGHTS.Clone();
		}
	}
}
