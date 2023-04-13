Shader "Arthur/ArrowUp"
{
    Properties
    {   
        _MaskMap("Mask Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Speed("Speed", Float) = 1
    }
    
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }         
        Blend SrcAlpha OneMinusSrcAlpha      
        ZWrite Off
        
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D _MaskMap;
			float _Speed;
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
                o.worldPosY = mul(unity_ObjectToWorld, v.vertex).y + _Speed * _Time.x;
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float texColor = tex2D(_MaskMap, float2(0, i.worldPosY)).r;
                
                if (texColor.r > 0)
                    discard;
                
                return _Color;
            }
            
            ENDHLSL
        }
    }
}
