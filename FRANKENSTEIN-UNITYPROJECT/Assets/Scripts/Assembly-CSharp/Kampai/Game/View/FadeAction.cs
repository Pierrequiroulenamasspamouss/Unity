namespace Kampai.Game.View
{
	public class FadeAction : global::Kampai.Game.View.KampaiAction
	{
		private global::Kampai.Game.View.ActionableObject obj;

		private float duration;

		private bool fadeIn;

		private global::System.Collections.Generic.LinkedList<GoTween> tweenList;

		private int materialCount;

		public FadeAction(global::Kampai.Game.View.ActionableObject obj, float duration, bool fadeIn, global::Kampai.Util.ILogger logger)
			: base(logger)
		{
			this.obj = obj;
			this.duration = duration;
			this.fadeIn = fadeIn;
			tweenList = new global::System.Collections.Generic.LinkedList<GoTween>();
		}

		public override void Abort()
		{
			foreach (GoTween tween in tweenList)
			{
				tween.destroy();
			}
			tweenList.Clear();
			base.Done = true;
		}

		public override void Execute()
		{
			global::Kampai.Game.View.MaterialInfo[] components = obj.gameObject.GetComponents<global::Kampai.Game.View.MaterialInfo>();
			if (components.Length == 0)
			{
				global::System.Collections.Generic.ICollection<global::UnityEngine.Material> coloredMaterials = GetColoredMaterials();
				foreach (global::UnityEngine.Material item in coloredMaterials)
				{
					global::Kampai.Game.View.MaterialInfo materialInfo = obj.gameObject.AddComponent<global::Kampai.Game.View.MaterialInfo>();
					materialInfo.material = item;
					materialInfo.color = item.color;
					if (materialInfo.material.HasProperty("_Alpha"))
					{
						materialInfo.alpha = materialInfo.material.GetFloat("_Alpha");
					}
					if (materialInfo.material.HasProperty("_ZWrite"))
					{
						materialInfo.zWrite = materialInfo.material.GetFloat("_ZWrite");
					}
				}
				components = obj.gameObject.GetComponents<global::Kampai.Game.View.MaterialInfo>();
			}
			if (components.Length == 0)
			{
				base.Done = true;
				return;
			}
			if (!fadeIn)
			{
				EnableColliders(false);
			}
			materialCount = components.Length;
			global::Kampai.Game.View.MaterialInfo[] array = components;
			foreach (global::Kampai.Game.View.MaterialInfo materialInfo2 in array)
			{
				global::UnityEngine.Color color = materialInfo2.color;
				global::UnityEngine.Color color2 = new global::UnityEngine.Color(color.r, color.g, color.b, 0f);
				if (!fadeIn && materialInfo2.material.HasProperty("_Alpha"))
				{
					materialInfo2.material.SetFloat("_Alpha", 0f);
				}
				if (!fadeIn && materialInfo2.material.HasProperty("_ZWrite"))
				{
					materialInfo2.material.SetFloat("_ZWrite", 0f);
				}
				tweenList.AddLast(Go.to(materialInfo2.material, duration, new GoTweenConfig().colorProp("color", (!fadeIn) ? color2 : color).onComplete(delegate(AbstractGoTween thisTween)
				{
					if (--materialCount <= 0)
					{
						if (fadeIn)
						{
							ClearMaterialInfo();
							EnableColliders(true);
						}
						base.Done = true;
					}
					thisTween.destroy();
				})));
			}
		}

		private void EnableColliders(bool enabled)
		{
			obj.UpdateColliders(enabled);
		}

		private void ClearMaterialInfo()
		{
			global::Kampai.Game.View.MaterialInfo[] components = obj.gameObject.GetComponents<global::Kampai.Game.View.MaterialInfo>();
			logger.Verbose("{0} Clear MaterialInfo Count: {1}", obj.name, components.Length);
			global::Kampai.Game.View.MaterialInfo[] array = components;
			foreach (global::Kampai.Game.View.MaterialInfo materialInfo in array)
			{
				materialInfo.material.color = materialInfo.color;
				if (materialInfo.material.HasProperty("_Alpha"))
				{
					materialInfo.material.SetFloat("_Alpha", materialInfo.alpha);
				}
				if (materialInfo.material.HasProperty("_ZWrite"))
				{
					materialInfo.material.SetFloat("_ZWrite", materialInfo.zWrite);
				}
				global::UnityEngine.Object.Destroy(materialInfo);
			}
		}

		private global::System.Collections.Generic.LinkedList<global::UnityEngine.Material> GetColoredMaterials()
		{
			global::System.Collections.Generic.LinkedList<global::UnityEngine.Material> linkedList = new global::System.Collections.Generic.LinkedList<global::UnityEngine.Material>();
			global::UnityEngine.Renderer[] componentsInChildren = obj.gameObject.GetComponentsInChildren<global::UnityEngine.Renderer>();
			global::UnityEngine.Renderer[] array = componentsInChildren;
			foreach (global::UnityEngine.Renderer renderer in array)
			{
				global::UnityEngine.Material[] materials = renderer.materials;
				foreach (global::UnityEngine.Material material in materials)
				{
					if (material.HasProperty("_Color"))
					{
						linkedList.AddLast(material);
					}
				}
			}
			logger.Verbose("{0} Colored Material Count: {1}", obj.name, linkedList.Count);
			return linkedList;
		}
	}
}
