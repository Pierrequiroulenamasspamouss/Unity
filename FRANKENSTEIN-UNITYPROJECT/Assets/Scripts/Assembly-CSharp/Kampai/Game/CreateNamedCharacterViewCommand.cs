namespace Kampai.Game
{
	public class CreateNamedCharacterViewCommand : global::strange.extensions.command.impl.Command
	{
		[Inject]
		public global::Kampai.Game.NamedCharacter namedCharacter { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		[Inject]
		public global::Kampai.Util.INamedCharacterBuilder builder { get; set; }

		[Inject]
		public global::Kampai.Game.InitCharacterObjectSignal initSignal { get; set; }

		[Inject(global::Kampai.Game.GameElement.NAMED_CHARACTER_MANAGER)]
		public global::UnityEngine.GameObject namedCharacterManager { get; set; }

		[Inject]
		public global::Kampai.Game.AddNamedCharacterSignal addCharacterSignal { get; set; }

		[Inject]
		public global::Kampai.Game.MapAnimationEventSignal mapAnimationEventSignal { get; set; }

		public override void Execute()
		{
			if (namedCharacter == null || namedCharacter.Definition == null)
			{
				logger.Error("Unable to load named character");
				return;
			}
			global::Kampai.Game.NamedCharacterDefinition definition = namedCharacter.Definition;
			global::Kampai.Game.View.NamedCharacterObject namedCharacterObject = builder.Build(namedCharacter);
			initSignal.Dispatch(namedCharacterObject, namedCharacter);
			namedCharacterObject.transform.parent = namedCharacterManager.transform;
			namedCharacterObject.transform.localEulerAngles = new global::UnityEngine.Vector3(0f, 135f, 0f);
			addCharacterSignal.Dispatch(namedCharacterObject);
			if (definition.Location != null)
			{
				global::Kampai.Game.Location location = definition.Location;
				namedCharacterObject.transform.position = new global::UnityEngine.Vector3(location.x, 0f, location.y);
			}
			int boundBuildingId = definition.VFXBuildingID;
			if (boundBuildingId <= 0)
			{
				return;
			}
			global::Kampai.Game.View.AnimEventHandler component = namedCharacterObject.GetComponent<global::Kampai.Game.View.AnimEventHandler>();
			if (component != null)
			{
				global::System.Action<global::Kampai.Game.View.AnimEventHandler> vFXScriptBinder = delegate(global::Kampai.Game.View.AnimEventHandler animEventHandler)
				{
					mapAnimationEventSignal.Dispatch(animEventHandler, boundBuildingId);
				};
				component.SetVFXScriptBinder(vFXScriptBinder);
			}
			else
			{
				logger.Error("Unable to map VFXBuildingID for {0} because there is no AnimEventHandler", definition.ID);
			}
		}
	}
}
