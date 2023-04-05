Shader "Learning/Unlit/H_WorldGradient"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _Gradient("Gradient", 2D) = "white" {}
    }
    
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D _Gradient;
			
			struct vertexInput
            {
                float4 vertex : POSITION;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldSpacePos : TEXCOORD1;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.worldSpacePos = mul(unity_ObjectToWorld, v.vertex).xyz;

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return tex2D(_Gradient, i.worldSpacePos.x); 
            }
            
            ENDHLSL
        }
    }
}
