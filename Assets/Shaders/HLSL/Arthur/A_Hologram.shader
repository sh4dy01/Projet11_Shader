Shader "Learning/Unlit/A_Hologram"
{
    // Propriétés exposées dans l'inspector
    // NE PAS OUBLIER DE FAIRE LE LIEN APRES DANS VOTRE CODE POUR Y ACCEDER
    Properties
    {   
        _Albedo("Hologram Lines", 2D) = "white" {} 
    	_TintColor("Tint", Color) = (0.34, 0.85, 0.92, 1)
    	_Speed("Speed", Float) = 1
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
			
            sampler2D _Albedo;
			float4 _TintColor;
			float _Speed;
			
			struct vertexInput
            {
                float4 vertex : POSITION;
				float4 vertexColor : COLOR;
				float2 uv : TEXCOORD0;
            };
			
            struct v2f 
            {
	            float4 vertex : SV_POSITION;
            	float4 vertexColor : COLOR;
            	float2 uv : TEXCOORD0;
            	float worldPositionY : TEXCOORD1;
            };

            v2f vert(vertexInput v)
            {
	            v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
            	o.vertexColor = v.vertexColor;
            	o.uv = v.uv;
            	o.worldPositionY = mul(unity_ObjectToWorld, v.vertex).y + _Time.y * _Speed;
	            return o;
            }

            float4 frag(v2f i) : SV_Target
            {
            	float text = tex2D(_Albedo, float2(0, i.worldPositionY)).r;

            	if (text < 0.05)
            		discard;
            	
                return _TintColor;
            }
            
            ENDHLSL
        }
    }
}
