Shader "Kampai/Water/WaterSlide" {
Properties {
 _AlphaMask ("Alpha Mask", 2D) = "gray" {}
 _diffuse1 ("diffuse1", 2D) = "gray" {}
 _diffuse2 ("diffuse2", 2D) = "gray" {}
 _time_scale ("time_scale", Float) = 10
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