// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:33037,y:32523,varname:node_2865,prsc:2|emission-8995-OUT,clip-6847-OUT;n:type:ShaderForge.SFN_Tex2d,id:665,x:32105,y:32345,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_665,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:812ad67abbba1fb4684e1df7f928f734,ntxv:0,isnm:False|UVIN-57-UVOUT;n:type:ShaderForge.SFN_Fresnel,id:5169,x:32165,y:32564,varname:node_5169,prsc:2|EXP-8408-OUT;n:type:ShaderForge.SFN_Multiply,id:7450,x:32362,y:32366,varname:node_7450,prsc:2|A-665-RGB,B-5169-OUT,C-8455-OUT;n:type:ShaderForge.SFN_Clamp01,id:7192,x:32576,y:32366,varname:node_7192,prsc:2|IN-7450-OUT;n:type:ShaderForge.SFN_Color,id:8284,x:32525,y:32194,ptovrint:False,ptlb:node_8284,ptin:_node_8284,varname:node_8284,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_SceneColor,id:7916,x:32314,y:32042,varname:node_7916,prsc:2;n:type:ShaderForge.SFN_Clamp01,id:7012,x:32510,y:32042,varname:node_7012,prsc:2|IN-7916-RGB;n:type:ShaderForge.SFN_Lerp,id:8995,x:32728,y:32071,varname:node_8995,prsc:2|A-7012-OUT,B-8284-RGB,T-7192-OUT;n:type:ShaderForge.SFN_Slider,id:8408,x:31757,y:32615,ptovrint:False,ptlb:Fresnel Slider,ptin:_FresnelSlider,varname:node_8408,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.5,max:3;n:type:ShaderForge.SFN_Slider,id:8455,x:32001,y:32762,ptovrint:False,ptlb:Transparent,ptin:_Transparent,varname:node_8455,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:2;n:type:ShaderForge.SFN_TexCoord,id:7761,x:31730,y:32024,varname:node_7761,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:9091,x:31932,y:32096,varname:node_9091,prsc:2,spu:0,spv:1|UVIN-7761-UVOUT,DIST-2871-OUT;n:type:ShaderForge.SFN_Panner,id:57,x:31932,y:32305,varname:node_57,prsc:2,spu:1,spv:0|UVIN-9091-UVOUT,DIST-2644-OUT;n:type:ShaderForge.SFN_Multiply,id:2644,x:31698,y:32328,varname:node_2644,prsc:2|A-9906-T,B-9817-OUT;n:type:ShaderForge.SFN_Time,id:9906,x:31451,y:32234,varname:node_9906,prsc:2;n:type:ShaderForge.SFN_Slider,id:4937,x:31315,y:32403,ptovrint:False,ptlb:ChangeSpeedVertical,ptin:_ChangeSpeedVertical,varname:node_4937,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0,max:2;n:type:ShaderForge.SFN_Slider,id:9817,x:31275,y:32539,ptovrint:False,ptlb:ChangeSpeedHorizontal,ptin:_ChangeSpeedHorizontal,varname:node_9817,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-2,cur:0,max:2;n:type:ShaderForge.SFN_Multiply,id:2871,x:31709,y:32181,varname:node_2871,prsc:2|A-9906-T,B-4937-OUT;n:type:ShaderForge.SFN_Tex2d,id:7309,x:32607,y:33030,ptovrint:False,ptlb:dissolve pattern,ptin:_dissolvepattern,varname:node_7309,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:aec5b123dea003743b0e8cbe07b6d75a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:7409,x:32046,y:32919,ptovrint:False,ptlb:dissolver amount,ptin:_dissolveramount,varname:node_7409,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:6847,x:32820,y:32889,varname:node_6847,prsc:2|A-98-OUT,B-7309-R;n:type:ShaderForge.SFN_RemapRange,id:98,x:32607,y:32775,varname:node_98,prsc:2,frmn:0,frmx:1,tomn:-0.6,tomx:0.6|IN-5500-OUT;n:type:ShaderForge.SFN_OneMinus,id:5500,x:32457,y:32843,varname:node_5500,prsc:2|IN-7409-OUT;proporder:665-8284-8408-8455-4937-9817-7309-7409;pass:END;sub:END;*/

Shader "Shader Forge/ForceFieldDome2" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _node_8284 ("node_8284", Color) = (1,1,1,1)
        _FresnelSlider ("Fresnel Slider", Range(0, 3)) = 1.5
        _Transparent ("Transparent", Range(0, 2)) = 2
        _ChangeSpeedVertical ("ChangeSpeedVertical", Range(-2, 2)) = 0
        _ChangeSpeedHorizontal ("ChangeSpeedHorizontal", Range(-2, 2)) = 0
        _dissolvepattern ("dissolve pattern", 2D) = "white" {}
        _dissolveramount ("dissolver amount", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float4 _node_8284;
            uniform float _FresnelSlider;
            uniform float _Transparent;
            uniform float _ChangeSpeedVertical;
            uniform float _ChangeSpeedHorizontal;
            uniform sampler2D _dissolvepattern; uniform float4 _dissolvepattern_ST;
            uniform float _dissolveramount;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float4 _dissolvepattern_var = tex2D(_dissolvepattern,TRANSFORM_TEX(i.uv0, _dissolvepattern));
                clip((((1.0 - _dissolveramount)*1.2+-0.6)+_dissolvepattern_var.r) - 0.5);
////// Lighting:
////// Emissive:
                float4 node_9906 = _Time;
                float2 node_57 = ((i.uv0+(node_9906.g*_ChangeSpeedVertical)*float2(0,1))+(node_9906.g*_ChangeSpeedHorizontal)*float2(1,0));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_57, _Texture));
                float3 emissive = lerp(saturate(sceneColor.rgb),_node_8284.rgb,saturate((_Texture_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelSlider)*_Transparent)));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _dissolvepattern; uniform float4 _dissolvepattern_ST;
            uniform float _dissolveramount;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _dissolvepattern_var = tex2D(_dissolvepattern,TRANSFORM_TEX(i.uv0, _dissolvepattern));
                clip((((1.0 - _dissolveramount)*1.2+-0.6)+_dissolvepattern_var.r) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float4 _node_8284;
            uniform float _FresnelSlider;
            uniform float _Transparent;
            uniform float _ChangeSpeedVertical;
            uniform float _ChangeSpeedHorizontal;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_9906 = _Time;
                float2 node_57 = ((i.uv0+(node_9906.g*_ChangeSpeedVertical)*float2(0,1))+(node_9906.g*_ChangeSpeedHorizontal)*float2(1,0));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_57, _Texture));
                o.Emission = lerp(saturate(sceneColor.rgb),_node_8284.rgb,saturate((_Texture_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelSlider)*_Transparent)));
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
