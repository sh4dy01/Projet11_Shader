Shader "Learning/Unlit/H_AnimateVertices"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _Amplitude("Amplitude", Float) = 1
    }
    
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			float _Amplitude;
			
			struct vertexInput
            {
                float4 vertex : POSITION;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
                v.vertex.y += sin(_Time.y) * _Amplitude;
                //v.vertex.xy += float2(sin(_Time.y), cos(_Time.y)) * _Amplitude;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return float4(1,1,1,0); 
            }
            
            ENDHLSL
        }
    }
}
