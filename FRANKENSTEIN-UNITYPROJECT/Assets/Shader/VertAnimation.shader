Shader "Kampai/Standard/Vert Animation" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _Scale ("Vertex Color Scale", Float) = 0
 _Speed ("Animation Speed", Float) = 1
 _BlendedColor ("Blended Color", Color) = (0,0,0,0)
[PerRendererData]  _FadeAlpha ("Fade Alpha", Range(0,1)) = 1
 _OffsetFactor ("Offset Factor", Float) = 0
 _OffsetUnits ("Offset Units", Float) = 0
[Enum(UnityEngine.Rendering.BlendMode)]  _SrcBlend ("Source Blend mode", Float) = 5
[Enum(UnityEngine.Rendering.BlendMode)]  _DstBlend ("Dest Blend mode", Float) = 10
[Enum(Kampai.Util.Graphics.CompareFunction)]  _ZTest ("ZTest", Float) = 4
[Enum(Kampai.Editor.AlphaMode)]  _Alpha ("Transparent", Float) = 1
}
	SubShader {
		Tags { "QUEUE"="Geometry+1" }
		Pass {
			ZTest [_ZTest]
			Blend [_SrcBlend] [_DstBlend]
			Offset [_OffsetFactor], [_OffsetUnits]
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Scale;
			float _Speed;
			fixed4 _Color;
			fixed4 _BlendedColor;
			float _Alpha;
			float _FadeAlpha;

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				float3 displacement = (2.0 * v.color.rgb - 1.0) * sin(_Time.y * _Speed) * _Scale;
				float4 animatedVertex = float4(v.vertex.xyz + displacement, v.vertex.w);
				
				o.vertex = mul(UNITY_MATRIX_MVP, animatedVertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 tex = tex2D(_MainTex, i.texcoord) * _Color;
				fixed4 col;
				col.rgb = lerp(tex.rgb, _BlendedColor.rgb, _BlendedColor.a);
				col.a = _Alpha + (tex.a * _FadeAlpha);
				return col;
			}
			ENDCG
		}
	}
}