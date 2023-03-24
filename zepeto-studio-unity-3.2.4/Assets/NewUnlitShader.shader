Shader "Unlit/NewUnlitShader"
{
    Properties {
        _MainTex ("Texture", 2D) = "black" {}
        _MaskTex ("Mask Texture", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
     
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
     
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
     
            sampler2D _MainTex;
            sampler2D _MaskTex;
     
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
     
            fixed4 frag (v2f i) : SV_Target {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 maskColor = tex2D(_MaskTex, i.uv);
                fixed4 finalColor = texColor * maskColor.a;
                finalColor.a = texColor.a;
                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
