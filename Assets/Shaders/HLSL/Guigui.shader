Shader "Learning/Unlit/Guigui"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _MainColor("Main Color" , Color) = (1,0.5,1,0)
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
			
			struct vertexInput
            {
                float4 vertex : POSITION;
			    float4 color : COLOR;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };
            //Vectex shader
			//Executé pour chaques vex
			//Fonction qui calcule la position finale du vertex dans l'espace écran
            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex); //Ligne obligatoire
                o.color = v.color;
                return o;
            }

			//Fragment shader / Pixel shader
            float4 frag(v2f i) : SV_Target
            {
                return i.color; 
            }
            
            ENDHLSL
        }
    }
}
