// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32953,y:32721,varname:node_3138,prsc:2|emission-4749-OUT,alpha-6399-OUT;n:type:ShaderForge.SFN_Tex2d,id:2994,x:31550,y:32971,varname:node_2994,prsc:2,tex:39e835a91e8cea347884a2289f537917,ntxv:0,isnm:False|UVIN-7665-UVOUT,TEX-281-TEX;n:type:ShaderForge.SFN_Parallax,id:7439,x:31920,y:32861,varname:node_7439,prsc:2|UVIN-4608-UVOUT,HEI-6310-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:281,x:31280,y:33239,ptovrint:False,ptlb:fog texture,ptin:_fogtexture,varname:node_281,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:39e835a91e8cea347884a2289f537917,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1166,x:32142,y:32987,cmnt:texture,varname:_node_2994_copy,prsc:2,tex:39e835a91e8cea347884a2289f537917,ntxv:0,isnm:False|UVIN-7439-UVOUT,TEX-281-TEX;n:type:ShaderForge.SFN_Multiply,id:6310,x:31729,y:32918,varname:node_6310,prsc:2|A-6429-OUT,B-2994-R;n:type:ShaderForge.SFN_ValueProperty,id:6429,x:31550,y:32903,ptovrint:False,ptlb:depth,ptin:_depth,varname:node_6429,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_RemapRange,id:5618,x:32527,y:32905,varname:node_5618,prsc:2,frmn:0,frmx:1,tomn:0.2,tomx:1|IN-1166-B;n:type:ShaderForge.SFN_Panner,id:4608,x:31209,y:32827,cmnt:texture speed,varname:node_4608,prsc:2,spu:0.02,spv:0|UVIN-3060-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:3060,x:30961,y:32905,varname:node_3060,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:8719,x:32138,y:33292,cmnt:circle mask,varname:node_8719,prsc:2,tex:39e835a91e8cea347884a2289f537917,ntxv:0,isnm:False|TEX-281-TEX;n:type:ShaderForge.SFN_Panner,id:7665,x:31209,y:33009,cmnt:parallax speed,varname:node_7665,prsc:2,spu:0.05,spv:0|UVIN-3060-UVOUT;n:type:ShaderForge.SFN_Color,id:9254,x:32527,y:32682,ptovrint:False,ptlb:node_9254,ptin:_node_9254,varname:node_9254,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:4749,x:32739,y:32789,varname:node_4749,prsc:2|A-9254-RGB,B-5618-OUT;n:type:ShaderForge.SFN_Slider,id:8273,x:32367,y:33485,ptovrint:False,ptlb:transperent,ptin:_transperent,varname:node_8273,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:6399,x:32511,y:33273,varname:node_6399,prsc:2|A-8719-G,B-8273-OUT;proporder:281-6429-9254-8273;pass:END;sub:END;*/

Shader "Shader Forge/fog_shader" {
    Properties {
        _fogtexture ("fog texture", 2D) = "white" {}
        _depth ("depth", Float ) = 0
        _node_9254 ("node_9254", Color) = (1,1,1,1)
        _transperent ("transperent", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _fogtexture; uniform float4 _fogtexture_ST;
            uniform float _depth;
            uniform float4 _node_9254;
            uniform float _transperent;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_3319 = _Time;
                float2 node_7665 = (i.uv0+node_3319.g*float2(0.05,0)); // parallax speed
                float4 node_2994 = tex2D(_fogtexture,TRANSFORM_TEX(node_7665, _fogtexture));
                float2 node_7439 = (0.05*((_depth*node_2994.r) - 0.5)*mul(tangentTransform, viewDirection).xy + (i.uv0+node_3319.g*float2(0.02,0)));
                float4 _node_2994_copy = tex2D(_fogtexture,TRANSFORM_TEX(node_7439.rg, _fogtexture)); // texture
                float3 emissive = (_node_9254.rgb*(_node_2994_copy.b*0.8+0.2));
                float3 finalColor = emissive;
                float4 node_8719 = tex2D(_fogtexture,TRANSFORM_TEX(i.uv0, _fogtexture)); // circle mask
                return fixed4(finalColor,(node_8719.g*_transperent));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
