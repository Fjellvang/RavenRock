// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/UnlitEffectsShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // white er default når ingen texture
        _DisplaceTex ("Displacement Texture", 2D) = "white" {} // white er default når ingen texture
        _Magnitude("Maginutude", Range(0,0.1)) = 1
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
            sampler2D _DisplaceTex; // skal defineres for at det virker
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
                float2 distuv = float2(i.uv.x + _Time.x * 2, i.uv.y + _Time.x * 2);

                float2 disp = tex2D(_DisplaceTex, distuv).xy;
                disp = ((disp * 2) - 1) * _Magnitude;

                float4 col = tex2D(_MainTex, i.uv + disp);
                //col *= float4(i.uv.x, i.uv.y,0,1);
                return col;
            }
            ENDCG
        }
    }
}
