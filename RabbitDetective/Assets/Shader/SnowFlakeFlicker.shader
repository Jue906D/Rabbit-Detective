Shader "Custom/SnowflakeFlicker"
{
	Properties
	{
		_MainTex("Base (RGB)", 2D) = "white" { }
		_NoiseTex("Noise Texture", 2D) = "white" { }
		_FlickerSpeed("Flicker Speed", Range(0.1, 5)) = 1.0
		_FlickerStrength("Flicker Strength", Range(0, 1)) = 0.5
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
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
				sampler2D _NoiseTex;
				float _FlickerSpeed;
				float _FlickerStrength;

				v2f vert(appdata v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				half4 frag(v2f i) : SV_Target
				{
					// 获取基础纹理和噪声纹理
					half4 baseColor = tex2D(_MainTex, i.uv);
					half noise = tex2D(_NoiseTex, i.uv).r;

					// 使用时间生成闪烁效果
					float flicker = sin(_Time.y * _FlickerSpeed + i.uv.x * 10.0) * 0.5 + 0.5;
					flicker = lerp(flicker, 1.0, _FlickerStrength); // 让闪烁逐渐稳定

					// 将噪声与闪烁值相乘，控制点的亮度和透明度
					baseColor.rgb *= flicker * noise;
					baseColor.a = flicker;

					return baseColor;
				}
				ENDCG
			}
		}
}
