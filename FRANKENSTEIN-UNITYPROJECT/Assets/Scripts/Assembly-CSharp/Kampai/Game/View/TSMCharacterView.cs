namespace Kampai.Game.View
{
	public class TSMCharacterView : global::Kampai.Game.View.NamedMinionView
	{
		private global::strange.extensions.signal.impl.Signal<string, global::System.Type, object> animSignal = new global::strange.extensions.signal.impl.Signal<string, global::System.Type, object>();

		internal global::strange.extensions.signal.impl.Signal RemoveCharacterSignal = new global::strange.extensions.signal.impl.Signal();

		private global::System.Collections.Generic.List<global::UnityEngine.Vector3> introPath;

		private global::System.Collections.Generic.List<global::UnityEngine.Vector3> reverseIntroPath;

		private float introTime;

		private int idleStateHash;

		private global::System.Collections.Generic.Dictionary<string, object> celebrateParams;

		public override global::strange.extensions.signal.impl.Signal<string, global::System.Type, object> AnimSignal
		{
			get
			{
				return animSignal;
			}
		}

		public override global::Kampai.Game.View.NamedCharacterObject Build(global::Kampai.Game.NamedCharacter character, global::UnityEngine.GameObject parent, global::Kampai.Util.ILogger logger, global::Kampai.Util.IMinionBuilder minionBuilder)
		{
			idleStateHash = global::UnityEngine.Animator.StringToHash("Base Layer.Idle");
			celebrateParams = new global::System.Collections.Generic.Dictionary<string, object>();
			celebrateParams.Add("isCelebrating", true);
			global::Kampai.Game.TSMCharacter tSMCharacter = character as global::Kampai.Game.TSMCharacter;
			global::Kampai.Game.TSMCharacterDefinition definition = tSMCharacter.Definition;
			introPath = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>(definition.IntroPath);
			reverseIntroPath = new global::System.Collections.Generic.List<global::UnityEngine.Vector3>(definition.IntroPath);
			reverseIntroPath.Reverse();
			introTime = definition.IntroTime;
			base.Build(character, parent, logger, minionBuilder);
			base.gameObject.SetLayerRecursively(8);
			return this;
		}

		internal void ShowTSMCharacter()
		{
			AnimatePosition(true);
		}

		internal void HideTSMCharacter(bool completedQuest)
		{
			if (completedQuest)
			{
				EnqueueAction(new global::Kampai.Game.View.SetAnimatorAction(this, null, logger, celebrateParams));
				EnqueueAction(new global::Kampai.Game.View.WaitForMecanimStateAction(this, idleStateHash, logger));
			}
			AnimatePosition(false);
		}

		private void AnimatePosition(bool show)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Vector3> path = ((!show) ? reverseIntroPath : introPath);
			EnqueueAction(new global::Kampai.Game.View.PathAction(this, path, introTime, logger));
			if (!show)
			{
				EnqueueAction(new global::Kampai.Game.View.SendSignalAction(RemoveCharacterSignal, logger));
			}
		}
	}
}
