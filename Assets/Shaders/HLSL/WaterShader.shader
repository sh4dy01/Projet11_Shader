Shader "Learning/Unlit/Water"
{
	Properties
	{
		// NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
		_WaveAmplitude("Wave Amplitude", Range(0, 1)) = 0.2
		_AlbedoTexture("Albedo", 2D) = "white" { }
		_DispTexture("Disp", 2D) = "white" { }
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
			sampler2D _DispTexture;

			PS_INPUT vert (VS_INPUT v)
			{
				PS_INPUT o;

				o.vertex = mul(unity_ObjectToWorld, v.vertex);
				o.vertex.y += sin(o.vertex.z - _Time.y) * _WaveAmplitude; // Wave effect.
				o.vertex = mul(UNITY_MATRIX_VP, o.vertex);

				o.normal = float4(mul((float3x3) unity_ObjectToWorld, v.normal.xyz), 1.0F);

				o.texCoord = v.texCoord;

				o.pixelWorldPos = mul(unity_ObjectToWorld, v.vertex);

				return o;
			}

			float4 frag(PS_INPUT i) : SV_Target
			{
				float3 dir = normalize(_WorldSpaceCameraPos - i.pixelWorldPos.xyz);
				float3 nor = normalize(i.normal.xyz);

				// Use displacement map to warp texture, and animate according to time (Unity's "_Time.y" = current time).
				float displace = tex2D(_DispTexture, i.texCoord - float2(-_Time.y, -_Time.y) * 0.1F).r * 0.2F;
				float3 light = tex2D(_AlbedoTexture, i.texCoord + displace - float2(_Time.y, -_Time.y) * 0.1F) * tex2D(_AlbedoTexture, i.texCoord + _Time.y * 0.05F);

				// Return water texture, with transparency according to camera angle.
				return float4(light, 1.0F - dot(dir, nor));
			}
			
			ENDHLSL
		}
	}
}
