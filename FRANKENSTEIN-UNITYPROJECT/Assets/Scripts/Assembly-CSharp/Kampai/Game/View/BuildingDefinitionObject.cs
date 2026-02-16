namespace Kampai.Game.View
{
	public abstract class BuildingDefinitionObject : global::Kampai.Game.View.ActionableObject, FootprintProperties
	{
		public int Width { get; protected set; }

		public int Depth { get; protected set; }

		public bool HasSidewalk { get; protected set; }

		public void Init(global::Kampai.Game.BuildingDefinition buildingDefinition, global::Kampai.Game.IDefinitionService definitionService)
		{
			string buildingFootprint = definitionService.GetBuildingFootprint(buildingDefinition.FootprintID);
			Width = BuildingUtil.GetFootprintWidth(buildingFootprint);
			Depth = BuildingUtil.GetFootprintDepth(buildingFootprint);
			HasSidewalk = buildingFootprint.Contains(".");
			base.Init();
		}
	}
}
