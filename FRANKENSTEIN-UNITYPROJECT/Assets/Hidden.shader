Shader "Kampai/Standard/Hidden" {
Properties {
 _Color ("Main Color", Color) = (1,1,1,1)
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _Boost ("Boost", Float) = 1
 _UVScroll ("UV Scroll, Base (XY) Unused (ZW)", Vector) = (0,0,0,0)
 _BlendedColor ("Blended Color", Color) = (0,0,0,0)
[Enum(UnityEngine.Rendering.CompareFunction)]  _ZTest ("ZTest", Float) = 4
}
	SubShader {
		Pass {
			ZTest [_ZTest]
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = v.vertex - float4(0.0, 0.0, 1000000.0, 0.0);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				return fixed4(0,0,0,0);
			}
			ENDCG
		}
	}
}