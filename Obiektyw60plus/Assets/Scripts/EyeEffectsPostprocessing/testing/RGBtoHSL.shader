// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/HSLShader" {

	Properties{

		_MainTex("Texture", 2D) = "white" {}

		_HueShift("HueShift", Float) = 0

		_Sat("Saturation", Float) = 1

		_Bright("Value", Float) = 1

	}

	SubShader{



		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		ZWrite Off

		Blend SrcAlpha OneMinusSrcAlpha

		Cull Off



		Pass

		{

			CGPROGRAM

			#pragma vertex vert

			#pragma fragment frag

			#pragma target 2.0



			#include "UnityCG.cginc"



			float3 rgb2hsl(float3 RGB, float3 shift)

			{

				float3 RESULT = float3(RGB);

				RESULT.x = (RGB.x + RGB.y + RGB.z)/3;
				RESULT.x *= shift.x;
				
				float minRGB = min(min(RGB.x, RGB.y), RGB.z);
				float inversX = 1 / ((RGB.x + RGB.y + RGB.z) / 3);

				RESULT.y = 1 - (minRGB*inversX);
				RESULT.y *= shift.y;

				float lL = 0.5*(RGB.x - RGB.x) + (RGB.x - RGB.z); 
				float lM = sqrt(pow((RGB.x - RGB.x), 2) + (RGB.x - RGB.z)*(RGB.y - RGB.z));

				RESULT.z = 1 / (cos(lL/lM));
				RESULT.z *= shift.z;
				return RESULT;

			}

		/*
			float3 hsl2rgb(float3 HSL, float3 shift)

			{

				float3 RESULT = float3(HSL, float3 shift);

				RESULT.x = (HSL.x + HSL.y + HSL.z) / 3;
				RESULT.x *= shift.x;

				float minRGB = min(min(RGB.x, RGB.y), RGB.z);
				float inversX = 1 / ((RGB.x + RGB.y + RGB.z) / 3);

				RESULT.y = 1 - (minRGB*inversX);
				RESULT.y *= shift.y;

				float lL = 0.5*(RGB.x - RGB.x) + (RGB.x - RGB.z);
				float lM = sqrt(pow((RGB.x - RGB.x), 2) + (RGB.x - RGB.z)*(RGB.y - RGB.z));

				RESULT.z = 1 / (cos(lL / lM));
				RESULT.z *= shift.z;
				return RESULT;

			}

		*/

			struct v2f {

			float4  pos : SV_POSITION;

			float2  uv : TEXCOORD0;

			};



			float4 _MainTex_ST;



			v2f vert(appdata_base v)

			{

				v2f o;

				o.pos = UnityObjectToClipPos(v.vertex);

				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

				return o;

			}



			sampler2D _MainTex;

			float _HueShift;

			float _Sat;

			float _Bright;



			half4 frag(v2f i) : COLOR

			{

			half4 col = tex2D(_MainTex, i.uv);

			float3 hsl = float3(_HueShift, _Sat, _Bright);



			return half4(half3(rgb2hsl(col, hsl)), col.a);

			}

			ENDCG

		}

	}

	Fallback "Particles/Alpha Blended"

}