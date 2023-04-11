Shader "Learning/Unlit/Fresnel"
{
    Properties
    {   
		// Fresnel Exponent : float entre 0.1 & 20
        // 2 couleurs : une BaseColor (celle du mesh) et une pour l'effet outline du fresnel       
    }
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"
			
            
            // Variables du bloc Properties
            
            struct vertexInput
            {
                float4 vertex : POSITION;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                
                // + 
                // Transférer la position & la normale en WORLD SPACE
            };

            v2f vert (vertexInput v)
            {
                v2f o;
               
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
               
                // TO DO 
                // position en float4 => w = 1
                // direction en float4 => w = 0
                // matrice: unity_ObjectToWorld
                
                // la normale en worldspace de la struct v2f doit être normalisée
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // TO DO: Une ligne à coder après chaque commentaire
                // Calculer le vecteur FragmentToCamera puis le normaliser
	            
                // Normaliser de nouveau la normale de la struct v2f
                
                // Calcul du produit scalaire entre le vecteur PixelToCamera (View vector) & la normale
	            
                // Visualiser le résultat de NdotV  => ligne temporaire, juste pour comprendre l'effet à cette étape
                
                // "Ajuster" le résultat obtenu
                
                // Utiliser la fonction pow(valueToRaise, FresnelExponent)
	            
                // lerp entre BaseColor, FresnelColor et le rim calculé ci-dessus.
	            return float4(0.9, 0.3, 0.2, 1.0);
            }
            ENDHLSL
        }
    }
}
