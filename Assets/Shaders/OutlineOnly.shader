Shader "Neon-OutlineSilhouette" {
	Properties{
		_OutlineColour("Outline Colour", Color) = (0.95,0.5,0.5,0.5)
		_OutlineWidth("Outline Width", Float) = 0.01
	}
		CGINCLUDE
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
	struct a2v {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};
	struct v2f {
		float4 pos : SV_POSITION;
	};
	uniform fixed4 _OutlineColour;
	uniform half _OutlineWidth;
	ENDCG

		SubShader{
		Tags{
		"Queue" = "Geometry+100"
		"IgnoreProjector" = "True"
	}

		Pass{
		Name "SILHOUETTEINTERIOR"
		Cull Back
		Blend One Zero
		Lighting Off
		CGPROGRAM
		v2f vert(a2v v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		return o;
	}
	fixed4 frag(v2f IN) : COLOR
	{
		return fixed4(0.0,0.0,0.0,1.0);
	}
		ENDCG
	}

		Pass{
		Name "OUTLINE"
		Cull Front
		Offset 10, 10
		Lighting Off
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
#include "UnityCG.cginc"
		v2f vert(a2v v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 vOffset = TransformViewToProjection(norm.xy);
		o.pos.xy += vOffset * o.pos.z * _OutlineWidth;
		return o;
	}
	fixed4 frag(v2f IN) : COLOR
	{
		return _OutlineColour;
	}
		ENDCG
	}
	}
}