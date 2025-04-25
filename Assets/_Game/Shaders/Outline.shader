Shader "Unlit/Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineWidth("Outline Width", Range(0, 10)) = 0.01
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent" "Queue"="Transparent"
        }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize; // Для масштабирования обводки
            fixed4 _OutlineColor;
            float _OutlineWidth;

            static float D = 0.7;
            static float2 _dir[8] = {
                float2(1, 0), float2(-1, 0), float2(0, 1), float2(0, -1),
                float2(D, D), float2(-D, D), float2(-D, -D), float2(D, -D)
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            float GetMaxAlfa(float2 uv)
            {
                float result = 0;
                for (int i = 0; i < 8; i++)
                {
                    // Масштабируем смещение в зависимости от размера текстуры
                    float2 offset = _dir[i] * _OutlineWidth * _MainTex_TexelSize.xy;
                    float2 sUV = uv + offset;
                    result = max(result, tex2D(_MainTex, sUV).a);
                }
                return result;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col *= i.color;

                fixed outlineAlpha = GetMaxAlfa(i.uv);

                if (outlineAlpha > col.a)
                {
                    col.rgb = _OutlineColor.rgb;
                    col.a = outlineAlpha;
                }

                return col;
            }
            ENDCG
        }
    }
}