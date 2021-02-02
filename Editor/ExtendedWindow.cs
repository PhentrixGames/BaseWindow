using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ExtendedWindow<T, U>: EditorWindow where T: EditorWindow where U: ScriptableObject
{
    protected T wnd;
    protected U selectedItem;
    protected VisualElement root;

    public static void Init(string name)
    {
        wnd = GetWindow<T>();
        wnd.titleContent = new GUIContent(name);
    }

    public void CreateGUI()
    {
        root = rootVisualElement;
    }

    public void PopulateList()
    {
        VisualElement ItemList = root.Q<VisualElement>("ItemList");
        string[] items = AssetDatabase.FindAssets("t:" + typeof(U).Name);

        for (int i = 0; i < items.Length; i++)
        {
            string path = AssetDatabase.GUIToAssetPath(items[i]);
            Button btn = new Button();
            btn.text = Path.GetFileNameWithoutExtension(path);
            btn.clickable.clicked += () =>
            {
                selectedItem = AssetDatabase.LoadAssetAtPath<U>(path);
            };
        }
    }

    protected abstract void UpdateItemInfo();

    public void Save()
    {
        EditorUtility.SetDirty(selectedItem);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
