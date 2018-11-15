﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Tint"
{
	Properties
	{
		_MainTex("", any) = "" {}
		//_YellowishCol("yellEffectCol", Color) = (255, 248, 207, 0.1)
	}

		CGINCLUDE
#include "UnityCG.cginc"

		struct v2f
	{
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};

	sampler2D _MainTex;
	float4 _YellowishCol;

	v2f vert(appdata_img v)
	{
		v2f o = (v2f)0;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;

		return o;
	}

	float4 frag(v2f input) : SV_Target
	{
		float4 result = tex2D(_MainTex, input.uv)*_YellowishCol; //*YellEyeColor;

		return result;
	}

		ENDCG
		SubShader
	{
		Pass
		{
			ZTest Always Cull Off ZWrite Off

			CGPROGRAM
#pragma vertex vert
#pragma fragment frag
			ENDCG
		}
	}
	Fallback off
}