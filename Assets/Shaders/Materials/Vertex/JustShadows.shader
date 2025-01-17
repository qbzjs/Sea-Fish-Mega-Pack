﻿Shader "Unlit/JustShadows"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags { "LightMode" = "ForwardBase" }
		LOD 100

		Pass
		{
			Tags{"LightMode" = "ShadowCaster"}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_shadowcaster
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float3 normal: NORMAL;
				float4 texcoord: TEXCOORD0;

			};

			struct v2f {
				V2F_SHADOW_CASTER;
			};

			v2f vert(appdata v) {
				v2f o;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
				return o;
			}

			float4 frag(v2f i) : SV_Target{
				SHADOW_CASTER_FRAGMENT(i)
			}
			ENDCG
		}
	}
}
