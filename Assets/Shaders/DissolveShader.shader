// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Dissolve"
{
    Properties
    {
        [PerRendererData] _MainTex ("Texture", 2D) = "white" {} // white er default når ingen texture
        _DissolveTex ("Dissolve Texture", 2D) = "white" {} // white er default når ingen texture
        _DissolveAmount("Dissolve Amount", Range(0,1)) = 1
        _EdgeSize("EdgeSize", Range(0,1)) = .1
        _Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
        Cull Off ZWrite Off ZTest Off
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
            sampler2D _DissolveTex; 
            float _DissolveAmount;
            float _EdgeSize;
            float4 _Color;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float4 originalTexture = tex2D(_MainTex, i.uv);
                float4 dissolveTexture = tex2D(_DissolveTex, i.uv);
                float remappedDissolve = _DissolveAmount * (1.01 + _EdgeSize) - _EdgeSize;
                float4 step1 = step(remappedDissolve, dissolveTexture);
                float4 step2 = step(remappedDissolve + _EdgeSize, dissolveTexture);
                float4 edge = step1 - step2;
                edge.a = originalTexture.a;
                edge *= _Color;

                originalTexture.a *= step1.r;
                float4 edgeColorArea = edge * _Color;
                float4 combinedColor = lerp(originalTexture, edgeColorArea, edge.r);

                return combinedColor;
            }
            ENDCG
        }
    }
}
