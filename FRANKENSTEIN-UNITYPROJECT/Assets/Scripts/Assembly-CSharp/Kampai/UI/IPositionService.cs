namespace Kampai.UI
{
	public interface IPositionService
	{
		global::System.Collections.Generic.List<global::UnityEngine.GameObject> HUDElementsToAvoid { get; set; }

		global::Kampai.UI.PositionData GetPositionData(global::UnityEngine.Vector3 worldPosition);

		global::Kampai.UI.SnappablePositionData GetSnappablePositionData(global::Kampai.UI.PositionData normalPositionData, global::Kampai.UI.ViewportBoundary boundary, bool avoidHudElements = false);
	}
}
