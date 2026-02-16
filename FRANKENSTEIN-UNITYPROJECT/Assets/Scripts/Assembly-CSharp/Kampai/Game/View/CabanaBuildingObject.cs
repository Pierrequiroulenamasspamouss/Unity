namespace Kampai.Game.View
{
	public class CabanaBuildingObject : global::Kampai.Game.View.RoutableBuildingObject
	{
		private string _sceneName;

		private global::UnityEngine.Transform _routingPoint;

		private global::UnityEngine.GameObject externalSceneObject;

		public void SetExternalScene(string sceneName)
		{
			_sceneName = sceneName;
			if (_sceneName == null)
			{
				return;
			}
			externalSceneObject = FindExternalSceneObject(_sceneName);
			if (externalSceneObject == null)
			{
				logger.Error("Cannot find external scene {0}", _sceneName);
				return;
			}
			externalSceneObject.SetActive(true);
			global::UnityEngine.BoxCollider component = externalSceneObject.GetComponent<global::UnityEngine.BoxCollider>();
			if (component != null)
			{
				global::UnityEngine.BoxCollider boxCollider;
				if ((boxCollider = base.gameObject.GetComponent<global::UnityEngine.BoxCollider>()) == null)
				{
					boxCollider = base.gameObject.AddComponent<global::UnityEngine.BoxCollider>();
				}
				boxCollider.center = component.center;
				boxCollider.size = component.size;
				colliders = new global::UnityEngine.Collider[1] { boxCollider };
				component.enabled = false;
			}
		}

		internal override void Init(global::Kampai.Game.Building building, global::Kampai.Util.ILogger logger, global::System.Collections.Generic.IDictionary<string, global::UnityEngine.RuntimeAnimatorController> controllers, global::Kampai.Game.IDefinitionService definitionService)
		{
			base.Init(building, logger, controllers, definitionService);
			if (routes != null && routes.Length > 0)
			{
				_routingPoint = routes[0];
			}
			if (_routingPoint == null)
			{
				_routingPoint = base.transform;
			}
		}

		public override void Cleanup()
		{
			if (_sceneName != null && externalSceneObject != null)
			{
				externalSceneObject.SetActive(false);
			}
			base.Cleanup();
		}

		private global::UnityEngine.GameObject FindExternalSceneObject(string prefabName)
		{
			global::UnityEngine.Transform transform = global::UnityEngine.GameObject.Find("External Scenes").transform;
			int i = 0;
			for (int childCount = transform.childCount; i < childCount; i++)
			{
				global::UnityEngine.Transform child = transform.GetChild(i);
				if (child.name == prefabName)
				{
					return child.gameObject;
				}
			}
			return null;
		}

		public global::UnityEngine.Transform GetRoutingPoint()
		{
			return _routingPoint;
		}
	}
}
