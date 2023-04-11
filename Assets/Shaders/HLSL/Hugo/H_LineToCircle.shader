Shader "Learning/Unlit/H_LineToCircle"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _Texture("Texture", 2D) = "white" {}
        _ScrollingSpeed("Scrolling Speed", float) = 0.1
    }
    
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D _Texture;
            float _ScrollingSpeed;
			
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
                float2 dist =  distance(i.uv, float2(0.5, 0.5));
                dist += _ScrollingSpeed * _Time.x;
                
                return tex2D(_Texture, dist); 
            }
            
            ENDHLSL
        }
    }
}
