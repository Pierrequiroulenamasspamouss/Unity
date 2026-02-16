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
                case "GAGFREQUENCY": // On gère explicitement le GagFrequency ici
                    reader.Read();
                    // Utilise SafeParseInt si tu l'as ajouté à ReaderUtil, 
                    // sinon garde Convert.ToInt32 mais avec la sécurité du case.
                    GagFrequency = global::Kampai.Util.ReaderUtil.SafeParseInt(reader.Value);
                    return true;

                case "ANIMATIONDEFINITIONS":
                    reader.Read();
                    AnimationDefinitions = global::Kampai.Util.ReaderUtil.PopulateList<global::Kampai.Game.BuildingAnimationDefinition>(reader, converters);
                    return true;

                default:
                    // Si la propriété n'est pas connue par cette classe, 
                    // on laisse la classe parente (RepairableBuildingDefinition) s'en occuper.
                    return base.DeserializeProperty(propertyName, reader, converters);
            }
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
