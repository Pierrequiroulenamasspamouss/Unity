Shader "Kampai/UI/AlphaMask" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _AlphaTex ("Alpha mask (R)", 2D) = "white" {}
 _Color ("Tint", Color) = (1,1,1,1)
 _Overlay ("Overlay", Color) = (0,0,0,0)
 _Desaturation ("Desaturation", Float) = 0
 _StencilComp ("Stencil Comparison", Float) = 8
 _Stencil ("Stencil ID", Float) = 0
 _StencilOp ("Stencil Operation", Float) = 0
 _StencilWriteMask ("Stencil Write Mask", Float) = 255
 _StencilReadMask ("Stencil Read Mask", Float) = 255
}
	SubShader {
		Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="true" }
		
		Pass {
			ZTest [unity_GUIZTestMode]
			ZWrite Off
			Cull Off
			Fog { Mode Off }
			Stencil {
				Ref [_Stencil]
				ReadMask [_StencilReadMask]
				WriteMask [_StencilWriteMask]
				Comp [_StencilComp]
				Pass [_StencilOp]
			}
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _AlphaTex;
			float4 _AlphaTex_ST;
			fixed4 _Color;
			fixed4 _Overlay;
			float _Desaturation;

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord0 : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float2 texcoord0 : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.texcoord0 = TRANSFORM_TEX(v.texcoord0, _MainTex);
				o.texcoord1 = TRANSFORM_TEX(v.texcoord1, _AlphaTex);
				fixed4 col = v.color * _Color;
				o.color.rgb = (_Overlay.rgb * _Overlay.a) + (col.rgb * (1.0 - _Overlay.a));
				o.color.a = col.a;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed3 texCol = tex2D(_MainTex, i.texcoord0).rgb * i.color.rgb;
				fixed3 desat = dot(texCol, fixed3(0.22, 0.707, 0.071));
				fixed4 col;
				col.rgb = lerp(texCol, desat, _Desaturation);
				col.a = tex2D(_AlphaTex, i.texcoord1).r * i.color.a;
				return col;
			}
			ENDCG
		}
	}
}