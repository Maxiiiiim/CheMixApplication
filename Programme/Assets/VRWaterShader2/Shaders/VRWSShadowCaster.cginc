



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

uniform float _OverallNormalStrength;
UNITY_DECLARE_TEX2D_NOSAMPLER(_OverallNormalStrengthMask); uniform float4 _OverallNormalStrengthMask_ST;
uniform float _OverallWaveHeight;
uniform sampler2D _OverallWaveHeightMask; uniform float4 _OverallWaveHeightMask_ST;

uniform float _TessellationNearCap;
uniform float _TessellationFarCap;
uniform float _TessellationStrength;

uniform float _ShadowWaveFactor;
uniform float _ShadowOpacityRatio;

UNITY_DECLARE_TEX2D(_BaseTexture); uniform float4 _BaseTexture_ST;
uniform float4 _BaseColor;

#include "UnityCG.cginc"
#include "UnityStandardUtils.cginc"

#define _ALPHABLEND_ON

#if (SHADER_TARGET < 30) || defined(SHADER_API_GLES) || defined(SHADER_API_D3D11_9X) || defined (SHADER_API_PSP2)
    #undef UNITY_USE_DITHER_MASK_FOR_ALPHABLENDED_SHADOWS
#endif

#if (defined(_ALPHABLEND_ON) || defined(_ALPHAPREMULTIPLY_ON)) && defined(UNITY_USE_DITHER_MASK_FOR_ALPHABLENDED_SHADOWS)
    #define UNITY_STANDARD_USE_DITHER_MASK 1
#endif

#if defined(_ALPHABLEND_ON) || defined(_ALPHAPREMULTIPLY_ON)
#define UNITY_STANDARD_USE_SHADOW_UVS 1
#endif

#if !defined(V2F_SHADOW_CASTER_NOPOS_IS_EMPTY) || defined(UNITY_STANDARD_USE_SHADOW_UVS)
#define UNITY_STANDARD_USE_SHADOW_OUTPUT_STRUCT 1
#endif

#ifdef UNITY_STANDARD_USE_DITHER_MASK
sampler3D   _DitherMaskLOD;
#endif

struct VertexInput {
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float4 tangent : TANGENT;
    float2 texcoord0 : TEXCOORD0;
    float2 texcoord1 : TEXCOORD1;
    float2 texcoord2 : TEXCOORD2;
};
struct VertexOutput {
    V2F_SHADOW_CASTER;
    float2 uv0 : TEXCOORD1;
    float2 uv1 : TEXCOORD2;
    float2 uv2 : TEXCOORD3;
    float4 posWorld : TEXCOORD4;
    float3 normalDir : TEXCOORD5;
};
VertexOutput vert (VertexInput v) {
    VertexOutput o = (VertexOutput)0;
    o.uv0 = v.texcoord0;
    o.uv1 = v.texcoord1;
    o.uv2 = v.texcoord2;
    o.normalDir = UnityObjectToWorldNormal(v.normal);
    o.posWorld = mul(unity_ObjectToWorld, v.vertex);
    o.pos = UnityObjectToClipPos( v.vertex );
    TRANSFER_SHADOW_CASTER(o)
    return o;
}
// テッセレーション
#include "VRWSTess.cginc"
float4 frag(VertexOutput i) : COLOR {
    i.normalDir = normalize(i.normalDir);
    float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
    float3 normalDirection = i.normalDir;

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

    #if defined(UNITY_STANDARD_USE_SHADOW_UVS)
        float4 _BaseTexture_var = UNITY_SAMPLE_TEX2D(_BaseTexture, TRANSFORM_TEX(i.uv0, _BaseTexture));

        half waveFactor = 1 - dot(float3(0,0,1), normalize(normalLocal)) * _ShadowWaveFactor;
        half alphaWave = saturate(waveFactor + (_ShadowWaveFactor-1)) ;
        half alpha = _BaseTexture_var.a*_BaseColor.a*(1-alphaWave) * _ShadowOpacityRatio;

        #if defined(_ALPHABLEND_ON) || defined(_ALPHAPREMULTIPLY_ON)
            #if defined(UNITY_STANDARD_USE_DITHER_MASK)
                half alphaRef = tex3D(_DitherMaskLOD, float3(i.pos.xy*0.25,alpha*0.9375)).a;
                clip (alphaRef - 0.01);
            #else
                clip (alpha - _CutoutCutoutAdjust);
            #endif
        #endif
    #endif
    SHADOW_CASTER_FRAGMENT(i)
}