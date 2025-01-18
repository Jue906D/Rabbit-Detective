Shader "Custom/OldFilmEffect"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" { }
		_GrainIntensity("Grain Intensity", Range(0, 1)) = 0.1
		_FlickerIntensity("Flicker Intensity", Range(0, 1)) = 0.05
		_GrainTexture("Grain Texture", 2D) = "white" { }
	}
		SubShader
	{
		Tags { "Queue" = "Background" }
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			sampler2D _GrainTexture;
			float _GrainIntensity;
			float _FlickerIntensity;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				half4 color = tex2D(_MainTex, i.uv);
				half4 grain = tex2D(_GrainTexture, i.uv) * _GrainIntensity;

				// 黑白效果
				//float gray = dot(color.rgb, float3(0.299, 0.587, 0.114));
				//color = float4(gray, gray, gray, 1);

				// 添加颗粒效果
				color.rgb += grain.rgb;

				// 添加闪烁效果（模拟老电影闪烁）
				color.rgb *= 1.0 + _FlickerIntensity * (sin(i.uv.x * 10.0) * 0.5 + 0.5);

				return color;
			}
			ENDCG
		}
	}
}
