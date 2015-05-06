#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UI_DoorSystem))]

public class UI_DoorSystemEditor : Editor {

    private SerializedObject m_Object;
    private SerializedProperty m_Property;

    doorType currentType;
    
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Ultimate Inventory & Crafting - Door System");
        EditorGUILayout.LabelField("");
        currentType = (doorType)EditorGUILayout.EnumPopup("Type Of Door :", currentType);
        m_Object.FindProperty("mode").stringValue = currentType.ToString();
        m_Object.ApplyModifiedProperties();
        EditorGUILayout.LabelField("");
        m_Property = m_Object.FindProperty("isLocked");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Is Locked"), true);
        m_Object.ApplyModifiedProperties();
        if (m_Object.FindProperty("isLocked").boolValue == true)
        {
            m_Property = m_Object.FindProperty("keyID");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Key ID :"), true);
            m_Object.ApplyModifiedProperties();
        }
        m_Property = m_Object.FindProperty("isOpen");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Is Opened"), true);
        m_Object.ApplyModifiedProperties();
        EditorGUILayout.LabelField("");
        if (m_Object.FindProperty("mode").stringValue == "MovingDoor")
        {
            m_Property = m_Object.FindProperty("ClosedPosition");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Closed Position :"), true);
            m_Object.ApplyModifiedProperties();
            m_Property = m_Object.FindProperty("OpenedPosition");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Opened Position :"), true);
            m_Object.ApplyModifiedProperties();
            m_Property = m_Object.FindProperty("moveSpeed");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Moving Speed :"), true);
            m_Object.ApplyModifiedProperties();
        }
        else
        {
            m_Property = m_Object.FindProperty("ClosedRot");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Closed Rotation :"), true);
            m_Object.ApplyModifiedProperties();
            m_Property = m_Object.FindProperty("OpenedRot");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Opened Rotation :"), true);
            m_Object.ApplyModifiedProperties();
            m_Property = m_Object.FindProperty("rotSpeed");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Rotating Speed :"), true);
            m_Object.ApplyModifiedProperties();
        }
        EditorGUILayout.LabelField("");
        m_Property = m_Object.FindProperty("toggleKey");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Toggle Key :"), true);
        m_Object.ApplyModifiedProperties();
        EditorGUILayout.LabelField("");
        m_Property = m_Object.FindProperty("doorPanel");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Door Panel :"), true);
        m_Object.ApplyModifiedProperties();
        EditorGUILayout.LabelField("");
        m_Property = m_Object.FindProperty("openDoor");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Open Door Sound :"), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("closeDoor");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Close Door Sound :"), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("doorLocked");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Locked Door Sound :"), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("unlockDoor");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Unlocking Door Sound :"), true);
        m_Object.ApplyModifiedProperties();
    }

    void OnEnable()
    {
        m_Object = new SerializedObject(target);

        if (m_Object.FindProperty("mode").stringValue == "MovingDoor")
        {
            currentType = doorType.MovingDoor;
        }
        else if (m_Object.FindProperty("mode").stringValue == "RotatingDoor")
        {
            currentType = doorType.RotatingDoor;
        }
    }

    public enum doorType
    {
        MovingDoor = 0,
        RotatingDoor = 1
    }
}
#endif 