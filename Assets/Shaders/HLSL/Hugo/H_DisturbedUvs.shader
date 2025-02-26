Shader "Learning/Unlit/H_DisturbedUvs"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _Albedo("Albedo", 2D) = "white" {}
        _Noise("Noise", 2D) = "white" {}
        _ScrollingSpeed("Scrolling Speed", vector) = (0, 0, 0, 1)
        _DisturbedFactor("Disturbed Factor", range(0, 0.2)) = 0
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
			sampler2D _Noise;
			float2 _ScrollingSpeed;
			float _DisturbedFactor;
			
			struct vertexInput
            {
                float4 vertex : POSITION;
			    float2 uv : TEXCOORD0;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                //get the red channel of the noise texture
                const float noise = tex2D(_Noise, i.uv).r;
                
                //disturbe the uv coordinates
                i.uv += noise * _DisturbedFactor + _ScrollingSpeed * _Time.x;
                
                return tex2D(_Albedo, i.uv);
            }
            
            ENDHLSL
        }
    }
}
