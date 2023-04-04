Shader "Learning/Unlit/Water"
{
	Properties
	{
		// NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
		_WaveAmplitude("Wave Amplitude", Range(0, 1)) = 0.2
		_AlbedoTexture("Albedo", 2D) = "white" { }
	}
	
	SubShader
	{
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			HLSLPROGRAM

			#pragma vertex vert  
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			struct VS_INPUT
			{
				float4 vertex : POSITION;
				float4 normal : NORMAL;
				float2 texCoord : TEXCOORD0;
			};
			
			struct PS_INPUT
			{
				float4 vertex : SV_POSITION;
				float4 normal : NORMAL;
				float2 texCoord : TEXCOORD0;
				float4 pixelWorldPos : TEXCOORD1;
			};

			float _WaveAmplitude;
			sampler2D _AlbedoTexture;

			PS_INPUT vert (VS_INPUT v)
			{
				PS_INPUT o;

				o.vertex = v.vertex;
				o.vertex.y += sin(o.vertex.x + _Time.y) *_WaveAmplitude;
				o.vertex = mul(UNITY_MATRIX_MVP, o.vertex);

				o.normal = float4(mul((float3x3) unity_ObjectToWorld, v.normal.xyz), 1.0F);

				o.texCoord = v.texCoord;

				o.pixelWorldPos = mul(unity_ObjectToWorld, v.vertex);

				return o;
			}

			float4 frag(PS_INPUT i) : SV_Target
			{
				float3 dir = normalize(_WorldSpaceCameraPos - i.pixelWorldPos.xyz);
				float3 nor = normalize(i.normal.xyz);

				float3 light = float3(0.2F, 0.5F, 1) * tex2D(_AlbedoTexture, i.texCoord);

				return float4(light, 1.0F - dot(dir, nor));
			}
			
			ENDHLSL
		}
	}
}
