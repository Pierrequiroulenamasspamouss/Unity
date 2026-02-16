Shader "Kampai/Water/Terrain Waves" {
Properties {
 _Diffuse ("Diffuse", 2D) = "white" {}
 _AlphaMask ("Alpha Mask", 2D) = "white" {}
 _Color ("Color", Color) = (0.5,0.5,0.5,1)
 _Speed ("Speed", Float) = 1
 _FadeAlpha ("Fade Alpha", Range(0,1)) = 1
[HideInInspector]  _Mode ("Rendering Queue", Float) = 0
[HideInInspector]  _LayerIndex ("Layer index", Float) = 0
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