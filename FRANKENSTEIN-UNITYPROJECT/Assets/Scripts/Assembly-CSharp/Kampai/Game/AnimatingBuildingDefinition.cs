namespace Kampai.Game
{
	public abstract class AnimatingBuildingDefinition : global::Kampai.Game.RepairableBuildingDefinition
	{
		public int GagFrequency;

		public global::System.Collections.Generic.IList<global::Kampai.Game.BuildingAnimationDefinition> AnimationDefinitions { get; set; }

		protected override bool DeserializeProperty(string propertyName, global::Newtonsoft.Json.JsonReader reader, JsonConverters converters)
		{
			switch (propertyName)
			{
			default:
			{
				int num;
						num = 1; // Added this line to remove use of unassigned variable error
				if (num == 1)
				{
					reader.Read();
					GagFrequency = global::System.Convert.ToInt32(reader.Value);
					break;
				}
				return base.DeserializeProperty(propertyName, reader, converters);
			}
			case "ANIMATIONDEFINITIONS":
				reader.Read();
				AnimationDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.BuildingAnimationDefinition>(reader, converters);
				break;
			}
			return true;
		}

		public virtual global::System.Collections.Generic.IList<string> AnimationControllerKeys()
		{
			global::System.Collections.Generic.IList<string> list = new global::System.Collections.Generic.List<string>();
			if (AnimationDefinitions != null && AnimationDefinitions.Count > 0)
			{
				foreach (global::Kampai.Game.BuildingAnimationDefinition animationDefinition in AnimationDefinitions)
				{
					if (!string.IsNullOrEmpty(animationDefinition.BuildingController))
					{
						list.Add(animationDefinition.BuildingController);
					}
					if (!string.IsNullOrEmpty(animationDefinition.MinionController))
					{
						list.Add(animationDefinition.MinionController);
					}
				}
			}
			return list;
		}
	}
}
