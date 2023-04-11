Shader "Learning/Unlit/Fresnel"
{
    Properties
    {   
		_Color("Color", Color) = (0.34, 0.85, 0.92, 1)
    	_BorderColor("Border Color", Color) = (0.84, 0.35, 0.22, 1)
        _BorderSize("Border Size", range(0,2)) = 0.5   
    }
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"
			
            
            float4 _Color, _BorderColor;
			float _BorderSize;
            
            struct vertexInput
            {
                float4 vertex : POSITION;
                float4 wordPos : TEXCOORD0;
				float3 normal : NORMAL;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 wordPos : TEXCOORD0;
				float3 worldSpaceNormal : NORMAL;
                // + 
                // Transf�rer la position & la normale en WORLD SPACE
            };

            v2f vert (vertexInput v)
            {
                v2f o;
            	
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
            	
                o.wordPos = mul(unity_ObjectToWorld, v.vertex);
				o.worldSpaceNormal = normalize(mul(unity_ObjectToWorld,v.normal));
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // TO DO: Une ligne � coder apr�s chaque commentaire
                // Calculer le vecteur FragmentToCamera puis le normaliser
				float3 FragToCam = normalize(_WorldSpaceCameraPos - i.wordPos);
            	
                // Normaliser de nouveau la normale de la struct v2f
            	i.worldSpaceNormal = normalize(i.worldSpaceNormal);
                
                // Calcul du produit scalaire entre le vecteur PixelToCamera (View vector) & la normale
            	float NdotV = dot(FragToCam, i.worldSpaceNormal);
	            
                // Visualiser le r�sultat de NdotV  => ligne temporaire, juste pour comprendre l'effet � cette �tape
                
                // "Ajuster" le r�sultat obtenu
                
                // Utiliser la fonction pow(valueToRaise, FresnelExponent)
	            
                // lerp entre BaseColor, FresnelColor et le rim calcul� ci-dessus.
	            return lerp(_BorderColor, _Color,pow(NdotV,_BorderSize));
            }
            ENDHLSL
        }
    }
}
