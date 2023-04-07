Shader "Learning/Unlit/Guigui"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _FirstTexture("texture",2D) = "white"{}
    	_SecondTexture("texture",2D) = "white"{}
    	_Range("range",range(0,100)) = 4
    }
    
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D _FirstTexture, _SecondTexture;
			float _Range;
			
			struct vertexInput
            {
                float4 vertex : POSITION;
			    float2 uv : TEXCOORD0;
				float3 worldPosition : TEXCOORD1;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
            	float2 uv : TEXCOORD0;
            	float3 worldPosition : TEXCOORD1;
            };
            //Vectex shader
			//Executé pour chaques vex
			//Fonction qui calcule la position finale du vertex dans l'espace écran
            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex); //Ligne obligatoire
            	o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
            	o.uv = v.uv;
                return o;
            }

			//Fragment shader / Pixel shader
            float4 frag(v2f i) : SV_Target
            {
            	float dist = distance(_WorldSpaceCameraPos, i.worldPosition);
            	float newRange = clamp(2*dist/_Range,0,1);
            	return lerp(tex2D(_FirstTexture,i.uv),tex2D(_SecondTexture,i.uv),newRange);
            }
            
            ENDHLSL
        }
    }
}
