// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33829,y:32473,varname:node_3138,prsc:2|emission-7524-OUT,clip-3264-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32988,y:32525,ptovrint:False,ptlb:color,ptin:_color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3985186,c2:0.7838622,c3:0.8602941,c4:1;n:type:ShaderForge.SFN_Tex2dAsset,id:4053,x:32242,y:32879,ptovrint:False,ptlb:cloud_txtr,ptin:_cloud_txtr,varname:node_4053,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8747,x:32502,y:33074,varname:node_8747,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-8198-UVOUT,TEX-4053-TEX;n:type:ShaderForge.SFN_Tex2d,id:233,x:32504,y:32674,varname:node_233,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-7731-UVOUT,TEX-4053-TEX;n:type:ShaderForge.SFN_TexCoord,id:7195,x:31587,y:32745,varname:node_7195,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:7731,x:32018,y:32663,varname:node_7731,prsc:2,spu:1.2,spv:1.6|UVIN-7195-UVOUT,DIST-3010-OUT;n:type:ShaderForge.SFN_Panner,id:8198,x:32022,y:33096,varname:node_8198,prsc:2,spu:-0.2,spv:-0.7|UVIN-7195-UVOUT,DIST-3010-OUT;n:type:ShaderForge.SFN_Blend,id:1430,x:32722,y:32878,varname:node_1430,prsc:2,blmd:17,clmp:True|SRC-233-R,DST-8747-R;n:type:ShaderForge.SFN_OneMinus,id:5292,x:32890,y:32878,varname:node_5292,prsc:2|IN-1430-OUT;n:type:ShaderForge.SFN_RemapRange,id:8036,x:33061,y:32878,varname:node_8036,prsc:2,frmn:0,frmx:1,tomn:-15,tomx:1|IN-5292-OUT;n:type:ShaderForge.SFN_Slider,id:7843,x:31246,y:33069,ptovrint:False,ptlb:time_slider,ptin:_time_slider,varname:node_7843,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.6153846,max:2;n:type:ShaderForge.SFN_Time,id:2232,x:31325,y:32921,varname:node_2232,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3010,x:31587,y:32961,varname:node_3010,prsc:2|A-2232-T,B-7843-OUT;n:type:ShaderForge.SFN_Fresnel,id:5190,x:33254,y:32741,varname:node_5190,prsc:2|EXP-9461-OUT;n:type:ShaderForge.SFN_Vector1,id:9461,x:33254,y:32662,varname:node_9461,prsc:2,v1:2;n:type:ShaderForge.SFN_Clamp01,id:3970,x:33254,y:32878,varname:node_3970,prsc:2|IN-8036-OUT;n:type:ShaderForge.SFN_Add,id:3264,x:33489,y:32806,varname:node_3264,prsc:2|A-5190-OUT,B-3970-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3086,x:32988,y:32345,ptovrint:False,ptlb:bright,ptin:_bright,varname:node_3086,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:7524,x:33254,y:32413,varname:node_7524,prsc:2|A-3086-OUT,B-7241-RGB;proporder:7241-4053-7843-3086;pass:END;sub:END;*/

Shader "Shader Forge/emp_shader" {
    Properties {
        _color ("color", Color) = (0.3985186,0.7838622,0.8602941,1)
        _cloud_txtr ("cloud_txtr", 2D) = "white" {}
        _time_slider ("time_slider", Range(0, 2)) = 0.6153846
        _bright ("bright", Float ) = 2
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _color;
            uniform sampler2D _cloud_txtr; uniform float4 _cloud_txtr_ST;
            uniform float _time_slider;
            uniform float _bright;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 node_2232 = _Time;
                float node_3010 = (node_2232.g*_time_slider);
                float2 node_7731 = (i.uv0+node_3010*float2(1.2,1.6));
                float4 node_233 = tex2D(_cloud_txtr,TRANSFORM_TEX(node_7731, _cloud_txtr));
                float2 node_8198 = (i.uv0+node_3010*float2(-0.2,-0.7));
                float4 node_8747 = tex2D(_cloud_txtr,TRANSFORM_TEX(node_8198, _cloud_txtr));
                clip((pow(1.0-max(0,dot(normalDirection, viewDirection)),2.0)+saturate(((1.0 - saturate(abs(node_233.r-node_8747.r)))*16.0+-15.0))) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (_bright*_color.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _cloud_txtr; uniform float4 _cloud_txtr_ST;
            uniform float _time_slider;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float4 node_2232 = _Time;
                float node_3010 = (node_2232.g*_time_slider);
                float2 node_7731 = (i.uv0+node_3010*float2(1.2,1.6));
                float4 node_233 = tex2D(_cloud_txtr,TRANSFORM_TEX(node_7731, _cloud_txtr));
                float2 node_8198 = (i.uv0+node_3010*float2(-0.2,-0.7));
                float4 node_8747 = tex2D(_cloud_txtr,TRANSFORM_TEX(node_8198, _cloud_txtr));
                clip((pow(1.0-max(0,dot(normalDirection, viewDirection)),2.0)+saturate(((1.0 - saturate(abs(node_233.r-node_8747.r)))*16.0+-15.0))) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
