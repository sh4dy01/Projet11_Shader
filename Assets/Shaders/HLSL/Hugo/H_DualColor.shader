Shader "Learning/Unlit/H_DualColor"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _Color1("Color1", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (1,1,1,1)
    }
    
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			float4 _Color1;
            float4 _Color2;
			
			struct vertexInput
            {
                float4 vertex : POSITION;						
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 worldSpacePos : TEXCOORD1;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.worldSpacePos = mul(unity_ObjectToWorld, v.vertex);
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return i.worldSpacePos < 0 ? _Color1 : _Color2;
            }
            
            ENDHLSL
        }
    }
}
