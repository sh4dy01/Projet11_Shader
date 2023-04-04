Shader "Learning/Unlit/H_Unlit_BlendTexturesWithMask"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _Albedo("Albedo", 2D) = "white" {}
        _Albedo2("Albedo2", 2D) = "white" {}
        _Mask("Mask", 2D) = "white" {}
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
            sampler2D _Mask;
			
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
                return lerp(tex2D(_Albedo, i.uv), tex2D(_Albedo2, i.uv), tex2D(_Mask, i.uv).r); // on utilise le canal rouge de la texture en niveau de gris
                // float4 demo = tex2D(_Albedo, i.uv);
                // return demo.rgba; <- base
                // return demo.brga; <- can swap color chanel
                // return demo.xyzw; <- same here
            }
            
            ENDHLSL
        }
    }
}
