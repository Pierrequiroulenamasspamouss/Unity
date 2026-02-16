namespace Kampai.Game
{
	public class CameraInstanceFocusCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public int buildingId { get; set; }

		[Inject]
		public global::UnityEngine.Vector3 focusPosition { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject(global::Kampai.Main.MainElement.CAMERA)]
		public global::UnityEngine.Camera camera { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.ShowHiddenBuildingsSignal showHiddenBuildingsSignal { get; set; }

		public override void Execute()
		{
			showHiddenBuildingsSignal.Dispatch();
			global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			global::System.Collections.Generic.LinkedList<global::Kampai.Game.View.ActionableObject> linkedList = new global::System.Collections.Generic.LinkedList<global::Kampai.Game.View.ActionableObject>();
			global::System.Collections.Generic.LinkedList<global::Kampai.Game.View.ActionableObject> linkedList2 = new global::System.Collections.Generic.LinkedList<global::Kampai.Game.View.ActionableObject>();
			component.GetOccludingObjects(focusPosition, buildingId, linkedList, linkedList2);
			foreach (global::Kampai.Game.View.ActionableObject item in linkedList)
			{
				if (item.ID != buildingId)
				{
					item.FadeGFX(0.5f, false);
					item.FadeSFX(0.5f, false);
				}
			}
			foreach (global::Kampai.Game.View.ActionableObject item2 in linkedList2)
			{
				if (item2.ID != buildingId)
				{
					item2.FadeSFX(0.5f, false);
				}
			}
		}
	}
}
