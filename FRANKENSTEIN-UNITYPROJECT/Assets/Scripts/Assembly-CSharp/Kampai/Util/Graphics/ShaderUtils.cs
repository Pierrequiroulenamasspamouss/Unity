namespace Kampai.Util.Graphics
{
	public static class ShaderUtils
	{
		public static global::Kampai.Util.Graphics.RenderQueue ConvertShaderBlendToRenderQueue(global::Kampai.Util.Graphics.BlendMode mode)
		{
			global::Kampai.Util.Graphics.RenderQueue result = global::Kampai.Util.Graphics.RenderQueue.Geometry;
			switch (mode)
			{
			case global::Kampai.Util.Graphics.BlendMode.Background:
				result = global::Kampai.Util.Graphics.RenderQueue.Background;
				break;
			case global::Kampai.Util.Graphics.BlendMode.Geometry:
				result = global::Kampai.Util.Graphics.RenderQueue.Geometry;
				break;
			case global::Kampai.Util.Graphics.BlendMode.AlphaTest:
				result = global::Kampai.Util.Graphics.RenderQueue.AlphaTest;
				break;
			case global::Kampai.Util.Graphics.BlendMode.Transparent:
				result = global::Kampai.Util.Graphics.RenderQueue.Transparent;
				break;
			case global::Kampai.Util.Graphics.BlendMode.Overlay:
				result = global::Kampai.Util.Graphics.RenderQueue.Overlay;
				break;
			}
			return result;
		}

		public static int GetMaterialRenderQueue(global::UnityEngine.Material material)
		{
			if (material == null)
			{
				return 0;
			}
			int num = -1;
			if (material.HasProperty("_Mode"))
			{
				num = (int)ConvertShaderBlendToRenderQueue((global::Kampai.Util.Graphics.BlendMode)material.GetFloat("_Mode"));
				if (material.HasProperty("_LayerIndex"))
				{
					num += (int)material.GetFloat("_LayerIndex");
				}
			}
			return num;
		}

		public static void EnableStencilShader(global::UnityEngine.Material material, int stencilRef, int count)
		{
			if (material == null || !material.HasProperty("__Stencil"))
			{
				return;
			}
			material.SetFloat("__Stencil", stencilRef);
			material.SetFloat("__StencilComp", 3f);
			material.SetFloat("__StencilReadMask", stencilRef);
			material.SetFloat("__StencilWriteMask", stencilRef);
			if (material.HasProperty("_Mode"))
			{
				material.SetFloat("_Mode", 2f);
				if (material.HasProperty("_LayerIndex"))
				{
					material.SetFloat("_LayerIndex", count);
				}
				material.renderQueue = 3000 + count;
			}
			if (material.HasProperty("_Alpha"))
			{
				material.SetFloat("_Alpha", 0f);
			}
		}
	}
}
