#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UI_Lootable))]

public class UI_LootableEditor : Editor
{
    private SerializedObject m_Object;
    private SerializedProperty m_Property;
    int tempSlots = 0;
    void OnEnable()
    {
        m_Object = new SerializedObject(target);
        m_Object.FindProperty("stackArray").arraySize = m_Object.FindProperty("slots").arraySize;
        tempSlots = m_Object.FindProperty("slots").arraySize;
    }
    public override void OnInspectorGUI()
    {
        m_Property = m_Object.FindProperty("containerID");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Container ID : "), true);
        m_Object.ApplyModifiedProperties();
        EditorGUILayout.LabelField("Set Slots : ");
        tempSlots = EditorGUILayout.IntField(tempSlots);
        m_Object.FindProperty("slots").arraySize = tempSlots;
        m_Object.FindProperty("stackArray").arraySize = tempSlots;
        m_Property = m_Object.FindProperty("slots");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Container Slots : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("guiSkin");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("GUI Skin : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("toggleContainerKey");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Toggle Key : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("maxDistance");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Maximum Distance : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("autoToggleDragDrop");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Auto toggle Drag&Drop : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("containerBG");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("GUI Background : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("bgPosition");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Background Position : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("bgSize");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Background Size : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("slotPos");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Slot's Position : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("slotSize");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Slot's Size :  "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("slotScroll");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Slot's Scroll :  "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("isAnimated");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Play Animation : "), true);
        m_Object.ApplyModifiedProperties();
        if (m_Object.FindProperty("isAnimated").boolValue == true)
        {
            m_Property = m_Object.FindProperty("animationHolder");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Animation Holder : "), true);
            m_Object.ApplyModifiedProperties();
            m_Property = m_Object.FindProperty("openAnim");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Open Animation : "), true);
            m_Object.ApplyModifiedProperties();
            m_Property = m_Object.FindProperty("closeAnim");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Close Animation : "), true);
            m_Object.ApplyModifiedProperties();
        }
    }
}
#endif 