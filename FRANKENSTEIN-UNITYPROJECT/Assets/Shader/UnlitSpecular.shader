Shader "Kampai/Standard/Specular" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
 _Boost ("Boost", Float) = 1
 _Spec ("Specular", Float) = 0.078125
 _UVScroll ("UV Scroll", Vector) = (0,0,0,0)
 _OffsetFactor ("Offset Factor", Float) = 0
 _OffsetUnits ("Offset Units", Float) = 0
[Enum(UnityEngine.Rendering.BlendMode)]  _SrcBlend ("Source Blend mode", Float) = 5
[Enum(UnityEngine.Rendering.BlendMode)]  _DstBlend ("Dest Blend mode", Float) = 10
[Enum(Kampai.Util.Graphics.CompareFunction)]  _ZTest ("ZTest", Float) = 4
[Enum(Opaque, 1, Transparent, 0)]  _Alpha ("Transparent", Float) = 1
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