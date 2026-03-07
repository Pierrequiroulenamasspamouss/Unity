Shader "Kampai/Transparent/Glass" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB)", 2D) = "gray" {}
 _MatCap ("MatCap (RGB)", 2D) = "gray" {}
 _Boost ("Boost", Float) = 1
[PerRendererData]  _FadeAlpha ("Fade Alpha", Range(0,1)) = 1
}
	SubShader {
		Tags { "QUEUE"="Transparent" }
		Pass {
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MatCap;
			fixed4 _Color;
			float _Boost;
			float _FadeAlpha;

			struct appdata_t {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord0 : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord0 = TRANSFORM_TEX(v.texcoord, _MainTex);
				float3 viewNorm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				o.texcoord1 = (viewNorm.xy * 0.5) + 0.5;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 cap = tex2D(_MatCap, i.texcoord1);
				fixed4 col;
				col.rgb = cap.rgb * _Boost * _Color.rgb;
				col.a = tex2D(_MainTex, i.texcoord0).r * _Color.a * _FadeAlpha;
				return col;
			}
			ENDCG
		}
	}
}