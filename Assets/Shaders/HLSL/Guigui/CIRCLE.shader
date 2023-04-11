Shader "Learning/Unlit/Player"
{
    // Propriétés exposées dans l'inspector
    // NE PAS OUBLIER DE FAIRE LE LIEN APRES DANS VOTRE CODE POUR Y ACCEDER
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue 
        //_ReflDistort("Reflection distort", Range(0,1.5)) = 0.5  // slider
    	//_WindDir("Wind Direction", Vector) = (1, 0, 0, 1)
        _Texture("Texture", 2D) = "white" {}
    	_Speed("speed", float) = 0
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
            sampler2D _Texture;
			float _Speed;
			
			
			struct vertexInput
            {
                float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 wordPosition : TEXCOORD1;
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
            	float2 uv : TEXCOORD0;
            };

            v2f vert(vertexInput v)
            {
	            v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex); // ETAPE OBLIGATOIRE DU VS. De l'espace objet à l'espace de clipping (écran)
            	o.uv = v.uv;
	            return o;
            }

            // Etape intermédiaire entre le VS et le FS, la rasterization. 
            // Le GPU va déterminer les fragments couverts par les polygones, et les données transférées
            // du VS au FS seront interpolées.

            // FRAGMENT SHADER: calcul la couleur finale du fragment/pixel
            float4 frag(v2f i) : SV_Target
            {
            	float dist = distance(i.uv, float2(0.5, 0.5));
            	dist += _Speed * _Time;
	            return tex2D(_Texture,dist);
            }
            
            ENDHLSL
        }
    }
}
