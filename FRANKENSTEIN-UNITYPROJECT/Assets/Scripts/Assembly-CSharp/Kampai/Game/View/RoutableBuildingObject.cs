namespace Kampai.Game.View
{
	public class RoutableBuildingObject : global::Kampai.Game.View.BuildingObject
	{
		protected global::UnityEngine.Transform[] routes;

		private bool routeToSlot;

		protected int stations;

		internal override void Init(global::Kampai.Game.Building building, global::Kampai.Util.ILogger logger, global::System.Collections.Generic.IDictionary<string, global::UnityEngine.RuntimeAnimatorController> controllers, global::Kampai.Game.IDefinitionService definitionService)
		{
			base.Init(building, logger, controllers, definitionService);
			global::Kampai.Game.BuildingDefinition definition = building.Definition;
			if (definition == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.BV_ILLEGAL_ROUTABLE_DEFINITION, building.Definition.ID.ToString());
			}
			routeToSlot = definition.RouteToSlot;
			stations = definition.WorkStations;
			if (building.IsBuildingRepaired())
			{
				routes = new global::UnityEngine.Transform[stations];
				for (int i = 0; i < stations; i++)
				{
					global::UnityEngine.GameObject route = GetRoute(i);
					routes[i] = route.transform;
				}
			}
		}

		internal global::UnityEngine.Vector3 GetRoutePosition(int routeIndex, global::Kampai.Game.Building building, global::UnityEngine.Vector3 startingPosition)
		{
			if (routeToSlot && routeIndex >= 0 && routeIndex < routes.Length)
			{
				return routes[routeIndex].position;
			}
			global::Kampai.Util.Point closestBuildingSidewalk = BuildingUtil.GetClosestBuildingSidewalk(building.Location, startingPosition, definitionService.GetBuildingFootprint(building.Definition.FootprintID));
			return new global::UnityEngine.Vector3(closestBuildingSidewalk.x, 0f, closestBuildingSidewalk.y);
		}

		private global::UnityEngine.GameObject GetRoute(int index)
		{
			global::UnityEngine.GameObject gameObject = base.gameObject.FindChild("route" + index);
			if (gameObject == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.BV_NO_ROUTE, "BV_NO_ROUTE: Building ID: {0}, Route Index: {1}", ID, index);
			}
			return gameObject;
		}

		internal int GetNumberOfStations()
		{
			return routes.Length;
		}

		internal virtual global::UnityEngine.Vector3 GetRouteRotation(int routeIndex)
		{
			if (routeIndex >= 0 && routeIndex < routes.Length)
			{
				return routes[routeIndex].rotation.eulerAngles;
			}
			return global::UnityEngine.Vector3.zero;
		}

		public virtual void MoveToRoutingPosition(global::Kampai.Game.View.CharacterObject characterObject, int routingIndex)
		{
			if (routingIndex < 0 || routingIndex >= routes.Length)
			{
				logger.Error("MoveToRoutingPosition: routingIndex {0} out of range (routes.Length={1})", routingIndex, routes.Length);
			}
			else
			{
				global::UnityEngine.Transform transform = routes[routingIndex].transform;
				characterObject.gameObject.transform.position = transform.position;
				characterObject.gameObject.transform.rotation = transform.rotation;
			}
		}
	}
}
