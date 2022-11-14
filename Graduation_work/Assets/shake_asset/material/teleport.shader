Shader "Custom/teleport"
{
    Properties
    {
       _MainTex("Texture",2D) = "white"{}
       _SubTex("Texture",2D) = "white"{}
       _MaskTex("Texture",2D) = "white"{}
       _Displacement("Displacement", Range(0.0,1.0)) = 1.0
       _Dis("Dis", Range(0.0,1.0)) = 0.0
    }
        SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha

      Pass
        {
            Cull front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"


            struct appdata
            {
               float3 normal : NORMAL;
               float4 vertex : POSITION;
               float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 normal : NORMAL;
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _SubTex;
            sampler2D _MaskTex;
            float _Displacement;
            float _Dis;
            float4 _MainTex_ST;


            v2f vert(appdata v)
            {
                v2f o;
                float3 n = UnityObjectToWorldNormal(v.normal);
                o.vertex = UnityObjectToClipPos(v.vertex)/* + float4(n * _Displacement, 0)*/;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 tiling = _MainTex_ST.xy;
                float2 offset = float2(_MainTex_ST.z/* + _SinTime.z*/, _MainTex_ST.w/* + (_Time.w / 10)*/);
                fixed4 sub = tex2D(_SubTex, i.uv);
                fixed4 mask = tex2D(_MaskTex, i.uv);
                fixed4 col = tex2D(_MainTex, i.uv/* * tiling + offset*/);
                /*col.a -= 0.9-smoothstep(_Dis, _Dis + 0.01, sub.r) + 0.1;*/
                fixed4 blend = smoothstep(_Dis, _Dis + 0.1, mask.r) * sub + (1 - smoothstep(_Dis, _Dis + 0.01, mask.r)) * col;
                return blend;
            }
            ENDCG
        }

      Pass
        {
            Cull back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"


            struct appdata
            {
               float3 normal : NORMAL;
               float4 vertex : POSITION;
               float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 normal : NORMAL;
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            sampler2D _SubTex;
            sampler2D _MaskTex;
            float _Displacement;
            float _Dis;
            float4 _MainTex_ST;


            v2f vert(appdata v)
            {
                v2f o;
                float3 n = UnityObjectToWorldNormal(v.normal);
                o.vertex = UnityObjectToClipPos(v.vertex)/* + float4(n * _Displacement, 0)*/;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 tiling = _MainTex_ST.xy;
                float2 offset = float2(_MainTex_ST.z/* + _SinTime.z*/, _MainTex_ST.w/* + (_Time.w / 10)*/);
                fixed4 sub = tex2D(_SubTex, i.uv);
                fixed4 mask = tex2D(_MaskTex, i.uv);
                fixed4 col = tex2D(_MainTex, i.uv/* * tiling + offset*/);
                /*col.a -= 0.9-smoothstep(_Dis, _Dis + 0.01, sub.r) + 0.1;*/
                fixed4 blend = smoothstep(_Dis, _Dis + 0.1, mask.r) * sub + (1 - smoothstep(_Dis, _Dis + 0.01, mask.r)) * col;
                return blend;
            }
            ENDCG
        }
    }
}
