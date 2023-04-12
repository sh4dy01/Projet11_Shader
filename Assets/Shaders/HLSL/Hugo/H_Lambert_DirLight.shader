Shader "Learning/Lit/H_Lambert_DirLight"
{
    Properties
    {
    	
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

				float NdotL = dot(i.worldNormal, -_WorldSpaceLightDir);
				
				NdotL = clamp(NdotL, 0.05f, 1);
				
				// To do => NdotL 
				// N & L dans le même espace et normalisés
				// L => direction reçue depuis le script C#. Forward de la DirLight
				// A inverser car côté shader, le vecteur de la lumière part depuis le fragment
				
				// dot retourne des valeurs entre -1 et 1, => clamp à utiliser

				
				return _LightColor * NdotL * _Intensity;
			}
			
            ENDHLSL
        }
    }
}
