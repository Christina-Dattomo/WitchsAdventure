#if UNITY_EDITOR
/*
 * Ultimate Inventory - By Nightbird0
 * */
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UInventory))]
public class UInventoryEditor : Editor
{
    private SerializedObject m_Object;
    private SerializedProperty m_Property;
    //Editor Variables
    bool showAllItems = false, updateTab = false;
  //  bool armedUnityTab = false;
    bool showCurrentItems = false, propertiesTab = false, guiTab = false, dragDropSettings = false,craftingTab = false;
    bool showWarn = false, audioTab = false, tab3D = false, saveSettings = false;
    bool showHelp = false, equipmentTab = false, showStatus = true, showLabels = true, stackTab = false;
    int warnID = 0;
    int tempSlots;
    float tempWeight;
    string[] tempArray;
    int selectedRecipe = 0;
    string id1 = "0", id2 = "0", id3 = "0", id4 = "0", id5 = "0", id6 = "0", id7 = "0", id8 = "0", id9 = "0", result = "0";
    string info = "";
    int tempIndex = 0;
    bool disableCraft = false;
    int tempRecipe = 0;
    int tempRowLength = 4;
    private static string ReadLine(string text, int lineNumber)
    {
        var reader = new System.IO.StringReader(text);

        string line;
        int currentLineNumber = 0;

        do
        {
            currentLineNumber += 1;
            line = reader.ReadLine();
        }
        while (line != null && currentLineNumber < lineNumber);

        return (currentLineNumber == lineNumber) ? line :
                                                   string.Empty;
    }

    void GeneratePreview()
    {
        for (int r = 0; r < tempSlots / tempRowLength + 1; r++)
        {
            for (int s = 0; s < tempRowLength; s++)
            {
                int thisIndex = r * tempRowLength + s;
                if (thisIndex < tempSlots)
                {
                    if (GUI.Button(new Rect(s * m_Object.FindProperty("slotSize").vector2Value.x, r * m_Object.FindProperty("slotSize").vector2Value.y, m_Object.FindProperty("slotSize").vector2Value.x, m_Object.FindProperty("slotSize").vector2Value.y), ""))
                    {
                     
                    }
                }
            }
        }
    }

    void GetData()
    {
        try
        {
            if (m_Object.FindProperty("slotsID").stringValue.Split('\n').Length != 0)
            {
                string[] tempArray = m_Object.FindProperty("slotsID").stringValue.Split('\n');
                tempIndex = m_Object.FindProperty("slotsID").stringValue.Split('\n').Length;
                string currentData = tempArray[selectedRecipe];
                id1 = (GetStrBetweenTags(currentData, "<1>", "</1>"));
                id2 = (GetStrBetweenTags(currentData, "<2>", "</2>"));
                id3 = (GetStrBetweenTags(currentData, "<3>", "</3>"));
                id4 = (GetStrBetweenTags(currentData, "<4>", "</4>"));
                id5 = (GetStrBetweenTags(currentData, "<5>", "</5>"));
                id6 = (GetStrBetweenTags(currentData, "<6>", "</6>"));
                id7 = (GetStrBetweenTags(currentData, "<7>", "</7>"));
                id8 = (GetStrBetweenTags(currentData, "<8>", "</8>"));
                id9 = (GetStrBetweenTags(currentData, "<9>", "</9>"));
                result = (GetStrBetweenTags(currentData, "<result>", "</result>"));
                info = (GetStrBetweenTags(currentData, "<info>", "</info>"));
                disableCraft = false;
                tempRecipe = selectedRecipe;
            }
            else
            {
                disableCraft = true;
            }
        }
        catch { }
    }
    bool importBool = false;
    public string GetStrBetweenTags(string value, string startTag, string endTag)
    {
        if (value.Contains(startTag) && value.Contains(endTag))
        {
            int index = value.IndexOf(startTag) + startTag.Length;
            return value.Substring(index, value.IndexOf(endTag) - index);
        }
        else
            return null;
    }

    void OnEnable()
    {

        m_Object = new SerializedObject(target);
        m_Object.FindProperty("stackArray").arraySize = m_Object.FindProperty("currentItems").arraySize;
        tempSlots = m_Object.FindProperty("currentItems").arraySize;
        tempWeight = m_Object.FindProperty("maxWeight").floatValue;
        m_Object.FindProperty("hotKeys").arraySize = m_Object.FindProperty("hotKeyItems").intValue;
        m_Object.ApplyModifiedProperties();
        tempRowLength = m_Object.FindProperty("rowLength").intValue;
        GetData();

    }

    public override void OnInspectorGUI()
    {
        
        DrawHeader();
        if (m_Object.FindProperty("hotKeyItems").intValue > m_Object.FindProperty("currentItems").arraySize)
        {
            m_Object.FindProperty("hotKeyItems").intValue = m_Object.FindProperty("currentItems").arraySize;
        }
        else
        {
            m_Object.FindProperty("hotKeys").arraySize = m_Object.FindProperty("hotKeyItems").intValue;
            m_Object.ApplyModifiedProperties();
        }

        Texture2D myText = null;
        myText = Resources.Load("UInventoryIMG") as Texture2D;
        GUILayout.Box(myText);

        EditorGUILayout.LabelField("Ultimate Inventory System - By GreekStudios.");
        EditorGUILayout.LabelField("Thanks for purchasing.");
        EditorGUILayout.LabelField("");
        if (showWarn == false)
        {
            if (propertiesTab == false)
            {
                if (GUILayout.Button("Show UInventory System Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    propertiesTab = true;
                }
            }
            else
            {
                if (GUILayout.Button("Hide UInventory System Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    propertiesTab = false;
                }
                EditorGUILayout.HelpBox("General Settings", MessageType.Info);
                m_Property = m_Object.FindProperty("invName");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Inventory Name : "), true);
                m_Object.ApplyModifiedProperties();
                /*
                m_Property = m_Object.FindProperty("allowStacking");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Allow Stacking : "), true);
                m_Object.ApplyModifiedProperties();
                if (m_Object.FindProperty("allowStacking").boolValue == true)
                {
                    m_Property = m_Object.FindProperty("maxStack");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Max Stack At : "), true);
                    m_Object.ApplyModifiedProperties();
                }
                 * */
                m_Property = m_Object.FindProperty("mainPlayer");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Main Player Gameobject : "), true);
                m_Object.ApplyModifiedProperties();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Inventory Mode : " + m_Object.FindProperty("inventoryMode").stringValue, GUILayout.Width(162));
                if (m_Object.FindProperty("inventoryMode").stringValue == "Slot")
                {

                    if (GUILayout.Button("Change To Weight Mode",GUILayout.Width(169)))
                    {
                        m_Object.FindProperty("inventoryMode").stringValue = "Weight";
                        m_Object.ApplyModifiedProperties();
                        m_Object.FindProperty("currentItems").arraySize = 100;
                        m_Object.ApplyModifiedProperties();
                        tempSlots = 100;
                        m_Object.FindProperty("hotKeys").arraySize = 100;
                        m_Object.ApplyModifiedProperties();
                    }
                }
                else
                {
                    if (GUILayout.Button("Change To Slot Mode", GUILayout.Width(169)))
                    {
                        m_Object.FindProperty("inventoryMode").stringValue = "Slot";
                        m_Object.ApplyModifiedProperties();
                        m_Object.FindProperty("currentItems").arraySize = 9;
                        m_Object.ApplyModifiedProperties();
                        tempSlots = 9;
                        m_Object.FindProperty("hotKeys").arraySize = tempSlots;
                        m_Object.ApplyModifiedProperties();
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (m_Object.FindProperty("inventoryMode").stringValue == "Slot")
                {
                    EditorGUILayout.LabelField("Inventory Slots : ");
                    EditorGUILayout.HelpBox("Here you can set your inventory slots:", MessageType.None,true);
                    EditorGUILayout.BeginHorizontal();
                    tempSlots = int.Parse(EditorGUILayout.TextField(tempSlots.ToString(), GUILayout.Width(100)));
                    if (GUILayout.Button("Set Slots", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("currentItems").arraySize = tempSlots;
                        m_Object.ApplyModifiedProperties();
                        m_Object.FindProperty("hotKeys").arraySize = tempSlots;
                        m_Object.ApplyModifiedProperties();
                        PlayerPrefs.SetInt("tempSlots", tempSlots);
                        PlayerPrefs.SetInt("tempRawLength", tempRowLength);
                        PlayerPrefs.SetFloat("sizex", m_Object.FindProperty("slotSize").vector2Value.x);
                        PlayerPrefs.SetFloat("sizey", m_Object.FindProperty("slotSize").vector2Value.y);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    EditorGUILayout.LabelField("Inventory Max Weight : ");
                    EditorGUILayout.HelpBox("Here you can set your inventory max weight, (Note : The inventory slots number will be overwritten) :", MessageType.None);
                    EditorGUILayout.BeginHorizontal();
                    tempWeight = float.Parse(EditorGUILayout.TextField(tempWeight.ToString(), GUILayout.Width(100)));
                    if (GUILayout.Button("Set Max Weight", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("maxWeight").floatValue = tempWeight;
                    }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.LabelField("");
                EditorGUILayout.LabelField("Row Length :");
                EditorGUILayout.HelpBox("Here you can set the length of each row", MessageType.None, true);
                EditorGUILayout.BeginHorizontal();
                tempRowLength = int.Parse(EditorGUILayout.TextField(tempRowLength.ToString(), GUILayout.Width(100)));
                if (GUILayout.Button("Set Row Length", GUILayout.Width(165)))
                {
                    m_Object.FindProperty("rowLength").intValue = tempRowLength;
                    m_Object.ApplyModifiedProperties();
                    PlayerPrefs.SetInt("tempSlots", tempSlots);
                    m_Object.ApplyModifiedProperties();
                    PlayerPrefs.SetInt("tempRowLength", tempRowLength);
                    PlayerPrefs.SetFloat("sizex", m_Object.FindProperty("slotSize").vector2Value.x);
                    PlayerPrefs.SetFloat("sizey", m_Object.FindProperty("slotSize").vector2Value.y);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.LabelField("");
                if (GUILayout.Button("Show Preview", GUILayout.Height(30)))
                {
                    PlayerPrefs.SetInt("tempSlots", tempSlots);
                    PlayerPrefs.SetInt("tempRawLength", tempRowLength);
                    PlayerPrefs.SetFloat("sizex", m_Object.FindProperty("slotSize").vector2Value.x);
                    PlayerPrefs.SetFloat("sizey", m_Object.FindProperty("slotSize").vector2Value.y);
                    UIPreview window = (UIPreview)EditorWindow.GetWindow(typeof(UIPreview),false,"Preview",true);
                    window.maxSize = new Vector2(800, 600);
                    window.minSize = window.maxSize;
                }
                
                EditorGUILayout.LabelField("");
                m_Property = m_Object.FindProperty("toggleKey");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Toggle Key :"), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("autoClose");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Auto Toggle Off : "), true);
                m_Object.ApplyModifiedProperties();
                if (m_Object.FindProperty("autoClose").boolValue == true)
                {
                    m_Property = m_Object.FindProperty("autoCloseTime");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Auto Close Time : "), true);
                    m_Object.ApplyModifiedProperties();
                }
                m_Property = m_Object.FindProperty("customActivate");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Custom Activate :"), true);
                m_Object.ApplyModifiedProperties();
                EditorGUILayout.HelpBox("Using custom mode you can write your own script that will be activated when you select an item from your inventory! If you uncheck it then when you select an item from your inventory it will set the 'active' boolean to 'true' and all the other items to 'false'. Edit the 'UI_CustomActivation.cs', check the documentation pdf for more.", MessageType.None);
                m_Property = m_Object.FindProperty("iconPreview");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Use Icons For Items : "), true);
                m_Object.ApplyModifiedProperties();
                if (m_Object.FindProperty("iconPreview").boolValue == true)
                {
                    m_Property = m_Object.FindProperty("emptyIcon");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Icon For Empty Slots :"), true);
                    m_Object.ApplyModifiedProperties();
                }
                EditorGUILayout.HelpBox("Check It To Display Icons For All The Equipped Items, And Add An Icon For All The Empty Slots (Optional). Assign Icons For Items On The 'UI_Item.cs' Script!", MessageType.None);
                m_Property = m_Object.FindProperty("closeAfterDrop");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Auto Close After Drop :"), true);
                m_Object.ApplyModifiedProperties();
                EditorGUILayout.HelpBox("Check It To Automatic Toggle Off The Inventory After Dropping An Item", MessageType.None);
                EditorGUILayout.LabelField("");
                EditorGUILayout.HelpBox("Hotkeys Menu Settings : ", MessageType.Info);
                m_Property = m_Object.FindProperty("fastMenu");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Hotkey Menu : "), true);
                m_Object.ApplyModifiedProperties();
                if (m_Object.FindProperty("fastMenu").boolValue == true)
                {
                  
                    m_Property = m_Object.FindProperty("hotKeyItems");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("The Number Of Items On Hotkey Menu : "), true);
                    m_Object.ApplyModifiedProperties();
                    EditorGUILayout.HelpBox("If you don't want to have hotkey for a slot then leave it empty!",MessageType.None);
                    m_Property = m_Object.FindProperty("hotKeys");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("All Hotkey Shortcuts"), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("dontDrawEmpty");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Don't Show Empty Slots : "), true);
                    m_Object.ApplyModifiedProperties();
                }

                m_Property = m_Object.FindProperty("showHideCursor");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Show/Hide cursor on toggle : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("disableMouseLook");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Enable/Disable mouse look script : "), true);
                m_Object.ApplyModifiedProperties();

                GUILayout.Box("END OF SYSTEM SETTINGS", GUILayout.Width(345));
                EditorGUILayout.LabelField("");
            }
            if (showAllItems == true)
            {
                if (GUILayout.Button("Hide All Items", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    showAllItems = false;
                }
                EditorGUILayout.HelpBox("Here you can assign all the items for your inventory :", MessageType.None);
                m_Property = m_Object.FindProperty("allItems");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("All Items Array"), true);
                m_Object.ApplyModifiedProperties();
                m_Object.FindProperty("stackArray").arraySize = m_Object.FindProperty("allItems").arraySize;
                m_Object.ApplyModifiedProperties();
                if (GUILayout.Button("Reset All Items", GUILayout.Width(335)))
                {
                    warnID = 1;
                    showWarn = true;
                }
                EditorGUILayout.HelpBox("All items should have 'UI_Item.cs' script attached!", MessageType.Info);
              /*
               *   EditorGUILayout.HelpBox("Here you can assign all the items for your inventory.", MessageType.None);
                m_Property = m_Object.FindProperty("allItemNames");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("All Item Names"), true);
                m_Object.ApplyModifiedProperties();
                if (GUILayout.Button("Reset All Item Names", GUILayout.Width(335)))
                {
                    warnID = 2;
                    showWarn = true;
                }
                EditorGUILayout.HelpBox("Remember, The name on element 0 is the name of the game object that is assigned on the element 0 above.", MessageType.None);
               * */

                GUILayout.Box("END OF ALL ITEMS",GUILayout.Width(345));
                EditorGUILayout.LabelField("");
            }
            else
            {
                if (GUILayout.Button("Show All Items", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    showAllItems = true;
                }
            }

            if (showCurrentItems == false)
            {
                if (GUILayout.Button("Show Equipped Items", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    showCurrentItems = true;
                }
            }
            else
            {
                if (GUILayout.Button("Hide Equipped Items", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    showCurrentItems = false;
                }
                EditorGUILayout.HelpBox("Here are all the equipped items :", MessageType.None);
                m_Property = m_Object.FindProperty("currentItems");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Equipped Items :"), true);
                m_Object.ApplyModifiedProperties();
                if (GUILayout.Button("Reset All Equipped Items", GUILayout.Width(335)))
                {
                    warnID = 3;
                    showWarn = true;
                }
                GUILayout.Box("END OF EQUIPPED ITEMS", GUILayout.Width(345));
                EditorGUILayout.LabelField("");
            }
            if (guiTab == false)
            {
                if (GUILayout.Button("Show GUI Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    guiTab = true;
                }
            }
            else
            {
                if (GUILayout.Button("Hide GUI Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    guiTab = false;
                }
                if (m_Object.FindProperty("customSettings").boolValue == false)
                {
                   /* if (GUILayout.Button("Enable Custom Settings",GUILayout.Width(335)))
                    {
                        m_Object.FindProperty("customSettings").boolValue = true;
                        m_Object.ApplyModifiedProperties();
                    }*/
                    EditorGUILayout.HelpBox("Build-In Settings : ",MessageType.Warning);

                    EditorGUILayout.HelpBox("Main GUI Dock : " + m_Object.FindProperty("mainGUIPos").stringValue, MessageType.Info);
                    EditorGUILayout.LabelField("Dock To :");
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Top Left", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("mainGUIPos").stringValue = "Top Left";
                        m_Object.ApplyModifiedProperties();
                    }
                    if (GUILayout.Button("Top Right", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("mainGUIPos").stringValue = "Top Right";
                        m_Object.ApplyModifiedProperties();
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Bottom Left", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("mainGUIPos").stringValue = "Bottom Left";
                        m_Object.ApplyModifiedProperties();
                    }
                    if (GUILayout.Button("Bottom Right", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("mainGUIPos").stringValue = "Bottom Right";
                        m_Object.ApplyModifiedProperties();
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Center Screen", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("mainGUIPos").stringValue = "Center Screen";
                        m_Object.ApplyModifiedProperties();
                    }
                    if (GUILayout.Button("Custom", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("mainGUIPos").stringValue = "Custom";
                        m_Object.ApplyModifiedProperties();
                    }
                    EditorGUILayout.EndHorizontal();
                    if (m_Object.FindProperty("mainGUIPos").stringValue == "Custom")
                    {
                        EditorGUILayout.HelpBox("Inventory Position : ", MessageType.None);
                        m_Property = m_Object.FindProperty("PixelOffSet");
                        EditorGUILayout.PropertyField(m_Property, new GUIContent("Buttons Offset : "), true);
                        m_Object.ApplyModifiedProperties();
                        EditorGUILayout.HelpBox("Scroll View Settings, X is controlling the scrollbar position, Y is controlling the scroll view size",MessageType.Info);
                        m_Property = m_Object.FindProperty("scrollSize");
                        EditorGUILayout.PropertyField(m_Property, new GUIContent("Scroll Settings : "), true);
                        m_Object.ApplyModifiedProperties();
                    }
                    EditorGUILayout.HelpBox("Hotkeys GUI Dock : " + m_Object.FindProperty("hotKeyGUIPos").stringValue, MessageType.Info);
                    EditorGUILayout.LabelField("Dock To :");
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Top Left", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("hotKeyGUIPos").stringValue = "Top Left";
                        m_Object.ApplyModifiedProperties();
                    }
                    if (GUILayout.Button("Bottom Left", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("hotKeyGUIPos").stringValue = "Bottom Left";
                        m_Object.ApplyModifiedProperties();
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Top Center", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("hotKeyGUIPos").stringValue = "Top Center";
                        m_Object.ApplyModifiedProperties();
                    }
                    if (GUILayout.Button("Bottom Center", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("hotKeyGUIPos").stringValue = "Bottom Center";
                        m_Object.ApplyModifiedProperties();
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndHorizontal();
                    if (GUILayout.Button("Custom", GUILayout.Width(165)))
                    {
                        m_Object.FindProperty("hotKeyGUIPos").stringValue = "Custom";
                        m_Object.ApplyModifiedProperties();
                    }
                    if (m_Object.FindProperty("hotKeyGUIPos").stringValue == "Custom")
                    {
                        EditorGUILayout.HelpBox("Hotkeys Menu Position : ", MessageType.None);
                        m_Property = m_Object.FindProperty("PixelOffSet2");
                        EditorGUILayout.PropertyField(m_Property, new GUIContent("Pixel Off Set : "), true);
                        m_Object.ApplyModifiedProperties();
                    }
                    EditorGUILayout.HelpBox("Style Settings", MessageType.Info);
                    EditorGUILayout.HelpBox("Main Background Image : ", MessageType.None);
                    m_Property = m_Object.FindProperty("backGroundIMG");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("BG Image :"), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("PixelOffSet3");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("BG Image Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("mainBGSize");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("BG Image Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    EditorGUILayout.HelpBox("Quick Background Image : ", MessageType.None);
                    m_Property = m_Object.FindProperty("quickBackGroundIMG");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Quick BG Image :"), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("quickBGPos");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Quick BG Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("quickBGSize");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Quick BG Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    EditorGUILayout.HelpBox("Inventory Slots : ", MessageType.None);
                    m_Property = m_Object.FindProperty("slotSize");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Slot Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("slotSize2");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Hotkeys Slot Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("emptySlotText");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Text On Empty Slot : "), true);
                    m_Object.ApplyModifiedProperties();
                   
                    if (m_Object.FindProperty("inventoryMode").stringValue == "Weight")
                    {
                        m_Property = m_Object.FindProperty("totalWeightLabel");
                        EditorGUILayout.PropertyField(m_Property, new GUIContent("Total Weight Position : "), true);
                        m_Object.ApplyModifiedProperties();
                    }



                }
                else
                {
                    
                   
                }
                m_Property = m_Object.FindProperty("GUISkin");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("GUI Skin : "), true);
                m_Object.ApplyModifiedProperties();

                EditorGUILayout.HelpBox("Item's Preview Settings : ", MessageType.Info);
                m_Property = m_Object.FindProperty("itemPreviewPos");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Preview Box Position : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("itemPreviewSize");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Preview Box Size : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("displayStats");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Display item's stats : "), true);
                m_Object.ApplyModifiedProperties();

                //_________________________________________________________________________________________
                GUILayout.Box("END OF GUI Settings", GUILayout.Width(345));
                EditorGUILayout.LabelField("");
            }

            if (audioTab == false)
            {
                if (GUILayout.Button("Show Audio Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    audioTab = true;
                }
            }
            else
            {
                if (GUILayout.Button("Hide Audio Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    audioTab = false;
                }
                m_Property = m_Object.FindProperty("pickUpSound");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Pickup Item Sound : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("dropSound");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Drop Item Sound : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("useSound");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Use Item Sound : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("cantPickUp");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Full Sound : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("volume");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Volume : "), true);
                m_Object.ApplyModifiedProperties();
            }
            if (dragDropSettings == false)
            {
                if (GUILayout.Button("Show Drag & Drop Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    dragDropSettings = true;
                }
            }
            else
            {
                if (GUILayout.Button("Hide Drag & Drop Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    dragDropSettings = false;
                }
                EditorGUILayout.HelpBox("Drag & Drop Settings", MessageType.Info);
                m_Property = m_Object.FindProperty("enabledDragDrop");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Enable Drag & Drop : "), true);
                m_Object.ApplyModifiedProperties();

                if (m_Object.FindProperty("enabledDragDrop").boolValue == true)
                {
                    m_Property = m_Object.FindProperty("dragMode");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Drag & Drop Toggle Key : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("dragIconSize");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Slot Icon Size : "), true);
                    m_Object.ApplyModifiedProperties();
                }
            }
            if (craftingTab == false)
            {
                if (GUILayout.Button("Show Crafting Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    craftingTab = true;
                }
            }
            else
            {
                if (GUILayout.Button("Hide Crafting Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    craftingTab = false;
                }
                EditorGUILayout.HelpBox("Crafting Recipes :", MessageType.Info);
                m_Property = m_Object.FindProperty("allowCrafting");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Enable Crafting : "), true);
                m_Object.ApplyModifiedProperties();
                if (m_Object.FindProperty("allowCrafting").boolValue == true)
                {
                    EditorGUILayout.BeginHorizontal();
                    //     EditorGUILayout.TextArea(m_Object.FindProperty("slotsID").stringValue,GUILayout.Width(200),GUILayout.Height(100));
                    EditorGUILayout.LabelField("Recipes : ", GUILayout.Width(70));
                    tempIndex = EditorGUILayout.IntField(tempIndex, GUILayout.Width(100));
                    if (GUILayout.Button("Apply"))
                    {
                        try
                        {
                            m_Object.FindProperty("craftingRecipes").arraySize = tempIndex;
                            m_Object.ApplyModifiedProperties();

                            if (disableCraft == false)
                            {
                                string final = "";
                                for (int i = 0; i < tempIndex; i++)
                                {
                                    string recipe = "<1></1> <2></2> <3></3> <4></4> <5></5> <6></6> <7></7> <8></8> <9></9> <result></result> <info></info>";
                                    final += recipe + System.Environment.NewLine;
                                }
                                final = final.Trim();
                                m_Object.FindProperty("slotsID").stringValue = final;
                            }
                        }
                        catch { }
                    }

                    EditorGUILayout.EndHorizontal();
                   
                    EditorGUILayout.HelpBox("If you modify the number of recipes they current recipes would be removed!", MessageType.Warning);

                    /*
                     *  m_Property = m_Object.FindProperty("craftingRecipes");
                     EditorGUILayout.PropertyField(m_Property, new GUIContent("Crafting Recipes : "), true);
                     m_Object.ApplyModifiedProperties();
                     //    EditorGUILayout.HelpBox("Set the id of the objects you want for each slot when crafting, let it empty for null. Remember each 9 elements are the 9 slots of each recipe!", MessageType.Warning);
                     m_Property = m_Object.FindProperty("slotsID");
                     EditorGUILayout.PropertyField(m_Property, new GUIContent("Craft Combination "), true);
                     m_Object.ApplyModifiedProperties();
                     * */
                    if (disableCraft == true)
                    {
                        GUI.enabled = false;
                    }
                    else
                    {
                        GUI.enabled = true;
                    }
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Selected Recipe : " + selectedRecipe.ToString());
                    if (GUILayout.Button("Previous", GUILayout.Width(100)))
                    {
                        try
                        {
                            if (selectedRecipe > 0)
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
                            if (selectedRecipe < m_Object.FindProperty("slotsID").stringValue.Split('\n').Length)
                            {
                                changeC(1);
                            }
                        }
                        catch { }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Jump To Recipe : ", GUILayout.Width(101));
                    tempRecipe = EditorGUILayout.IntField(tempRecipe, GUILayout.Width(116));
                    if (GUILayout.Button("Jump", GUILayout.Width(100)))
                    {
                        if (tempRecipe > 0 && tempRecipe <= m_Object.FindProperty("slotsID").stringValue.Split('\n').Length)
                        {
                            selectedRecipe = tempRecipe;
                            GetData();
                        }
                        else
                        {
                            tempRecipe = selectedRecipe;
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.LabelField("");
                    EditorGUILayout.LabelField("Recipe Content : ");

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Slot 1 :", GUILayout.Width(50)); id1 = EditorGUILayout.TextField(id1, GUILayout.Width(50));
                    //   EditorGUILayout.EndHorizontal();
                    // EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Slot 2 :", GUILayout.Width(50)); id2 = EditorGUILayout.TextField(id2, GUILayout.Width(50));
                    // EditorGUILayout.EndHorizontal();
                    // EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Slot 3 :", GUILayout.Width(50)); id3 = EditorGUILayout.TextField(id3, GUILayout.Width(50));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Slot 4 :", GUILayout.Width(50)); id4 = EditorGUILayout.TextField(id4, GUILayout.Width(50));
                    // EditorGUILayout.EndHorizontal();
                    //  EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Slot 5 :", GUILayout.Width(50)); id5 = EditorGUILayout.TextField(id5, GUILayout.Width(50));
                    // EditorGUILayout.EndHorizontal();
                    // EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Slot 6 :", GUILayout.Width(50)); id6 = EditorGUILayout.TextField(id6, GUILayout.Width(50));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Slot 7 :", GUILayout.Width(50)); id7 = EditorGUILayout.TextField(id7, GUILayout.Width(50));
                    //  EditorGUILayout.EndHorizontal();
                    //  EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Slot 8 :", GUILayout.Width(50)); id8 = EditorGUILayout.TextField(id8, GUILayout.Width(50));
                    //  EditorGUILayout.EndHorizontal();
                    //  EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Slot 9 :", GUILayout.Width(50)); id9 = EditorGUILayout.TextField(id9, GUILayout.Width(50));
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.LabelField("Result Item : ", GUILayout.Width(100)); result = EditorGUILayout.TextField(result, GUILayout.Width(100));
                    EditorGUILayout.LabelField("Description : ", GUILayout.Width(100)); info = EditorGUILayout.TextArea(info, GUILayout.Width(250), GUILayout.Height(50));
                    EditorGUILayout.LabelField("");
                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("Save Recipe", GUILayout.Width(120), GUILayout.Height(30)))
                    {
                        try
                        {
                            string toSave = "<1>" + id1 + "</1> <2>" + id2 + "</2> <3>" + id3 + "</3> <4>" + id4 + "</4> <5>" + id5 + "</5> <6>" + id6 + "</6> <7>" + id7 + "</7> <8>" + id8 + "</8> <9>" + id9 + "</9> <result>" + result + "</result> <info>" + info + "</info>";
                            string[] allLines = m_Object.FindProperty("slotsID").stringValue.Split('\n');
                            allLines[selectedRecipe] = toSave;
                            m_Object.FindProperty("slotsID").stringValue = "";
                            for (int i = 0; i < allLines.Length; i++)
                            {
                                m_Object.FindProperty("slotsID").stringValue += allLines[i] + System.Environment.NewLine;
                            }
                            m_Object.FindProperty("slotsID").stringValue = m_Object.FindProperty("slotsID").stringValue.Trim();
                            m_Object.ApplyModifiedProperties();
                            System.IO.File.WriteAllText("C:\\Users\\John\\Desktop\\b2.txt", m_Object.FindProperty("slotsID").stringValue);
                            GetData();
                        }
                        catch { }
                    }
                    if (GUILayout.Button("Import Recipes", GUILayout.Width(120), GUILayout.Height(30)))
                    {
                        importBool = !importBool;
                    }
                    if (GUILayout.Button("Export Recipes", GUILayout.Width(120), GUILayout.Height(30)))
                    {
                        string toSave = m_Object.FindProperty("slotsID").stringValue;
                        try
                        {
                            System.IO.File.WriteAllText(System.Environment.CurrentDirectory + "\\UltimateInventory_Crafting_Recipes.txt", toSave, System.Text.Encoding.Unicode);
                            Debug.Log("Your recipes has been exported at your project directory [ " + System.Environment.CurrentDirectory + " ]");
                        }
                        catch (System.Exception exception)
                        {
                            Debug.LogException(exception);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    if (importBool == true)
                    {
                        try
                        {
                            if (System.IO.File.Exists(System.Environment.CurrentDirectory + "\\UltimateInventory_Crafting_Recipes.txt") == false)
                            {
                                EditorGUILayout.HelpBox("Place the 'UltimateInventory_Crafting_Recipes.txt' into your project directory : [ " + System.Environment.CurrentDirectory + " ]", MessageType.Info);
                            }
                            else
                            {
                                EditorGUILayout.HelpBox("Recipes file has been found ( " + System.Environment.CurrentDirectory + "\\UltimateInventory_Crafting_Recipes.txt" + " ), click 'Import' to continue!", MessageType.Info);
                                EditorGUILayout.BeginHorizontal();
                                if (GUILayout.Button("Import", GUILayout.Width(150), GUILayout.Height(25)))
                                {
                                    string toLoad = System.IO.File.ReadAllText(System.Environment.CurrentDirectory + "\\UltimateInventory_Crafting_Recipes.txt");
                                    m_Object.FindProperty("slotsID").stringValue = toLoad;
                                    m_Object.ApplyModifiedProperties();
                                    importBool = false;
                                    Debug.Log("Recipes has been loaded, refresh the inspector to continue!");
                                    
                                }
                                if (GUILayout.Button("Cancel", GUILayout.Width(150), GUILayout.Height(25)))
                                {
                                    importBool = false;
                                }
                                EditorGUILayout.EndHorizontal();
                            }
                        }
                        catch (System.Exception e)
                        { 
                            Debug.LogException(e);
                            EditorGUILayout.HelpBox("Error, [ " + e.Message + " ]", MessageType.Error);
                        }
                    }
                    EditorGUILayout.LabelField("");
                    EditorGUILayout.HelpBox("Crafting Settings : ", MessageType.Info);
                    m_Property = m_Object.FindProperty("toggleCrafting");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Toggle Key : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("craftingGUIPos");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Craft Slots Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("craftingSize");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Scroll Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("craftingResultPos");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Result Slot Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("craftingBG");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Crafting Background : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("craftingBGpos");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Background Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("craftingBGSize");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Background Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("infoPos");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Item Description Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("infoSize");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Item Description Size : "), true);
                    m_Object.ApplyModifiedProperties();


                    GUI.enabled = true;
                }
                else
                {
                }
                EditorGUILayout.LabelField("");
            }
            if (saveSettings == false)
            {
                if (GUILayout.Button("Show Save & Load Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    saveSettings = true;
                }
            }
            else
            {
                if (GUILayout.Button("Hide Save & Load Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    saveSettings = false;
                }
                EditorGUILayout.HelpBox("Encryption Settings : ",MessageType.Info);
                m_Property = m_Object.FindProperty("PasswordHash");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Password Hash : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("SaltKey");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Salt Key : "), true);
                m_Object.ApplyModifiedProperties();
                EditorGUILayout.LabelField("");

            }
            if (equipmentTab == false)
            {
                if (GUILayout.Button("Show Player Equipment Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    equipmentTab = true;
                }
            }
            else if (equipmentTab == true)
            {
                if (GUILayout.Button("Hide Player Equipment Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    equipmentTab = false;
                }
                EditorGUILayout.HelpBox("Player Equipment : ", MessageType.Info);
                EditorGUILayout.LabelField("");
                m_Property = m_Object.FindProperty("allowPlayerEquipment");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Allow Player Equipment : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("equipmentToggleKey");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Toggle Key : "), true);
                m_Object.ApplyModifiedProperties();
                if (showStatus == true)
                {
                    if (GUILayout.Button("Hide Player Stats Options : "))
                    {
                        showStatus = false;
                    }
                    m_Property = m_Object.FindProperty("aHealth");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Stats : Allow Health"), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("aArmor");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Stats : Allow Armor"), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("aStrength");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Stats : Allow Strength"), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("aAbility");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Stats : Allow Ability"), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("aCold");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Stats : Allow Cold"), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("aSpeed");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Stats : Allow Speed"), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("aStealth");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Stats : Allow Stealth"), true);
                    m_Object.ApplyModifiedProperties();
                }
                else
                {
                    if (GUILayout.Button("Show Player Stats Options : "))
                    {
                        showStatus = true;
                    }
                }
                EditorGUILayout.HelpBox("Player Equipment GUI :  ", MessageType.Info);
                m_Property = m_Object.FindProperty("equipmentWindowBG");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Background Image : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("equipmentWindowPos");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Background Position : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("equipmentWindowSize");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Background Size  : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("slotsPosition");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Equipment Slots Position : "), true);
                m_Object.ApplyModifiedProperties();
                m_Property = m_Object.FindProperty("equipmentSlotSize");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Slot Size : "), true);
                m_Object.ApplyModifiedProperties();
                if (showLabels == false)
                {
                    if (GUILayout.Button("Show Label Settings : ", GUILayout.Width(335), GUILayout.Height(22)))
                    {
                        showLabels = true;
                    }
                }
                else
                {
                    if (GUILayout.Button("Hide Label Settings : ", GUILayout.Width(335), GUILayout.Height(22)))
                    {
                        showLabels = false;
                    }
                    EditorGUILayout.HelpBox("Effects Label : ", MessageType.None);
                    m_Property = m_Object.FindProperty("equipmentDataString8");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Effects Label : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataPos8");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Effects Label Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataSize8");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Effects Label Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    GUI.enabled = true;
                    EditorGUILayout.HelpBox("Health Label : ", MessageType.None);
                    GUI.enabled = m_Object.FindProperty("aHealth").boolValue;
                    m_Property = m_Object.FindProperty("equipmentDataString1");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Health Label : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataPos1");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Health Label Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataSize1");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Health Label Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    GUI.enabled = true;
                    EditorGUILayout.HelpBox("Armor Label : ", MessageType.None);
                    GUI.enabled = m_Object.FindProperty("aArmor").boolValue;
                    m_Property = m_Object.FindProperty("equipmentDataString2");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Armor Label : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataPos2");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Armor Label Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataSize2");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Armor Label Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    GUI.enabled = true;
                    EditorGUILayout.HelpBox("Strength Label : ", MessageType.None);
                    GUI.enabled = m_Object.FindProperty("aStrength").boolValue;
                    m_Property = m_Object.FindProperty("equipmentDataString3");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Strength Label : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataPos3");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Strength Label Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataSize3");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Strength Label Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    GUI.enabled = true;
                    EditorGUILayout.HelpBox("Ability Label : ", MessageType.None);
                    GUI.enabled = m_Object.FindProperty("aAbility").boolValue;
                    m_Property = m_Object.FindProperty("equipmentDataString4");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Ability Label : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataPos4");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Ability Label Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataSize4");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Ability Label Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    GUI.enabled = true;
                    EditorGUILayout.HelpBox("Cold Label : ", MessageType.None);
                    GUI.enabled = m_Object.FindProperty("aCold").boolValue;
                    m_Property = m_Object.FindProperty("equipmentDataString5");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Cold Label : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataPos5");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Cold Label Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataSize5");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Cold Label Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    GUI.enabled = true;
                    EditorGUILayout.HelpBox("Speed Label : ", MessageType.None);
                    GUI.enabled = m_Object.FindProperty("aSpeed").boolValue;
                    m_Property = m_Object.FindProperty("equipmentDataString6");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Speed Label : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataPos6");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Speed Label Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataSize6");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Speed Label Size : "), true);
                    m_Object.ApplyModifiedProperties();
                    GUI.enabled = true;
                    EditorGUILayout.HelpBox("Stealth Label : ", MessageType.None);
                    GUI.enabled = m_Object.FindProperty("aStealth").boolValue;
                    m_Property = m_Object.FindProperty("equipmentDataString7");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Stealth Label : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataPos7");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Stealth Label Position : "), true);
                    m_Object.ApplyModifiedProperties();
                    m_Property = m_Object.FindProperty("equipmentDataSize7");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Stealth Label Size : "), true);
                    m_Object.ApplyModifiedProperties();
                }
                

            }
            if (stackTab == false)
            {
                if (GUILayout.Button("Show Stacking Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    stackTab = true;
                }
            }
            else
            {
                if (GUILayout.Button("Hide Stacking Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    stackTab = false;
                }
                m_Property = m_Object.FindProperty("allowStacking");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Allow Stacking : "), true);
                m_Object.ApplyModifiedProperties();
                if (m_Object.FindProperty("allowStacking").boolValue == true)
                {
                    m_Property = m_Object.FindProperty("maxStack");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Maximum Stack Amount : "), true);
                    m_Object.ApplyModifiedProperties();
                }
            }
            /*
            if (tab3D == false)
            {
                GUI.enabled = false;
                if (GUILayout.Button("Show 3D Preview Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    tab3D = true;
                }
            }
            else
            {
                if (GUILayout.Button("Hide 3D Preview Settings", GUILayout.Width(335), GUILayout.Height(32)))
                {
                    tab3D = false;
                }
                m_Property = m_Object.FindProperty("allow3DPreview");
                EditorGUILayout.PropertyField(m_Property, new GUIContent("Allow 3D Preview : "), true);
                m_Object.ApplyModifiedProperties();
                if (m_Object.FindProperty("allow3DPreview").boolValue == true)
                {
                    m_Property = m_Object.FindProperty("k3DToggle");
                    EditorGUILayout.PropertyField(m_Property, new GUIContent("Toggle Key For 3D Preview : "), true);
                    m_Object.ApplyModifiedProperties();
                }
           //     EditorGUILayout.HelpBox("If You Allow 3D Preview Of Equipped Items Then When The User Press : " + m_Object.FindProperty("k3DToggle").stringValue + " And Then Double Click The Item He Wants He Have a 3D Preview Of The Selected Item.", MessageType.Info);
            }
             */
            EditorGUILayout.LabelField("");
            GUI.enabled = true;
            EditorGUILayout.BeginHorizontal();
            if (updateTab == false)
            {
                if (GUILayout.Button("About UInventory", GUILayout.Width(165)))
                {
                    updateTab = true;
                }
            }
            else
            {
                if (GUILayout.Button("Close About Tab", GUILayout.Width(165)))
                {
                    updateTab = false;
                }
            }
            /*
            if (armedUnityTab == false)
            {
                if (GUILayout.Button("About ArmedUnity.com", GUILayout.Width(165)))
                {
                    armedUnityTab = true;
                }
            } 
             * */
            if (showHelp == false)
            {
                if (GUILayout.Button("Help", GUILayout.Width(165)))
                {
                    showHelp = true;
                }
            }
            else
            {
                if (GUILayout.Button("Close Help", GUILayout.Width(165)))
                {
                    showHelp = false;
                }
            }
            EditorGUILayout.EndHorizontal();
            if (showHelp == true)
            {
                EditorGUILayout.HelpBox("In the main folder of UInventory System is a documentation.pdf file, if you still need help then email at : paraskevlos@yahoo.gr", MessageType.Warning);
            }
            if (updateTab == true)
            {
                EditorGUILayout.LabelField("");
                EditorGUILayout.BeginVertical();
                EditorGUILayout.HelpBox("Ultimate Inventory & Crafting - About",MessageType.None);
                EditorGUILayout.LabelField("By : GreekStudios");
                EditorGUILayout.LabelField("Version : IV Beta ");
                EditorGUILayout.LabelField("Email : paraskevlos@yahoo.gr");
                EditorGUILayout.EndVertical();
                EditorGUILayout.BeginHorizontal("");
                if (GUILayout.Button("http://greekstudios.url.ph/", GUILayout.Width(335)))
                {
                }
              
                EditorGUILayout.EndHorizontal();
            }
            /*
            if (armedUnityTab == true)
            {
                EditorGUILayout.LabelField("");
                EditorGUILayout.HelpBox("ArmedUnity is one of the biggest communities about Unity3d and by far the most helpful and friendly. Please click the following button to visit www.armedunity.com", MessageType.None);
                EditorGUILayout.BeginHorizontal("");
                if (GUILayout.Button("Visit ArmedUnity", GUILayout.Width(165)))
                {
                    try
                    {
                        System.Diagnostics.Process.Start("http://www.armedunity.com");
                    }
                    catch { }
                }
                if (GUILayout.Button("Close ArmedUnity Tab", GUILayout.Width(165)))
                {
                    armedUnityTab = false;
                }
                EditorGUILayout.EndHorizontal();
            }
             * */

            
        }
        else
        {
            if (warnID == 1)
            {
                EditorGUILayout.HelpBox("Are you sure you want to reset the 'All Items' array ?", MessageType.Warning);
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Reset",GUILayout.Width(165)))
                {
                    m_Object.FindProperty("allItems").arraySize = 0;
                    warnID = 0;
                    showWarn = false;
                }
                if (GUILayout.Button("Cancel", GUILayout.Width(165)))
                {
                    warnID = 0;
                    showWarn = false;
                }
                EditorGUILayout.EndHorizontal();
            }
            else if (warnID == 2)
            {
                EditorGUILayout.HelpBox("Are you sure you want to reset the 'All Item Names' array ?", MessageType.Warning);
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Reset", GUILayout.Width(165)))
                {
                    m_Object.FindProperty("allItemNames").arraySize = 0;
                    m_Object.ApplyModifiedProperties();
                    warnID = 0;
                    showWarn = false;
                }
                if (GUILayout.Button("Cancel", GUILayout.Width(165)))
                {
                    warnID = 0;
                    showWarn = false;
                }
                EditorGUILayout.EndHorizontal();
            }
            else if (warnID == 3)
            {
                EditorGUILayout.HelpBox("Are you sure you want to reset the 'All Item Names' array ?", MessageType.Warning);
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("Reset", GUILayout.Width(165)))
                {
                    int tmp = tempSlots;
                    m_Object.FindProperty("currentItems").arraySize = 0;
                    m_Object.ApplyModifiedProperties();
                    m_Object.FindProperty("currentItems").arraySize = tmp;
                    m_Object.ApplyModifiedProperties();
                    tempSlots = tmp;
                    warnID = 0;
                    showWarn = false;
                }
                if (GUILayout.Button("Cancel", GUILayout.Width(165)))
                {
                    warnID = 0;
                    showWarn = false;
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    void changeC(int c)
    {
        selectedRecipe = selectedRecipe + (c);
        GetData();
    }

    void SaveRecipes()
    {
        UInventory i = target as UInventory;

        for (int x = 0; x < i.slotsID.Length; x++)
        {
            
        }
    }

}
#endif 