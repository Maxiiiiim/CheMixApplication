--------------------
VRWaterShader2
--------------------
VR&Singlepass Ready general purpose water shader.
Version 2.0

--------------------
For Japanese (日本の方向け)
--------------------
こちらで日本人向けに機能を解説したサイトを用意しております。是非ご確認下さい。
https://synqark.github.io/VRWaterShader2-Doc/
I'm sorry. This site is for Japanese.

--------------------
Thank you for purchasing my asset!
--------------------
If you have some questions. Please contact below:
Email:virtually.synqarks@gmail.com
Twitter:@synqark

--------------------
TL;DR
--------------------
Please Open [Assets/VRWaterShader2/DemoScenes/DemoScene.unity]
And copy your scene from one of water which you want to use.
Enjoy!

--------------------
Shader Parameters.
--------------------
At first, I recommend you pick up from any Materials from DemoScenes.
Parameters are here:

[Base]
 Base Color: Base Texture and Color
 Emission Color: Emission Texture and Color
 Culling: Culling Mode (Back is default.)
 ZWrite: Enable ZWrite (Checked Recommended is you use high wave ocean)

[1st wave - 4th wave]
2nd wave - 4th wave is switchable.
 Height map : Water Wave Heightmap
 Normal map : Water Wave Normalmap (baked from Heightmap recommended.)
 U and V speed : U and V animation speed Height&Normapmap
 Tiling : Water Wave Tiling
 Offset : Water Wave Offset

[Wave misc]
PBR like and etc parameters.
 Metallic
 Gloss
 Normal Strength : Strength of wave Normal maps
 Wave Height : Strength of wave Heightmaps

[Reflection]
You can Setting Reflection Parameters.
Usually use Reflection Probe in scene.
or You can choose custom cubemap for nice result.
 Strength : Strength of Reflection.
 Glossiness : Gloss value of cubemap.
 Fresnel : How much limits Cubemap affects only horizon (Fresnel)
 Use Custom Cubemap : Check to use your custom Cubemap (Uncheck to use Scene reflection probes.)
 Cubemap : custom cubemap texture.
 Color : multiplied cubemap texture.

[Blending]
Blending Water Mesh and other Opaque Meshes.
 Blend Distance : Blend Distance (unit)
 Blend Alpha : Alpha to blend
 Use Color Blend : Check it if you want blend to another color.
 Additive Blend : Another color blends as Additive.
 Blend Texture : Another color and Texture
 Scroll Speed : Another texture scroll speed
 Tiling : Another texture tiling
 Offset : Another texture offset
 Emission factor : Gain this if another color is part of emission.

[Tessellation]
Uses tessellation (Heightmap as Displacement map)
 Strength : Partition Strength
 Near Cap : Set distance you get max partition
 Far Cap : Set distance you get no partition (original mesh)

[SSRefraction]
Screen Space Refraction.
 Strength : Overall Refraction Strength with Mask Texture
 Decay Distance : Far limit of refraction affects.
 Refraction Fresnel : How much limits Refraction affect only periphery (Fresnel)

--------------------
Release notes.
--------------------
v2.0
- overhauled shader codes
(old version is in [VRWaterShader(legacy)])