using UnityEditor;

public abstract class ExtendedWindow<T>: EditorWindow where T: EditorWindow
{
    VisualElement root;
    public static void Init(string name)
    {
        T wnd = GetWindow<T>();
        wnd.titleContent = new GUIContent(name);
    }

    public void CreateGUI()
    {
        root = rootVisualElement;
    }
}
