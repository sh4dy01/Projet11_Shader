Shader "Learning/Unlit/H_ScrollingUvs"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _Albedo("Albedo", 2D) = "white" {}
        _ScrollingSpeed("Scrolling Speed", vector) = (0, 0, 0, 1)
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
			float2 _ScrollingSpeed;
			
			struct vertexInput
            {
                float4 vertex : POSITION;
			    float2 uv : TEXCOORD1;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD1;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv + float2(_ScrollingSpeed.x * _Time.x, _ScrollingSpeed.y * _Time.x);
                
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
