uniform sampler2D _GrabTexture;

uniform sampler2D _CameraDepthTexture;

UNITY_DECLARE_TEX2D(_BaseTexture); uniform float4 _BaseTexture_ST;
uniform float4 _BaseColor;

uniform sampler2D _EmissionTexture; uniform float4 _EmissionTexture_ST;
uniform float4 _EmissionColor;

uniform sampler2D _FirstWaveHeightmap;
uniform sampler2D _FirstWaveNormalmap; uniform float4 _FirstWaveNormalmap_ST;
uniform float4 _FirstWaveUandVspeed;
uniform float _FirstWaveNormalStrength;
uniform float _FirstWaveHeightStrength;


uniform fixed _SecondWaveEnableThis;
uniform sampler2D _SecondWaveHeightmap;
uniform sampler2D _SecondWaveNormalmap; uniform float4 _SecondWaveNormalmap_ST;
uniform float4 _SecondWaveUandVspeed;
uniform float _SecondWaveNormalStrength;
uniform float _SecondWaveHeightStrength;

uniform fixed _ThirdWaveEnableThis;
uniform sampler2D _ThirdWaveHeightmap;
uniform sampler2D _ThirdWaveNormalmap; uniform float4 _ThirdWaveNormalmap_ST;
uniform float4 _ThirdWaveUandVspeed;
uniform float _ThirdWaveNormalStrength;
uniform float _ThirdWaveHeightStrength;

uniform fixed _FourthWaveEnableThis;
uniform sampler2D _FourthWaveHeightmap;
uniform sampler2D _FourthWaveNormalmap; uniform float4 _FourthWaveNormalmap_ST;
uniform float4 _FourthWaveUandVspeed;
uniform float _FourthWaveNormalStrength;
uniform float _FourthWaveHeightStrength;

uniform float _OverallMetallic;
UNITY_DECLARE_TEX2D_NOSAMPLER(_OverallMetallicMask); uniform float4 _OverallMetallicMask_ST;
uniform float _OverallGloss;
UNITY_DECLARE_TEX2D_NOSAMPLER(_OverallGlossMask); uniform float4 _OverallGlossMask_ST;
uniform float _OverallNormalStrength;
UNITY_DECLARE_TEX2D_NOSAMPLER(_OverallNormalStrengthMask); uniform float4 _OverallNormalStrengthMask_ST;
uniform float _OverallWaveHeight;
uniform sampler2D _OverallWaveHeightMask; uniform float4 _OverallWaveHeightMask_ST;

uniform fixed _CubemapUseCustomCubemap;
uniform samplerCUBE _CubemapCustomCubemap;
uniform float4 _CubemapBaseColor;
uniform float _CubemapGlossiness;
uniform float _CubemapStrength;
uniform float _CubemapFresnel;

uniform float _TessellationNearCap;
uniform float _TessellationFarCap;
uniform float _TessellationStrength;

//[Toggle(BLEND_ENABLED)]
uniform float _BlendBlendDistance;
uniform float _BlendBlendAlpha;
//[Toggle(BLEND_COLOR_ENABLED)]
uniform float4 _BlendColor;
uniform sampler2D _BlendTexture; uniform float4 _BlendTexture_ST;
uniform float4 _BlendTextureUandVspeedZWNotused;
uniform float _BlendEmissionFactor;

uniform float _SSRefractionDecaydistance;
uniform float _SSRefractionRefractionFresnel;
uniform float _SSRefractionStrength;
UNITY_DECLARE_TEX2D_NOSAMPLER(_SSRefractionStrengthMask); uniform float4 _SSRefractionStrengthMask_ST;

float3 EnvironmentReflection( float3 Dir , float Glossiness ){
    // Glossiness
    half mip = (1.0 - Glossiness) * UNITY_SPECCUBE_LOD_STEPS;
    // sample the default reflection cubemap, using the reflection vector
    half4 skyData = UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0, Dir, mip);
    // decode cubemap data into actual color
    half3 skyColor = DecodeHDR (skyData, unity_SpecCube0_HDR);
    return skyColor;
}

struct VertexInput {
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float4 tangent : TANGENT;
    float2 texcoord0 : TEXCOORD0;
    float2 texcoord1 : TEXCOORD1;
    float2 texcoord2 : TEXCOORD2;
};

struct VertexOutput {
    float4 pos : SV_POSITION;
    float2 uv0 : TEXCOORD0;
    float2 uv1 : TEXCOORD1;
    float2 uv2 : TEXCOORD2;
    float4 posWorld : TEXCOORD3;
    float3 normalDir : TEXCOORD4;
    float3 tangentDir : TEXCOORD5;
    float3 bitangentDir : TEXCOORD6;
    float4 projPos : TEXCOORD7;
    #ifdef VRWS_BASE
        UNITY_FOG_COORDS(8)
        #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
            float4 ambientOrLightmapUV : TEXCOORD9;
        #endif
        half3 ambient : TEXCOORD10;
    #else
        LIGHTING_COORDS(8,9)
        UNITY_FOG_COORDS(10)
    #endif
};

VertexOutput vert (VertexInput v) {
    VertexOutput o = (VertexOutput)0;
    o.uv0 = v.texcoord0;
    o.uv1 = v.texcoord1;
    o.uv2 = v.texcoord2;
    #ifdef VRWS_BASE
        #ifdef LIGHTMAP_ON
            o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
            o.ambientOrLightmapUV.zw = 0;
        #elif UNITY_SHOULD_SAMPLE_SH
        #endif
        #ifdef DYNAMICLIGHTMAP_ON
            o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
        #endif
    #endif
    o.normalDir = UnityObjectToWorldNormal(v.normal);
    o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
    o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
    o.posWorld = mul(unity_ObjectToWorld, v.vertex);
    float3 lightColor = _LightColor0.rgb;
    o.pos = UnityObjectToClipPos( v.vertex );
    UNITY_TRANSFER_FOG(o,o.pos);
    o.projPos = ComputeScreenPos (o.pos);
    COMPUTE_EYEDEPTH(o.projPos.z);
    #ifdef VRWS_BASE
        o.ambient = Shade4PointLights(
                            unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
                            unity_LightColor[0].rgb, unity_LightColor[1].rgb,
                            unity_LightColor[2].rgb, unity_LightColor[3].rgb,
                            unity_4LightAtten0, o.posWorld, o.normalDir
        );
    #endif
    #ifdef VRWS_ADD
        TRANSFER_VERTEX_TO_FRAGMENT(o)
    #endif
    return o;
}

// テッセレーション
#include "VRWSTess.cginc"

float4 frag(VertexOutput i) : COLOR {
    i.normalDir = normalize(i.normalDir);
    float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
    float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);

////// Lighting:
    #ifdef VRWS_BASE
        float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
    #else
        float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
    #endif

    float3 lightColor = _LightColor0.rgb;
    float3 halfDirection = normalize(viewDirection+lightDirection);

    #ifdef VRWS_BASE
        float attenuation = 1;
        float3 attenColor = attenuation * lightColor + i.ambient;
    #else
        UNITY_LIGHT_ATTENUATION(attenuation,i, i.posWorld.xyz);
        float3 attenColor = attenuation * lightColor;
    #endif

////// Wave:
    float time = _Time.g;
    float3 texturedNormal = float3(0,0,0);

    // FIRST
    float2 first_uv_pos = (i.uv0+(float2(_FirstWaveUandVspeed.r,_FirstWaveUandVspeed.g)*time));
    float3 _FirstWaveNormalmap_var = UnpackNormal(tex2D(_FirstWaveNormalmap,TRANSFORM_TEX(first_uv_pos, _FirstWaveNormalmap)));
    texturedNormal = _FirstWaveNormalmap_var.rgb * _FirstWaveNormalStrength;

    // SECOND
    #ifdef SECOND_ENABLED
        float2 second_uv_pos = (i.uv0+(float2(_SecondWaveUandVspeed.r,_SecondWaveUandVspeed.g)*time));
        float3 _SecondWaveNormalmap_var = UnpackNormal(tex2D(_SecondWaveNormalmap,TRANSFORM_TEX(second_uv_pos, _SecondWaveNormalmap)));
        texturedNormal += _SecondWaveNormalmap_var.rgb * _SecondWaveNormalStrength;
    #endif

    // THIRD
    #ifdef THIRD_ENABLED
        float2 third_uv_pos = (i.uv0+(float2(_ThirdWaveUandVspeed.r,_ThirdWaveUandVspeed.g)*time));
        float3 _ThirdWaveNormalmap_var = UnpackNormal(tex2D(_ThirdWaveNormalmap,TRANSFORM_TEX(third_uv_pos, _ThirdWaveNormalmap)));
        texturedNormal += _ThirdWaveNormalmap_var.rgb * _ThirdWaveNormalStrength;
    #endif

    // FOURTH
    #ifdef FOURTH_ENABLED
        float2 fourth_uv_pos = (i.uv0+(float2(_FourthWaveUandVspeed.r,_FourthWaveUandVspeed.g)*time));
        float3 _FourthWaveNormalmap_var = UnpackNormal(tex2D(_FourthWaveNormalmap,TRANSFORM_TEX(fourth_uv_pos, _FourthWaveNormalmap)));
        texturedNormal += _FourthWaveNormalmap_var.rgb * _FourthWaveNormalStrength;
    #endif


    float _OverallNormalStrengthMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_OverallNormalStrengthMask, _BaseTexture, TRANSFORM_TEX(i.uv0, _OverallNormalStrengthMask)).r;
    float _OverallNormalStrength_var = _OverallNormalStrength *  _OverallNormalStrengthMask_var;

    float3 normalLocal = float3( (texturedNormal.rg * _OverallNormalStrength_var), 1.0);
    float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals

    #ifdef VRWS_BASE
        float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
    #endif

    // 境界ブレンドが有効である場合は、濃さ
    #ifdef BLEND_ENABLED
        float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
        float partZ = max(0,i.projPos.z - _ProjectionParams.g);
        float2 blend_uv_pos = (i.uv0+(float2(_BlendTextureUandVspeedZWNotused.r,_BlendTextureUandVspeedZWNotused.g)*time));
        float4 _BlendTexture_var = tex2D(_BlendTexture,TRANSFORM_TEX(blend_uv_pos, _BlendTexture));
        float3 _BlendColor_var = (_BlendColor.rgb*_BlendTexture_var.rgb);
        float _BlendOpacity = lerp(_BlendBlendAlpha, 1.0, saturate((sceneZ-partZ)/_BlendBlendDistance));
        float _BlendColorFactor = lerp(0, 1.0, saturate((sceneZ-partZ)/_BlendBlendDistance));
    #else
        float _BlendOpacity = 1.0;
    #endif

    //屈折が反映されたGrabbedTexture
    float _SSRefractionStrengthMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_SSRefractionStrengthMask, _BaseTexture, TRANSFORM_TEX(i.uv0, _SSRefractionStrengthMask)).r;
    float _SSRefractionStrength_var = _SSRefractionStrength * _SSRefractionStrengthMask_var;

    float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (mul( UNITY_MATRIX_V, float4((pow(1.0-max(0,dot(normalDirection, viewDirection)),_SSRefractionRefractionFresnel)*i.normalDir*_SSRefractionStrength_var*saturate(((distance(i.posWorld.rgb,_WorldSpaceCameraPos)-_SSRefractionDecaydistance)/(0.0-_SSRefractionDecaydistance)))*(-1.0)),0) ).xyz.rgb.rg*_BlendOpacity);
    float4 sceneColor = tex2D(_GrabTexture, sceneUVs);

    float _OverallGlossMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_OverallGlossMask, _BaseTexture, TRANSFORM_TEX(i.uv0, _OverallGlossMask)).r;
    float _OverallGloss_var = _OverallGloss * _OverallGlossMask_var;

/////// GI Data:
    #ifdef VRWS_BASE
        UnityLight light;
        #ifdef LIGHTMAP_OFF
            light.color = lightColor;
            light.dir = lightDirection;
            light.ndotl = LambertTerm (normalDirection, light.dir);
        #else
            light.color = half3(0.f, 0.f, 0.f);
            light.ndotl = 0.0f;
            light.dir = half3(0.f, 0.f, 0.f);
        #endif
        UnityGIInput d;
        d.light = light;
        d.worldPos = i.posWorld.xyz;
        d.worldViewDir = viewDirection;
        d.atten = attenuation;
        #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
            d.ambient = 0;
            d.lightmapUV = i.ambientOrLightmapUV;
        #else
            d.ambient = i.ambientOrLightmapUV;
        #endif
        #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
            d.boxMin[0] = unity_SpecCube0_BoxMin;
            d.boxMin[1] = unity_SpecCube1_BoxMin;
        #endif
        #if UNITY_SPECCUBE_BOX_PROJECTION
            d.boxMax[0] = unity_SpecCube0_BoxMax;
            d.boxMax[1] = unity_SpecCube1_BoxMax;
            d.probePosition[0] = unity_SpecCube0_ProbePosition;
            d.probePosition[1] = unity_SpecCube1_ProbePosition;
        #endif
        d.probeHDR[0] = unity_SpecCube0_HDR;
        d.probeHDR[1] = unity_SpecCube1_HDR;
        Unity_GlossyEnvironmentData ugls_en_data;
        ugls_en_data.roughness = 1.0 - _OverallGloss_var;
        ugls_en_data.reflUVW = viewReflectDirection;
        UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
        lightDirection = gi.light.dir;
        lightColor = gi.light.color;
    #endif

///////// Gloss:
    float perceptualRoughness = 1.0 - _OverallGloss_var;
    float roughness = perceptualRoughness * perceptualRoughness;
    float specPow = exp2( _OverallGloss_var * 10.0 + 1.0 );

////// Specular:
    float NdotL = saturate(dot( normalDirection, lightDirection ));
    float LdotH = saturate(dot(lightDirection, halfDirection));
    float NdotV = abs(dot( normalDirection, viewDirection ));
    float NdotH = saturate(dot( normalDirection, halfDirection ));
    float VdotH = saturate(dot( viewDirection, halfDirection ));

    float _OverallMetallicMask_var = UNITY_SAMPLE_TEX2D_SAMPLER(_OverallMetallicMask, _BaseTexture, TRANSFORM_TEX(i.uv0, _OverallMetallicMask)).r;
    float3 specularColor = (_OverallMetallic*_OverallMetallicMask_var*_BlendOpacity);

    float specularMonochrome;
    float4 _BaseTexture_var = UNITY_SAMPLE_TEX2D(_BaseTexture, TRANSFORM_TEX(i.uv0, _BaseTexture));

    float3 _BaseColor_var = (_BaseColor.rgb*_BaseTexture_var.rgb);
    #if defined(BLEND_ENABLED) && defined(BLEND_COLOR_ENABLED)
        #ifdef BLEND_ADDITIVE
            float3 diffuseColor = _BaseColor_var + _BlendColor_var.rgb * (1-_BlendColorFactor);
        #else
            float3 diffuseColor = lerp(_BlendColor_var.rgb, _BaseColor_var, _BlendColorFactor);
        #endif
    #else
        float3 diffuseColor = _BaseColor_var;
    #endif
    diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
    specularMonochrome = 1.0-specularMonochrome;
    float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
    float normTerm = GGXTerm(NdotH, roughness);
    float specularPBL = (visTerm*normTerm) * UNITY_PI;
    #ifdef UNITY_COLORSPACE_GAMMA
        specularPBL = sqrt(max(1e-4h, specularPBL));
    #endif
    specularPBL = max(0, specularPBL * NdotL);
    #if defined(_SPECULARHIGHLIGHTS_OFF)
        specularPBL = 0.0;
    #endif

    specularPBL *= any(specularColor) ? 1.0 : 0.0;
    float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);

    #ifdef VRWS_BASE
        half surfaceReduction;
        #ifdef UNITY_COLORSPACE_GAMMA
            surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
        #else
            surfaceReduction = 1.0/(roughness*roughness + 1.0);
        #endif
        half grazingTerm = saturate( _OverallGloss + specularMonochrome );
        float4 _CubemapCustomCubemap_var = texCUBElod(_CubemapCustomCubemap,float4(viewReflectDirection,_CubemapGlossiness));
        float3 indirectSpecular = (gi.indirect.specular);
        #ifdef CUSTOM_CUBEMAP_ENABLED
            indirectSpecular +=  (_CubemapBaseColor.rgb*_CubemapCustomCubemap_var.rgb)            *_CubemapCustomCubemap_var.a * _CubemapStrength*pow(1.0-max(0,dot(normalDirection, viewDirection)),_CubemapFresnel) * 5.0;
        #else
            indirectSpecular +=  EnvironmentReflection( viewReflectDirection , _CubemapGlossiness )                             * _CubemapStrength*pow(1.0-max(0,dot(normalDirection, viewDirection)),_CubemapFresnel) * 5.0;
        #endif
        indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
        indirectSpecular *= surfaceReduction;
        float3 specular = directSpecular + indirectSpecular;
    #else
        float3 specular = directSpecular;
    #endif

/////// Diffuse:
    half fd90 = 0.5 + 2 * LdotH * LdotH * (1-_OverallGloss);
    float nlPow5 = Pow5(1-NdotL);
    float nvPow5 = Pow5(1-NdotV);
    float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;

    #ifdef VRWS_BASE
        float3 indirectDiffuse = float3(0,0,0);
        indirectDiffuse += gi.indirect.diffuse;
        float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
    #else
        float3 diffuse = directDiffuse * diffuseColor;
    #endif

////// Emissive:
    #ifdef VRWS_BASE
        float4 _EmissionTexture_var = tex2D(_EmissionTexture,TRANSFORM_TEX(i.uv0, _EmissionTexture));
        float3 _EmissionColor_var = (_EmissionColor.rgb*_EmissionTexture_var.rgb);
        #if defined(BLEND_ENABLED)
            #ifdef BLEND_COLOR_ENABLED
                float3 blendEmissiveColor = _BlendColor_var.rgb * _BlendEmissionFactor;
                float3 emissive = lerp(blendEmissiveColor,_EmissionColor_var, _BlendOpacity);
            #else
                float3 emissive = lerp(float3(0,0,0),_EmissionColor_var, _BlendOpacity);
            #endif
        #else
            float3 emissive = _EmissionColor_var;
        #endif
    #endif

/// Final Color:
    #ifdef VRWS_BASE
        float finalalpha = (_BaseColor.a * _BaseTexture_var.a);
        float3 finalColor = diffuse * finalalpha + specular + emissive;
        fixed4 finalRGBA = fixed4(lerp( sceneColor.rgb, finalColor, finalalpha*_BlendOpacity),1);
    #else
        float finalalpha = (_BaseColor.a * _BaseTexture_var.a);
        float3 finalColor = diffuse * finalalpha * _BlendOpacity + specular;
        fixed4 finalRGBA = fixed4(finalColor,0);
    #endif

    UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
    return finalRGBA;
}