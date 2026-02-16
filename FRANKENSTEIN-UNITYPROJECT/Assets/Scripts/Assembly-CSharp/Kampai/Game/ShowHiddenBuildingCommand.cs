namespace Kampai.Game
{
	public class ShowHiddenBuildingCommand : global::strange.extensions.command.impl.Command
	{
		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			global::System.Collections.Generic.ICollection<global::Kampai.Game.View.ActionableObject> fadedObjects = component.GetFadedObjects();
			foreach (global::Kampai.Game.View.ActionableObject item in fadedObjects)
			{
				item.FadeGFX(0.5f, true);
				item.FadeSFX(0.5f, true);
			}
		}
	}
}
