Shader "Kampai/Standard/Vertex Color" {
Properties {
 _MainTex ("MainTex", 2D) = "white" {}
 _boost ("Boost", Float) = 0
}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			Name "FORWARDBASE"
			Tags { "LIGHTMODE"="ForwardBase" "RenderType"="Opaque" }
			Fog { Mode Off }
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _boost;

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.color = v.color;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 tex = tex2D(_MainTex, i.texcoord);
				float diff = 1.0 + UNITY_LIGHTMODEL_AMBIENT.r;
				
				fixed4 col;
				col.rgb = diff * (tex.rgb + i.color.rgb * _boost) * tex.rgb;
				col.a = 1.0;
				return col;
			}
			ENDCG
		}
	}
	Fallback "Mobile/Diffuse"
}