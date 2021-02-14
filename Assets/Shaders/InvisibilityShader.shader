Shader "Unlit/InvisibilityShader"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex ("Texture", 2D) = "white" {}
        _Tiling("Tiling", Vector) = (1.0,1.0,0,0)
        _Offset("Offset", Vector) = (.0,.0,0,0)
        _DistortionTiling("Distortion Tiling", Vector) = (1.0,1.0,0,0)
        _DistortionOffset("Distortion Offset", Vector) = (.0,.0,0,0)
        _DistortionStrength("DistortionStrength", Range(0.1,1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
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

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float2 _Tiling;
            float2 _Offset;
            float2 _DistortionTiling;
            float2 _DistortionOffset;
            float _DistortionStrength;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 distortionTextureUV = i.uv * _DistortionTiling + _DistortionOffset;
                float2 distortionNoiseOffset = tex2D(_NoiseTex, distortionTextureUV).xy * _DistortionStrength;
                fixed4 col = tex2D(_MainTex, i.uv * _Tiling + _Offset);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
