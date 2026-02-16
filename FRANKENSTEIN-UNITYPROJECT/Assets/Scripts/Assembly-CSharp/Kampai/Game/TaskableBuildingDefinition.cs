using Kampai.Util;

namespace Kampai.Game
{
	public abstract class TaskableBuildingDefinition : global::Kampai.Game.AnimatingBuildingDefinition
	{
		public global::System.Collections.Generic.IList<global::Kampai.Game.SlotUnlock> SlotUnlocks { get; set; }

		public int DefaultSlots { get; set; }

		public int RushCost { get; set; }

		public string ModalDescription { get; set; }

		public int GagID { get; set; }

		public global::UnityEngine.Vector3 HarvestableIconOffset { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "SLOTUNLOCKS":
				reader.Read();
                    //SlotUnlocks = global::Kampai.Util.ReaderUtil.PopulateList(reader, converters, global::Kampai.Util.ReaderUtil.ReadSlotUnlock);
                    SlotUnlocks = ReaderUtil.PopulateList<SlotUnlock>(
        reader,
        converters,
        ReaderUtil.ReadSlotUnlock
    );

                    break;
			case "DEFAULTSLOTS":
				reader.Read();
				DefaultSlots = global::System.Convert.ToInt32(reader.Value);
				break;
			case "RUSHCOST":
				reader.Read();
				RushCost = global::System.Convert.ToInt32(reader.Value);
				break;
			case "MODALDESCRIPTION":
				reader.Read();
				ModalDescription = global::Kampai.Util.ReaderUtil.ReadString(reader, converters);
				break;
			case "GAGID":
				reader.Read();
				GagID = global::System.Convert.ToInt32(reader.Value);
				break;
			case "HARVESTABLEICONOFFSET":
				reader.Read();
				HarvestableIconOffset = global::Kampai.Util.ReaderUtil.ReadVector3(reader, converters);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}
	}
}
