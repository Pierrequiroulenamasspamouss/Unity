namespace Kampai.Game
{
	public class CruiseShipModel
	{
		public int[] slots = new int[2];

		public global::System.Collections.Generic.List<int> queue = new global::System.Collections.Generic.List<int>();

		public CruiseShipModel()
		{
			int i = 0;
			for (int num = slots.Length; i < num; i++)
			{
				slots[i] = -1;
			}
		}

		public int GetSlotForId(int id)
		{
			int i = 0;
			for (int num = slots.Length; i < num; i++)
			{
				if (slots[i] == id)
				{
					return i;
				}
			}
			return -1;
		}
	}
}
