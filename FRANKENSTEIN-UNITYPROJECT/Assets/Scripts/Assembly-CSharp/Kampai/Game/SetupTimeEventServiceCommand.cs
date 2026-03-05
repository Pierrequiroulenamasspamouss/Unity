namespace Kampai.Game
{
	internal sealed class SetupTimeEventServiceCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::UnityEngine.GameObject contextView { get; set; }

		public override void Execute()
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("TimeEventService");
			gameObject.AddComponent<global::Kampai.Game.TimeEventService>();
			// Removed double bind causing BinderExceptions
			gameObject.transform.parent = contextView.transform;
		}
	}
}

