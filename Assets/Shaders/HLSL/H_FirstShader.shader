Shader "Learning/Unlit/H_FirstShader"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _MainColor("Color", Color) = (1,1,1,1)
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

			// Data contenues dans chaque vertex
			struct vertexInput
            {
                float4 vertex : POSITION;
			    float4 vertexColor : COLOR;
            };

			/**
			 * v2f => VERTEX TO FRAGMENT
			 * 
			 * Data calculées dans le vertex shader puis interpolées et envoyées au fragment shader
			 */
            struct v2f
            {
                float4 vertex : SV_POSITION;
			    float4 vertexColor : COLOR;
            };

			/*
			 * VERTEX SHADER
			 * 
			 * exécuté pour chaque vertex à chaque frame
			 * Fonction qui calcule la position finale du vertex dans l'espace écran
			 */
            v2f vert (vertexInput v)
            {
                v2f o; // o = output
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex); // ligne obligatoire
            	o.vertexColor = v.vertexColor;
            	
                return o;
            }

			// RASTERIZER
			
			/*
			 * FRAGMENT ou PIXEL shader
			 * 
			 * Pour chaque pixel couvert par vos triangles / polygones
			 */
            float4 frag(v2f i) : SV_Target
            {
                return i.vertexColor; // RGBA
            }
            
            ENDHLSL
        }
    }
}
