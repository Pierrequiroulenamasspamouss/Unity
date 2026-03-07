Shader "Kampai/Water/Flowing Water" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _WaterTex ("Normal (RG) Waves (B)", 2D) = "white" {}
 _ColorLookup ("Color Lookup (RGB)", 2D) = "white" {}
 _AlphaTex ("Alpha Mask (Grey)", 2D) = "white" {}
 _Speed ("Speed", Float) = 5
 _Distance ("Distance", Float) = 10
 _Distortion ("Distortion", Float) = 0.1
 _WavePower ("Wave Power", Float) = 0.25
 _MaxIntensity ("Max Intensity", Float) = 0.5
 _AlphaScalar ("Alpha Scalar", Float) = 1
 _LightmapDistort ("Lightmap Distortion", Float) = 0.1
 _BlendedColor ("Blended Color", Color) = (0,0,0,0)
[PerRendererData]  _FadeAlpha ("Fade Alpha", Range(0,1)) = 1
 _Color ("Color", Color) = (1,1,1,1)
[Enum(Kampai.Util.Graphics.ColorMask)]  _ColorMask ("Color Mask", Float) = 15
[HideInInspector] [Enum(Kampai.Util.Graphics.BlendMode)]  _Mode ("Rendering Queue", Float) = 0
[HideInInspector]  _LayerIndex ("Layer index", Float) = 0
 _OffsetFactor ("Offset Factor", Float) = 0
 _OffsetUnits ("Offset Units", Float) = 0
[Enum(UnityEngine.Rendering.BlendMode)]  _SrcBlend ("Source Blend mode", Float) = 5
[Enum(UnityEngine.Rendering.BlendMode)]  _DstBlend ("Dest Blend mode", Float) = 10
[Enum(Kampai.Util.Graphics.CompareFunction)]  _ZTest ("ZTest", Float) = 4
[Enum(Kampai.Editor.ToggleValue)]  _ZWrite ("ZWrite", Float) = 1
[Enum(UnityEngine.Rendering.CullMode)]  _Cull ("Cull", Float) = 2
}
	SubShader {
		Tags { "LIGHTMODE"="Always" "RenderType"="Opaque" }
		Pass {
			ZTest [_ZTest]
			ZWrite [_ZWrite]
			Cull [_Cull]
			Fog { Mode Off }
			Blend [_SrcBlend] [_DstBlend]
			ColorMask [_ColorMask]
			Offset [_OffsetFactor], [_OffsetUnits]
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _WaterTex;
			float4 _WaterTex_ST;
			sampler2D _ColorLookup;
			sampler2D _AlphaTex;
			float _Speed;
			float _Distance;
			float _Distortion;
			float _WavePower;
			float _MaxIntensity;
			float _AlphaScalar;
			float _LightmapDistort;
			fixed4 _BlendedColor;
			float _FadeAlpha;
			fixed4 _Color;

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
				float2 texcoord2 : TEXCOORD2;
				float texcoord3 : TEXCOORD3;
				float2 texcoord4 : TEXCOORD4;
				#ifdef LIGHTMAP_ON
				float2 texcoord5 : TEXCOORD5;
				#endif
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				
				float t = frac(_Time.x * _Speed);
				float t2 = frac(t + 0.5);
				float2 flowDir = (v.color.xy - 0.5) * _Distance;
				float2 mainUV = TRANSFORM_TEX(v.texcoord0, _MainTex);
				float2 waterUV = mainUV * _WaterTex_ST.xy;
				
				o.texcoord0 = mainUV;
				o.texcoord1 = waterUV - flowDir * t;
				o.texcoord2 = waterUV - flowDir * t2;
				o.texcoord3 = (cos(6.2831853 * t) * 0.5) + 0.5;
				o.texcoord4 = float2(v.color.z, 0.0);
				
				#ifdef LIGHTMAP_ON
				o.texcoord5 = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif
				
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 lookup = tex2D(_ColorLookup, i.texcoord4);
				fixed4 w1 = tex2D(_WaterTex, i.texcoord1);
				fixed4 w2 = tex2D(_WaterTex, i.texcoord2);
				
				float validFlow = clamp(1.02 - i.color.z, 0.0, 1.0);
				float2 blendedNorm = lerp(w1.xy, w2.xy, i.texcoord3);
				float2 distortionVec = ((blendedNorm * 2.0) - 1.0) * validFlow * _MaxIntensity;
				float4 wCombined = tex2D(_WaterTex, distortionVec - 0.5);
				float cap = wCombined.z * _WavePower;
				
				float edgeMask = clamp(10.0 * i.color.z, 0.0, 1.0);
				float2 finalDistortion = distortionVec * _Distortion * edgeMask;
				
				float2 finalDir = i.texcoord0 - finalDistortion;
				fixed4 mainTex = tex2D(_MainTex, finalDir);
				
				float validArea = clamp(i.color.z * _AlphaScalar, 0.0, 1.0);
				
				float3 colWave = clamp(lookup.rgb * cap * 2.0, 0.0, 1.0);
				float3 waveOverlay = clamp(colWave * i.color.w * 4.0, 0.0, 1.0);
				float3 mixed = lerp(mainTex.rgb, waveOverlay, validArea);
				
				float3 blended = lerp(mixed, _BlendedColor.rgb, _BlendedColor.a);
				
				fixed4 col;
				#ifdef LIGHTMAP_ON
				float2 lmUV = i.texcoord5 - (_LightmapDistort * finalDistortion);
				fixed4 lmTex = UNITY_SAMPLE_TEX2D(unity_Lightmap, lmUV);
				col.rgb = blended * (2.0 * DecodeLightmap(lmTex));
				#else
				col.rgb = blended;
				#endif
				
				col.a = tex2D(_AlphaTex, finalDir).r * _Color.a * _FadeAlpha;
				return col;
			}
			ENDCG
		}
	}
}