namespace Kampai.Game
{
	internal sealed class LoadExternalScenesCommand : global::strange.extensions.command.impl.Command
	{
		private readonly global::System.Collections.Generic.List<string> scenes = new global::System.Collections.Generic.List<string> { "Unique_CabanaDisheveled01", "Unique_CabanaDisheveled02", "Unique_CabanaDisheveled03" };

		[Inject]
		public global::Kampai.Util.IRoutineRunner runner { get; set; }

		[Inject]
		public global::Kampai.Util.ILogger logger { get; set; }

		public override void Execute()
		{
			global::UnityEngine.GameObject gameObject = new global::UnityEngine.GameObject("External Scenes");
			int i = 0;
			for (int count = scenes.Count; i < count; i++)
			{
				LoadExternalScene(scenes[i]);
			}
			runner.StartCoroutine(OrganizeScenesAfterFrame(gameObject.transform));
		}

		private void LoadExternalScene(string name)
		{
			global::UnityEngine.Application.LoadLevelAdditive(name);
		}

		private global::System.Collections.IEnumerator OrganizeScenesAfterFrame(global::UnityEngine.Transform root)
		{
			yield return null;
			int i = 0;
			for (int ilen = scenes.Count; i < ilen; i++)
			{
				OrganizeScene(scenes[i], root);
			}
		}

		private void OrganizeScene(string name, global::UnityEngine.Transform root)
		{
			global::UnityEngine.GameObject gameObject = global::UnityEngine.GameObject.Find("ExternalScene_" + name);
			if (gameObject == null)
			{
				logger.Error("Cannot move external scene {0} to external scene root. It either failed to load or your external scene is set up incorrectly.", name);
			}
			else
			{
				gameObject.transform.parent = root;
				gameObject.name = name;
				gameObject.SetActive(false);
			}
		}
	}
}
