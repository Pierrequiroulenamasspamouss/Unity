namespace Kampai.Game
{
	public class CruiseShipService
	{
		[Inject]
		public global::Kampai.Game.CruiseShipModel model { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.VillainGotoBoatSignal gotoBoatSignal { get; set; }

		[Inject]
		public global::Kampai.Game.VillainGotoCarpetSignal gotoCarpetSignal { get; set; }

		public void MoveIn(int id)
		{
			int slotForId = model.GetSlotForId(-1);
			if (slotForId == -1)
			{
				model.queue.Add(id);
			}
			else
			{
				model.slots[slotForId] = id;
			}
			gotoBoatSignal.Dispatch(new global::Kampai.Util.Tuple<int, int>(id, slotForId));
		}

		public bool MoveOut(int id)
		{
			int slotForId = model.GetSlotForId(id);
			if (slotForId == -1)
			{
				model.queue.Remove(id);
			}
			else if (model.queue.Count > 0)
			{
				int index = model.queue.Count - 1;
				int num = model.queue[index];
				model.queue.RemoveAt(index);
				model.slots[slotForId] = num;
				gotoBoatSignal.Dispatch(new global::Kampai.Util.Tuple<int, int>(num, slotForId));
			}
			else
			{
				model.slots[slotForId] = -1;
			}
			gotoCarpetSignal.Dispatch(id);
			return true;
		}
	}
}
