--------------------
VRWaterShader
--------------------
VR&Singlepass Ready general purpose water shader.
Version 1.3

--------------------
ご購入ありがとうございます
--------------------
ご意見・ご要望・質問などありましたら、下記へご連絡下さい
Email:virtually.synqarks@gmail.com
Twitter:@synqark

--------------------
とりあえず使いたい！
--------------------
Assets/VRWaterShader/DemoScenes/DemoScene.unityを開き、
使いたいものに一番近いGameObjectをお使いのシーンにコピペしてください。

--------------------
インストール方法・フォルダ構成
--------------------
VRWaterShader.unitypackageをインポートしてください。
フォルダ構成は以下のようになります。
(Version 1.1以前にあったSceneはDemoScenes/DemoScene.unityに統合しました。）

Assets/VRWaterShader
  DemoScenes
  Models
  Shaders
  Textures

また、シェーダーリストに以下のシェーダーが追加されます。
VRWaterShader/Standard
VRWaterShader/Double Sided
VRWaterShader/ZWrite Standard
VRWaterShader/ZWrite Double Sided
VRWaterShader/_GLES30/Standard
VRWaterShader/_GLES30/Double Sided
VRWaterShader/_GLES30/ZWrite Standard
VRWaterShader/_GLES30/ZWrite Double Sided

--------------------
パラメーターについて
--------------------
下記に説明を記載しました。
最初はDemoScenesフォルダ以下にある各種サンプルマテリアル・メッシュを活用頂くことを推奨します。

[Base]
　Materialにそのまま張り付ける色およびテクスチャ。
 Texture:
 Color:
 is as Emission: TextureおよびColorをEmissionパラメーターにも送ります

[Primary / Secondary]
　波を構成する法線およびハイトマップを定義します。
　Secondaryはオプション（「Enable This?」のチェックで合成）
 Heightmap
 Normalmap
 HeightStrength : Heightmapがどの程度の強度を持つか
 U and V speed : 波の流れる速度をXとYで指定

[Cubemap]
　環境光として指定するCubemapです。
　SceneのSkyboxと同じものを推奨しますが、特別に光らせたい場合などは、別途用意できます。
【GLES30未対応】Use Custom Cubemap : 独自のキューブマップを反射させる場合にチェックします。デフォルトではシーンのリフレクションを使用します。
【GLES30未対応】Base Color : Cubemapの色を指定します。 Custom Cubemapと乗算されます。
 Custom Cubemap : 独自のキューブマップを指定します。
【GLES30未対応】Glossiness : Cubemapのぼけ具合を指定します。
 Strength : 環境光の濃さ
 Fresnel : 環境光をどの程度水平線に近い部分にのみ限定するか（フレネル）

[Blend]（※RealtimeまたはMixedで、ShadowがOnのDirectional Lightをシーンに配置しないと効果がありません）
　水とほかのオブジェクトとの境界でブレンドします。
 Blend Distance : ブレンド距離（メートル）
【GLES30未対応】Color : Blendする色を指定します
【GLES30未対応】Blend Alpha : Blendする色の透明度を指定します。（GLES30では透明になります。）

[Tessellation]（※GLES30シェーダーでは無効化されました。）
　Heightmapをディスプレイスメントマップとしてテッセレーションを行います。
 Strength : 分割の最大強度
 Near Cap : 最大強度を得る距離（これより近くても同じ強度になる）
 Far Cap : テッセレーションを開始する距離（これより遠いと元のメッシュのままになる）

[Overall]
　上記パラメータを加味したメッシュに対するPBR的パラメーターとその他
 Metallic
 Gloss
 Normal Strength : 指定したNormalmapの強度
 Wave Height : 指定したHeightmapの強度

[SSRefraction]
　メッシュに対する「スクリーンスペース屈折」の設定。
　上記パラメータの適用後の物体における「法線の逆側」にスクリーンスペース屈折を適用します。
 Decay Distance : 減衰距離（メートル）近いほど「物体が遠くにあるとき」の不自然な屈折を抑制
 Refraction Fresnel : 屈折をどの程度水平に近い部分に限定するか（フレネル）
 Strength : 屈折そのものの強度

--------------------
リリースノート
--------------------
v1.4 -
  Unity2018をサポート
v1.3 -
　デモシーンを Assets/VRWaterShader/DemoScenes/DemoScene.unity に統一しました。
　シェーダーパラメーターに以下の項目を追加しました。WebGL(GLES30)版は未対応です。
　・[Cubemap] Use Custom Cubemap
　・[Cubemap] Base Color
　・[Cubemap] Custom Cubemap (旧: Cubemap Texture）
　・[Cubemap] Glossiness
　・[Blend] Color
　・[Blend] Blend Alpha
v1.1 -
  以下のシェーダーを同梱しました。
  ・Zバッファ有効版
  ・WebGL(GLES30)対応版　（テッセレーションは無効になりました）
v1.0 - 発行
