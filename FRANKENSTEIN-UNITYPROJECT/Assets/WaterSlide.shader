Shader "Kampai/Water/WaterSlide" {
Properties {
 _AlphaMask ("Alpha Mask", 2D) = "gray" {}
 _diffuse1 ("diffuse1", 2D) = "gray" {}
 _diffuse2 ("diffuse2", 2D) = "gray" {}
 _time_scale ("time_scale", Float) = 10
}
	SubShader {
		Tags { "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
		Pass {
			Name "FORWARDBASE"
			Tags { "LIGHTMODE"="ForwardBase" "QUEUE"="Geometry" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _AlphaMask;
			float4 _AlphaMask_ST;
			sampler2D _diffuse1;
			float4 _diffuse1_ST;
			sampler2D _diffuse2;
			float4 _diffuse2_ST;
			float _time_scale;

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float2 texcoord0 : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
				float2 texcoord2 : TEXCOORD2;
				float2 texcoord3 : TEXCOORD3;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				float scroll = frac(_Time.x * _time_scale);
				
				float2 d1UV = v.texcoord * _diffuse1_ST.xy;
				o.texcoord0 = float2(d1UV.x - scroll, d1UV.y);
				
				float2 d2UV = v.texcoord * _diffuse2_ST.xy;
				o.texcoord1 = float2(d2UV.x - scroll, d2UV.y);
				
				float2 aUV = v.texcoord * _AlphaMask_ST.xy;
				o.texcoord2 = float2(aUV.x - scroll, aUV.y);
				o.texcoord3 = float2(aUV.x - scroll, aUV.y);
				
				o.color = v.color;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 t1 = tex2D(_diffuse1, i.texcoord0);
				fixed4 t2 = tex2D(_diffuse2, i.texcoord1);
				
				fixed4 col;
				col.rgb = clamp(1.0 - ((1.0 - t1.rgb) * (1.0 - t2.rgb)), 0.0, 1.0);
				
				fixed4 a1 = tex2D(_AlphaMask, i.texcoord2);
				fixed4 a2 = tex2D(_AlphaMask, i.texcoord3);
				col.a = i.color.a * clamp(a1.r + a2.b, 0.0, 1.0);
				
				return col;
			}
			ENDCG
		}
	}
}