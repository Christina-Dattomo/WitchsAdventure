#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UI_BuildingSystemUI))]

public class UI_BuildingSystemUIEditor : Editor {

	private SerializedObject m_Object;
    private SerializedProperty m_Property;

    bool tab1 = false, tab2 = false;
    string name = "Building Name", des = "Description", id = "0", need = "";

    void OnEnable()
    {
        m_Object = new SerializedObject(target);
        need = "1,4" + System.Environment.NewLine + "2,1";
    }
    int selectedItem = 0;
    public override void OnInspectorGUI()
    {
      DrawDefaultInspector();
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Ultimate Inventory & Crafting - Building System UI Properties");
        EditorGUILayout.LabelField("");
        EditorGUILayout.BeginHorizontal();
        GUI.enabled = !tab1;
        if (GUILayout.Button("Properties Tab",GUILayout.Width(150),GUILayout.Height(30)))
        {
            tab1 = true;
        }
        GUI.enabled = !tab2;
        if (GUILayout.Button("Item Manager Tab", GUILayout.Width(150), GUILayout.Height(30)))
        {
            tab2 = true;
        }
        EditorGUILayout.EndHorizontal();
        GUI.enabled = true;
        if (tab1 == true)
        {
            EditorGUILayout.LabelField("");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Properties :");
            if (GUILayout.Button("Close Properties",GUILayout.Width(125),GUILayout.Height(25)))
                tab1 = false;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("");

            m_Property = m_Object.FindProperty("buildingSystem");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Building System UI GO :"), true);
            m_Object.ApplyModifiedProperties();
        }
        if (tab2 == true)
        {
            EditorGUILayout.LabelField("");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Building Items Manager :");
            if (GUILayout.Button("Close Manager", GUILayout.Width(125), GUILayout.Height(25)))
                tab2 = false;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("");
            EditorGUILayout.BeginHorizontal();
             m_Property = m_Object.FindProperty("totalBuildings");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Total Buildings : "), true);
            if (GUILayout.Button("Apply"))
            {
                m_Object.ApplyModifiedProperties();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Selected Building : " + selectedItem.ToString());
            if (GUILayout.Button("Previous", GUILayout.Width(100)))
            {
                try
                {
                    if (selectedItem > 0)
                    {
                        changeC(-1);
                    }
                }
                catch { }
            }
            if (GUILayout.Button("Next", GUILayout.Width(100)))
            {
                try
                {
                    if (selectedItem < m_Object.FindProperty("totalBuildings").intValue)
                    {
                        changeC(1);
                    }
                }
                catch { }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("");
            EditorGUILayout.LabelField("Building Properties");
            EditorGUILayout.LabelField("");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Building Name : ");
            name = EditorGUILayout.TextField(name);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Building Description : ");
            des = EditorGUILayout.TextField(des);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Building ID : ");
            id = EditorGUILayout.TextField(id);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.HelpBox("Note : Building ID is the index of the building on the 'UI_BuildingSystem'", MessageType.None);
            EditorGUILayout.LabelField("Resources Needed :");
            need = EditorGUILayout.TextArea(need);
            EditorGUILayout.HelpBox("Declare what items are needed in order to build an building, type the item's id and then the quantity needed separated by comma. To add multiple items change line.", MessageType.Warning);
            if (GUILayout.Button("Add Building"))
            {
                string output = "";
                output = "<name>" + name + "</name>" + System.Environment.NewLine +
                    "<des>" + des + "</des>" + System.Environment.NewLine +
                    "<id>" + id + "</id>" + System.Environment.NewLine;
            }
        }
    }
    void changeC(int c)
    {
        selectedItem = selectedItem + (c);
        
    }
}
#endif