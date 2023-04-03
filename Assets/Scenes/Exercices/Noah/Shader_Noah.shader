Shader "Learning/Unlit/Shader_Noah"
{
	Properties
	{   
		// NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
		MainColor("Main Color", Color) = (1, 1, 1, 1)
	}

	SubShader
	{
		Pass
		{
			Cull Off

			HLSLPROGRAM
			#pragma vertex vert  
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			struct vertexInput
			{
				float4 vertex : POSITION;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			float4 MainColor;

			v2f vert(vertexInput v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				return MainColor;
			}
			
			ENDHLSL
		}
	}
}
