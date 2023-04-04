Shader "Learning/Unlit/H_Unlit_Camera_Blending"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _DistanceRange("Distance Range", Range(0, 100)) = 10
        _Albedo("Albedo1", 2D) = "white" {}
        _Albedo2("Albedo2", 2D) = "white" {}
    }
    
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"
            
			sampler2D _Albedo;
			sampler2D _Albedo2;
			float _DistanceRange;
			
			struct vertexInput
            {
                float4 vertex : POSITION; // position en object/local space
			    float2 uv : TEXCOORD0;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldSpacePos : TEXCOORD1;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
                
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;

                //Get the world space position of the vertex
                o.worldSpacePos = mul(unity_ObjectToWorld, v.vertex);
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float d = distance(_WorldSpaceCameraPos, i.worldSpacePos); // distance entre la caméra et la position du vertex dans l'espace monde
                d = clamp(d / _DistanceRange, 0, 1);
                
                return lerp(tex2D(_Albedo, i.uv),
                            tex2D(_Albedo2, i.uv),
                            d);
            }
            
            ENDHLSL
        }
    }
}
