Shader "Kampai/Standard/Minion" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "gray" {}
 _MatCapBase ("MatCapBase (RGB)", 2D) = "gray" {}
 _LightColor ("Light Color (RGB)", Color) = (0.733,0.706,0.525,1)
[HideInInspector]  _Mode ("Rendering Queue", Float) = 0
[HideInInspector]  _LayerIndex ("Layer index", Float) = 0
 __Stencil ("Ref", Float) = 0
[Enum(UnityEngine.Rendering.CompareFunction)]  __StencilComp ("Comparison", Float) = 8
 __StencilReadMask ("Read Mask", Float) = 255
 __StencilWriteMask ("Write Mask", Float) = 255
[Enum(UnityEngine.Rendering.StencilOp)]  __StencilPassOp ("Pass Operation", Float) = 0
[Enum(UnityEngine.Rendering.StencilOp)]  __StencilFailOp ("Fail Operation", Float) = 0
[Enum(UnityEngine.Rendering.StencilOp)]  __StencilZFailOp ("ZFail Operation", Float) = 0
}
	//DummyShaderTextExporter
	
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Lambert
#pragma target 3.0
		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};
		void surf(Input IN, inout SurfaceOutput o)
		{
			float4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
		}
		ENDCG
	}
}