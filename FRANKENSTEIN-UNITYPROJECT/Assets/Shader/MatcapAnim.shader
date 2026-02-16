Shader "Kampai/Standard/Matcap Add+VertAnim" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _MatCapTex ("MatCap (RGB)", 2D) = "gray" {}
 _Boost ("Boost", Float) = 1
 _MatCapBoost ("MatCap Boost", Float) = 1
 _Scale ("Vertex Color Scale", Float) = 0
 _Speed ("Animation Speed", Float) = 1
 _BlendedColor ("Blended Color", Color) = (0,0,0,0)
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