#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UI_Item))]

public class UI_ItemEditor : Editor
{

    private SerializedObject m_Object;
    private SerializedProperty m_Property;

    bool showEquipmentO = true;

    void OnEnable()
    {
        m_Object = new SerializedObject(target);

        if (m_Object.FindProperty("itemOption").stringValue == "Item")
        {
            option = itemOptions.Item;
        }
        else if (m_Object.FindProperty("itemOption").stringValue == "Equipment")
        {
            option = itemOptions.Equipment;
        }
        else if (m_Object.FindProperty("itemOption").stringValue == "Food")
        {
            option = itemOptions.Food;
        }
        else if (m_Object.FindProperty("itemOption").stringValue == "Drink")
        {
            option = itemOptions.Drink;
        }
        if (m_Object.FindProperty("equipmentOption").stringValue == "Helmet")
        {
            eOption = equipmentOptions.Helmet;
        }
        else if (m_Object.FindProperty("equipmentOption").stringValue == "Chest")
        {
            eOption = equipmentOptions.Chest;
        }
        else if (m_Object.FindProperty("equipmentOption").stringValue == "Legs")
        {
            eOption = equipmentOptions.Legs;
        }
        else if (m_Object.FindProperty("equipmentOption").stringValue == "Boots")
        {
            eOption = equipmentOptions.Boots;
        }
    }
    itemOptions option;
    equipmentOptions eOption;
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("");
        EditorGUILayout.LabelField("Ultimate Inventory & Crafting - Item Properties : ");
        EditorGUILayout.LabelField("");

        m_Property = m_Object.FindProperty("itemName");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Item Name : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("maxStack");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Max Stack : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("itemDescription");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Item Description : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("itemIndex");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Item Index : "), true);
        m_Object.ApplyModifiedProperties();
        option = (itemOptions)EditorGUILayout.EnumPopup("Type Of Item :", option);
        m_Object.FindProperty("itemOption").stringValue = option.ToString();
        m_Object.ApplyModifiedProperties();
        if (m_Object.FindProperty("itemOption").stringValue == "Equipment")
        {
            eOption = (equipmentOptions)EditorGUILayout.EnumPopup("Type Of Equipment : ", eOption);
            m_Object.FindProperty("equipmentOption").stringValue = eOption.ToString();
            m_Object.ApplyModifiedProperties();
        }
        m_Property = m_Object.FindProperty("itemWeight");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Item Weight : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("iconPreview");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Icon Preview : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("pickUpItem");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Pickup Key : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("destroyOnUse");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Destroy On Use : "), true);
        m_Object.ApplyModifiedProperties();
        m_Property = m_Object.FindProperty("displayMessage");
        EditorGUILayout.PropertyField(m_Property, new GUIContent("Popup Message When Near : "), true);
        m_Object.ApplyModifiedProperties();

        EditorGUILayout.LabelField("");

        if (m_Object.FindProperty("itemOption").stringValue == "Food")
        {
            m_Property = m_Object.FindProperty("foodValue");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Food Value : "), true);
            m_Object.ApplyModifiedProperties();
            m_Object.FindProperty("destroyOnUse").boolValue = true;
            m_Object.ApplyModifiedProperties();
            m_Property = m_Object.FindProperty("isPoisoned");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Is Poisoned : "), true);
            m_Object.ApplyModifiedProperties();
            m_Property = m_Object.FindProperty("poisonTime");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Poisoning Time : "), true);
            m_Object.ApplyModifiedProperties();
        }

        if (m_Object.FindProperty("itemOption").stringValue == "Drink")
        {
            m_Property = m_Object.FindProperty("thirstValue");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Thirst Value : "), true);
            m_Object.ApplyModifiedProperties();
            m_Property = m_Object.FindProperty("isPoisoned");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Is Poisoned : "), true);
            m_Object.ApplyModifiedProperties();
            m_Property = m_Object.FindProperty("poisonTime");
            EditorGUILayout.PropertyField(m_Property, new GUIContent("Poisoning Time : "), true);
            m_Object.ApplyModifiedProperties();
            m_Object.FindProperty("destroyOnUse").boolValue = true;
            m_Object.ApplyModifiedProperties();
        }
        if (m_Object.FindProperty("itemOption").stringValue == "Equipment")
        {
            if (showEquipmentO == false)
            {
                if (GUILayout.Button("Show Equipment Options : "))
                {
                    showEquipmentO = true;
                }
            }
            else
            {
                if (GUILayout.Button("Hide Equipment Options : "))
                {
                    showEquipmentO = false;
                }
                EditorGUILayout.HelpBox("Item Stats : ", MessageType.Info);
                m_Property = m_Object.FindProperty("health");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Health : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("armor");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Armor : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("strength");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Strength : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("ability");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Ability : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("cold");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Cold : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("speed");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Speed : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("stealth");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Stealth : "), true);
                m_Object.ApplyModifiedProperties();
                EditorGUILayout.HelpBox("Set a value to zero to not include it.", MessageType.None);
                EditorGUILayout.HelpBox("A value CAN be negative.", MessageType.None);
            }
        }
    }
    public enum itemOptions
    {
        Item = 0,
        Equipment = 1,
        Food = 2,
        Drink = 3
    }

    enum equipmentOptions
    {
        Helmet = 0,
        Chest = 1,
        Legs = 2,
        Boots = 3
    }
}

#endif 