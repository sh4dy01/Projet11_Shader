Shader "Learning/Lit/H_ToonShading"
{
    Properties
    {
    	_GradientMap("Gradient Map", 2D) = "white" {}
    }

    SubShader
    {
		Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			
			#include "UnityCG.cginc"

			// Récupérez les data depuis le script LightData, attaché sur votre Directional Light
            sampler2D _GradientMap;
            float4 _LightColor;
            float3 _WorldSpaceLightDir;
            float _Intensity;
			
			struct vertexInput
			{
				float4 vertex : POSITION;
            	float3 normal : NORMAL; 
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
			};

			v2f vert(vertexInput v)
			{
				v2f o;

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.worldNormal = normalize(mul(unity_ObjectToWorld, float4(v.normal, 0)).xyz);
				
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				i.worldNormal = normalize(i.worldNormal);

				const float NdotL = saturate(dot(i.worldNormal, -_WorldSpaceLightDir));
				
				return tex2D(_GradientMap, NdotL) * _LightColor* _Intensity;
			}
			
            ENDHLSL
        }
    }
}
