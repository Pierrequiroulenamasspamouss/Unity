namespace Kampai.Game.View
{
	internal sealed class CoordinatedAnimation : global::UnityEngine.MonoBehaviour, global::Kampai.Util.Identifiable
	{
		private global::Kampai.Game.GachaAnimationDefinition Def;

		private global::Kampai.Util.ILogger logger;

		private global::UnityEngine.Transform[] routes;

		private global::UnityEngine.GameObject GO;

		private global::UnityEngine.Object src;

		private global::Kampai.Util.VFXScript vfxScript;

		public int ID
		{
			get
			{
				return Def.ID;
			}
			set
			{
			}
		}

		internal void Init(global::Kampai.Game.GachaAnimationDefinition definition, global::UnityEngine.Transform parent, global::UnityEngine.Vector3 centerPoint, global::UnityEngine.Vector3 lookPoint, global::Kampai.Util.ILogger log)
		{
			Def = definition;
			logger = log;
			int minions = definition.Minions;
			src = global::Kampai.Util.KampaiResources.Load(definition.Prefab);
			if (src == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.AN_UNABLE_TO_LOAD_PREFAB);
			}
			GO = global::UnityEngine.Object.Instantiate(src) as global::UnityEngine.GameObject;
			if (GO == null)
			{
				logger.Fatal(global::Kampai.Util.FatalCode.AN_UNABLE_TO_LOAD_PREFAB);
			}
			global::UnityEngine.Transform transform = GO.transform;
			transform.parent = parent;
			transform.position = centerPoint;
			transform.LookAt(lookPoint);
			routes = new global::UnityEngine.Transform[minions];
			for (int i = 0; i < minions; i++)
			{
				global::UnityEngine.Transform transform2 = transform.Find("route" + i);
				if (transform2 == null)
				{
					logger.Fatal(global::Kampai.Util.FatalCode.AN_NO_SUCH_ROUTE, "Minion {0} in coordinated animation {1}", i, ID);
				}
				routes[i] = transform2;
			}
			vfxScript = GO.GetComponent<global::Kampai.Util.VFXScript>();
			if (vfxScript != null)
			{
				vfxScript.Init();
			}
		}

		public global::Kampai.Util.VFXScript GetVFXScript()
		{
			return vfxScript;
		}

		public global::UnityEngine.Transform[] GetRoutingSlots()
		{
			return routes;
		}

		public void OnDestroy()
		{
			if (GO != null)
			{
				global::UnityEngine.Object.Destroy(GO);
			}
		}
	}
}
