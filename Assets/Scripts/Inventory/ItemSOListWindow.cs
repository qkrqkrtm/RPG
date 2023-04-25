using Inventory.Model;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class ItemSOListWindow : EditorWindow
{
    

    private List<ItemSO> itemSOs;

    [MenuItem("Window/ItemSO List")]
    private static void OpenWindow()
    {
        var window = GetWindow<ItemSOListWindow>();
        window.titleContent = new GUIContent("ItemSO List");
    }

    private void OnEnable()
    {
        itemSOs = new List<ItemSO>();
        var guids = AssetDatabase.FindAssets("t:ItemSO");
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var itemSO = AssetDatabase.LoadAssetAtPath<ItemSO>(path);
            itemSOs.Add(itemSO);
        }
    }

    private void OnGUI()
    {
        foreach (var itemSO in itemSOs)
        {
            EditorGUILayout.ObjectField(itemSO, typeof(ItemSO), false);
            EditorGUILayout.Toggle("IsStackable", itemSO.IsStackable);
            EditorGUILayout.IntField("MaxStackSize", itemSO.MaxStackSize);
            itemSO.Name = EditorGUILayout.TextField("Name", itemSO.Name);
            itemSO.Description = EditorGUILayout.TextField("Description", itemSO.Description, GUILayout.MaxHeight(200f));
            itemSO.ItemImage = EditorGUILayout.ObjectField("ItemImage", itemSO.ItemImage, typeof(Sprite), false) as Sprite;
            itemSO.ItemMesh = EditorGUILayout.ObjectField("ItemMesh", itemSO.ItemMesh, typeof(Mesh), false) as Mesh;
            itemSO.Material = EditorGUILayout.ObjectField("Material", itemSO.Material, typeof(Material), false) as Material;
        }
    }
}
