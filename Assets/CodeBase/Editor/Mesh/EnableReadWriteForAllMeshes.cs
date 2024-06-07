using UnityEditor;
using UnityEngine;

public class EnableReadWriteForAllMeshes : MonoBehaviour
{
    [MenuItem("Tools/Enable Read/Write For All Meshes")]
    static void EnableReadWrite()
    {
        string[] guids = AssetDatabase.FindAssets("t:Mesh");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;
            if (importer != null)
            {
                importer.isReadable = true;
                importer.SaveAndReimport();
            }
        }
        Debug.Log("Read/Write Enabled for all meshes.");
    }
}