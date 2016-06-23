﻿Shader "Custom/LogoShader" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Normal("Normal Map", 2D) = "white"{}
		_Color("Color Texture" , Color) = (0,0,0,0.0)
		_RimColor("Rim Color", Color) = (0.26,0.19,0.16,0.0)
		_RimPower("Rim Power", Range(0.5,8.0)) = 3.0
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
	#pragma surface surf Lambert

	struct Input {
		float2 uv_MainTex;
		float2 uv_Normal;
		float3 viewDir;
	};

	sampler2D _Normal;
	sampler2D _MainTex;
	float4 _Color;
	float4 _RimColor;
	float _RimPower;

	void surf(Input IN, inout SurfaceOutput o) {
		float4 tex = tex2D(_MainTex, IN.uv_MainTex);
		o.Albedo = tex.rgb * _Color;
		o.Normal = UnpackNormal(tex2D(_Normal, IN.uv_Normal));
		half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
		o.Emission = _RimColor.rgb * pow(rim, _RimPower);
	}
	ENDCG
	}
		Fallback "Diffuse"
}