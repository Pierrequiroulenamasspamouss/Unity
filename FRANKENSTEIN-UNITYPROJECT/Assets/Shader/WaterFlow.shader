Shader "Kampai/Water/Flowing Water" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _WaterTex ("Normal (RG) Waves (B)", 2D) = "white" {}
 _ColorLookup ("Color Lookup (RGB)", 2D) = "white" {}
 _Speed ("Speed", Float) = 5
 _Distance ("Distance", Float) = 10
 _Distortion ("Distortion", Float) = 0.1
 _WavePower ("Wave Power", Float) = 0.25
 _MaxIntensity ("Max Intensity", Float) = 0.5
 _AlphaScalar ("Alpha Scalar", Float) = 1
 _LightmapDistort ("Lightmap Distortion", Float) = 0.1
 _BlendedColor ("Blended Color", Color) = (0,0,0,0)
[HideInInspector]  _Mode ("Rendering Queue", Float) = 0
[HideInInspector]  _LayerIndex ("Layer index", Float) = 0
 _OffsetFactor ("Offset Factor", Float) = 0
 _OffsetUnits ("Offset Units", Float) = 0
[Enum(UnityEngine.Rendering.BlendMode)]  _SrcBlend ("Source Blend mode", Float) = 5
[Enum(UnityEngine.Rendering.BlendMode)]  _DstBlend ("Dest Blend mode", Float) = 10
[Enum(Kampai.Util.Graphics.CompareFunction)]  _ZTest ("ZTest", Float) = 4
[Enum(Opaque, 1, Transparent, 0)]  _Alpha ("Alpha", Float) = 1
[Enum(Off, 0, On, 1)]  _ZWrite ("ZWrite", Float) = 1
[Enum(UnityEngine.Rendering.CullMode)]  _Cull ("Cull", Float) = 2
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