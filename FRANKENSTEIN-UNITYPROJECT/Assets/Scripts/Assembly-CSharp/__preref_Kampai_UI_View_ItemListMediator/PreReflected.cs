namespace __preref_Kampai_UI_View_ItemListMediator
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.UI.View.ItemListMediator();
			ConstructorParameters = global::System.Type.EmptyTypes;
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>[35]
			{
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.PickControllerModel), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).model = (global::Kampai.Common.PickControllerModel)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.BuildMenuDefinitionLoadedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).defLoadedSignal = (global::Kampai.UI.View.BuildMenuDefinitionLoadedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.AddStoreTabSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).addTabSignal = (global::Kampai.UI.View.AddStoreTabSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.OnTabClickedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).tabClickSignal = (global::Kampai.UI.View.OnTabClickedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MoveTabMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).moveTabSignal = (global::Kampai.UI.View.MoveTabMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.MoveBuildMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).moveBaseMenuSignal = (global::Kampai.UI.View.MoveBuildMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.StoreButtonClickSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).storeButtonSignal = (global::Kampai.UI.View.StoreButtonClickSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.SetNewUnlockForStoreTabSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).setNewUnlockForTabSignal = (global::Kampai.UI.View.SetNewUnlockForStoreTabSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.PlayGlobalSoundFXSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).audioSignal = (global::Kampai.Main.PlayGlobalSoundFXSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IDefinitionService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).definitionService = (global::Kampai.Game.IDefinitionService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IPlayerService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).playerService = (global::Kampai.Game.IPlayerService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.IQuestService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).questService = (global::Kampai.Game.IQuestService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).glassCanvas = (global::UnityEngine.GameObject)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.Camera), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).myCamera = (global::UnityEngine.Camera)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.BuildingUtilities), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).buildingUtil = (global::Kampai.Game.BuildingUtilities)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.DisableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).disableCameraSignal = (global::Kampai.Game.DisableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.EnableCameraBehaviourSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).enableCameraSignal = (global::Kampai.Game.EnableCameraBehaviourSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::strange.extensions.context.api.ICrossContextCapable), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).gameContext = (global::strange.extensions.context.api.ICrossContextCapable)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.DragAndDropPickSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).dragAndDropSignal = (global::Kampai.Common.DragAndDropPickSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.BuildMenuOpenedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).buildMenuOpened = (global::Kampai.UI.View.BuildMenuOpenedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HighlightStoreItemSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).highlightStoreItemSignal = (global::Kampai.UI.View.HighlightStoreItemSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.DragFromStoreSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).dragFromStoreSignal = (global::Kampai.UI.View.DragFromStoreSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.HideStoreHighlightSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).hideHightlightSignal = (global::Kampai.UI.View.HideStoreHighlightSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UpdateUIButtonsSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).updateStoreButtonsSignal = (global::Kampai.UI.View.UpdateUIButtonsSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.IBuildMenuService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).buildMenuService = (global::Kampai.UI.IBuildMenuService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Main.ILocalizationService), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).localService = (global::Kampai.Main.ILocalizationService)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Game.CameraAutoZoomSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).autoZoomSignal = (global::Kampai.Game.CameraAutoZoomSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.PopupMessageSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).popupMessageSignal = (global::Kampai.UI.View.PopupMessageSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Util.ILogger), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).logger = (global::Kampai.Util.ILogger)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.Common.AppPauseSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).pauseSignal = (global::Kampai.Common.AppPauseSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.ItemListView), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).view = (global::Kampai.UI.View.ItemListView)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIAddedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).uiAddedSignal = (global::Kampai.UI.View.UIAddedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.UIRemovedSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).uiRemovedSignal = (global::Kampai.UI.View.UIRemovedSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::Kampai.UI.View.CloseAllOtherMenuSignal), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).closeAllOtherMenuSignal = (global::Kampai.UI.View.CloseAllOtherMenuSignal)val;
				}),
				new global::System.Collections.Generic.KeyValuePair<global::System.Type, global::System.Action<object, object>>(typeof(global::UnityEngine.GameObject), delegate(object target, object val)
				{
					((global::Kampai.UI.View.ItemListMediator)target).contextView = (global::UnityEngine.GameObject)val;
				})
			};
			object[] array = new object[35];
			array[12] = global::Kampai.Main.MainElement.UI_GLASSCANVAS;
			array[13] = global::Kampai.Main.MainElement.CAMERA;
			array[17] = global::Kampai.Game.GameElement.CONTEXT;
			array[34] = global::strange.extensions.context.api.ContextKeys.CONTEXT_VIEW;
			SetterNames = array;
		}
	}
}
