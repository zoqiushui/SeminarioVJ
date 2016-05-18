﻿Shader "Custom/MinimapColor" 
{
	Properties 
	{
		_MainTex("Main texture", 2D) = "white" {}
		_Color("Color", Color) = (1,0,0,1)
	}
	SubShader
		{
			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

			Lighting Off
			Fog{ Mode Off }
			ZWrite Off

			Pass
			{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv_MainTex : TEXCOORD0;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			float4 frag(v2f IN) : COLOR
			{
				float4 base = tex2D(_MainTex, IN.uv_MainTex);
				if (base.a == 1)
					return  _Color;
				else
				{
					base.a = 0;
					return (base / 2);
				}
			}
				ENDCG
		}
	}
}

