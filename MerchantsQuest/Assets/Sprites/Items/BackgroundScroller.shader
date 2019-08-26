Shader "BackgroundScroller"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}
		_XModifier("X Modifier", float) = 0
		_Columns("Columns", int) = 4
		_Rows("Rows", int) = 4
		_RotationSpeed("Rotation Speed", float) = 2.0
		_AnimationSpeed("Animation Speed", float) = 10
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "PreviewType" = "Plane"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _XModifier;
			float _RotationSpeed;
			float _AnimationSpeed;
			uint _Columns;
			uint _Rows;

            v2f vert (appdata v)
            {
                v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
				float2 spriteSize = float2(1.0f / _Columns, 1.0f / _Rows);
				uint totalFrames = _Columns * _Rows;
				uint index = _Time.y * _AnimationSpeed;

				uint indexX = index % _Columns;
				uint indexY = floor((index % totalFrames) / _Columns);
				float2 offset = (spriteSize.x * indexX, -spriteSize.y *indexY);

				float2 newUV = v.uv * spriteSize;
				newUV.y = newUV.y + spriteSize.y*(_Rows -1);
				o.uv = newUV + offset;
				o.uv.x += (_Time.y * 0.1) ;
				o.uv.y += sin(_Time.y * 0.001);
				
				/*

				uint index = index % _Columns
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);*/
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
