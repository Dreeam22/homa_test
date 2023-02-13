using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Icon_ImportTool : AssetPostprocessor
{
     void OnPreprocessTexture()
    {
         TextureImporter importer = assetImporter as TextureImporter;
     
        if (importer.importSettingsMissing)
            {
                if (EditorUtility.DisplayDialog("Texture imported", 
                "Do you want to set the newly imported Texture as “Sprite (2D and UI)” ?", 
                "Yes", "No"))
                {
                    importer.textureType = TextureImporterType.Sprite;
                    importer.SaveAndReimport();
                }               
            }
        else
            { 
                return;
            }
    }
}
