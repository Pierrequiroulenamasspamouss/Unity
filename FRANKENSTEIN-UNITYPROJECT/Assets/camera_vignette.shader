Shader "Kampai/Particles/Camera Vignette" {
Properties {
 _Color ("Color", Color) = (0,0,0,1)
 _Gradient ("Gradient", Range(0.25,6)) = 3.88122
 _Size ("Size", Range(-0.35,0.15)) = -0.0585087
 _Min ("Transparent Min", Range(0,1)) = 0
 _Max ("Transparent Max", Range(0,1)) = 0.5
[HideInInspector] [Enum(Kampai.Util.Graphics.BlendMode)]  _Mode ("Rendering Queue", Float) = 0
[HideInInspector]  _LayerIndex ("Layer index", Float) = 0
 _OffsetFactor ("Offset Factor", Float) = 0
 _OffsetUnits ("Offset Units", Float) = 0
[Enum(Kampai.Editor.ToggleValue)]  _ZWrite ("ZWrite", Float) = 0
[Enum(UnityEngine.Rendering.CullMode)]  _Cull ("Cull", Float) = 2
[Enum(Kampai.Util.Graphics.CompareFunction)]  _ZTest ("ZTest", Float) = 4
[Enum(UnityEngine.Rendering.BlendMode)]  _SrcBlend ("Source Blend mode", Float) = 5
[Enum(UnityEngine.Rendering.BlendMode)]  _DstBlend ("Dest Blend mode", Float) = 10
}
	SubShader {
		Tags { "LIGHTMODE"="ForwardBase" }
		Pass {
			ZTest [_ZTest]
			ZWrite [_ZWrite]
			Cull [_Cull]
			Fog { Mode Off }
			Blend [_SrcBlend] [_DstBlend]
			Offset [_OffsetFactor], [_OffsetUnits]
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 _Color;
			float _Gradient;
			float _Size;
			float _Min;
			float _Max;

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.color.a = (v.color.a + _Size) * _Gradient;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col;
				col.rgb = _Color.rgb * i.color.rgb;
				col.a = clamp(i.color.a, _Min, _Max);
				return col;
			}
			ENDCG
		}
	}
}