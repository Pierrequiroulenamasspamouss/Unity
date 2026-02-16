namespace __preref_Kampai_Game_SocialClaimRewardItem
{
	internal sealed class PreReflected : global::strange.extensions.reflector.impl.ReflectedClass
	{
		public PreReflected()
		{
			PreGenerated = true;
			Constructor = (object[] p) => new global::Kampai.Game.SocialClaimRewardItem((int)p[0], (global::Kampai.Game.SocialClaimRewardItem.ClaimState)(int)p[1]);
			ConstructorParameters = new global::System.Type[2]
			{
				typeof(int),
				typeof(global::Kampai.Game.SocialClaimRewardItem.ClaimState)
			};
			PostConstructors = KampaiPreprocessedModule.EmptyPostConstructorsArray;
			Setters = KampaiPreprocessedModule.EmptySettersArray;
			SetterNames = KampaiPreprocessedModule.EmptyObjectsArray;
		}
	}
}
