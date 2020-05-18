using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace VRWaterShader2
{
    public class VRWS2Inspector : ShaderGUI
    {
        #region MaterialProperties

        // ZWrite, Culling
        MaterialProperty ZWrite;
        MaterialProperty Cull;

        // Base Color, Texture
        MaterialProperty BaseTexture;
        MaterialProperty BaseColor;
        // Emission
        MaterialProperty EmissionTexture;
        MaterialProperty EmissionColor;
        // First wave map
        MaterialProperty FirstWaveHeightmap;
        MaterialProperty FirstWaveNormalmap;
        MaterialProperty FirstWaveNormalStrength;
        MaterialProperty FirstWaveHeightStrength;
        MaterialProperty FirstWaveUandVspeed;

        // Second wave map
        MaterialProperty SecondWaveEnableThis;
        MaterialProperty SecondWaveHeightmap;
        MaterialProperty SecondWaveNormalmap;
        MaterialProperty SecondWaveNormalStrength;
        MaterialProperty SecondWaveHeightStrength;
        MaterialProperty SecondWaveUandVspeed;

        // Third wave map
        MaterialProperty ThirdWaveEnableThis;
        MaterialProperty ThirdWaveHeightmap;
        MaterialProperty ThirdWaveNormalmap;
        MaterialProperty ThirdWaveNormalStrength;
        MaterialProperty ThirdWaveHeightStrength;
        MaterialProperty ThirdWaveUandVspeed;

        // Fourth wave map
        MaterialProperty FourthWaveEnableThis;
        MaterialProperty FourthWaveHeightmap;
        MaterialProperty FourthWaveNormalmap;
        MaterialProperty FourthWaveNormalStrength;
        MaterialProperty FourthWaveHeightStrength;
        MaterialProperty FourthWaveUandVspeed;

        // Cubemap Reflection
        MaterialProperty CubemapUseCustomCubemap;
        MaterialProperty CubemapBaseColor;
        MaterialProperty CubemapCustomCubemap;
        MaterialProperty CubemapGlossiness;
        MaterialProperty CubemapStrength;
        MaterialProperty CubemapFresnel;

        // Depth Blending
        MaterialProperty BlendEnableThis;
        MaterialProperty BlendBlendDistance;
        MaterialProperty BlendBlendAlpha;
        MaterialProperty BlendEnableColor;
        MaterialProperty BlendIsAdditive;
        MaterialProperty BlendColor;
        MaterialProperty BlendTexture;
        MaterialProperty BlendTextureUandVspeedZWNotused;
        MaterialProperty BlendEmissionFactor;

        // Tessellation
        MaterialProperty TessellationStrength;
        MaterialProperty TessellationNearCap;
        MaterialProperty TessellationFarCap;

        // PBR
        MaterialProperty OverallMetallic;
        MaterialProperty OverallMetallicMask;
        MaterialProperty OverallGloss;
        MaterialProperty OverallGlossMask ;
        MaterialProperty OverallNormalStrength;
        MaterialProperty OverallNormalStrengthMask;
        MaterialProperty OverallWaveHeight;
        MaterialProperty OverallWaveHeightMask;

        // Shadow
        MaterialProperty ShadowWaveFactor;
        MaterialProperty ShadowOpacityRatio;

        // Screen-Space Reflection
        MaterialProperty SSRefractionDecaydistance;
        MaterialProperty SSRefractionRefractionFresnel;
        MaterialProperty SSRefractionStrength;
        MaterialProperty SSRefractionStrengthMask;

        #endregion

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            Material material = materialEditor.target as Material;
            Shader shader = material.shader;

            // // shader.nameによって調整可能なプロパティを制御する。
            // bool isOpaque = shader.name.Contains("Opaque");
            // bool isFade = shader.name.Contains("Fade");
            // bool isCutout = shader.name.Contains("Cutout");

            ZWrite = FindProperty("_ZWrite", props);
            Cull = FindProperty("_Cull", props);

            BaseTexture = FindProperty("_BaseTexture", props);
            BaseColor = FindProperty("_BaseColor", props);

            EmissionTexture = FindProperty("_EmissionTexture", props);
            EmissionColor = FindProperty("_EmissionColor", props);

            FirstWaveHeightmap = FindProperty("_FirstWaveHeightmap", props);
            FirstWaveNormalmap = FindProperty("_FirstWaveNormalmap", props);
            FirstWaveNormalStrength = FindProperty("_FirstWaveNormalStrength", props);
            FirstWaveHeightStrength = FindProperty("_FirstWaveHeightStrength", props);
            FirstWaveUandVspeed = FindProperty("_FirstWaveUandVspeed", props);

            SecondWaveEnableThis = FindProperty("_SecondWaveEnableThis", props);
            SecondWaveHeightmap = FindProperty("_SecondWaveHeightmap", props);
            SecondWaveNormalmap = FindProperty("_SecondWaveNormalmap", props);
            SecondWaveNormalStrength = FindProperty("_SecondWaveNormalStrength", props);
            SecondWaveHeightStrength = FindProperty("_SecondWaveHeightStrength", props);
            SecondWaveUandVspeed = FindProperty("_SecondWaveUandVspeed", props);

            ThirdWaveEnableThis = FindProperty("_ThirdWaveEnableThis", props);
            ThirdWaveHeightmap = FindProperty("_ThirdWaveHeightmap", props);
            ThirdWaveNormalmap = FindProperty("_ThirdWaveNormalmap", props);
            ThirdWaveNormalStrength = FindProperty("_ThirdWaveNormalStrength", props);
            ThirdWaveHeightStrength = FindProperty("_ThirdWaveHeightStrength", props);
            ThirdWaveUandVspeed = FindProperty("_ThirdWaveUandVspeed", props);

            FourthWaveEnableThis = FindProperty("_FourthWaveEnableThis", props);
            FourthWaveHeightmap = FindProperty("_FourthWaveHeightmap", props);
            FourthWaveNormalmap = FindProperty("_FourthWaveNormalmap", props);
            FourthWaveNormalStrength = FindProperty("_FourthWaveNormalStrength", props);
            FourthWaveHeightStrength = FindProperty("_FourthWaveHeightStrength", props);

            FourthWaveUandVspeed = FindProperty("_FourthWaveUandVspeed", props);
            CubemapUseCustomCubemap = FindProperty("_CubemapUseCustomCubemap", props);
            CubemapBaseColor = FindProperty("_CubemapBaseColor", props);
            CubemapCustomCubemap = FindProperty("_CubemapCustomCubemap", props);
            CubemapGlossiness = FindProperty("_CubemapGlossiness", props);
            CubemapStrength = FindProperty("_CubemapStrength", props);
            CubemapFresnel = FindProperty("_CubemapFresnel", props);

            BlendEnableThis = FindProperty("_BlendEnableThis", props);
            BlendBlendDistance = FindProperty("_BlendBlendDistance", props);
            BlendBlendAlpha = FindProperty("_BlendBlendAlpha", props);
            BlendEnableColor = FindProperty("_BlendEnableColor", props);
            BlendIsAdditive = FindProperty("_BlendIsAdditive", props);
            BlendColor = FindProperty("_BlendColor", props);
            BlendTexture = FindProperty("_BlendTexture", props);
            BlendTextureUandVspeedZWNotused = FindProperty("_BlendTextureUandVspeedZWNotused", props);
            BlendEmissionFactor = FindProperty("_BlendEmissionFactor", props);

            ShadowWaveFactor = FindProperty("_ShadowWaveFactor", props);
            ShadowOpacityRatio = FindProperty("_ShadowOpacityRatio", props);

            TessellationStrength = FindProperty("_TessellationStrength", props);
            TessellationNearCap = FindProperty("_TessellationNearCap", props);
            TessellationFarCap = FindProperty("_TessellationFarCap", props);

            OverallMetallic = FindProperty("_OverallMetallic", props);
            OverallMetallicMask = FindProperty("_OverallMetallicMask", props);
            OverallGloss = FindProperty("_OverallGloss", props);
            OverallGlossMask = FindProperty("_OverallGlossMask", props);
            OverallNormalStrength = FindProperty("_OverallNormalStrength", props);
            OverallNormalStrengthMask = FindProperty("_OverallNormalStrengthMask", props);
            OverallWaveHeight = FindProperty("_OverallWaveHeight", props);
            OverallWaveHeightMask = FindProperty("_OverallWaveHeightMask", props);

            SSRefractionDecaydistance = FindProperty("_SSRefractionDecaydistance", props);
            SSRefractionRefractionFresnel = FindProperty("_SSRefractionRefractionFresnel", props);
            SSRefractionStrength = FindProperty("_SSRefractionStrength", props);
            SSRefractionStrengthMask = FindProperty("_SSRefractionStrengthMask", props);

            EditorGUIUtility.labelWidth = 0f;

            EditorGUI.BeginChangeCheck();
            {
                UIHelper.ShurikenHeader("Common");
                UIHelper.DrawWithGroup(() => {
                    materialEditor.TexturePropertySingleLine(new GUIContent("Base Color","Base Color and Texture"), BaseTexture, BaseColor);
                    materialEditor.TextureScaleOffsetProperty(BaseTexture);

                    materialEditor.TexturePropertySingleLine(new GUIContent("Emission Color","Emission Color and Texture"), EmissionTexture, EmissionColor);
                    materialEditor.TextureScaleOffsetProperty(EmissionTexture);

                    materialEditor.ShaderProperty(Cull, "Culling");
                    materialEditor.ShaderProperty(ZWrite, "ZWrite");
                });

                UIHelper.ShurikenHeader("1st wave");
                UIHelper.DrawWithGroup(() => {
                    materialEditor.TexturePropertySingleLine(new GUIContent("Height map", "Heightmap and strength (1st wave)"), FirstWaveHeightmap, FirstWaveHeightStrength);
                    materialEditor.TexturePropertySingleLine(new GUIContent("Normal map", "Normalmap and strength (1st wave)"), FirstWaveNormalmap, FirstWaveNormalStrength);
                    Vector2Property(FirstWaveUandVspeed, "Scroll Speed (U, V)");
                    materialEditor.TextureScaleOffsetProperty(FirstWaveNormalmap);
                });

                // 2nd Wave
                UIHelper.ShurikenHeader("2nd wave");
                materialEditor.DrawShaderPropertySameLIne(SecondWaveEnableThis);
                var IsShowSecondWave = SecondWaveEnableThis.floatValue;
                if(IsShowSecondWave > 0)
                {
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Height map", "Heightmap and strength (1st wave)"), SecondWaveHeightmap, SecondWaveHeightStrength);
                        materialEditor.TexturePropertySingleLine(new GUIContent("Normal map", "Normalmap and strength (1st wave)"), SecondWaveNormalmap, SecondWaveNormalStrength);
                        Vector2Property(SecondWaveUandVspeed, "Scroll Speed (U, V)");
                        materialEditor.TextureScaleOffsetProperty(SecondWaveNormalmap);
                    });
                }

                // 3rd Wave
                UIHelper.ShurikenHeader("3rd wave");
                materialEditor.DrawShaderPropertySameLIne(ThirdWaveEnableThis);
                var IsShowThirdWave = ThirdWaveEnableThis.floatValue;
                if(IsShowThirdWave > 0)
                {
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Height map", "Heightmap and strength (1st wave)"), ThirdWaveHeightmap, ThirdWaveHeightStrength);
                        materialEditor.TexturePropertySingleLine(new GUIContent("Normal map", "Normalmap and strength (1st wave)"), ThirdWaveNormalmap, ThirdWaveNormalStrength);
                        Vector2Property(ThirdWaveUandVspeed, "Scroll Speed (U, V)");
                        materialEditor.TextureScaleOffsetProperty(ThirdWaveNormalmap);
                    });
                }
                // 4th Wave
                UIHelper.ShurikenHeader("4th wave");
                materialEditor.DrawShaderPropertySameLIne(FourthWaveEnableThis);
                var IsShowFourthWave = FourthWaveEnableThis.floatValue;
                if(IsShowFourthWave > 0)
                {
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Height map", "Heightmap and strength (1st wave)"), FourthWaveHeightmap, FourthWaveHeightStrength);
                        materialEditor.TexturePropertySingleLine(new GUIContent("Normal map", "Normalmap and strength (1st wave)"), FourthWaveNormalmap, FourthWaveNormalStrength);
                        Vector2Property(FourthWaveUandVspeed, "Scroll Speed (U, V)");
                        materialEditor.TextureScaleOffsetProperty(FourthWaveNormalmap);
                    });
                }


                UIHelper.ShurikenHeader("Wave misc");
                UIHelper.DrawWithGroup(() => {
                    // OverallMetallicMask
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Metallic", "Metallic with Mask Texture"), OverallMetallicMask, OverallMetallic);
                        materialEditor.TextureScaleOffsetPropertyIndent(OverallMetallicMask);
                    });
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Gloss", "Gloss with Mask Texture"), OverallGlossMask, OverallGloss);
                        materialEditor.TextureScaleOffsetPropertyIndent(OverallGlossMask);
                    });
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Normal Strength", "Normal Strength with Mask Texture"), OverallNormalStrengthMask, OverallNormalStrength);
                        materialEditor.TextureScaleOffsetPropertyIndent(OverallNormalStrengthMask);
                    });
                    UIHelper.DrawWithGroup(() => {
                        materialEditor.TexturePropertySingleLine(new GUIContent("Wave Height", "Wave Height with Mask Texture"), OverallWaveHeightMask, OverallWaveHeight);
                        materialEditor.TextureScaleOffsetPropertyIndent(OverallWaveHeightMask);
                    });
                });

                UIHelper.ShurikenHeader("Reflection");
                UIHelper.DrawWithGroup(() => {
                    materialEditor.ShaderProperty(CubemapStrength, "Strength");
                    materialEditor.ShaderProperty(CubemapGlossiness, "Glossiness");
                    materialEditor.ShaderProperty(CubemapFresnel, "Fresnel");
                    materialEditor.ShaderProperty(CubemapUseCustomCubemap, "Use Custom Cubemap");
                    var IsUseCutsomCubemap = CubemapUseCustomCubemap.floatValue;
                    if(IsUseCutsomCubemap > 0)
                    {
                        EditorGUI.indentLevel++;
                        materialEditor.ShaderProperty(CubemapCustomCubemap, "Cubemap");
                        materialEditor.ShaderProperty(CubemapBaseColor, "Color");
                        EditorGUI.indentLevel --;
                    }
                });

                UIHelper.ShurikenHeader("Blending");
                materialEditor.DrawShaderPropertySameLIne(BlendEnableThis);
                var IsUseBlend = BlendEnableThis.floatValue;
                if(IsUseBlend > 0)
                {
                    UIHelper.DrawWithGroup(() => {
                        EditorGUILayout.HelpBox("Blend needs Realtime Directional Light." + Environment.NewLine + "RealtimeのDirectional Lightがない場合はオフにすることを強く推奨",MessageType.Warning);
                        materialEditor.ShaderProperty(BlendBlendDistance, "Blend Distance");
                        materialEditor.ShaderProperty(BlendBlendAlpha, "Alpha");
                        materialEditor.ShaderProperty(BlendEnableColor, "Use Color Blend");
                        var IsBlendEnableColor = BlendEnableColor.floatValue;
                        if(IsBlendEnableColor > 0)
                        {
                            materialEditor.ShaderProperty(BlendIsAdditive, "Additive Blend");
                            materialEditor.TexturePropertySingleLine(new GUIContent("Blend Texture", "Normalmap and strength (1st wave)"), BlendTexture, BlendColor);
                            Vector2Property(BlendTextureUandVspeedZWNotused, "Scroll Speed (U, V)");
                            materialEditor.TextureScaleOffsetProperty(BlendTexture);
                            materialEditor.ShaderProperty(BlendEmissionFactor, "Emission factor");
                        }
                    });
                }

                UIHelper.ShurikenHeader("Tessellation");
                UIHelper.DrawWithGroup(() => {
                    materialEditor.ShaderProperty(TessellationStrength, "Strength");
                    materialEditor.ShaderProperty(TessellationNearCap, "Near Cap");
                    materialEditor.ShaderProperty(TessellationFarCap, "Far Cap");
                });

                UIHelper.ShurikenHeader("Refraction");
                UIHelper.DrawWithGroup(() => {
                    materialEditor.TexturePropertySingleLine(new GUIContent("Strength", "Strength with Mask Texture"), SSRefractionStrengthMask, SSRefractionStrength);
                    materialEditor.TextureScaleOffsetPropertyIndent(SSRefractionStrengthMask);
                    materialEditor.ShaderProperty(SSRefractionDecaydistance, "Decay Distance");
                    materialEditor.ShaderProperty(SSRefractionRefractionFresnel, "Refraction Fresnel");
                });

                UIHelper.ShurikenHeader("Shadow");
                UIHelper.DrawWithGroup(() => {
                    materialEditor.ShaderProperty(ShadowOpacityRatio, "Opacity Ratio");
                    materialEditor.ShaderProperty(ShadowWaveFactor, "Wave Normal Factor");
                });
            }
            EditorGUI.EndChangeCheck();
        }

        static void Vector2Property(MaterialProperty property, string name)
        {
            EditorGUI.BeginChangeCheck();
            Vector2 vector2 = EditorGUILayout.Vector2Field(name,new Vector2(property.vectorValue.x, property.vectorValue.y),null);
            if (EditorGUI.EndChangeCheck())
                property.vectorValue = new Vector4(vector2.x, vector2.y);
        }
    }

    static class UIHelper
    {
        static int HEADER_HEIGHT = 22;
        public static void DrawShaderPropertySameLIne(this MaterialEditor editor, MaterialProperty property) {
            Rect r = EditorGUILayout.GetControlRect(true,0,EditorStyles.layerMaskField);
            r.y -= HEADER_HEIGHT;
            r.height = MaterialEditor.GetDefaultPropertyHeight(property);
            editor.ShaderProperty(r, property, " ");
        }
        private static Rect DrawShuriken(string title, Vector2 contentOffset) {
            var style = new GUIStyle("ShurikenModuleTitle");
            style.margin = new RectOffset(0, 0, 8, 0);
            style.font = new GUIStyle(EditorStyles.boldLabel).font;
            style.border = new RectOffset(15, 7, 4, 4);
            style.fixedHeight = HEADER_HEIGHT;
            style.contentOffset = contentOffset;
            var rect = GUILayoutUtility.GetRect(16f, HEADER_HEIGHT, style);
            GUI.Box(rect, title, style);
            return rect;
        }
        public static void ShurikenHeader(string title)
        {
            DrawShuriken(title,new Vector2(6f, -2f));
        }
        public static bool ShurikenFoldout(string title, bool display)
        {
            var rect = DrawShuriken(title,new Vector2(20f, -2f));
            var e = Event.current;
            var toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
            if (e.type == EventType.Repaint) {
                EditorStyles.foldout.Draw(toggleRect, false, false, display, false);
            }
            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition)) {
                display = !display;
                e.Use();
            }
            return display;
        }
        public static void Vector2Property(MaterialProperty property, string name)
        {
            EditorGUI.BeginChangeCheck();
            Vector2 vector2 = EditorGUILayout.Vector2Field(name,new Vector2(property.vectorValue.x, property.vectorValue.y),null);
            if (EditorGUI.EndChangeCheck())
                property.vectorValue = new Vector4(vector2.x, vector2.y);
        }
        public static void Vector4Property(MaterialProperty property, string name)
        {
            EditorGUI.BeginChangeCheck();
            Vector4 vector4 = EditorGUILayout.Vector2Field(name,property.vectorValue,null);
            if (EditorGUI.EndChangeCheck())
                property.vectorValue = vector4;
        }
        public static void Vector2PropertyZW(MaterialProperty property, string name)
        {
            EditorGUI.BeginChangeCheck();
            Vector2 vector2 = EditorGUILayout.Vector2Field(name,new Vector2(property.vectorValue.x, property.vectorValue.y),null);
            if (EditorGUI.EndChangeCheck())
                property.vectorValue = new Vector4(vector2.x, vector2.y);
        }
        public static void TextureScaleOffsetPropertyIndent(this MaterialEditor editor, MaterialProperty property)
        {
            EditorGUI.indentLevel ++;
            editor.TextureScaleOffsetProperty(property);
            EditorGUI.indentLevel --;
        }
        public static void DrawWithGroup(Action action)
        {
            EditorGUILayout.BeginVertical( GUI.skin.box );
            action();
            EditorGUILayout.EndVertical();
        }
        public static void DrawWithGroupHorizontal(Action action)
        {
            EditorGUILayout.BeginHorizontal( GUI.skin.box );
            action();
            EditorGUILayout.EndHorizontal();
        }
    }
}