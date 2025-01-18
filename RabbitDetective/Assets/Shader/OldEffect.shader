// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "lijia/OldEffect" {
	Properties{
		//ԭͼ
		_MainTex("MainTex", 2D) = "white" {}
	//��Ӱͼ
	_VignetteTex("VignetteTex", 2D) = "white" {}
	_VignetteAmount("Vignette Opacity", Range(0, 1)) = 1
		//����
		_ScratchesTex("ScratchesTex", 2D) = "white" {}
		_ScratchesXSpeed("ScratchesXSpeed", float) = 100
		_ScratchesYSpeed("ScratchesYSpeed", float) = 100
			//�ҳ�
			_DustTex("DustTex", 2D) = "white" {}
			_DustXSpeed("_DustXSpeed", float) = 100
			_DustYSpeed("_DustYSpeed", float) = 100
				//�Ͼɵĺ�ɫ��
				_SepiaColor("_SepiaColor", Color) = (1, 1, 1, 1)

				_RandomValue("RandomValue", float) = 1.0
				_EffectAmount("Old Film Effect Amount", Range(0, 1)) = 1
	}

		SubShader{
		Tags{"RenderType" = "Opaque"}
		Pass {
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

		sampler2D _MainTex;
		float4 _MainTex_ST;

		sampler2D _VignetteTex;
		sampler2D _ScratchesTex;
		sampler2D _DustTex;

		float _EffectAmount;
		float _RandomValue;
		float _VignetteAmount;

		float _ScratchesXSpeed;
		float _ScratchesYSpeed;
		float _DustXSpeed;
		float _DustYSpeed;

		fixed4 _SepiaColor;

		struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
		};

		v2f vert(appdata_base v)
		{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
		return o;
		}

		fixed4 frag(v2f i) : COLOR
		{
			//���������� uv.yֵ����һЩ�������ʵ�ֶ�����Ч�� _SinTime��Unity���õı��� ������ȡһ��-1��1������ֵ
			half2 mainTexUV = half2(i.uv.x, i.uv.y + (_RandomValue*_SinTime.z * 0.005));
			fixed4 mainTex = tex2D(_MainTex, mainTexUV);

			fixed4 vignetteTex = tex2D(_VignetteTex, i.uv);

			half2 scratchesUV = half2(i.uv.x + (_RandomValue * _SinTime.z * _ScratchesXSpeed),
			i.uv.y + (_RandomValue * _Time.x * _ScratchesYSpeed));
			fixed4 scratchesTex = tex2D(_ScratchesTex, scratchesUV);

			half2 dustUV = half2(i.uv.x + (_RandomValue * _SinTime.z * _DustXSpeed),
			i.uv.y + (_Time.x * _DustYSpeed));
			fixed4 dustTex = tex2D(_DustTex, dustUV);

			//���YIQ ֵ
			fixed lum = dot(fixed3(0.299, 0.587, 0.114), mainTex.rgb);

			fixed4 finalColor = lum + lerp(_SepiaColor, _SepiaColor + fixed4(0.1f, 0.1f, 0.1f, 0.1f), _RandomValue);

			fixed3 constantWhite = fixed3(1, 1, 1);

			finalColor = lerp(finalColor, finalColor * vignetteTex, _VignetteAmount);
			finalColor.rgb *= lerp(scratchesTex, constantWhite, _RandomValue);
			finalColor.rgb *= lerp(dustTex, constantWhite, (_RandomValue * _SinTime.z));
			finalColor = lerp(mainTex, finalColor, _EffectAmount);

			return finalColor;
			}

			ENDCG
			}
			}
				FallBack "Diffuse"
}