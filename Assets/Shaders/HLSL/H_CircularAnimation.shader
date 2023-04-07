Shader "Learning/Unlit/H_CircularExtrude"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _NoiseMap("Noise Map", 2D) = "white" {}
        _ScalingFactor("Scaling Factor", float) = 1.0
        _ScrollingSpeed("Scrolling Speed", float) = 1
    }
    
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D _NoiseMap;
			float _ScalingFactor, _ScrollingSpeed;
			
			struct vertexInput
            {
                float4 vertex : POSITION;
			    float3 normal : NORMAL;
			    float4 uv : TEXCOORD0;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (vertexInput v)
            {
                v2f o;

                v.uv.xy += float2(_ScrollingSpeed *  _Time.y, 0);
                
                v.vertex.xyz += v.normal * _ScalingFactor * tex2Dlod(_NoiseMap, v.uv).r;
                
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
                
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return tex2D(_NoiseMap, i.uv); 
            }
            
            ENDHLSL
        }
    }
}
