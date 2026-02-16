namespace Kampai.Game.View
{
	public class VignetteView : global::strange.extensions.mediation.impl.View
	{
		private const string key_VignetteSize = "_Size";

		private float? initialVignetteSize;

		internal void SetVignetteSize(float? size)
		{
			if (!initialVignetteSize.HasValue)
			{
				initialVignetteSize = base.renderer.material.GetFloat("_Size");
			}
			float value = initialVignetteSize.Value;
			if (size.HasValue)
			{
				value = size.Value;
			}
			base.renderer.material.SetFloat("_Size", value);
		}
	}
}
