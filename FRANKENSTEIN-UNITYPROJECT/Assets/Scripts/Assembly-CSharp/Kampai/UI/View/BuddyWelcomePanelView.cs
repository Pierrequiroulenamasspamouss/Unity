namespace Kampai.UI.View
{
	public class BuddyWelcomePanelView : global::Kampai.UI.View.PopupMenuView
	{
		public global::UnityEngine.UI.Text WelcomeTitle;

		public global::UnityEngine.UI.Text Name;

		public float FadeOutTime = 2f;

		private global::Kampai.UI.IPositionService positionService;

		private global::Kampai.Game.View.CharacterObject characterObject;

		public bool Initialized { get; set; }

		public void SetUpInjections(global::Kampai.UI.IPositionService positionService)
		{
			this.positionService = positionService;
		}

		public void SetUpCharacterObject(global::Kampai.Game.View.CharacterObject characterObject)
		{
			this.characterObject = characterObject;
		}

		public void Init(string title, string name)
		{
			base.Init();
			WelcomeTitle.text = title;
			Name.text = name;
		}

		internal void OnUpdatePosition(global::Kampai.UI.PositionData positionData)
		{
			base.gameObject.transform.position = positionData.WorldPositionInUI;
			base.gameObject.transform.localPosition = VectorUtils.ZeroZ(base.gameObject.transform.localPosition);
		}

		internal void LateUpdate()
		{
			if (Initialized && !(characterObject == null))
			{
				global::Kampai.UI.PositionData positionData = ((!(characterObject is global::Kampai.Game.View.VillainView)) ? positionService.GetPositionData(characterObject.GetIndicatorPosition() + global::UnityEngine.Vector3.up) : positionService.GetPositionData(characterObject.GetIndicatorPosition() + global::Kampai.Util.GameConstants.UI.VILLAIN_UI_OFFSET));
				OnUpdatePosition(positionData);
			}
		}
	}
}
