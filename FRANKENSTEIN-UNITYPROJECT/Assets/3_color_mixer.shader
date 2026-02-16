Shader "Kampai/Transparent/3 Color Blend" {
Properties {
 _TransparencyMask ("Channel Map (RGBA)", 2D) = "gray" {}
 _colorR ("Color R", Color) = (0.0661765,0.0661765,0.0661765,1)
 _colorG ("Color G", Color) = (0.0205991,0.366995,0.933824,1)
 _colorB ("Color B", Color) = (0.522059,0.822008,1,1)
 _color_boost ("Color Boost", Float) = 1
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