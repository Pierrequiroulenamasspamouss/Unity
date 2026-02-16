namespace Kampai.Game
{
	[global::Kampai.Util.RequiresJsonConverter]
	public class ItemDefinition : global::Kampai.Game.TaxonomyDefinition, global::Kampai.Util.IBuilder<global::Kampai.Game.Instance>
	{
		public float BasePremiumCost { get; set; }

		public int BaseGrindCost { get; set; }

		public float TSMRewardMultipler { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			case "BASEPREMIUMCOST":
				reader.Read();
				BasePremiumCost = global::System.Convert.ToSingle(reader.Value);
				break;
			case "BASEGRINDCOST":
				reader.Read();
				BaseGrindCost = global::System.Convert.ToInt32(reader.Value);
				break;
			case "TSMREWARDMULTIPLER":
				reader.Read();
				TSMRewardMultipler = global::System.Convert.ToSingle(reader.Value);
				break;
			default:
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			return true;
		}

		public virtual global::Kampai.Game.Instance Build()
		{
			return new global::Kampai.Game.Item(this);
		}
	}
}
