Shader "Learning/Unlit/H_AlphaClipping"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _Albedo("Albedo", 2D) = "white" {}
    }
    
    SubShader
    {
		Pass
        {
            //Désactive le culling -> on voit des deux côtés le model
            Cull Off
            
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D _Albedo;
			
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
                float4 text = tex2D(_Albedo, i.uv);
                
                if (text.a < 0.05)
                    discard; // Enlève le pixel du rendu

                return text;
            }
            
            ENDHLSL
        }
    }
}
