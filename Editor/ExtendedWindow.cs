using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace PhentrixGames.Editor
{
    public abstract class ExtendedWindow<T, U> : EditorWindow where T : EditorWindow where U : ScriptableObject
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

        /// <summary>
        /// Populates an ItemList visual element with buttons for each scriptableobject of type U
        /// </summary>
        /// <param name="itemList">Name of ItemList visual element</param>
        public void PopulateList(string itemList)
        {
            VisualElement ItemList = root.Q<VisualElement>(itemList);
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

        /// <summary>
        /// Saves and Refreshs AssetDatabase
        /// </summary>
        public void Save()
        {
            EditorUtility.SetDirty(selectedItem);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}