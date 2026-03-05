namespace Kampai.UI.Controller
{
	public class ShowOfflinePopupCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public bool isShow { get; set; }

		public override void Execute()
		{
			// NOTE: The popup_Error_LostConnectivity prefab contains Animator components that
			// crash Unity 4.7 (Invalid MotionSet index in mecanim::animation::EvaluateAvatarSM)
			// when instantiated at runtime. This popup is disabled until the prefab is fixed.
			// The network connection lost state is still tracked via NetworkModel.isConnectionLost.
			global::UnityEngine.Debug.Log("ShowOfflinePopupCommand: connection lost (popup suppressed to prevent Unity 4.7 animator crash). isShow=" + isShow);
		}
	}
}
