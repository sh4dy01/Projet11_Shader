Shader "Learning/Unlit/H_TransparentFresnel"
{
    Properties
    {
        _FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
        _FresnelExponent ("Fresnel Exponent", Range(0.1, 20)) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			float4 _FresnelColor;
			float _FresnelExponent;
            
            // Variables du bloc Properties
            
            struct vertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                // + 
                // Transférer la position & la normale en WORLD SPACE
            };

            v2f vert (vertexInput v)
            {
                v2f o;
                
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
               
                // TO DO 
                // position en float4 => w = 1
                // direction en float4 => w = 0
                // matrice: unity_ObjectToWorld

                o.worldNormal = mul(unity_ObjectToWorld, float4(v.normal, 0)).xyz;
                o.worldNormal = normalize(o.worldNormal);
                
                // la normale en worldspace de la struct v2f doit être normalisée
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
              {
                // Calculer le vecteur FragmentToCamera puis le normaliser

                float3 fragToCam = normalize(_WorldSpaceCameraPos - i.worldPos);
	            
                // Normaliser de nouveau la normale de la struct v2f

                i.worldNormal = normalize(i.worldNormal);
                
                // Calcul du produit scalaire entre le vecteur PixelToCamera (View vector) & la normale

                float NdotV = dot(fragToCam, i.worldNormal);
                
                // Visualiser le résultat de NdotV  => ligne temporaire, juste pour comprendre l'effet à cette étape
                
                // "Ajuster" le résultat obtenu
                NdotV = 1 - NdotV;
                // Utiliser la fonction pow(valueToRaise, FresnelExponent)
	            
                // lerp entre BaseColor, FresnelColor et le rim calculé ci-dessus.
                return float4(_FresnelColor.rgb, pow(NdotV, _FresnelExponent));
            }
            ENDHLSL
        }
    }
}
