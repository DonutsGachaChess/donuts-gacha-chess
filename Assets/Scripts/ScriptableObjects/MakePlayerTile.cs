using UnityEngine;
using UnityEditor;

public class MakePlayerTile
{
    [MenuItem("Assets/Create/PlayerTile")]
    public static void CreateMyAsset()
    {
        PlayerTile asset = ScriptableObject.CreateInstance<PlayerTile>();

        AssetDatabase.CreateAsset(asset, "Assets/PlayerTile.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
