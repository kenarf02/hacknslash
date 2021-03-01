Shader "Custom/Outlie"
{
    Properties
    {
       Properties {
                _Color ("Color", Color) = (1,1,1,1)
                _Outline ("Outline Color", Color) = (0,0,0,1)
                _MainTex ("Albedo (RGB)", 2D) = "white" {}
                _Glossiness ("Smoothness", Range(0,1)) = 0.5
                _Size ("Outline Thickness", Float) = 1.5
            }
            SubShader {
                Tags { "RenderType"="Opaque" }
                LOD 200
 
                // render outline
 
                Pass {
                Stencil {
                    Ref 1
                    Comp NotEqual
                }
           
                Cull Off
                ZWrite Off
       
                    CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag
                    #include "UnityCG.cginc"
                    half _Size;
                    fixed4 _Outline;
                    struct v2f {
                        float4 pos : SV_POSITION;
                    };
                    v2f vert (appdata_base v) {
                        v2f o;
                        v.vertex.xyz += v.normal * _Size;
                        o.pos = UnityObjectToClipPos (v.vertex);
                        return o;
                    }
                    half4 frag (v2f i) : SV_Target
                    {
                        return _Outline;
                    }
                    ENDCG
                }
           
                Tags { "RenderType"="Opaque" }
                LOD 200
   
                // render model
       
                Stencil {
                    Ref 1
                    Comp always
                    Pass replace
                }
 
 
                CGPROGRAM
                // Physically based Standard lighting model, and enable shadows on all light types
                #pragma surface surf Standard fullforwardshadows
                // Use shader model 3.0 target, to get nicer looking lighting
                #pragma target 3.0
                sampler2D _MainTex;
                struct Input {
                    float2 uv_MainTex;
                };
                half _Glossiness;
                fixed4 _Color;
                void surf (Input IN, inout SurfaceOutputStandard o) {
                    // Albedo comes from a texture tinted by color
                    fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
                    o.Albedo = c.rgb;
                    // Metallic and smoothness come from slider variables
                    o.Smoothness = _Glossiness;
                    o.Alpha = c.a;
                }
                ENDCG
            }
            FallBack "Diffuse"
        }
}
