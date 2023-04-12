Shader "Learning/Lit/Lambert_DirLight"
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
			
			struct vertexInput
			{
				float4 vertex : POSITION;				
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				// normal en world space
			};

			v2f vert(vertexInput v)
			{
				v2f o;

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				
				// To do
				
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				// To do => NdotL 
				// N & L dans le même espace et normalisés
				// L => direction reçue depuis le script C#. Forward de la DirLight
				// A inverser car côté shader, le vecteur de la lumière part depuis le fragment
				
				// dot retourne des valeurs entre -1 et 1, => clamp à utiliser
				
				return float4(0.8, 0.3, 0.1, 1.0);
			}
			
            ENDHLSL
        }
    }
}
