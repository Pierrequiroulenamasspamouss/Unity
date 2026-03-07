Shader "Kampai/Transparent/Vertex Color Stepped Anim" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGBA)", 2D) = "white" {}
 _Boost ("Boost", Float) = 1
 _NumFrames ("Number Frames", Float) = 4
 _ScrollSpeed ("Scroll Speed", Float) = 4
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
			float _Boost;
			fixed4 _Color;
			float _NumFrames;
			float _ScrollSpeed;

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				float2 step = float2(1.0 / _NumFrames, 0.0);
				float2 animOffset = floor(_Time.y * _ScrollSpeed) * step;
				o.texcoord = frac(v.texcoord + animOffset);
				o.color = v.color;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 tex = tex2D(_MainTex, i.texcoord);
				return tex * _Boost * _Color * i.color;
			}
			ENDCG
		}
	}
}