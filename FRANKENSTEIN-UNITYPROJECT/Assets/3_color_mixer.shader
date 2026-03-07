Shader "Kampai/Transparent/3 Color Blend" {
Properties {
 _TransparencyMask ("Channel Map (RGBA)", 2D) = "gray" {}
 _colorR ("Color R", Color) = (0.0661765,0.0661765,0.0661765,1)
 _colorG ("Color G", Color) = (0.0205991,0.366995,0.933824,1)
 _colorB ("Color B", Color) = (0.522059,0.822008,1,1)
 _color_boost ("Color Boost", Float) = 1
}
	SubShader {
		Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="true" "RenderType"="Transparent" }
		LOD 200
		
		Pass {
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _TransparencyMask;
			float4 _TransparencyMask_ST;
			fixed4 _colorR;
			fixed4 _colorG;
			fixed4 _colorB;
			float _color_boost;

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _TransparencyMask);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 mask = tex2D(_TransparencyMask, i.texcoord);
				fixed4 col;
				col.rgb = lerp(lerp(_colorR.rgb, _colorG.rgb, mask.g), _colorB.rgb, mask.b) * _color_boost;
				col.a = mask.a * step(0.5, mask.a);
				return col;
			}
			ENDCG
		}
	}
}