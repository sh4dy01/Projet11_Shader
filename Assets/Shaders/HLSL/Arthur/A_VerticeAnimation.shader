Shader "Learning/Unlit/A_VerticeAnimation"
{
    Properties
    {   
		
        //_ReflDistort("Reflection distort", Range(0,1.5)) = 0.5  // slider
        //_TintColor("Tint", Color) = (0.34, 0.85, 0.92, 1) // color
        //_WindDir("Wind Direction", Vector) = (1, 0, 0, 1)
    	_Amplitude("Amplitude", Float) = 0.07
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			
			// PROPERTIES
			float _Amplitude;

			
			struct vertexInput
            {
                float4 vertex : POSITION;						
            };
			
            struct v2f
            {
	            float4 vertex : SV_POSITION;
            	float3 worldspacePos : TEXCOORD0;
            };

            v2f vert(vertexInput v)
            {
	            v2f o;
            	v.vertex.y += _Amplitude * sin(_Time.y);
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);

            	o.worldspacePos = mul(unity_ObjectToWorld, v.vertex).xyz;
            	
	            return o;
            }
			
            float4 frag(v2f i) : SV_Target
            {
            	float value = saturate(i.worldspacePos.y);
	            return float4(value, value, value, 1);
            }
            
            ENDHLSL
        }
    }
}
