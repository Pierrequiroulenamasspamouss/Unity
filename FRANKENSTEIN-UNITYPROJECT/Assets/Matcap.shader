Shader "Kampai/Standard/Matcap" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _MatCapTex ("MatCap (RGB)", 2D) = "white" {}
 _Boost ("Boost", Float) = 1
 _MatCapBoost ("MatCap Boost", Float) = 1
 _BlendedColor ("Blended Color", Color) = (0,0,0,0)
[PerRendererData]  _FadeAlpha ("Fade Alpha", Range(0,1)) = 1
[HideInInspector] [Enum(Kampai.Util.Graphics.BlendMode)]  _Mode ("Rendering Queue", Float) = 0
[HideInInspector]  _LayerIndex ("Layer index", Float) = 0
 _OffsetFactor ("Offset Factor", Float) = 0
 _OffsetUnits ("Offset Units", Float) = 0
 _TransparencyLM ("Transmissive Color", 2D) = "white" {}
 _VertexAnimScale ("Vertex Color Scale", Float) = 0
 _VertexAnimSpeed ("Animation Speed", Float) = 1
[Enum(UnityEngine.Rendering.BlendMode)]  _SrcBlend ("Source Blend mode", Float) = 5
[Enum(UnityEngine.Rendering.BlendMode)]  _DstBlend ("Dest Blend mode", Float) = 10
[Enum(Kampai.Util.Graphics.CompareFunction)]  _ZTest ("ZTest", Float) = 4
[Enum(Kampai.Editor.AlphaMode)]  _Alpha ("Transparent", Float) = 1
[Enum(Kampai.Editor.ToggleValue)]  _ZWrite ("ZWrite", Float) = 1
[Enum(UnityEngine.Rendering.CullMode)]  _Cull ("Cull", Float) = 2
[Enum(Kampai.Editor.ToggleValue)]  _VertexColor ("Vertex Color", Float) = 0
[Enum(Kampai.Editor.MatCapBlend)]  _MatCapBlend ("MatCap Blend", Float) = 0
[Enum(Kampai.Util.Graphics.ColorMask)]  _ColorMask ("Color Mask", Float) = 15
 __Stencil ("Ref", Float) = 0
[Enum(UnityEngine.Rendering.CompareFunction)]  __StencilComp ("Comparison", Float) = 8
 __StencilReadMask ("Read Mask", Float) = 255
 __StencilWriteMask ("Write Mask", Float) = 255
[Enum(UnityEngine.Rendering.StencilOp)]  __StencilPassOp ("Pass Operation", Float) = 0
[Enum(UnityEngine.Rendering.StencilOp)]  __StencilFailOp ("Fail Operation", Float) = 0
[Enum(UnityEngine.Rendering.StencilOp)]  __StencilZFailOp ("ZFail Operation", Float) = 0
}
	//DummyShaderTextExporter
	
	SubShader {
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass {
            Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]
            ZTest [_ZTest]
            Cull [_Cull]
            ColorMask [_ColorMask]

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MatCapTex;
			float4 _Color;
			float _Boost;
			float _MatCapBoost;
			float4 _BlendedColor;
			float _VertexColor;

			struct appdata_t {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float2 matcapUV : TEXCOORD1;
				float4 color : COLOR;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				
                float3 viewNormal = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
                o.matcapUV = viewNormal.xy * 0.5 + 0.5;

				o.color = v.color;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
				fixed4 mc = tex2D(_MatCapTex, i.matcapUV) * _MatCapBoost;
				
				col.rgb *= mc.rgb * 2.0;

				if (_VertexColor > 0.5) col *= i.color;
				col.rgb *= _Boost;
				col.rgb += (_BlendedColor.rgb * _BlendedColor.a);
				return col;
			}
			ENDCG
		}
	}
}