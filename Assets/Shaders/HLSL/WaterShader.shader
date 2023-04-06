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
				float3 normal : NORMAL;
				float2 texCoord : TEXCOORD0;
			};
			
			struct PS_INPUT
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
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
				o.pixelWorldPos = o.vertex;
				o.vertex.y += sin(o.vertex.z - _Time.y) * _WaveAmplitude; // Wave effect.
				o.vertex = mul(UNITY_MATRIX_VP, o.vertex);

				o.normal = mul((float3x3) unity_ObjectToWorld, v.normal);

				o.texCoord = v.texCoord;

				return o;
			}

			float4 frag(PS_INPUT i) : SV_Target
			{
				float3 dir = normalize(_WorldSpaceCameraPos - i.pixelWorldPos.xyz);
				float3 nor = normalize(i.normal);

				float3 lightDir = float3(0, -1, 0);
				float3 S = normalize(reflect(lightDir, nor));


				// Use displacement map to warp texture, and animate according to time (Unity's "_Time.y" = current time).
				float displace = tex2D(_DispTexture, i.texCoord - float2(-_Time.y, -_Time.y) * 0.1F).r * 0.2F;
				float3 texColor = tex2D(_AlbedoTexture, i.texCoord + displace - float2(_Time.y, -_Time.y) * 0.1F) * tex2D(_AlbedoTexture, i.texCoord + _Time.y * 0.05F);

				// Water texture, with transparency according to camera angle.
				float4 finalColor = float4(texColor, 1.0F - dot(dir, nor));

				finalColor += pow(max(0, dot(S, dir)), 80) * pow(finalColor.b + 0.5F, 10);
				finalColor.w = saturate(finalColor.w);

				return finalColor;
			}
			
			ENDHLSL
		}
	}
}
