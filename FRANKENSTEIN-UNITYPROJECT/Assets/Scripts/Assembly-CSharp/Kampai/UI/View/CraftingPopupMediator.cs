namespace Kampai.UI.View
{
	public class CraftingPopupMediator : global::Kampai.UI.View.AbstractGenericPopupMediator<global::Kampai.UI.View.CraftingPopupView>
	{
		public override void Register(global::Kampai.Game.ItemDefinition itemDef, global::UnityEngine.Vector3 itemCenter)
		{
			base.view.Display(itemCenter, base.playerService, base.definitionService, base.localizationService);
			base.view.SetName(base.localizationService.GetString(itemDef.LocalizedKey));
			global::Kampai.Game.IngredientsItemDefinition ingredientsItemDefinition = itemDef as global::Kampai.Game.IngredientsItemDefinition;
			if (ingredientsItemDefinition != null)
			{
				base.view.SetTime((int)ingredientsItemDefinition.TimeToHarvest);
				base.view.PopulateIngredients(ingredientsItemDefinition);
			}
		}
	}
}
