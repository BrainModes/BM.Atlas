// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.36 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.36;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:14,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2595,x:35028,y:33032,varname:node_2595,prsc:2|diff-5451-OUT,spec-6406-OUT,gloss-2157-OUT,emission-379-OUT,alpha-5893-OUT,voffset-3053-OUT;n:type:ShaderForge.SFN_Fresnel,id:8605,x:33539,y:32966,varname:node_8605,prsc:2|EXP-7605-OUT;n:type:ShaderForge.SFN_Multiply,id:379,x:34211,y:33006,varname:node_379,prsc:2|A-9212-OUT,B-8605-OUT;n:type:ShaderForge.SFN_Slider,id:7605,x:33002,y:32957,ptovrint:False,ptlb:Fresnel Coverage,ptin:_FresnelCoverage,cmnt:Controls how large the fresnel is from just a hiarline to full coverage,varname:node_7605,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:10,cur:3.099558,max:0;n:type:ShaderForge.SFN_Tex2d,id:2724,x:33371,y:32745,ptovrint:False,ptlb:Fresnel Map,ptin:_FresnelMap,cmnt:Use a tile texture for best results,varname:node_2724,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:2,isnm:False|UVIN-7676-UVOUT;n:type:ShaderForge.SFN_Panner,id:7676,x:33123,y:32609,varname:node_7676,prsc:2,spu:1,spv:1|UVIN-7898-UVOUT,DIST-9800-OUT;n:type:ShaderForge.SFN_TexCoord,id:7898,x:32281,y:33629,varname:node_7898,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ViewVector,id:9142,x:32584,y:32747,varname:node_9142,prsc:2;n:type:ShaderForge.SFN_Dot,id:9800,x:32891,y:32594,varname:node_9800,prsc:2,dt:0|A-9914-OUT,B-9142-OUT;n:type:ShaderForge.SFN_Vector3,id:9914,x:32618,y:32632,varname:node_9914,prsc:2,v1:1,v2:0,v3:0;n:type:ShaderForge.SFN_Noise,id:44,x:33694,y:32760,varname:node_44,prsc:2|XY-822-UVOUT;n:type:ShaderForge.SFN_Blend,id:1712,x:33864,y:32696,varname:node_1712,prsc:2,blmd:10,clmp:True|SRC-2724-RGB,DST-44-OUT;n:type:ShaderForge.SFN_Time,id:7701,x:33043,y:33110,varname:node_7701,prsc:2;n:type:ShaderForge.SFN_Panner,id:822,x:33372,y:33089,varname:node_822,prsc:2,spu:1,spv:1|UVIN-7676-UVOUT,DIST-7701-TSL;n:type:ShaderForge.SFN_Panner,id:3286,x:32658,y:33612,varname:node_3286,prsc:2,spu:0.25,spv:0|UVIN-7898-UVOUT,DIST-9491-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2220,x:32857,y:33623,varname:node_2220,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3286-UVOUT;n:type:ShaderForge.SFN_Frac,id:3989,x:33025,y:33623,varname:node_3989,prsc:2|IN-2220-OUT;n:type:ShaderForge.SFN_Vector1,id:5156,x:33040,y:33777,varname:node_5156,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Subtract,id:1804,x:33224,y:33652,varname:node_1804,prsc:2|A-3989-OUT,B-5156-OUT;n:type:ShaderForge.SFN_Abs,id:7368,x:33357,y:33652,varname:node_7368,prsc:2|IN-1804-OUT;n:type:ShaderForge.SFN_Multiply,id:5037,x:33513,y:33668,varname:node_5037,prsc:2|A-7368-OUT,B-2504-OUT;n:type:ShaderForge.SFN_Vector1,id:2504,x:33370,y:33782,varname:node_2504,prsc:2,v1:2;n:type:ShaderForge.SFN_Power,id:673,x:33725,y:33668,varname:node_673,prsc:2|VAL-5037-OUT,EXP-3953-OUT;n:type:ShaderForge.SFN_NormalVector,id:8744,x:33646,y:34072,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:8747,x:34014,y:33804,varname:node_8747,prsc:2|A-673-OUT,B-4633-OUT,C-8744-OUT;n:type:ShaderForge.SFN_Multiply,id:2801,x:33678,y:33425,varname:node_2801,prsc:2|A-44-OUT,B-4659-OUT;n:type:ShaderForge.SFN_Slider,id:4659,x:33224,y:33464,ptovrint:False,ptlb:Geometry Noise Amount,ptin:_GeometryNoiseAmount,cmnt:Controls how much noise is added to the geometry deformation,varname:node_4659,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.8580906,max:2;n:type:ShaderForge.SFN_Multiply,id:3053,x:33973,y:33515,varname:node_3053,prsc:2|A-2801-OUT,B-8747-OUT;n:type:ShaderForge.SFN_Multiply,id:9491,x:32885,y:33341,varname:node_9491,prsc:2|A-7701-T,B-5510-OUT;n:type:ShaderForge.SFN_Slider,id:5510,x:32287,y:33402,ptovrint:False,ptlb:Geometry Animation Speed,ptin:_GeometryAnimationSpeed,cmnt:Controls the speed of the deformation of the geometry,varname:node_5510,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:15;n:type:ShaderForge.SFN_Multiply,id:9212,x:34088,y:32738,varname:node_9212,prsc:2|A-1712-OUT,B-648-OUT;n:type:ShaderForge.SFN_Slider,id:648,x:33830,y:32598,ptovrint:False,ptlb:Fresnel Opacity,ptin:_FresnelOpacity,cmnt:Changes the opacity of the fresnel,varname:node_648,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3.015251,max:5;n:type:ShaderForge.SFN_Color,id:1861,x:34680,y:32650,ptovrint:False,ptlb:Diffuse Color,ptin:_DiffuseColor,varname:node_1861,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Tex2d,id:1982,x:34680,y:32844,ptovrint:False,ptlb:Main Texture,ptin:_MainTexture,varname:node_1982,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5451,x:34991,y:32717,varname:node_5451,prsc:2|A-1861-RGB,B-1982-RGB;n:type:ShaderForge.SFN_Slider,id:5893,x:34448,y:33267,ptovrint:False,ptlb:Opacity,ptin:_Opacity,cmnt:Controls the opactiy of the diffuse maps while the fresnel is still set to additive,varname:node_5893,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.4707271,max:1;n:type:ShaderForge.SFN_Slider,id:3953,x:33261,y:33979,ptovrint:False,ptlb:Geometry Bulge Shape,ptin:_GeometryBulgeShape,cmnt:Changes shape of deformation from a thin ridge to full coverage of the geometry,varname:node_3953,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3.098212,max:25;n:type:ShaderForge.SFN_Slider,id:4633,x:33275,y:34087,ptovrint:False,ptlb:Geometry Bulge Scale,ptin:_GeometryBulgeScale,cmnt:Changes the scale of the deformation from nothing to totally crazy,varname:node_4633,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1420085,max:2;n:type:ShaderForge.SFN_Slider,id:6406,x:34509,y:33868,ptovrint:False,ptlb:Specularity,ptin:_Specularity,varname:node_6406,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.757207,max:10;n:type:ShaderForge.SFN_Slider,id:2157,x:34509,y:33963,ptovrint:False,ptlb:Gloss,ptin:_Gloss,varname:node_2157,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:2,cur:0.3738189,max:0;n:type:ShaderForge.SFN_E,id:1979,x:32501,y:33960,varname:node_1979,prsc:2;proporder:1861-1982-6406-2157-5893-2724-7605-648-4659-5510-3953-4633;pass:END;sub:END;*/

Shader "KunstfagShaders/BrokenHologram" {
    Properties {
        _DiffuseColor ("Diffuse Color", Color) = (0.5,0.5,0.5,1)
        _MainTexture ("Main Texture", 2D) = "white" {}
        _Specularity ("Specularity", Range(0, 10)) = 1.757207
        _Gloss ("Gloss", Range(2, 0)) = 0.3738189
        _Opacity ("Opacity", Range(0, 1)) = 0.4707271
        _FresnelMap ("Fresnel Map", 2D) = "black" {}
        _FresnelCoverage ("Fresnel Coverage", Range(10, 0)) = 3.099558
        _FresnelOpacity ("Fresnel Opacity", Range(0, 5)) = 3.015251
        _GeometryNoiseAmount ("Geometry Noise Amount", Range(0, 2)) = 0.8580906
        _GeometryAnimationSpeed ("Geometry Animation Speed", Range(0, 15)) = 0
        _GeometryBulgeShape ("Geometry Bulge Shape", Range(0, 25)) = 3.098212
        _GeometryBulgeScale ("Geometry Bulge Scale", Range(0, 2)) = 0.1420085
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcAlpha
            ZWrite Off
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float _FresnelCoverage;
            uniform sampler2D _FresnelMap; uniform float4 _FresnelMap_ST;
            uniform float _GeometryNoiseAmount;
            uniform float _GeometryAnimationSpeed;
            uniform float _FresnelOpacity;
            uniform float4 _DiffuseColor;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _Opacity;
            uniform float _GeometryBulgeShape;
            uniform float _GeometryBulgeScale;
            uniform float _Specularity;
            uniform float _Gloss;
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
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
                float4 node_7701 = _Time + _TimeEditor;
                float2 node_7676 = (o.uv0+dot(float3(1,0,0),viewDirection)*float2(1,1));
                float2 node_822 = (node_7676+node_7701.r*float2(1,1));
                float2 node_44_skew = node_822 + 0.2127+node_822.x*0.3713*node_822.y;
                float2 node_44_rnd = 4.789*sin(489.123*(node_44_skew));
                float node_44 = frac(node_44_rnd.x*node_44_rnd.y*(1+node_44_skew.x));
                v.vertex.xyz += ((node_44*_GeometryNoiseAmount)*(pow((abs((frac((o.uv0+(node_7701.g*_GeometryAnimationSpeed)*float2(0.25,0)).r)-0.5))*2.0),_GeometryBulgeShape)*_GeometryBulgeScale*v.normal));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Specularity,_Specularity,_Specularity);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float3 diffuseColor = (_DiffuseColor.rgb*_MainTexture_var.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float2 node_7676 = (i.uv0+dot(float3(1,0,0),viewDirection)*float2(1,1));
                float4 _FresnelMap_var = tex2D(_FresnelMap,TRANSFORM_TEX(node_7676, _FresnelMap)); // Use a tile texture for best results
                float4 node_7701 = _Time + _TimeEditor;
                float2 node_822 = (node_7676+node_7701.r*float2(1,1));
                float2 node_44_skew = node_822 + 0.2127+node_822.x*0.3713*node_822.y;
                float2 node_44_rnd = 4.789*sin(489.123*(node_44_skew));
                float node_44 = frac(node_44_rnd.x*node_44_rnd.y*(1+node_44_skew.x));
                float3 emissive = ((saturate(( node_44 > 0.5 ? (1.0-(1.0-2.0*(node_44-0.5))*(1.0-_FresnelMap_var.rgb)) : (2.0*node_44*_FresnelMap_var.rgb) ))*_FresnelOpacity)*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelCoverage));
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,_Opacity);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform float _FresnelCoverage;
            uniform sampler2D _FresnelMap; uniform float4 _FresnelMap_ST;
            uniform float _GeometryNoiseAmount;
            uniform float _GeometryAnimationSpeed;
            uniform float _FresnelOpacity;
            uniform float4 _DiffuseColor;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _Opacity;
            uniform float _GeometryBulgeShape;
            uniform float _GeometryBulgeScale;
            uniform float _Specularity;
            uniform float _Gloss;
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
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
                float4 node_7701 = _Time + _TimeEditor;
                float2 node_7676 = (o.uv0+dot(float3(1,0,0),viewDirection)*float2(1,1));
                float2 node_822 = (node_7676+node_7701.r*float2(1,1));
                float2 node_44_skew = node_822 + 0.2127+node_822.x*0.3713*node_822.y;
                float2 node_44_rnd = 4.789*sin(489.123*(node_44_skew));
                float node_44 = frac(node_44_rnd.x*node_44_rnd.y*(1+node_44_skew.x));
                v.vertex.xyz += ((node_44*_GeometryNoiseAmount)*(pow((abs((frac((o.uv0+(node_7701.g*_GeometryAnimationSpeed)*float2(0.25,0)).r)-0.5))*2.0),_GeometryBulgeShape)*_GeometryBulgeScale*v.normal));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
				UNITY_LIGHT_ATTENUATION(attenuation, i, i.posWorld.xyz);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Gloss;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Specularity,_Specularity,_Specularity);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _MainTexture_var = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float3 diffuseColor = (_DiffuseColor.rgb*_MainTexture_var.rgb);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * _Opacity,0);
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
            ColorMask RGB
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _GeometryNoiseAmount;
            uniform float _GeometryAnimationSpeed;
            uniform float _GeometryBulgeShape;
            uniform float _GeometryBulgeScale;
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
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
                float4 node_7701 = _Time + _TimeEditor;
                float2 node_7676 = (o.uv0+dot(float3(1,0,0),viewDirection)*float2(1,1));
                float2 node_822 = (node_7676+node_7701.r*float2(1,1));
                float2 node_44_skew = node_822 + 0.2127+node_822.x*0.3713*node_822.y;
                float2 node_44_rnd = 4.789*sin(489.123*(node_44_skew));
                float node_44 = frac(node_44_rnd.x*node_44_rnd.y*(1+node_44_skew.x));
                v.vertex.xyz += ((node_44*_GeometryNoiseAmount)*(pow((abs((frac((o.uv0+(node_7701.g*_GeometryAnimationSpeed)*float2(0.25,0)).r)-0.5))*2.0),_GeometryBulgeShape)*_GeometryBulgeScale*v.normal));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
