Shader "Custom/FlickeringTextShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Text Color", Color) = (1,1,1,1)
        _FlickerSpeed ("Flicker Speed", Float) = 10.0
        _FlickerIntensity ("Flicker Intensity", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _FlickerSpeed;
            float _FlickerIntensity;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float flicker = sin(_Time.y * _FlickerSpeed) * _FlickerIntensity;
                float4 col = tex2D(_MainTex, i.uv) * _Color;
                col.rgb += flicker;
                return col;
            }
            ENDCG
        }
    }
}
