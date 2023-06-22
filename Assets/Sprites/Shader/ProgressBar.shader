// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyShader/ProgressBar"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Main Tex (RGBA)", 2D) = "white" {}
		_Progress ("Progress", Range(0.0,1.0)) = 0.0
		[Enum(Verticle,0,Horizontal,1)] _Direction ("Direction", Int) = 1
		[Enum(None,0,OnlyGray,1)] _OnlyGray ("OnlyGray", Int) = 0
	}

	SubShader
	{
		 Tags
         {
             "Queue" = "Transparent"
             "IgnoreProjector" = "True"
             "RenderType" = "Transparent"
             "PreviewType" = "Plane"
             "CanUseSpriteAtlas" = "True"
         }

		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform float4 _Color;
			uniform float _Progress;
			uniform float _Direction;
			uniform float _OnlyGray;

			struct v2f
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert (appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.uv = TRANSFORM_UV(0);
				 
				return o;
			}

			half4 frag( v2f i ) : COLOR
			{
				half4 color = tex2D( _MainTex, i.uv);
				if (_OnlyGray == 0)
					color.a *= (_Direction == 0 ? i.uv.y : i.uv.x) < _Progress;
				else if ((_Direction == 0 ? i.uv.y : i.uv.x) >= _Progress)
					color.rgb = lerp(color.rgb, dot(color.rgb, float3(0.3, 0.59, 0.11)), 1);
				return color*_Color;
			}

			ENDCG
		}
	}
}