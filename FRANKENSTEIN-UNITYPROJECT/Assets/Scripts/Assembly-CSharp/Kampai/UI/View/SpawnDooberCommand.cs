namespace Kampai.UI.View
{
    public class SpawnDooberCommand : global::Kampai.UI.View.DooberCommand, global::strange.extensions.pool.api.IPoolable, global::Kampai.Util.IFastPooledCommand<global::UnityEngine.Vector3, global::Kampai.UI.View.DestinationType, int, bool>, global::Kampai.Util.IFastPooledCommandBase
    {
        private int itemDefinitionId;

        private global::Kampai.UI.View.DestinationType type;

        private float iconWidth = 70f;

        private float zOffset = 10f;

        [Inject]
        public global::Kampai.Game.IDefinitionService definitionService { get; set; }

        [Inject(global::Kampai.Game.GameElement.CONTEXT)]
        public global::strange.extensions.context.api.ICrossContextCapable gameContext { get; set; }

        [Inject(global::Kampai.UI.View.UIElement.CAMERA)]
        public global::UnityEngine.Camera uiCamera { get; set; }

        // --- FIX: REMOVED [Inject] ATTRIBUTE ---
        // This prevents the "Attempt to Instantiate a null binding" crash.
        // We will find the HUD manually in Execute().
        public global::UnityEngine.GameObject hud { get; set; }

        [Inject]
        public ILocalPersistanceService localPersistence { get; set; }

        [Inject]
        public global::Kampai.UI.View.PeekHUDSignal peekHUDSignal { get; set; }

        public void Execute(global::UnityEngine.Vector3 iconPos, global::Kampai.UI.View.DestinationType _type, int itemDefinitionID, bool fromWorldCanvas)
        {
            // --- FIX: ROBUST HUD LOOKUP ---
            if (hud == null)
            {
                // Try to find the HUD in the scene if it wasn't injected
                hud = global::UnityEngine.GameObject.Find("HUD") ??
                      global::UnityEngine.GameObject.Find("HUD_Phone") ??
                      global::UnityEngine.GameObject.Find("HUD_Tablet");

                // If HUD is still missing (because assets failed), abort gracefully.
                if (hud == null)
                {
                    return;
                }
            }
            // ------------------------------

            base.fromWorldCanvas = fromWorldCanvas;
            iconPosition = iconPos;
            type = _type;
            itemDefinitionId = itemDefinitionID;
            global::UnityEngine.Transform transform = base.glassCanvas.transform;
            global::UnityEngine.GameObject gameObject = CreateTweenObject(transform);
            global::Kampai.UI.View.KampaiImage component = gameObject.GetComponent<global::Kampai.UI.View.KampaiImage>();
            global::UnityEngine.Vector3 destination = global::UnityEngine.Vector3.zero;
            peekHUDSignal.Dispatch(3f);
            switch (type)
            {
                case global::Kampai.UI.View.DestinationType.XP:
                    destination = GetXPGlassPosition();
                    itemDefinitionId = 2;
                    break;
                case global::Kampai.UI.View.DestinationType.GRIND:
                    destination = GetGrindGlassPosition();
                    itemDefinitionId = 0;
                    break;
                case global::Kampai.UI.View.DestinationType.PREMIUM:
                    destination = GetPremiumGlassPosition();
                    itemDefinitionId = 1;
                    break;
                case global::Kampai.UI.View.DestinationType.STORAGE:
                    destination = GetStorageGlassPosition();
                    break;
                case global::Kampai.UI.View.DestinationType.STICKER:
                    destination = GetTikiHutGlassPosition();
                    gameContext.injectionBinder.GetInstance<global::Kampai.Game.ToggleStickerbookGlowSignal>().Dispatch(true);
                    localPersistence.PutDataPlayer("StickerbookGlow", "Enable");
                    break;
            }
            component.sprite = GetIconFromDefinitionID(itemDefinitionId);
            component.material = global::Kampai.Util.KampaiResources.Load<global::UnityEngine.Material>("CircleIconAlphaMaskMat");
            component.maskSprite = GetMaskFromDefinitionId(itemDefinitionId);
            destination.z = zOffset;
            TweenToDestination(gameObject, destination, 1f, type);
        }

        private global::UnityEngine.Vector3 GetXPGlassPosition()
        {
            if (base.dooberModel.XPGlassPosition == global::UnityEngine.Vector3.zero)
            {
                // Safety check
                if (hud != null && hud.transform.Find("group_XPMeter/icn_XP") != null)
                    base.dooberModel.XPGlassPosition = hud.transform.Find("group_XPMeter/icn_XP").position;
            }
            return base.dooberModel.XPGlassPosition;
        }

        private global::UnityEngine.Vector3 GetGrindGlassPosition()
        {
            if (base.dooberModel.GrindGlassPosition == global::UnityEngine.Vector3.zero)
            {
                if (hud != null && hud.transform.Find("group_Currency_Grind/icn_Currency_Grind") != null)
                    base.dooberModel.GrindGlassPosition = hud.transform.Find("group_Currency_Grind/icn_Currency_Grind").position;
            }
            return base.dooberModel.GrindGlassPosition;
        }

        private global::UnityEngine.Vector3 GetPremiumGlassPosition()
        {
            if (base.dooberModel.PremiumGlassPosition == global::UnityEngine.Vector3.zero)
            {
                if (hud != null && hud.transform.Find("group_Currency_Premium/icn_Currency_Premium") != null)
                    base.dooberModel.PremiumGlassPosition = hud.transform.Find("group_Currency_Premium/icn_Currency_Premium").position;
            }
            return base.dooberModel.PremiumGlassPosition;
        }

        private global::UnityEngine.Vector3 GetStorageGlassPosition()
        {
            if (base.dooberModel.StorageGlassPosition == global::UnityEngine.Vector3.zero)
            {
                if (hud != null && hud.transform.Find("group_Storage/icn_Storage") != null)
                    base.dooberModel.StorageGlassPosition = hud.transform.Find("group_Storage/icn_Storage").position;
            }
            return base.dooberModel.StorageGlassPosition;
        }

        private global::UnityEngine.Vector3 GetTikiHutGlassPosition()
        {
            if (base.dooberModel.TikiBarWorldPosition == global::UnityEngine.Vector3.zero)
            {
                global::UnityEngine.GameObject instance = gameContext.injectionBinder.GetInstance<global::UnityEngine.GameObject>(global::Kampai.Game.GameElement.BUILDING_MANAGER);
                if (instance != null)
                {
                    global::Kampai.Game.View.BuildingManagerView component = instance.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
                    global::Kampai.Game.View.BuildingObject buildingObject = component.GetBuildingObject(313);
                    if (buildingObject != null)
                        base.dooberModel.TikiBarWorldPosition = global::UnityEngine.Camera.main.WorldToViewportPoint(buildingObject.transform.position);
                }
            }
            return uiCamera.ViewportToWorldPoint(base.dooberModel.TikiBarWorldPosition);
        }

        private global::UnityEngine.GameObject CreateTweenObject(global::UnityEngine.Transform glassTransform)
        {
            global::UnityEngine.Vector2 screenStartPosition = GetScreenStartPosition();
            global::UnityEngine.GameObject original = global::Kampai.Util.KampaiResources.Load("TweeningDoober") as global::UnityEngine.GameObject;

            // FIX: If resource is missing, create a dummy object to prevent crash
            if (original == null)
            {
                original = new global::UnityEngine.GameObject("DummyTweenObject");
                original.AddComponent<global::UnityEngine.RectTransform>();
                original.AddComponent<global::Kampai.UI.View.KampaiImage>();
            }

            global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(original) as global::UnityEngine.GameObject;
            gameObject.name = "ScreenTween Object";
            global::UnityEngine.RectTransform component = gameObject.GetComponent<global::UnityEngine.RectTransform>();
            component.anchorMin = global::UnityEngine.Vector2.zero;
            component.anchorMax = global::UnityEngine.Vector2.zero;
            gameObject.transform.SetParent(glassTransform, false);
            gameObject.transform.localPosition = global::UnityEngine.Vector3.zero;
            gameObject.transform.position = new global::UnityEngine.Vector3(gameObject.transform.position.x, gameObject.transform.position.y, zOffset);
            component.offsetMin = new global::UnityEngine.Vector2(screenStartPosition.x - iconWidth / 2f, screenStartPosition.y - iconWidth / 2f);
            component.offsetMax = new global::UnityEngine.Vector2(screenStartPosition.x + iconWidth / 2f, screenStartPosition.y + iconWidth / 2f);
            gameObject.transform.localScale = global::UnityEngine.Vector3.one;
            return gameObject;
        }

        private global::UnityEngine.Sprite GetIconFromDefinitionID(int id)
        {
            global::Kampai.Game.DisplayableDefinition displayableDefinition = definitionService.Get<global::Kampai.Game.DisplayableDefinition>(id);
            if (displayableDefinition != null)
            {
                return UIUtils.LoadSpriteFromPath(displayableDefinition.Image);
            }
            return null;
        }

        private global::UnityEngine.Sprite GetMaskFromDefinitionId(int id)
        {
            global::Kampai.Game.DisplayableDefinition displayableDefinition = definitionService.Get<global::Kampai.Game.DisplayableDefinition>(id);
            if (displayableDefinition != null)
            {
                return UIUtils.LoadSpriteFromPath(displayableDefinition.Mask);
            }
            return null;
        }
    }
}