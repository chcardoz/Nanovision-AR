Shader "Magnifying Glass/Image" {
	Properties {
		[PerRendererData] _MainTex ("Main", 2D) = "white" {}
		_Color            ("Tint", Color) = (1, 1, 1, 1)
		_StencilComp      ("Stencil Comparison", Float) = 8
		_Stencil          ("Stencil ID", Float) = 0
		_StencilOp        ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask  ("Stencil Read Mask", Float) = 255
		_ColorMask        ("Color Mask", Float) = 15
		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		[Header(Magnifying Glass)]
		_DarkColor                ("Dark Color", Color) = (0.5, 0.5, 0.5, 1)
		_ComplicatedCenterRadial1 ("Center Radial", Vector) = (0.5, 0.5, 0.2, 0.2)
		_ComplicatedAmount1       ("Amount", Float) = 0.85
		_ComplicatedRadiusInner1  ("Radius Inner", Float) = 0.2
		_ComplicatedRadiusOuter1  ("Radius Outer", Float) = 0.27
	}
	SubShader {
		Tags {
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}
		Stencil {
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}
		Cull Off Lighting Off ZWrite Off ZTest [unity_GUIZTestMode] Blend SrcAlpha OneMinusSrcAlpha ColorMask [_ColorMask]
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			#include "Utils.cginc"
			MG_DECLARE_UNIFORM(1);
			fixed4 _Color, _DarkColor, _TextureSampleAdd;
			float4 _ClipRect;

			struct v2f
			{
				float4 pos    : SV_POSITION;
				fixed4 color  : COLOR;
				float2 uv     : TEXCOORD0;
				float4 wldpos : TEXCOORD1;
			};
			v2f vert (appdata_full v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.color = v.color * _Color;
				o.uv = v.texcoord;
				o.wldpos = v.vertex;
				return o;
			}
			float4 frag (v2f i) : SV_Target
			{
				float4 c = 0.0;
				if (MagnifyingGlassComplicated_IsInCircle(i.uv, _ComplicatedCenterRadial1, _ComplicatedRadiusOuter1))
					c = MagnifyingGlassComplicated_SampleTexture(i.uv, _ComplicatedCenterRadial1, _ComplicatedAmount1, _ComplicatedRadiusInner1, _ComplicatedRadiusOuter1);
				else
					c = tex2D(_MainTex, i.uv) * _DarkColor;
				c *= i.color;

#ifdef UNITY_UI_CLIP_RECT
				c.a *= UnityGet2DClipping(i.wldpos.xy, _ClipRect);
#endif
#ifdef UNITY_UI_ALPHACLIP
				clip(c.a - 0.001);
#endif
				return c;
			}
			ENDCG
		}
	}
	FallBack Off
}