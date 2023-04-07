Shader "Learning/Unlit/SphereMovement"
{
    // Propriétés exposées dans l'inspector
    // NE PAS OUBLIER DE FAIRE LE LIEN APRES DANS VOTRE CODE POUR Y ACCEDER
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue 
    	_Intensity("Intensity", float) = 1
    	_Speed("speed", float ) = 3
    	_Texture("texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  // On définit le vertex shader. Nom de la fonction: vert (peut être changé)
            #pragma fragment frag  // On définit le fragment shader. Nom de la fonction: frag (peut être changé)

            #include "UnityCG.cginc"
			
			// Ecrivez ici les variables exposées dans le bloc Properties
			float _Intensity;
			float _Speed;
			sampler2D _Texture;
			
			
			struct vertexInput
            {
                float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };
            
            // Autre exemple d'une struct contenant beaucoup plus de data 
            /*
            struct vertexInput {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
                float4 texcoord2 : TEXCOORD2;
                float4 texcoord3 : TEXCOORD3;
                float4 color : COLOR;
            };
            */
			
            // Données qui vont être interpolées par le Rasterizer et qui seront en input du fragment shader
            // Chaque variable de cette struct doit être calculée dans le vertex shader !
            struct v2f   // v2f = vertex to fragment     ou p-e appelé vertexOutput
            {
	            float4 vertex : SV_POSITION; // => SV_POSITION signifie que c'est la position en clip space
            	float4 uv : TEXCOORD0;
            	float3 normal : NORMAL;
            };

            v2f vert(vertexInput v)
            {
	            v2f o;
				v.uv.xy += float2(_Time.x * _Speed, 0);
            	float text = tex2Dlod(_Texture, (v.uv)).r;
            	v.vertex.xyz += v.normal * text * _Intensity;
            	 
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex); // ETAPE OBLIGATOIRE DU VS. De l'espace objet à l'espace de clipping (écran)
            	o.uv = v.uv;
            	o.normal = v.normal;

            	
	            return o;
            }

            // Etape intermédiaire entre le VS et le FS, la rasterization. 
            // Le GPU va déterminer les fragments couverts par les polygones, et les données transférées
            // du VS au FS seront interpolées.

            // FRAGMENT SHADER: calcul la couleur finale du fragment/pixel
            float4 frag(v2f i) : SV_Target
            {
	            return tex2D(_Texture, i.uv);
            }
            
            ENDHLSL
        }
    }
}
