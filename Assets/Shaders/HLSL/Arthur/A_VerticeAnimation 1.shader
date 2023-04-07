Shader "Learning/Unlit/A_VerticeAnimation2"
{
    Properties
    {   
		
        //_ReflDistort("Reflection distort", Range(0,1.5)) = 0.5  // slider
        //_TintColor("Tint", Color) = (0.34, 0.85, 0.92, 1) // color
        //_WindDir("Wind Direction", Vector) = (1, 0, 0, 1)
    	_Speed("Speed", Float) = 0.07
    	_Albedo("Hologram Lines", 2D) = "white" {}
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
			float _Speed;
			sampler2D _Albedo;

			
			struct vertexInput
            {
                float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };
			
            struct v2f
            {
	            float4 vertex : SV_POSITION;
            	float4 uv : TEXCOORD0;
            };

            v2f vert(vertexInput v)
            {
	            v2f o;
				v.uv.xy += float2(_Time.x * _Speed, 0);
				v.vertex.xyz += v.normal * 1 * tex2Dlod(_Albedo, v.uv).r;
            	
            	
            	o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
            	o.uv = v.uv;
            	
	            return o;
            }
			
            float4 frag(v2f i) : SV_Target
            {
				return tex2D(_Albedo, i.uv);
            }
            
            ENDHLSL
        }
    }
}
