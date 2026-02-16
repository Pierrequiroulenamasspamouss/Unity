public static class DooberUtil
{
	public static void CheckForTween(global::Kampai.Game.Transaction.TransactionDefinition transactionDef, global::System.Collections.Generic.List<global::UnityEngine.GameObject> items, bool allowTweenToStorage, global::UnityEngine.Camera uiCamera, global::Kampai.UI.View.SpawnDooberSignal tweenSignal, global::Kampai.Game.IDefinitionService definitionService)
	{
		for (int i = 0; i < transactionDef.Outputs.Count; i++)
		{
			global::Kampai.Util.QuantityItem quantityItem = transactionDef.Outputs[i];
			global::UnityEngine.RectTransform transform = items[i].transform as global::UnityEngine.RectTransform;
			DetermineTweenToUse(quantityItem.ID, transform, allowTweenToStorage, uiCamera, tweenSignal, definitionService);
		}
	}

	public static void CheckForTween(global::System.Collections.Generic.List<global::Kampai.UI.View.RewardSliderView> views, bool allowTweenToStorage, global::UnityEngine.Camera uiCamera, global::Kampai.UI.View.SpawnDooberSignal tweenSignal, global::Kampai.Game.IDefinitionService definitionService)
	{
		for (int i = 0; i < views.Count; i++)
		{
			global::UnityEngine.RectTransform transform = views[i].icon.transform as global::UnityEngine.RectTransform;
			DetermineTweenToUse(views[i].ID, transform, allowTweenToStorage, uiCamera, tweenSignal, definitionService);
		}
	}

	private static void DetermineTweenToUse(int id, global::UnityEngine.RectTransform transform, bool allowTweenToStorage, global::UnityEngine.Camera uiCamera, global::Kampai.UI.View.SpawnDooberSignal tweenSignal, global::Kampai.Game.IDefinitionService definitionService)
	{
		global::Kampai.UI.View.DestinationType destinationType = GetDestinationType(id, definitionService);
		if (allowTweenToStorage || destinationType != global::Kampai.UI.View.DestinationType.STORAGE)
		{
			tweenSignal.Dispatch(uiCamera.WorldToScreenPoint(transform.position), destinationType, id, false);
		}
	}

	public static global::Kampai.UI.View.DestinationType GetDestinationType(int definitionID, global::Kampai.Game.IDefinitionService definitionService)
	{
		global::Kampai.Game.Definition definition = definitionService.Get<global::Kampai.Game.Definition>(definitionID);
		switch (definitionID)
		{
		case 0:
			return global::Kampai.UI.View.DestinationType.GRIND;
		case 1:
			return global::Kampai.UI.View.DestinationType.PREMIUM;
		case 2:
			return global::Kampai.UI.View.DestinationType.XP;
		default:
			if (definition is global::Kampai.Game.StickerDefinition)
			{
				return global::Kampai.UI.View.DestinationType.STICKER;
			}
			return global::Kampai.UI.View.DestinationType.STORAGE;
		}
	}
}
