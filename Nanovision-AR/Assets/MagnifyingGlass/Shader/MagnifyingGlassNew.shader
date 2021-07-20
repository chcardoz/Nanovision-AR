Shader "Magnifying Glass/New" {
	Properties {
		_MainTex            ("Albedo", 2D) = "white" {}
		_TintColor          ("Tint", Color) = (1, 1, 1, 1)
		_DistortionStrength ("DistortionStrength", Range(-0.2, 0.2)) = 0
		_DistortionCircle   ("DistortionCircle", Range(0, 1)) = 0
		_AlphaStep          ("Alpha Step", Range(0, 1)) = 0.8
		[Toggle(TEXTURE)] _ ("Texture", Float) = 1
	}
	CGINCLUDE
		#include "UnityCG.cginc"
		sampler2D _Global_GrabTex, _MainTex;
		float _DistortionStrength, _DistortionCircle, _AlphaStep;
		float4 _TintColor;

		struct v2f
		{
			float4 pos : SV_POSITION;
			float4 scrpos : TEXCOORD0;
			float2 uv : TEXCOORD1;
		};
		v2f vert (appdata_base v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			o.scrpos = ComputeScreenPos(o.pos);
			o.uv = v.texcoord;
			return o;
		}
		float4 frag (v2f input) : SV_Target
		{
			float2 diff = float2(distance(0.5, input.uv.x), distance(0.5, input.uv.y)) * 2.0;
			float dist = saturate(length(diff));
			float strength = 1.0 - dist;
			strength = _DistortionCircle * strength + (1.0 - _DistortionCircle);

			float4 scrUv = UNITY_PROJ_COORD(input.scrpos);
			float2 dir = input.uv - 0.5;
			dir *= strength * _DistortionStrength;
			scrUv += float4(dir.x, dir.y, 0, 0);
			float4 c = tex2Dproj(_Global_GrabTex, scrUv) * _TintColor;
#if TEXTURE
			fixed4 albedo = tex2D(_MainTex, input.uv);
			return lerp(c, albedo, step(_AlphaStep, albedo.a));
#else
			return c;
#endif
		}
	ENDCG
	SubShader {
		Tags { "Queue"="Transparent+1" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature TEXTURE
			ENDCG
		}
	}
	FallBack Off
}