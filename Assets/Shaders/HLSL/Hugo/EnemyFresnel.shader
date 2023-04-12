Shader "Learning/Unlit/H_Fresnel"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
        _FresnelExponent ("Fresnel Exponent", Range(0.1, 2)) = 2
        _Variation ("Variation", Range(0.1, 1)) = 0
    }
    SubShader
    {
        Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			float4 _MainColor;
			float4 _FresnelColor;
			float _FresnelExponent;
			float _Variation;
            
            // Variables du bloc Properties
            
            struct vertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
                
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

                o.worldNormal = mul(unity_ObjectToWorld, float4(v.normal, 0)).xyz;
                o.worldNormal = normalize(o.worldNormal);
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 fragToCam = normalize(_WorldSpaceCameraPos - i.worldPos);
                
                i.worldNormal = normalize(i.worldNormal);
                
                float NdotV = dot(fragToCam, i.worldNormal);
                NdotV = 1 - NdotV;
                
                _FresnelExponent = lerp(_FresnelExponent, _FresnelExponent + _Variation, abs(_SinTime.w));
	            
                return lerp(_MainColor, _FresnelColor, pow(NdotV, _FresnelExponent));
            }
            ENDHLSL
        }
    }
}
