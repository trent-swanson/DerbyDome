// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:1,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32719,y:32712,varname:node_2865,prsc:2|emission-1687-OUT,clip-8083-OUT;n:type:ShaderForge.SFN_Slider,id:4866,x:31044,y:32729,ptovrint:False,ptlb:ChangeSpeedVertical,ptin:_ChangeSpeedVertical,varname:node_4866,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:9488,x:31030,y:32881,ptovrint:False,ptlb:ChangeSpeedHorizontal,ptin:_ChangeSpeedHorizontal,varname:node_9488,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Time,id:1762,x:31122,y:32552,varname:node_1762,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7447,x:31411,y:32725,varname:node_7447,prsc:2|A-1762-T,B-9488-OUT;n:type:ShaderForge.SFN_Multiply,id:6426,x:31422,y:32576,varname:node_6426,prsc:2|A-1762-T,B-4866-OUT;n:type:ShaderForge.SFN_Panner,id:7670,x:31637,y:32674,varname:node_7670,prsc:2,spu:1,spv:0|UVIN-2015-UVOUT,DIST-7447-OUT;n:type:ShaderForge.SFN_Panner,id:2015,x:31622,y:32447,varname:node_2015,prsc:2,spu:0,spv:1|UVIN-570-UVOUT,DIST-6426-OUT;n:type:ShaderForge.SFN_TexCoord,id:570,x:31407,y:32409,varname:node_570,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:8531,x:31830,y:32713,ptovrint:False,ptlb:node_8531,ptin:_node_8531,varname:node_8531,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:812ad67abbba1fb4684e1df7f928f734,ntxv:0,isnm:False|UVIN-7670-UVOUT;n:type:ShaderForge.SFN_Multiply,id:3863,x:32066,y:32670,varname:node_3863,prsc:2|A-8531-RGB,B-6393-OUT,C-3419-OUT;n:type:ShaderForge.SFN_Clamp01,id:8866,x:32247,y:32645,varname:node_8866,prsc:2|IN-3863-OUT;n:type:ShaderForge.SFN_Lerp,id:1687,x:32314,y:32361,varname:node_1687,prsc:2|A-8983-OUT,B-5063-OUT,T-8866-OUT;n:type:ShaderForge.SFN_Fresnel,id:6393,x:31830,y:32899,varname:node_6393,prsc:2|NRM-7976-OUT,EXP-5753-OUT;n:type:ShaderForge.SFN_Slider,id:5753,x:31442,y:33034,ptovrint:False,ptlb:Fresnel Slider,ptin:_FresnelSlider,varname:node_5753,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.60559,max:4;n:type:ShaderForge.SFN_Slider,id:3419,x:31741,y:33127,ptovrint:False,ptlb:Transparent,ptin:_Transparent,varname:node_3419,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Slider,id:8177,x:31741,y:33283,ptovrint:False,ptlb:disssolve amount,ptin:_disssolveamount,varname:node_8177,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_OneMinus,id:2617,x:32185,y:33188,varname:node_2617,prsc:2|IN-8177-OUT;n:type:ShaderForge.SFN_RemapRange,id:2428,x:32359,y:33057,varname:node_2428,prsc:2,frmn:0,frmx:1,tomn:-0.6,tomx:0.6|IN-2617-OUT;n:type:ShaderForge.SFN_Tex2d,id:5238,x:32214,y:33410,ptovrint:False,ptlb:Dissolve Pattern,ptin:_DissolvePattern,varname:node_5238,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:aec5b123dea003743b0e8cbe07b6d75a,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:8083,x:32516,y:33141,varname:node_8083,prsc:2|A-2428-OUT,B-5238-RGB;n:type:ShaderForge.SFN_NormalVector,id:7976,x:31510,y:32852,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:5063,x:32013,y:32350,varname:node_5063,prsc:2|A-2862-RGB,B-3440-OUT;n:type:ShaderForge.SFN_Color,id:2862,x:31714,y:32227,ptovrint:False,ptlb:glowColour,ptin:_glowColour,varname:node_2862,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:3440,x:31856,y:32556,ptovrint:False,ptlb:GlowIntensity,ptin:_GlowIntensity,varname:node_3440,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:2;n:type:ShaderForge.SFN_Clamp01,id:8983,x:32153,y:32206,varname:node_8983,prsc:2|IN-8814-RGB;n:type:ShaderForge.SFN_SceneColor,id:8814,x:31907,y:32059,varname:node_8814,prsc:2;proporder:4866-9488-8531-5753-3419-8177-5238-2862-3440;pass:END;sub:END;*/

Shader "Shader Forge/ForceFieldDome3" {
    Properties {
        _ChangeSpeedVertical ("ChangeSpeedVertical", Range(-1, 1)) = 0
        _ChangeSpeedHorizontal ("ChangeSpeedHorizontal", Range(-1, 1)) = 0
        _node_8531 ("node_8531", 2D) = "white" {}
        _FresnelSlider ("Fresnel Slider", Range(0, 4)) = 2.60559
        _Transparent ("Transparent", Range(0, 2)) = 1
        _disssolveamount ("disssolve amount", Range(0, 1)) = 0
        _DissolvePattern ("Dissolve Pattern", 2D) = "white" {}
        _glowColour ("glowColour", Color) = (1,1,1,1)
        _GlowIntensity ("GlowIntensity", Range(0, 2)) = 1
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
            Cull Front
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
            uniform float _ChangeSpeedVertical;
            uniform float _ChangeSpeedHorizontal;
            uniform sampler2D _node_8531; uniform float4 _node_8531_ST;
            uniform float _FresnelSlider;
            uniform float _Transparent;
            uniform float _disssolveamount;
            uniform sampler2D _DissolvePattern; uniform float4 _DissolvePattern_ST;
            uniform float4 _glowColour;
            uniform float _GlowIntensity;
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
                o.normalDir = UnityObjectToWorldNormal(-v.normal);
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
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float4 _DissolvePattern_var = tex2D(_DissolvePattern,TRANSFORM_TEX(i.uv0, _DissolvePattern));
                clip((((1.0 - _disssolveamount)*1.2+-0.6)+_DissolvePattern_var.rgb) - 0.5);
////// Lighting:
////// Emissive:
                float4 node_1762 = _Time;
                float2 node_7670 = ((i.uv0+(node_1762.g*_ChangeSpeedVertical)*float2(0,1))+(node_1762.g*_ChangeSpeedHorizontal)*float2(1,0));
                float4 _node_8531_var = tex2D(_node_8531,TRANSFORM_TEX(node_7670, _node_8531));
                float3 emissive = lerp(saturate(sceneColor.rgb),(_glowColour.rgb*_GlowIntensity),saturate((_node_8531_var.rgb*pow(1.0-max(0,dot(i.normalDir, viewDirection)),_FresnelSlider)*_Transparent)));
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
            Cull Front
            
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
            uniform float _disssolveamount;
            uniform sampler2D _DissolvePattern; uniform float4 _DissolvePattern_ST;
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
                float4 _DissolvePattern_var = tex2D(_DissolvePattern,TRANSFORM_TEX(i.uv0, _DissolvePattern));
                clip((((1.0 - _disssolveamount)*1.2+-0.6)+_DissolvePattern_var.rgb) - 0.5);
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
            uniform float _ChangeSpeedVertical;
            uniform float _ChangeSpeedHorizontal;
            uniform sampler2D _node_8531; uniform float4 _node_8531_ST;
            uniform float _FresnelSlider;
            uniform float _Transparent;
            uniform float4 _glowColour;
            uniform float _GlowIntensity;
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
                o.normalDir = UnityObjectToWorldNormal(-v.normal);
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
                
                float4 node_1762 = _Time;
                float2 node_7670 = ((i.uv0+(node_1762.g*_ChangeSpeedVertical)*float2(0,1))+(node_1762.g*_ChangeSpeedHorizontal)*float2(1,0));
                float4 _node_8531_var = tex2D(_node_8531,TRANSFORM_TEX(node_7670, _node_8531));
                o.Emission = lerp(saturate(sceneColor.rgb),(_glowColour.rgb*_GlowIntensity),saturate((_node_8531_var.rgb*pow(1.0-max(0,dot(i.normalDir, viewDirection)),_FresnelSlider)*_Transparent)));
                
                float3 diffColor = float3(0,0,0);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0, specColor, specularMonochrome );
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
