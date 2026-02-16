namespace Kampai.UI.View
{
	public class CompositeBuildingMenuView : global::strange.extensions.mediation.impl.View
	{
		public global::UnityEngine.UI.Text TitleLabel;

		public global::UnityEngine.UI.Text DescriptionLabel;

		public global::UnityEngine.UI.Text ShuffleButtonLabel;

		public global::UnityEngine.UI.Text MignettesButtonLabel;

		public global::Kampai.UI.View.ButtonView ShuffleButton;

		public global::Kampai.UI.View.ButtonView MignettesButton;

		public void Init(global::Kampai.Game.CompositeBuilding building, global::Kampai.Main.ILocalizationService localizationService)
		{
			TitleLabel.text = localizationService.GetString(building.Definition.LocalizedKey);
			ShuffleButtonLabel.text = localizationService.GetString("CompositeMenu_Shuffle");
			MignettesButtonLabel.text = localizationService.GetString("CompositeMenu_Mignettes");
			DescriptionLabel.text = localizationService.GetString("CompositeMenu_PiecesOwned", building.AttachedCompositePieceIDs.Count, building.Definition.MaxPieces);
			if (building.AttachedCompositePieceIDs.Count < 2)
			{
				ShuffleButton.GetComponent<global::UnityEngine.UI.Button>().interactable = false;
				ShuffleButton.GetComponent<global::Kampai.UI.View.KampaiImage>().color = global::UnityEngine.Color.gray;
			}
			MignettesButton.PlaySoundOnClick = false;
		}
	}
}
