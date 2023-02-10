using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class StoreProduct_AutoAdd : MonoBehaviour
{
    static List<StoreItem> NewStoreItem = new List<StoreItem>();

   [MenuItem("Tools/Auto Add New Store Product")]
   public static void AutoAddNewStoreProduct(){
    TextAsset StoreSpreadsheet = Resources.Load<TextAsset>("Data - Feuille 1");
    
    string[] lines = StoreSpreadsheet.text.Split(new char[]{'\n'});

    for (int i = 1; i < lines.Length-1; i++){
        string[] row = lines[i].Split(new char[] { ',' });

        if (row[1] != ""){          
            StoreItem newItem = new StoreItem();

            newItem.Id = Store.Instance.StoreItems.Count;
            newItem.Name = row[0];
            int.TryParse(row[1], out newItem.Price);
            newItem.Icon = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/1_Graphics/Store/" +newItem.Name+".png", typeof(Sprite));
            newItem.Prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/2_Prefabs/"+newItem.Name+".prefab", typeof(GameObject));

            if (newItem.Icon== null){
                EditorUtility.DisplayDialog("Icon not found", "Icon for " + newItem.Name+" wasn't found. Cancelling auto add.","Understood" );
                return;
            }
            else if (newItem.Prefab == null){
                EditorUtility.DisplayDialog("Prefab not found", "Prefab for " + newItem.Name+" wasn't found. Cancelling auto add.","Understood" );
                return;
            }
            
            if(!Store.Instance.StoreItems.Exists(x => x.Name == newItem.Name)){
                Store.Instance.StoreItems.Add(newItem);                   
            }       
        }
    }

   }
}
