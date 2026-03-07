Shader "Kampai/Standard/Minion" {
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
	//DummyShaderTextExporter
	
	SubShader {
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _MatCapBase;
			float4 _LightColor;

			struct appdata_t {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float2 matcapUV : TEXCOORD1;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				
                float3 viewNormal = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
                o.matcapUV = viewNormal.xy * 0.5 + 0.5;

				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				fixed4 mc = tex2D(_MatCapBase, i.matcapUV) * _LightColor;
				
				col.rgb *= mc.rgb * 2.0;

				return col;
			}
			ENDCG
		}
	}
}