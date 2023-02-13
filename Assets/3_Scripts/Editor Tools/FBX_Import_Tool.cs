using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FBX_Import_Tool : MonoBehaviour
{
    [MenuItem("Tools/Create Prefab from selected FBX")]
    public static void CreatePrefabFromFBX(){
        GameObject[] objectArray = Selection.gameObjects;
    
        foreach (GameObject gameObject in objectArray) {
            //Set Prefab
            if (!Directory.Exists("Assets/2_Prefabs"))
                AssetDatabase.CreateFolder("Assets", "2_Prefabs");
            string localPath = "Assets/2_Prefabs/" + gameObject.name + ".prefab";
            
            CheckPath(localPath, gameObject.name, out localPath);
            if (localPath ==null)
                {return;}
                
            bool prefabSuccess;
            GameObject instanceRoot = (GameObject)PrefabUtility.InstantiatePrefab(gameObject);
            
            GameObject instanceVariant = PrefabUtility.SaveAsPrefabAssetAndConnect(instanceRoot, localPath, InteractionMode.UserAction, out prefabSuccess);
            if (prefabSuccess == true)
                Debug.Log("Prefab was saved successfully");
            else
                Debug.Log("Prefab failed to save" + prefabSuccess);

            GameObject.DestroyImmediate(instanceRoot);

            //Set Material
            var newMat = (Material)AssetDatabase.LoadAssetAtPath("Assets/1_Graphics/Materials/Purple.mat", typeof(Material));

            SkinnedMeshRenderer instanceMesh = instanceVariant.transform.GetComponentInChildren<SkinnedMeshRenderer>();

            Material[] tempArray = instanceMesh.sharedMaterials;
            tempArray[0] = newMat; //Head
            tempArray[1] = newMat; //Body    

            instanceMesh.sharedMaterials = tempArray;

            //Set Collider
            instanceVariant.AddComponent<CapsuleCollider>();
            var coll = instanceVariant.GetComponent<CapsuleCollider>();
            coll.center = new Vector3(0,0.55f,0);
            coll.radius = 0.2f;
            coll.height = 1.1f;

            //Set Animator
            var animator = (RuntimeAnimatorController)AssetDatabase.LoadAssetAtPath("Assets/1_Graphics/AnimatorControllers/Controller.controller", typeof(RuntimeAnimatorController));
            instanceVariant.GetComponent<Animator>().runtimeAnimatorController = animator;

        }
    }

    private static void CheckPath(string _path, string _GOName, out string newPath, int i = 1){        
        newPath = _path;
        if (File.Exists(_path)){
            int option = EditorUtility.DisplayDialogComplex("File detected",
            Path.GetFileName(_path) + " already exists. \n What do you want to do ?",
            "Overwrite",
            "Create another one",
            "Cancel");

            switch(option){
                case 0: 
                File.Delete(_path);
                newPath = _path;
                break;

                case 1:
                _path = "Assets/2_Prefabs/" + _GOName + $"{i}" + ".prefab";
                CheckPath(_path, _GOName, out _path, i+1);
                newPath = _path;
                break;

                case 2:
                newPath = null;
                return;

                default:
                break;
            }


               /*if (EditorUtility.DisplayDialog("File detected", Path.GetFileName(_path) + " already exists. \n What do you want to do ?", "Overwrite", "Create another one")){
                    File.Delete(_path);
               }
               else
               {
                    _path = "Assets/2_Prefabs/" + _GOName + $"{i}" + ".prefab";
                    CheckPath(_path, _GOName, out _path, i+1);
               }*/
        }
    }
}
