using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;
using System.IO;
public class FBX_Preset_Apply : AssetPostprocessor
{
    void OnPreprocessModel(){
        ModelImporter importer = assetImporter as ModelImporter;

        if (importer.importSettingsMissing){
            Preset preset = (Preset)AssetDatabase.LoadAssetAtPath("Assets/Resources/FBXImporter.preset", typeof(Preset));
            preset.ApplyTo(importer);
            importer.avatarSetup = ModelImporterAvatarSetup.CreateFromThisModel;
            importer.SaveAndReimport();
           
            Debug.Log("applying presets");
        }

    }
}
