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
			};
			
			struct PS_INPUT
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float4 pixelWorldPos : TEXCOORD1;
				float4 clipPos : TEXCOORD2;
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
				o.clipPos = o.vertex;

				o.normal = mul((float3x3) unity_ObjectToWorld, v.normal);

				return o;
			}

			sampler2D _CameraColorTexture;
			sampler2D _CameraDepthTexture;

			float4 frag(PS_INPUT i) : SV_Target
			{
				float3 dir = normalize(_WorldSpaceCameraPos - i.pixelWorldPos.xyz);
				float3 nor = normalize(i.normal);

				float3 lightDir = float3(0, -1, 0);
				float3 S = normalize(reflect(lightDir, nor));

				// Map UVs directly from world space to keep consistent texture size with scaled water planes.
				float2 texCoord = i.pixelWorldPos.xz * 0.2F;

				// Use displacement map to warp texture, and animate according to time (Unity's "_Time.y" = current time).
				float displace = tex2D(_DispTexture, texCoord - float2(-_Time.y, -_Time.y) * 0.1F).r * 0.2F;
				float3 texColor = tex2D(_AlbedoTexture, texCoord + displace - float2(_Time.y, -_Time.y) * 0.1F) * tex2D(_AlbedoTexture, texCoord.yx + _Time.y * 0.05F);

				// Water texture, with transparency according to camera angle.
				float4 finalColor = float4(texColor, 0.0F);//1.0F - dot(dir, nor));

				finalColor += pow(max(0, dot(S, dir)), 80) * pow(finalColor.b + 0.5F, 10);

				float2 screenUVs = (i.clipPos.xy / i.clipPos.w) * 0.5F + 0.5F;
				screenUVs.y = 1.0F - screenUVs.y;
				float zRaw = LinearEyeDepth(tex2D(_CameraDepthTexture, screenUVs).r);

				float pxDist = (zRaw - i.vertex.w + 1.0F);
				float alpha = saturate(1.0F - 1.0F / (pxDist * pxDist));
				//finalColor.rgb *= tex2D(_CameraColorTexture, screenUVs).rgb;
				finalColor.w = alpha;

				return finalColor;
			}
			
			ENDHLSL
		}
	}
}
