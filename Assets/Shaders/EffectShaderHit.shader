// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/EffectShaderHit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "black" {} // white er default når ingen texture
        _Magnitude("Maginutude", Range(0,5)) = 1
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            //Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex; // skal defineres for at det virker
            float _Magnitude;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float2 center = (0.5,0.5);
                float dist = distance(center, i.uv);
                float4 col = tex2D(_MainTex, i.uv);
                col.yz *= 1 - (dist * _Magnitude);
                return col;
            }
            ENDCG
        }
    }
}
