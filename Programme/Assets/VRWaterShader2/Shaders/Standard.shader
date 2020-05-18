// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "VRWaterShader2/Standard" {
    Properties {
        // Culling, ZWrite
        [Enum(Off, 0, On, 1)]_ZWrite("ZWrite", Float) = 0
        [KeywordEnum(None, Front, Back)] _Cull("Culling", Int) = 2

        // Base Color, Texture
        _BaseTexture ("[Base] Texture", 2D) = "white" {}
        _BaseColor ("[Base] Color", Color) = (0.3,0.5,0.9,0.5)

        // Emission
        _EmissionTexture ("[Emission] Texture", 2D) = "white" {}
        _EmissionColor ("[Emission] Texture", Color) = (0.0, 0.0, 0.0, 1.0)

        // First wave map
        _FirstWaveHeightmap ("[First] Heightmap", 2D) = "black" {}
        _FirstWaveNormalmap ("[First] Normalmap", 2D) = "bump" {}
        _FirstWaveNormalStrength ("[First] Normal Strength", Range(-1, 1)) = 0.5
        _FirstWaveHeightStrength ("[First] Height Strength", Range(-1, 1)) = 0.5
        _FirstWaveUandVspeed ("[First] U and V speed (Z W Not used)", Vector) = (0.1,0.1,0,0)

        // Second wave map
        [Toggle(SECOND_ENABLED)] _SecondWaveEnableThis ("[Second] Enable This?", Float ) = 1
        _SecondWaveHeightmap ("[Second] Heightmap", 2D) = "black" {}
        _SecondWaveNormalmap ("[Second] Normalmap", 2D) = "bump" {}
        _SecondWaveNormalStrength ("[Second] Normal Strength", Range(-1, 1)) = 0.5
        _SecondWaveHeightStrength ("[Second] Height Strength", Range(-1, 1)) = 0.5
        _SecondWaveUandVspeed ("[Second] U and V speed (Z W Not used)", Vector) = (-0.05,-0.075,0,0)

        // Third wave map
        [Toggle(THIRD_ENABLED)] _ThirdWaveEnableThis ("[Third] Enable This?", Float ) = 1
        _ThirdWaveHeightmap ("[Third] Heightmap", 2D) = "black" {}
        _ThirdWaveNormalmap ("[Third] Normalmap", 2D) = "bump" {}
        _ThirdWaveNormalStrength ("[Second] Normal Strength", Range(-1, 1)) = 0.5
        _ThirdWaveHeightStrength ("[Third] Height Strength", Range(-1, 1)) = 0.5
        _ThirdWaveUandVspeed ("[Third] U and V speed (Z W Not used)", Vector) = (-0.05,-0.075,0,0)

        // Fourth wave map
        [Toggle(FOURTH_ENABLED)] _FourthWaveEnableThis ("[Fourth] Enable This?", Float ) = 1
        _FourthWaveHeightmap ("[Fourth] Heightmap", 2D) = "black" {}
        _FourthWaveNormalmap ("[Fourth] Normalmap", 2D) = "bump" {}
        _FourthWaveNormalStrength ("[Second] Normal Strength", Range(-1, 1)) = 0.5
        _FourthWaveHeightStrength ("[Fourth] Height Strength", Range(-1, 1)) = 0.5
        _FourthWaveUandVspeed ("[Fourth] U and V speed (Z W Not used)", Vector) = (-0.05,-0.075,0,0)

        // Cubemap Reflection
        [Toggle(CUSTOM_CUBEMAP_ENABLED)] _CubemapUseCustomCubemap ("[Cubemap] Use Custom Cubemap", Float ) = 0
        _CubemapBaseColor ("[Cubemap] Base Color", Color) = (1,1,1,1)
        _CubemapCustomCubemap ("[Cubemap] Custom Cubemap", Cube) = "_Skybox" {}
        _CubemapGlossiness ("[Cubemap] Glossiness", Range(0, 1)) = 1
        _CubemapStrength ("[Cubemap] Strength", Range(0, 1)) = 0.4807122
        _CubemapFresnel ("[Cubemap] Fresnel", Range(0, 20)) = 4.316328

        // Depth Blending
        [Toggle(BLEND_ENABLED)] _BlendEnableThis ("[Blend] Enable This?", Float ) = 1
        _BlendBlendDistance ("[Blend] Blend Distance", Range(0, 10)) = 0.6824926
        _BlendBlendAlpha ("[Blend] Blend Alpha", Range(0, 1)) = 0.5400593
        [Toggle(BLEND_COLOR_ENABLED)] _BlendEnableColor ("[Blend] Enable This?", Float ) = 0
        [Toggle(BLEND_ADDITIVE)] _BlendIsAdditive ("[Blend] Is Additive Blend", Float ) = 0
        _BlendColor ("[Blend] Color", Color) = (1.0,1.0,1.0,1.0)
        _BlendTexture ("[Blend] Texture", 2D) = "white" {}
        _BlendTextureUandVspeedZWNotused ("[Fourth] U and V speed (Z W Not used)", Vector) = (-0.05,-0.075,0,0)
        _BlendEmissionFactor ("[Blend] Emission value", Range(0, 1)) = 0.0

        // Tessellation
        _TessellationStrength ("[Tessellation] Strength", Range(0, 100)) = 2.48368
        _TessellationNearCap ("[Tessellation] Near Cap", Float ) = 0
        _TessellationFarCap ("[Tessellation] Far Cap", Float ) = 20

        // PBR
        _OverallMetallic ("[Overall] Metallic", Range(0, 1)) = 0.9128559
        _OverallMetallicMask ("[Overall] Metallic Mask", 2D) = "white" {}
        _OverallGloss ("[Overall] Gloss", Range(0, 1)) = 0.796635
        _OverallGlossMask ("[Overall] Gloss Mask", 2D) = "white" {}
        _OverallNormalStrength ("[Overall] Normal Strength", Range(0, 1)) = 0.7343698
        _OverallNormalStrengthMask ("[Overall] Normal Strength Mask", 2D) = "white" {}

        _OverallWaveHeight ("[Overall] Wave Height", Range(0, 30)) = 2.826293
        _OverallWaveHeightMask ("[Overall] Wave Height Mask", 2D) = "white" {}

        // Shadow
        _ShadowOpacityRatio ("[Shadow] Shadow Opacity Ratio", Range(0, 1)) = 1
        _ShadowWaveFactor ("[Shadow] Shadow Wave Factor", Range(0, 50)) = 5

        // Screen-Space Reflection
        _SSRefractionDecaydistance ("[SSRefraction] Decay distance", Range(0, 100)) = 50
        _SSRefractionRefractionFresnel ("[SSRefraction] Refraction Fresnel", Range(0, 5)) = 5

        _SSRefractionStrength ("[SSRefraction] Strength", Range(0, 1)) = 0
        _SSRefractionStrengthMask ("[SSRefraction] Strength Mask", 2D) = "white" {}

    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull [_Cull]
            ZWrite [_ZWrite]

            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #define VRWS_BASE
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles
            #pragma target 5.0
            #pragma shader_feature BLEND_ENABLED
            #pragma shader_feature BLEND_COLOR_ENABLED
            #pragma shader_feature BLEND_ADDITIVE
            #pragma shader_feature SECOND_ENABLED
            #pragma shader_feature THIRD_ENABLED
            #pragma shader_feature FOURTH_ENABLED
            #pragma shader_feature CUSTOM_CUBEMAP_ENABLED

            #include "VRWSCore.cginc"
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull [_Cull]
            ZWrite [_ZWrite]

            CGPROGRAM
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #define VRWS_ADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles
            #pragma shader_feature BLEND_ENABLED
            #pragma shader_feature BLEND_COLOR_ENABLED
            #pragma shader_feature BLEND_ADDITIVE
            #pragma shader_feature SECOND_ENABLED
            #pragma shader_feature THIRD_ENABLED
            #pragma shader_feature FOURTH_ENABLED
            #pragma shader_feature CUSTOM_CUBEMAP_ENABLED

            #include "VRWSCore.cginc"
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
            #pragma hull hull
            #pragma domain domain
            #pragma vertex tessvert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "Tessellation.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles
            #pragma target 5.0
            #pragma shader_feature BLEND_ENABLED
            #pragma shader_feature BLEND_COLOR_ENABLED
            #pragma shader_feature BLEND_ADDITIVE
            #pragma shader_feature SECOND_ENABLED
            #pragma shader_feature THIRD_ENABLED
            #pragma shader_feature FOURTH_ENABLED
            #pragma shader_feature CUSTOM_CUBEMAP_ENABLED

            #include "VRWSShadowCaster.cginc"
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "VRWaterShader2.VRWS2Inspector"
}
