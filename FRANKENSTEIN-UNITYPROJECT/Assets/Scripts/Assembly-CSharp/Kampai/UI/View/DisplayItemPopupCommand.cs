namespace Kampai.UI.View
{
	public class DisplayItemPopupCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int itemDefinitionID { get; set; }

		[Inject]
		public global::UnityEngine.RectTransform imageTransform { get; set; }

		[Inject]
		public global::Kampai.UI.View.UIPopupType popupType { get; set; }

		[Inject]
		public global::Kampai.Game.IDefinitionService definitionService { get; set; }

		[Inject(global::Kampai.UI.View.UIElement.CAMERA)]
		public global::UnityEngine.Camera uiCamera { get; set; }

		[Inject]
		public global::Kampai.UI.View.IGUIService guiService { get; set; }

		public override void Execute()
		{
			global::UnityEngine.Vector3[] array = new global::UnityEngine.Vector3[4];
			imageTransform.GetWorldCorners(array);
			global::UnityEngine.Vector3 position = default(global::UnityEngine.Vector3);
			global::UnityEngine.Vector3[] array2 = array;
			foreach (global::UnityEngine.Vector3 vector in array2)
			{
				position += vector;
			}
			position /= 4f;
			global::UnityEngine.Vector3 vector2 = uiCamera.WorldToViewportPoint(position);
			global::Kampai.Game.ItemDefinition value = definitionService.Get<global::Kampai.Game.ItemDefinition>(itemDefinitionID);
			switch (popupType)
			{
			case global::Kampai.UI.View.UIPopupType.GENERIC:
			{
				global::Kampai.UI.View.IGUICommand iGUICommand2 = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.LoadUntrackedInstance, "cmp_GenericTooltip");
				global::Kampai.UI.View.GUIArguments args2 = iGUICommand2.Args;
				args2.Add(vector2);
				args2.Add(typeof(global::Kampai.Game.ItemDefinition), value);
				guiService.Execute(iGUICommand2);
				break;
			}
			case global::Kampai.UI.View.UIPopupType.CRAFTING:
			{
				global::Kampai.UI.View.IGUICommand iGUICommand = guiService.BuildCommand(global::Kampai.UI.View.GUIOperation.LoadUntrackedInstance, "cmp_CraftingTooltip");
				global::Kampai.UI.View.GUIArguments args = iGUICommand.Args;
				args.Add(vector2);
				args.Add(typeof(global::Kampai.Game.ItemDefinition), value);
				guiService.Execute(iGUICommand);
				break;
			}
			}
		}
	}
}
