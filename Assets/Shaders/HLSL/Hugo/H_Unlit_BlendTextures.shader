Shader "Learning/Unlit/H_Unlit_BlendTextures"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _Albedo("Albedo", 2D) = "white" {}
        _Albedo2("Albedo2", 2D) = "white" {}
        _Blend("Blend", Range(0,1)) = 0.5
    }
    
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D _Albedo;
			sampler2D _Albedo2;
            float _Blend;
			
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
                return lerp(tex2D(_Albedo, i.uv), tex2D(_Albedo2, i.uv), _Blend);
            }
            
            ENDHLSL
        }
    }
}
