Shader "Kampai/Standard/Minion_LOD1" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "gray" {}
 _MatCapBase ("MatCapBase (RGB)", 2D) = "gray" {}
 _LightColor ("Light Color (RGB)", Color) = (0.733,0.706,0.525,1)
[HideInInspector]  _Mode ("Rendering Queue", Float) = 0
[HideInInspector]  _LayerIndex ("Layer index", Float) = 0
  __Stencil ("Ref", Float) = 0
[Enum(UnityEngine.Rendering.CompareFunction)]  __StencilComp ("Comparison", Float) = 8
  __StencilReadMask ("Read Mask", Float) = 255
  __StencilWriteMask ("Write Mask", Float) = 255
[Enum(UnityEngine.Rendering.StencilOp)]  __StencilPassOp ("Pass Operation", Float) = 0
[Enum(UnityEngine.Rendering.StencilOp)]  __StencilFailOp ("Fail Operation", Float) = 0
[Enum(UnityEngine.Rendering.StencilOp)]  __StencilZFailOp ("ZFail Operation", Float) = 0
}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass {
			Tags { "LIGHTMODE"="Always" "RenderType"="Opaque" }
			Stencil {
				Ref [__Stencil]
				ReadMask [__StencilReadMask]
				WriteMask [__StencilWriteMask]
				Comp [__StencilComp]
				Pass [__StencilPassOp]
				Fail [__StencilFailOp]
				ZFail [__StencilZFailOp]
			}
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MatCapBase;
			fixed4 _LightColor;

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord0 : TEXCOORD0;
				float2 texcoord1 : TEXCOORD1;
				float4 color : COLOR;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord0 = TRANSFORM_TEX(v.texcoord, _MainTex);
				
				float3 viewNorm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
				o.texcoord1 = viewNorm.xy * 0.5 + 0.5;
				
				o.color = v.color;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed3 lightBase = _LightColor.rgb + (i.color.b * 0.25);
				fixed4 mainTex = tex2D(_MainTex, i.texcoord0);
				fixed4 matCap = tex2D(_MatCapBase, i.texcoord1);
				
				fixed3 finalColor = ((matCap.g * i.color.r * 1.75) + pow(mainTex.rgb, float3(2.25, 2.25, 2.25))) * (lightBase + (lightBase * 0.6));
				
				return fixed4(finalColor, 1.0);
			}
			ENDCG
		}
	}
	Fallback "VertexLit"
}