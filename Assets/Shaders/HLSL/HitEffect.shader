Shader "Arthur/HitEffect"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
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
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return float4(1,1,1,0.3);
            }
            
            ENDHLSL
        }
    }
}
