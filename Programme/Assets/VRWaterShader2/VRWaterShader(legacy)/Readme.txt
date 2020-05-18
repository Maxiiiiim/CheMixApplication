--------------------
VRWaterShader
--------------------
VR&Singlepass Ready general purpose water shader.
Version 1.3

--------------------
Thank you for purchasing my asset!
--------------------
If you have some questions. Please contact below:
Email:virtually.synqarks@gmail.com
Twitter:@synqark

--------------------
TL;DR
--------------------
Please Open [Assets/VRWaterShader/DemoScenes/DemoScene.unity]
And copy your scene from one of water which you want to use.
Enjoy!

--------------------
How to import asset.
--------------------
Please import VRWaterShader.unitypackage.
Directory hierarchy is here:
(Old version Scenes are Merged to DemoScenes/DemoScene.unity)

Assets/VRWaterShader
  DemoScenes
  Models
  Shaders
  Textures

And I add shaders below:
 VRWaterShader/Standard
 VRWaterShader/Double Sided
 VRWaterShader/ZWrite Standard
 VRWaterShader/ZWrite Double Sided
 VRWaterShader/_GLES30/Standard
 VRWaterShader/_GLES30/Double Sided
 VRWaterShader/_GLES30/ZWrite Standard
 VRWaterShader/_GLES30/ZWrite Double Sided

--------------------
Shader Parameters.
--------------------
At first, I recommend you pick up from any Materials from DemoScenes.
Parameters are here:

[Base]
 Texture:Base Texture
 Color:Base Color
 is as Emission: Connect these colors to emission.

[Primary / Secondary]
 Heightmap : Water Wave Heightmap
 Normalmap : Water Wave Normalmap (baked from Heightmap recommended.)
 HeightStrength : How High Heightmap affects.
 U and V speed : U and V animation speed Height&Normapmap

[Cubemap]
�@Cubemap affects material as Ambient Light.
  Skybox Texture recommended. but, if set brighter than sky. You can use other cubemap to get better result.
 Use Custom Cubemap : Check to use your custom Cubemap (Uncheck to use Scene reflection probes.)
 Base Color : Base color of your cubemap. will be multiplied with cubemap texture.
 Glossiness : Gloss value of cubemap.
 Strength : Strength of Ambient Light.
 Fresnel : How much limits Cubemap affects only horizon (Fresnel)

[Blend]
    Blending Water Mesh and other Opaque Meshes.
 Blend Distance : Blend Distance (unit)
 Color : Color to blend.
 Blend Alpha : Alpha to blend

[Tessellation] (It is omitted from GLES30/ Shaders)
    Uses tessellation (Heightmap as Displacement map)
 Strength : Partition Strength
 Near Cap : Set distance you get max partition
 Far Cap : Set distance you get no partition (original mesh)

[Overall]
�@PBR like and etc parameters.
 Metallic
 Gloss
 Normal Strength : Strength of primary and secondary Normalmaps
 Wave Height : Strength of primary and secondary Heightmaps

[SSRefraction]
�@Screen Space Refraction.
 Decay Distance : Far limit of refraction affects.
 Refraction Fresnel : How much limits Refraction affect only periphery (Fresnel)
 Strength : Overall Refraction Strength

--------------------
Release notes.
--------------------
v1.4 -
  Support Unity2018.1 ~
v1.3 -
  Merged DemoScenes to Assets/VRWaterShader/DemoScenes/DemoScene.unity
  Added few parameters to shader (except GLES30 versions. I'm sorry!)
  -[Cubemap] Use Custom Cubemap
  -[Cubemap] Base Color
  -[Cubemap] Custom Cubemap (��: Cubemap Texture�j
  -[Cubemap] Glossiness
  -[Blend] Color
  -[Blend] Blend Alpha

v1.1 -
  Additional Shaders below:
  -Enabled ZWrite
  -Runnable as GLES30 (enabled height map instead of tessellation)
v1.0 - published initial version.
