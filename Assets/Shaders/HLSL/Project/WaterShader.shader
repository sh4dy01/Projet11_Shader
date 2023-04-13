Shader "Learning/Unlit/Water"
{
	Properties
	{
		// NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
		_WaveAmplitude("Wave Amplitude", Range(0, 1)) = 0.2
		_AlbedoTexture("Albedo", 2D) = "white" { }

		_WaterColor("Water Color", Color) = (0.2, 0.5, 1.0, 0)
		_FoamColor("Foam Color", Color) = (1, 1, 1, 0)
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
			#include "UnityLightingCommon.cginc"
			
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

			float4 _WaterColor;
			float4 _FoamColor;

			PS_INPUT vert (VS_INPUT v)
			{
				PS_INPUT o;

				o.vertex = mul(unity_ObjectToWorld, v.vertex);
				o.pixelWorldPos = o.vertex;

				o.vertex.y += sin(o.pixelWorldPos.z + _Time.y) * _WaveAmplitude; // Wave effect.
				o.vertex = mul(UNITY_MATRIX_VP, o.vertex);
				o.clipPos = o.vertex;

				o.normal = mul(unity_ObjectToWorld, v.normal);
				o.normal.x += cos(o.pixelWorldPos.z + _Time.y) * _WaveAmplitude;

				return o;
			}

			sampler2D _CameraColorTexture;
			sampler2D _CameraDepthTexture;

			float4 frag(PS_INPUT i) : SV_Target
			{
				float3 dir = normalize(_WorldSpaceCameraPos - i.pixelWorldPos.xyz);
				float3 nor = normalize(i.normal);


				// Map UVs directly from world space to keep consistent texture size with scaled water planes.
				float2 texCoord = i.pixelWorldPos.xz * 0.05F;

				// Use displacement map to warp texture, and animate according to time (Unity's "_Time.y" = current time).
				float3 texColor = tex2D(_AlbedoTexture, texCoord.yx + _Time.y * 0.05F) * tex2D(_AlbedoTexture, texCoord - float2(_Time.y, -_Time.y) * 0.1F);

				float lum = dot(texColor, float3(0.24F, 0.7F, 0.06F));
				float4 finalColor = lum > 0.3F ? _FoamColor : _WaterColor;


				// Diffuse lighting.
				float b = max(0.1F, dot(nor, _WorldSpaceLightPos0));
				finalColor *= b * _LightColor0;

				// Specular.
				float3 V = reflect(-dir, nor);
				finalColor += pow(max(0, dot(V, _WorldSpaceLightPos0.xyz)), 200) * 4 * _LightColor0;


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
