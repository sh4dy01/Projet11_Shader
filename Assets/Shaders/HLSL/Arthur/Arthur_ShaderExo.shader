Shader "Learning/Unlit/Arthur_ShaderExo"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _MainColor("Main Color", Color) = (1,1,1,1)
    }
    
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			float4 _MainColor;

			// Data de chaque vertex
			struct vertexInput
            {
                float4 vertex : POSITION;
			    float4 vertexColor : COLOR;
            };

			// Vertex to fragment (v2f)
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 vertexColor : COLOR;
            };
			
            // VERTEX SHADER
			// Exécuté pour chaque vex
			// Fonction qui calcule la position finale du vertex dans l'espace écran
            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex); // LIGNE OBLIGATOIRE. Calcul la position du vertex dans l'espace écran
                o.vertexColor = v.vertexColor;
                return o;
            }

			// Fragment shader / Pixel shader
			// Exécuté pour chaque fragment/pixel couvert par vos polygones.
            float4 frag(v2f i) : SV_Target
            {
            	//return float4(1,1,0,1);
            	//return _MainColor;
                return i.vertexColor;
            }
            
            ENDHLSL
        }
    }
}
