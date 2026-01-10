Shader "Custom/HueSatLightSpriteLit"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _Hue ("Hue", Range(0,1)) = 0
        _Saturation ("Saturation", Range(0,2)) = 1
        _Lightness ("Lightness", Range(0,2)) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            float _Hue;
            float _Saturation;
            float _Lightness;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            v2f vert (appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.uv = TRANSFORM_TEX(IN.texcoord, _MainTex);
                OUT.color = IN.color * _Color;
                return OUT;
            }

            float3 HueShift(float3 col, float hue)
            {
                float angle = hue * 6.28318530718;
                float s = sin(angle);
                float c = cos(angle);

                float3x3 mat = float3x3(
                    0.299 + 0.701 * c + 0.168 * s, 0.587 - 0.587 * c + 0.330 * s, 0.114 - 0.114 * c - 0.497 * s,
                    0.299 - 0.299 * c - 0.328 * s, 0.587 + 0.413 * c + 0.035 * s, 0.114 - 0.114 * c + 0.292 * s,
                    0.299 - 0.3 * c + 1.25 * s,     0.587 - 0.588 * c - 1.05 * s, 0.114 + 0.886 * c - 0.203 * s
                );

                return mul(mat, col);
            }

            float3 AdjustSaturation(float3 col, float sat)
            {
                float grey = dot(col, float3(0.299, 0.587, 0.114));
                return lerp(grey.xxx, col, sat);
            }

            float3 AdjustLightness(float3 col, float light)
            {
                return col * light;
            }

            fixed4 frag (v2f IN) : SV_Target
            {
                float4 c = tex2D(_MainTex, IN.uv) * IN.color;

                // PREMULTIPLY RGB BY ALPHA
                float alpha = c.a;
                float3 rgb = c.rgb * alpha;

                // Apply color operations in premultiplied space
                rgb = HueShift(rgb, _Hue);
                rgb = AdjustSaturation(rgb, _Saturation);
                rgb = AdjustLightness(rgb, _Lightness);

                // UNPREMULTIPLY (avoid divide by zero)
                if (alpha > 0.0001)
                    rgb /= alpha;

                return float4(rgb, alpha);
            }
            ENDCG
        }
    }
}
