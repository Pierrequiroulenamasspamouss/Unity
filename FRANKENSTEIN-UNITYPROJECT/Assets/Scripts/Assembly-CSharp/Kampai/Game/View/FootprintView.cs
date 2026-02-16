namespace Kampai.Game.View
{
	public class FootprintView : global::strange.extensions.mediation.impl.View
	{
		private global::Kampai.Game.View.ActionableObject parentObject;

		private global::UnityEngine.Transform cachedTransform;

		private global::UnityEngine.GameObject green;

		private global::UnityEngine.GameObject red;

		private int cachedMinRenderQueue;

		public void Init()
		{
			cachedTransform = base.transform;
			global::UnityEngine.Transform greenTransform = base.transform.Find("Green");
			global::UnityEngine.Transform redTransform = base.transform.Find("Red");
			
			if (greenTransform != null)
			{
				green = greenTransform.gameObject;
				if (green.renderer != null && green.renderer.sharedMaterial != null)
				{
					cachedMinRenderQueue = green.renderer.sharedMaterial.renderQueue + 1;
				}
			}
			
			if (redTransform != null)
			{
				red = redTransform.gameObject;
			}
		}

		public void ToggleFootprint(bool enable)
		{
			base.gameObject.SetActive(enable);
		}

		public void ParentFootprint(global::Kampai.Game.View.ActionableObject parentObject, global::UnityEngine.Transform parentTransform, int width, int height)
		{
			this.parentObject = parentObject;
			if (null != parentObject)
			{
				parentObject.UpdateMinRenderQueue(cachedMinRenderQueue);
				parentObject.SetZTestFunction(global::Kampai.Util.Graphics.CompareFunction.Always);
			}
			cachedTransform.parent = parentTransform;
			cachedTransform.localPosition = new global::UnityEngine.Vector3((float)width / 2f - 0.5f, 0f, (float)(-height) / 2f + 0.5f);
			cachedTransform.position = new global::UnityEngine.Vector3(cachedTransform.position.x, 0f, cachedTransform.position.z);
			cachedTransform.localScale = new global::UnityEngine.Vector3(width, height, 1f);
		}

		public void Reset()
		{
			if (null != parentObject)
			{
				parentObject.ResetRenderQueue();
				parentObject.SetZTestFunction(global::Kampai.Util.Graphics.CompareFunction.LEqual);
			}
			parentObject = null;
			cachedTransform.parent = null;
			cachedTransform.position = global::UnityEngine.Vector3.zero;
			cachedTransform.localScale = global::UnityEngine.Vector3.one;
		}

		public void UpdateFootprint(bool valid)
		{
			green.SetActive(valid);
			red.SetActive(!valid);
		}
	}
}
