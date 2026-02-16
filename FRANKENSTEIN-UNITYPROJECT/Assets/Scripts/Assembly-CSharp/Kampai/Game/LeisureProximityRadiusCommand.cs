namespace Kampai.Game
{
	public class LeisureProximityRadiusCommand
	{
		private global::System.Collections.Generic.Dictionary<global::Kampai.Game.LeisureBuilding, global::System.Collections.Generic.List<int>> leisureToMinionMap = new global::System.Collections.Generic.Dictionary<global::Kampai.Game.LeisureBuilding, global::System.Collections.Generic.List<int>>();

		private global::System.Collections.Generic.List<global::Kampai.Game.LeisureBuilding> leisureBuildings = new global::System.Collections.Generic.List<global::Kampai.Game.LeisureBuilding>();

		private global::System.Collections.Generic.List<global::Kampai.Game.Minion> minions = new global::System.Collections.Generic.List<global::Kampai.Game.Minion>();

		private global::System.Collections.Generic.List<global::Kampai.Game.LeisureBuilding> filteredLeisureBuildings = new global::System.Collections.Generic.List<global::Kampai.Game.LeisureBuilding>();

		[Inject]
		public global::Kampai.Game.IPlayerService playerService { get; set; }

		[Inject(global::Kampai.Game.GameElement.BUILDING_MANAGER)]
		public global::UnityEngine.GameObject buildingManager { get; set; }

		[Inject(global::Kampai.Game.GameElement.MINION_MANAGER)]
		public global::UnityEngine.GameObject minionManager { get; set; }

		[Inject]
		public global::Kampai.Game.SendMinionToLeisureSignal sendMinionToLeisureSignal { get; set; }

		[Inject]
		public global::Kampai.Game.ITimeService timeService { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randomService { get; set; }

		[Inject]
		public global::Kampai.Common.PickControllerModel model { get; set; }

		private void clearLeisureToMinionsMap()
		{
			global::System.Collections.Generic.Dictionary<global::Kampai.Game.LeisureBuilding, global::System.Collections.Generic.List<int>>.Enumerator enumerator = leisureToMinionMap.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					enumerator.Current.Value.Clear();
				}
			}
			finally
			{
				enumerator.Dispose();
			}
		}

		public void Execute()
		{
			global::Kampai.Game.View.BuildingManagerView component = buildingManager.GetComponent<global::Kampai.Game.View.BuildingManagerView>();
			global::Kampai.Game.View.MinionManagerView component2 = minionManager.GetComponent<global::Kampai.Game.View.MinionManagerView>();
			leisureBuildings.Clear();
			playerService.GetInstancesByType(ref leisureBuildings);
			minions.Clear();
			playerService.GetInstancesByType(ref minions);
			clearLeisureToMinionsMap();
			global::System.Collections.Generic.Dictionary<global::Kampai.Game.LeisureBuilding, global::System.Collections.Generic.List<int>> dictionary = leisureToMinionMap;
			for (int i = 0; i < leisureBuildings.Count; i++)
			{
				global::Kampai.Game.LeisureBuilding leisureBuilding = leisureBuildings[i];
				if (leisureBuilding.State != global::Kampai.Game.BuildingState.Idle)
				{
					continue;
				}
				int? selectedBuilding = model.SelectedBuilding;
				if (selectedBuilding.GetValueOrDefault() == leisureBuilding.ID && selectedBuilding.HasValue)
				{
					continue;
				}
				global::Kampai.Game.View.LeisureBuildingObjectView buildingObject = component.GetBuildingObject(leisureBuilding.ID) as global::Kampai.Game.View.LeisureBuildingObjectView;
				for (int j = 0; j < minions.Count; j++)
				{
					global::Kampai.Game.Minion minion = minions[j];
					global::Kampai.Game.View.MinionObject minionObject = component2.Get(minion.ID);
					if (!(minionObject == null) && IsMinionEligible(leisureBuilding, buildingObject, minion, minionObject))
					{
						if (!dictionary.ContainsKey(leisureBuilding))
						{
							dictionary.Add(leisureBuilding, new global::System.Collections.Generic.List<int>());
						}
						dictionary[leisureBuilding].Add(minion.ID);
					}
				}
			}
			global::System.Collections.Generic.List<global::Kampai.Game.LeisureBuilding> list = FilterBuildings(dictionary);
			if (list.Count <= 0)
			{
				return;
			}
			global::Kampai.Game.LeisureBuilding leisureBuilding2 = SelectRandomBuilding(list);
			int num = 0;
			global::System.Collections.Generic.List<int> list2 = dictionary[leisureBuilding2];
			for (int k = 0; k < list2.Count; k++)
			{
				int second = list2[k];
				if (num == leisureBuilding2.Definition.WorkStations)
				{
					break;
				}
				sendMinionToLeisureSignal.Dispatch(new global::Kampai.Util.Tuple<int, int, int>(leisureBuilding2.ID, second, timeService.GameTimeSeconds()));
				num++;
			}
		}

		private global::System.Collections.Generic.List<global::Kampai.Game.LeisureBuilding> FilterBuildings(global::System.Collections.Generic.Dictionary<global::Kampai.Game.LeisureBuilding, global::System.Collections.Generic.List<int>> d)
		{
			global::System.Collections.Generic.List<global::Kampai.Game.LeisureBuilding> list = filteredLeisureBuildings;
			list.Clear();
			global::System.Collections.Generic.Dictionary<global::Kampai.Game.LeisureBuilding, global::System.Collections.Generic.List<int>>.Enumerator enumerator = d.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					global::Kampai.Game.LeisureBuilding key = enumerator.Current.Key;
					if (key.Definition.WorkStations > enumerator.Current.Value.Count)
					{
						continue;
					}
					if (list.Count == 0)
					{
						list.Add(key);
						continue;
					}
					if (key.Definition.ProximityPriority > list[0].Definition.ProximityPriority)
					{
						list.Clear();
					}
					list.Add(key);
				}
				return list;
			}
			finally
			{
				enumerator.Dispose();
			}
		}

		private bool IsMinionEligible(global::Kampai.Game.LeisureBuilding building, global::Kampai.Game.View.LeisureBuildingObjectView buildingObject, global::Kampai.Game.Minion minion, global::Kampai.Game.View.MinionObject minionObject)
		{
			if (minion.State != global::Kampai.Game.MinionState.Idle)
			{
				return false;
			}
			float num = global::UnityEngine.Vector3.Distance(buildingObject.transform.position, minionObject.transform.position);
			if (num > building.Definition.ProximityRadius)
			{
				return false;
			}
			if (timeService.GameTimeSeconds() - minion.LastLeisureTime < minion.Definition.LeisureCooldownTime)
			{
				return false;
			}
			return true;
		}

		private global::Kampai.Game.LeisureBuilding SelectRandomBuilding(global::System.Collections.Generic.List<global::Kampai.Game.LeisureBuilding> lbs)
		{
			int index = randomService.NextInt(lbs.Count);
			return lbs[index];
		}
	}
}
