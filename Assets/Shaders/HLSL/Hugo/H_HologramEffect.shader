Shader "Learning/Unlit/H_HologramEffect"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _MaskMap("Mask Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _ScrollingSpeed("Scrolling Speed", Float) = 0.0
    }
    
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D _MaskMap;
			float _ScrollingSpeed;
			float4 _Color;
			
			struct vertexInput
            {
                float4 vertex : POSITION;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float worldPosY : TEXCOORD0;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.worldPosY = mul(unity_ObjectToWorld, v.vertex).y - _ScrollingSpeed * _Time.x;
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float texelColor = tex2D(_MaskMap, float2(0, i.worldPosY)).r;
                
                if (texelColor.r <= 0.05)
                    discard;
                
                return _Color;
            }
            
            ENDHLSL
        }
    }
}
