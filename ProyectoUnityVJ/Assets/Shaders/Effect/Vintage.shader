Shader "Customs/Vintage" 
{
	Properties 
	{
		_MainTex("Main texture", 2D) = "white" {}
		_NoiseTex("Noise", 2D) = "black"{}
		_NoiseRange("Noise Range", Range(0,100)) = 0
		_VigneteTex("Vignete", 2D) = "black"{}
		_Color("Color", Color) = (1,0,0,1)
		
	}
	SubShader 
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			//#pragma vertex vert_img
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _NoiseTex;
			float4 _NoiseTex_ST;
			sampler2D _VigneteTex;
			float4 _VigneteTex_ST;
			float4 _Color;
			float _NoiseRange;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv_MainTex : TEXCOORD0;
				float2 uv_NoiseTex : TEXCOORD1;
				float2 uv_VigneteTex : TEXCOORD2;
			};
			
			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv_NoiseTex = TRANSFORM_TEX(v.texcoord, _NoiseTex);
				o.uv_VigneteTex = TRANSFORM_TEX(v.texcoord, _VigneteTex); 
				return o;
			}


			//float4 frag(v2f_img IN) : COLOR
			float4 frag(v2f IN) : COLOR
			{
				float4 base = tex2D(_MainTex, IN.uv_MainTex);
				float4 noise = tex2D(_NoiseTex, IN.uv_NoiseTex) * (_NoiseRange/100);
				float4 vig = tex2D(_VigneteTex, IN.uv_VigneteTex);
				
				float4 sepia = (((base.r + base.g + base.b)/2) + _Color )/ 2 ;
				sepia.a = 1;

				float4 final = sepia;

				if (_NoiseRange != 0)
					final = final * noise;
				
				if (vig.a == 1)
				{
					return final;
				}else if (vig.a > 0 && vig.a < 1)
				{
					return vig * final;
				}
				return vig;
				
				
			}
			ENDCG
		}
	}
}

