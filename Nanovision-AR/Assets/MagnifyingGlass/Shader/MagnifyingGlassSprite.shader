Shader "Magnifying Glass/Sprite" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color                     ("Tint", Color) = (1, 1, 1, 1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[Header(Magnifying Glass)]
		_DarkColor                ("Dark Color", Color) = (0.5, 0.5, 0.5, 1)
		_ComplicatedCenterRadial1 ("Center Radial", Vector) = (0, 0, 0, 0)
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
		Cull Off Lighting Off ZWrite Off Blend One OneMinusSrcAlpha
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "Utils.cginc"
			MG_DECLARE_UNIFORM(1);
			fixed4 _Color, _DarkColor;

			struct v2f
			{
				float4 pos   : SV_POSITION;
				fixed4 color : COLOR;
				float2 uv    : TEXCOORD0;
			};
			v2f vert (appdata_full v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.color = v.color * _Color;
				o.uv = v.texcoord;
#ifdef PIXELSNAP_ON
				o.pos = UnityPixelSnap(o.pos);
#endif
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
				c.rgb *= c.a;
				return c;
			}
			ENDCG
		}
	}
	FallBack Off
}