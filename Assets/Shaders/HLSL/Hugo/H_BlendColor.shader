Shader "Learning/Unlit/H_BlendColor"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _LerpColor("Lerp Color", Color) = (1,1,1,1)
        _LerpIntensity("Lerp Intensity", Range(0,1)) = 0.5
    }
    
    SubShader
    {
		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			float4 _BaseColor, _LerpColor;
			float _LerpIntensity;
			
			struct vertexInput
            {
                float4 vertex : POSITION;
			    float4 color : COLOR;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return lerp(_BaseColor, _LerpColor, _LerpIntensity);
            }
            
            ENDHLSL
        }
    }
}
