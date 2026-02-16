namespace Kampai.Util
{
	public class DummyCharacterBuilder : global::Kampai.Util.IDummyCharacterBuilder
	{
		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Main.PlayLocalAudioSignal audioSignal { get; set; }

		[Inject]
		public global::Kampai.Main.StartLoopingAudioSignal startLoopingAudioSignal { get; set; }

		[Inject]
		public global::Kampai.Main.StopLocalAudioSignal stopAudioSignal { get; set; }

		[Inject]
		public global::Kampai.Main.PlayMinionStateAudioSignal minionStateAudioSignal { get; set; }

		[Inject]
		public global::Kampai.Game.IDLCService dlcService { get; set; }

		[Inject]
		public global::Kampai.Util.IMinionBuilder minionBuilder { get; set; }

		[Inject]
		public global::Kampai.Util.IRoutineRunner routineRunner { get; set; }

		[Inject]
		public global::Kampai.Common.IRandomService randomService { get; set; }

		public global::Kampai.Game.View.DummyCharacterObject BuildMinion(global::Kampai.Game.Minion minion, string baseModelPath, global::Kampai.Game.CharacterUIAnimationDefinition characterUIAnimationDefinition, global::UnityEngine.Transform parent, bool isHigh, global::UnityEngine.Vector3 villainScale, global::UnityEngine.Vector3 villainPositionOffset)
		{
			string arg = dlcService.GetDownloadQualityLevel().ToUpper();
			string path = string.Format("{0}_{1}", baseModelPath, arg);
			global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(global::Kampai.Util.KampaiResources.Load(path)) as global::UnityEngine.GameObject;
			global::Kampai.Game.View.DummyCharacterObject dummyCharacterObject = gameObject.AddComponent<global::Kampai.Game.View.DummyCharacterObject>();
			dummyCharacterObject.Init(minion, logger, routineRunner, randomService);
			dummyCharacterObject.Build(minion, characterUIAnimationDefinition, parent, logger, isHigh, villainScale, villainPositionOffset, minionBuilder);
			global::Kampai.Game.View.AnimEventHandler animEventHandler = gameObject.AddComponent<global::Kampai.Game.View.AnimEventHandler>();
			animEventHandler.Init(dummyCharacterObject, dummyCharacterObject.localAudioEmitter, audioSignal, stopAudioSignal, minionStateAudioSignal, startLoopingAudioSignal);
			return dummyCharacterObject;
		}

		public global::Kampai.Game.View.DummyCharacterObject BuildNamedChacter(global::Kampai.Game.NamedCharacter namedCharacter, global::UnityEngine.Transform parent, bool isHigh, global::UnityEngine.Vector3 villainScale, global::UnityEngine.Vector3 villainPositionOffset)
		{
			string arg = dlcService.GetDownloadQualityLevel().ToUpper();
			string prefab = namedCharacter.Definition.Prefab;
			string text = string.Format("{0}_{1}", prefab, arg);
			global::UnityEngine.Object obj = global::Kampai.Util.KampaiResources.Load(text);
			global::UnityEngine.GameObject gameObject;
			if (obj == null)
			{
				logger.Error("NamedCharacterBuilder: Failed to load {0}.", text);
				gameObject = new global::UnityEngine.GameObject(text + "(FAILED TO LOAD)");
			}
			else
			{
				gameObject = global::UnityEngine.Object.Instantiate(obj) as global::UnityEngine.GameObject;
			}
			global::Kampai.Game.View.DummyCharacterObject dummyCharacterObject = gameObject.AddComponent<global::Kampai.Game.View.DummyCharacterObject>();
			dummyCharacterObject.Init(namedCharacter, logger, routineRunner, randomService);
			dummyCharacterObject.Build(namedCharacter, namedCharacter.Definition.CharacterAnimations.characterUIAnimationDefinition, parent, logger, isHigh, villainScale, villainPositionOffset, minionBuilder);
			global::Kampai.Game.View.AnimEventHandler animEventHandler = gameObject.AddComponent<global::Kampai.Game.View.AnimEventHandler>();
			animEventHandler.Init(dummyCharacterObject, dummyCharacterObject.localAudioEmitter, audioSignal, stopAudioSignal, minionStateAudioSignal, startLoopingAudioSignal);
			return dummyCharacterObject;
		}
	}
}
