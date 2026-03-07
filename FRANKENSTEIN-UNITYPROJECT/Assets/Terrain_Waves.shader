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
	SubShader {
		Tags { "LIGHTMODE"="Always" "QUEUE"="Background+1" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
		
		Pass {
			ZTest Always
			ZWrite Off
			Fog { Mode Off }
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _Diffuse;
			float4 _Diffuse_ST;
			sampler2D _AlphaMask;
			float4 _AlphaMask_ST;
			fixed4 _Color;
			float _Speed;
			float _FadeAlpha;

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord0 : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
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
				float2 timeOffset = float2(0.0, frac(_Time.y * _Speed));
				o.texcoord0 = TRANSFORM_TEX(v.texcoord0, _Diffuse) + timeOffset;
				o.texcoord1 = TRANSFORM_TEX(v.texcoord1, _AlphaMask);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 diff = tex2D(_Diffuse, i.texcoord0);
				fixed4 col;
				col.rgb = _Color.rgb + diff.rgb;
				col.a = diff.r * tex2D(_AlphaMask, i.texcoord1).r * _Color.a * _FadeAlpha;
				return col;
			}
			ENDCG
		}
	}
}