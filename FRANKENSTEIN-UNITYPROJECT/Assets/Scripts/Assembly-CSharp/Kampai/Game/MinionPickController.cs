namespace Kampai.Game
{
	public class MinionPickController : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Common.PickControllerModel model { get; set; }

		[Inject]
		public global::UnityEngine.GameObject minionGameObject { get; set; }

		[Inject]
		public global::Kampai.Game.TapMinionSignal tapMinionSignal { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Game.TSMCharacterSelectedSignal tsmCharacterSelectedSignal { get; set; }

		public override void Execute()
		{
			global::Kampai.Game.View.TSMCharacterView componentInParent = minionGameObject.GetComponentInParent<global::Kampai.Game.View.TSMCharacterView>();
			if (componentInParent != null)
			{
				tsmCharacterSelectedSignal.Dispatch();
				return;
			}
			global::Kampai.Game.View.MinionObject componentInParent2 = minionGameObject.GetComponentInParent<global::Kampai.Game.View.MinionObject>();
			tapMinionSignal.Dispatch(componentInParent2.ID);
		}
	}
}
