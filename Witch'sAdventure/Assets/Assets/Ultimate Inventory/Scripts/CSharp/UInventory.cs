using UnityEngine;
using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.IO;

[RequireComponent (typeof(AudioSource))]
public class UInventory : MonoBehaviour {

    public GameObject mainPlayer;
    public GameObject[] allItems;
    public string[] allItemNames;
    public GameObject[] currentItems;
    public int[] slotsStack;
    public string inventoryMode = "Slot";
    public float maxWeight = 50f;
    public KeyCode toggleKey = KeyCode.I;
    public string mainGUIPos = "Center Screen",hotKeyGUIPos = "Bottom Center";
    public bool autoClose = false;
    public int autoCloseTime = 10;
    public bool fastMenu = true;
    public string emptySlotText = "Empty";
    public GUISkin GUISkin;
    public KeyCode[] hotKeys;
    public KeyCode dragMode = KeyCode.Mouse1;
    public int hotKeyItems = 0;
    public bool dontDrawEmpty = false;
    public string invName = "Inventory";
    public Texture2D invBG;
    public bool useIMG;
    public bool pauseGameOnToggle = false;
    public bool dontWorkPaused = true;
    public bool isOpen = false;
    public bool customActivate = true;
    public bool iconPreview = true;
    public Texture2D emptyIcon;
    public AudioClip pickUpSound, dropSound, useSound,cantPickUp;
    public float volume = 1f;
    public bool closeAfterDrop = false;
    public bool allow3DPreview = true;
    public KeyCode k3DToggle = KeyCode.B;
    public float currentWeight = 0f;
    public int numberOfWSlots = 0;
    public Vector2 PixelOffSet,PixelOffSet2,PixelOffSet3;
    public Texture2D backGroundIMG,quickBackGroundIMG;
   // public int maxStack = 10; No longer used after 4.1
    public bool allowStacking = true;
    public bool customSettings = false;
    public Vector2 slotSize = new Vector2(75,75);
    public Vector2 slotSize2 = new Vector2(75, 75);
    public Vector2 scrollSize = new Vector2(368, 380);
    public Vector2 quickBGPos = new Vector2(0, 0);
    public Vector2 quickBGSize = new Vector2(0, 0);
    public Vector2 mainBGSize = new Vector2(500, 580);
    public Vector2 totalWeightLabel = new Vector2(-27.8f, 118.5f);
    public Vector2 dragIconSize = new Vector2(48, 48);
    public  bool isMovingItems = false;
    public bool hasSelected = false;
    public int selectedSlot = 0;
    public bool enabledDragDrop = true;
    Vector2 mouseXY;
    public int[] craftingRecipes;
    public bool allowCrafting = true;
    public KeyCode toggleCrafting = KeyCode.E;
    public Vector2 craftingGUIPos = new Vector2(100, 50);
    Vector2 scrollCrafting;
    public Vector2 craftingSize = new Vector2(250, 250);
    public Vector2 craftingResultPos = new Vector2(235, 75);
    GameObject craftedItem;
    public Vector2 infoPos = new Vector2(0, 0);
    public Vector2 craftingBGpos = new Vector2(0, 0);
    public Vector2 craftingBGSize = new Vector2(360, 375);
    public Vector2 infoSize = new Vector2(328, 121);
    public Texture2D craftingBG;
    public bool showHideCursor = true;
    public bool disableMouseLook = true;
    public bool allowPlayerEquipment = true;
    public GameObject headE, chestE, legsE, bootsE;
    public int[] stackArray;
    /* Stats 23249531 */ public bool aHealth = true, aArmor = true, aStrength = true, aAbility = true, aCold = true, aSpeed = true, aStealth = true;
    public bool useImageOnEmptyEquip = false;
    public string headEmptyText = "Head", chestEmptyText = "Chest", legsEmptyText = "Legs", bootsEmptyChest = "Boots";
    public Texture2D headEmptyIcon, chestEmptyIcon, legsEmptyIcon, bootsEmptyIcon;
    public Texture2D equipmentWindowBG;
    public Vector2 equipmentWindowSize = new Vector2(220,300);
    public Vector2 equipmentWindowPos;
    public Vector2 equipmentDataPos1, equipmentDataPos2, equipmentDataPos3, equipmentDataPos4, equipmentDataPos5, equipmentDataPos6, equipmentDataPos7, equipmentDataPos8;
    public Vector2 equipmentDataSize1, equipmentDataSize2, equipmentDataSize3, equipmentDataSize4, equipmentDataSize5, equipmentDataSize6, equipmentDataSize7, equipmentDataSize8;
    public string equipmentDataString1, equipmentDataString2, equipmentDataString3, equipmentDataString4, equipmentDataString5, equipmentDataString6, equipmentDataString7, equipmentDataString8;
    public Vector2 equipmentSlotSize = new Vector2(64, 64);
    public KeyCode equipmentToggleKey = KeyCode.I;
    public Vector2 slotsPosition;
    public Vector2 itemPreviewPos = new Vector2(0, 0), itemPreviewSize = new Vector2(400, 200);
    public bool displayStats = true;
    public bool pauseOnToggle = false;
    public float quickSlotHeight = 0;
        //-->
  float x1, x2, y1, y2;
    string currentINFO = "";
    public string slotsID = "";
    public int resultID;
    bool crafting = false;
    bool equipT = false;
    float tHealth = 0, tArmor = 0, tStrength = 0, tAbility = 0, tCold = 0, tSpeed = 0, tStealth = 0;
    //Version 4
    public int rowLength = 4;



    public int CountItem(int index)
    {
        try
        {
            int n = 0;
            for (int i = 0; i < currentItems.Length; i++)
            {
                if (currentItems[i] != null)
                {
                    if (currentItems[i].GetComponent<UI_Item>().itemIndex == index)
                    {
                        n += stackArray[i];
                    }
                }
            }
            return n;
        }
        catch { return 000; }
    }

    public void RemoveItems(int index, int amount)
    {
        try
        {
            for (int i = 0; i < currentItems.Length; i++)
            {
                if (currentItems[i] != null)
                {
                    if (currentItems[i].GetComponent<UI_Item>().itemIndex == index)
                    {
                        if (amount > 0)
                        {
                            if (stackArray[i] > amount)
                            {
                                stackArray[i] -= amount;
                                amount = 0;
                            }
                            else if (stackArray[i] == amount)
                            {
                                stackArray[i] = 0;
                                currentItems[i] = null;
                                amount = 0;
                            }
                            else if (stackArray[i] < amount)
                            {
                                amount -= stackArray[i];
                                stackArray[i] = 0;
                                currentItems[i] = null;
                            }
                        }
                    }
                }
            }
        }
        catch { }
    }

    void SaveInventory()
    {
        try
        {
            string saveFile = "";

            for (int i = 0; i < currentItems.Length; i++)
            {
                if (currentItems[i] != null)
                {
                    saveFile += "<id>" + currentItems[i].GetComponent<UI_Item>().itemIndex.ToString() + "</id> " + "<slot>" + i.ToString() + "</slot>" + System.Environment.NewLine;
                }
                else
                {
                    saveFile += "<id></id> " + "<slot>" + i.ToString() + "</slot>" + System.Environment.NewLine;
                }
            }
            string encrypted = Encrypt(saveFile);
            PlayerPrefs.SetString("UInventorySave", encrypted);

        }
        catch { }
    }
    public static string GetStrBetweenTags(string value,string startTag, string endTag)
    {
        try
        {
            if (value.Contains(startTag) && value.Contains(endTag))
            {
                int index = value.IndexOf(startTag) + startTag.Length;
                return value.Substring(index, value.IndexOf(endTag) - index);
            }
            else
                return null;
        }
        catch { return null; }
    }
    string id1 = "0", id2 = "0", id3 = "0", id4 = "0", id5 = "0", id6 = "0", id7 = "0", id8 = "0", id9 = "0", result = "0";
    string info = "";
    GameObject[] craftingSlots = new GameObject[9];
    bool stopSearching = false;

    public string PasswordHash = "P@@Sw0rd";
    public string SaltKey = "S@LT&KEY";
    static readonly string VIKey = "@1B2c3D4e5F6g7H8";

    public string Encrypt(string plainText)
    {
        try
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return System.Convert.ToBase64String(cipherTextBytes);
        }
        catch
        {
            return null;
        }
    }
    public string Decrypt(string encryptedText)
    {
        try
        {
            byte[] cipherTextBytes = System.Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
        catch
        {
            return null;
        }
    }
    int screenX = 100;
    GameObject getCrafted()
    {
       
            if (slotsID.Split('\n').Length != 0)
            {
                string[] tempArray = slotsID.Split('\n');
                if (stopSearching == false)
                {
                    for (int ix = 0; ix < craftingRecipes.Length; ix++)
                    {

                        for (int i = 0; i < tempArray.Length; i++)
                        {
                            string currentData = tempArray[ix];
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

                            string s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", s8 = "", s9 = "";

                            if (craftingSlots[0] != null)
                            {
                                s1 = craftingSlots[0].gameObject.GetComponent<UI_Item>().itemIndex.ToString();
                            }
                            else
                            {
                                s1 = "";
                            }
                            if (craftingSlots[1] != null)
                            {
                                s2 = craftingSlots[1].gameObject.GetComponent<UI_Item>().itemIndex.ToString();
                            }
                            else
                            {
                                s2 = "";
                            }
                            if (craftingSlots[2] != null)
                            {
                                s3 = craftingSlots[2].gameObject.GetComponent<UI_Item>().itemIndex.ToString();
                            }
                            else
                            {
                                s3 = "";
                            }
                            if (craftingSlots[3] != null)
                            {
                                s4 = craftingSlots[3].gameObject.GetComponent<UI_Item>().itemIndex.ToString();
                            }
                            else
                            {
                                s4 = "";
                            }
                            if (craftingSlots[4] != null)
                            {
                                s5 = craftingSlots[4].gameObject.GetComponent<UI_Item>().itemIndex.ToString();
                            }
                            else
                            {
                                s5 = "";
                            }
                            if (craftingSlots[5] != null)
                            {
                                s6 = craftingSlots[5].gameObject.GetComponent<UI_Item>().itemIndex.ToString();
                            }
                            else
                            {
                                s6 = "";
                            }
                            if (craftingSlots[6] != null)
                            {
                                s7 = craftingSlots[6].gameObject.GetComponent<UI_Item>().itemIndex.ToString();
                            }
                            else
                            {
                                s7 = "";
                            }
                            if (craftingSlots[7] != null)
                            {
                                s8 = craftingSlots[7].gameObject.GetComponent<UI_Item>().itemIndex.ToString();
                            }
                            else
                            {
                                s8 = "";
                            }
                            if (craftingSlots[8] != null)
                            {
                                s9 = craftingSlots[8].gameObject.GetComponent<UI_Item>().itemIndex.ToString();
                            }
                            else
                            {
                                s9 = "";
                            }

                            if (id1 == s1 && id2 == s2 && id3 == s3 && id4 == s4 && id5 == s5 && id6 == s6 && id7 == s7 && id8 == s8 && id9 == s9)
                            {
                                craftedItem = allItems[int.Parse(result)];
                                currentINFO = info;
                                return allItems[int.Parse(result)];
                            }
                            else
                            {
                                    craftedItem = null;
                                    currentINFO = "Empty";
                            }

                        }
                    }
                }
            }
            else
            {

            }
            return null;
    }

    void CraftingFuntion()
    {
        try
        {
            craftedItem = getCrafted();
        }
        catch { }
    }

    void LoadInventory()
    {
        try
        {
            string decrypted = PlayerPrefs.GetString("UInventorySave");
            string loadFile = Decrypt(decrypted);

            string[] lines = loadFile.Split("\r\n".ToCharArray());
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "")
                {
                    try
                    {
                        if (GetStrBetweenTags(lines[i], "<id>", "</id>") != null)
                        {
                            currentItems[int.Parse(GetStrBetweenTags(lines[i], "<slot>", "</slot>"))] = allItems[int.Parse(GetStrBetweenTags(lines[i], "<id>", "</id>"))];
                        }
                        else
                        {
                            currentItems[int.Parse(GetStrBetweenTags(lines[i], "<slot>", "</slot>"))] = null;
                        }
                    }
                    catch
                    {
                        currentItems[int.Parse(GetStrBetweenTags(lines[i], "<slot>", "</slot>"))] = null;
                    }
                }
            }
            //
        }
        catch { }
    }
   
    void Start()
    {       
        isOpen = false;
        if (showHideCursor == true)
        {
            Cursor.visible = false;
            Screen.lockCursor = true;
        }
     

    }
    string itemPreview = "";
    string previousPreview = "";
    void DrawEquipmentTab()
    {
        GUILayout.BeginVertical();
        if (headE == null)
        {
            if (GUILayout.Button(new GUIContent(headEmptyText, ""), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
            {
                if (isMovingItems == true)
                {
                    if (hasSelected == true)
                    {
                        if (allItems[selectedSlot].GetComponent<UI_Item>().itemOption == "Equipment" && allItems[selectedSlot].GetComponent<UI_Item>().equipmentOption == "Helmet")
                        {
                            headE = allItems[selectedSlot];
                            hasSelected = false;
                            tHealth += headE.GetComponent<UI_Item>().health;
                            tArmor += headE.GetComponent<UI_Item>().armor;
                            tStrength += headE.GetComponent<UI_Item>().strength;
                            tAbility += headE.GetComponent<UI_Item>().ability;
                            tSpeed += headE.GetComponent<UI_Item>().speed;
                            tCold += headE.GetComponent<UI_Item>().cold;
                            tStealth += headE.GetComponent<UI_Item>().stealth;
                        }
                    }
                }
            }
            itemPreview = GUI.tooltip;
        }
        else
        {
            if (iconPreview == true)
            {
                if (GUILayout.Button(new GUIContent(headE.GetComponent<UI_Item>().iconPreview, headE.GetComponent<UI_Item>().itemIndex.ToString()), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
                {
                    if (hasSelected == false)
                    {
                        hasSelected = true;
                        selectedSlot = headE.GetComponent<UI_Item>().itemIndex;
                        tHealth -= headE.GetComponent<UI_Item>().health;
                        tArmor -= headE.GetComponent<UI_Item>().armor;
                        tStrength -= headE.GetComponent<UI_Item>().strength;
                        tAbility -= headE.GetComponent<UI_Item>().ability;
                        tSpeed -= headE.GetComponent<UI_Item>().speed;
                        tCold -= headE.GetComponent<UI_Item>().cold;
                        tStealth -= headE.GetComponent<UI_Item>().stealth;
                        headE = null;
                    }

                }
                itemPreview = GUI.tooltip;
            }
            else
            {
                if (GUILayout.Button(new GUIContent(headE.GetComponent<UI_Item>().itemName, headE.GetComponent<UI_Item>().itemIndex.ToString()), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
                {
                    if (hasSelected == false)
                    {
                        hasSelected = true;
                        selectedSlot = headE.GetComponent<UI_Item>().itemIndex;
                        headE = null;
                    }

                }
                itemPreview = GUI.tooltip;
            }
        }

        if (chestE == null)
        {
            if (GUILayout.Button(new GUIContent(chestEmptyText, ""), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
            {
                if (isMovingItems == true)
                {
                    if (hasSelected == true)
                    {
                        if (allItems[selectedSlot].GetComponent<UI_Item>().itemOption == "Equipment" && allItems[selectedSlot].GetComponent<UI_Item>().equipmentOption == "Chest")
                        {
                            chestE = allItems[selectedSlot];
                            hasSelected = false;
                            tHealth += chestE.GetComponent<UI_Item>().health;
                            tArmor += chestE.GetComponent<UI_Item>().armor;
                            tStrength += chestE.GetComponent<UI_Item>().strength;
                            tAbility += chestE.GetComponent<UI_Item>().ability;
                            tSpeed += chestE.GetComponent<UI_Item>().speed;
                            tCold += chestE.GetComponent<UI_Item>().cold;
                            tStealth += chestE.GetComponent<UI_Item>().stealth;
                        }
                    }
                }
            }
            itemPreview = GUI.tooltip;
        }
        else
        {
            if (iconPreview == true)
            {
                if (GUILayout.Button(new GUIContent(chestE.GetComponent<UI_Item>().iconPreview, chestE.GetComponent<UI_Item>().itemIndex.ToString()), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
                {
                    if (hasSelected == false)
                    {
                        hasSelected = true;
                        selectedSlot = chestE.GetComponent<UI_Item>().itemIndex;
                        tHealth -= chestE.GetComponent<UI_Item>().health;
                        tArmor -= chestE.GetComponent<UI_Item>().armor;
                        tStrength -= chestE.GetComponent<UI_Item>().strength;
                        tAbility -= chestE.GetComponent<UI_Item>().ability;
                        tSpeed -= chestE.GetComponent<UI_Item>().speed;
                        tCold -= chestE.GetComponent<UI_Item>().cold;
                        tStealth -= chestE.GetComponent<UI_Item>().stealth;
                        chestE = null;
                    }

                }
                itemPreview = GUI.tooltip;
            }
            else
            {
                if (GUILayout.Button(new GUIContent(chestE.GetComponent<UI_Item>().itemName, chestE.GetComponent<UI_Item>().itemIndex.ToString()), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
                {
                    if (hasSelected == false)
                    {
                        hasSelected = true;
                        selectedSlot = chestE.GetComponent<UI_Item>().itemIndex;
                        chestE = null;
                    }

                }
                itemPreview = GUI.tooltip;
            }
        }

        if (legsE == null)
        {
            if (GUILayout.Button(new GUIContent(legsEmptyText, ""), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
            {
                if (isMovingItems == true)
                {
                    if (hasSelected == true)
                    {
                        if (allItems[selectedSlot].GetComponent<UI_Item>().itemOption == "Equipment" && allItems[selectedSlot].GetComponent<UI_Item>().equipmentOption == "Legs")
                        {
                            legsE = allItems[selectedSlot];
                            hasSelected = false;
                            tHealth += legsE.GetComponent<UI_Item>().health;
                            tArmor += legsE.GetComponent<UI_Item>().armor;
                            tStrength += legsE.GetComponent<UI_Item>().strength;
                            tAbility += legsE.GetComponent<UI_Item>().ability;
                            tSpeed += legsE.GetComponent<UI_Item>().speed;
                            tCold += legsE.GetComponent<UI_Item>().cold;
                            tStealth += legsE.GetComponent<UI_Item>().stealth;
                        }
                    }
                }
            }
            itemPreview = GUI.tooltip;
        }
        else
        {
            if (iconPreview == true)
            {
                if (GUILayout.Button(new GUIContent(legsE.GetComponent<UI_Item>().iconPreview, legsE.GetComponent<UI_Item>().itemIndex.ToString()), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
                {
                    if (hasSelected == false)
                    {
                        hasSelected = true;
                        selectedSlot = legsE.GetComponent<UI_Item>().itemIndex;
                        tHealth -= legsE.GetComponent<UI_Item>().health;
                        tArmor -= legsE.GetComponent<UI_Item>().armor;
                        tStrength -= legsE.GetComponent<UI_Item>().strength;
                        tAbility -= legsE.GetComponent<UI_Item>().ability;
                        tSpeed -= legsE.GetComponent<UI_Item>().speed;
                        tCold -= legsE.GetComponent<UI_Item>().cold;
                        tStealth -= legsE.GetComponent<UI_Item>().stealth;
                        legsE = null;
                    }

                }
                itemPreview = GUI.tooltip;
            }
            else
            {
                if (GUILayout.Button(new GUIContent(legsE.GetComponent<UI_Item>().itemName, legsE.GetComponent<UI_Item>().itemIndex.ToString()), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
                {
                    if (hasSelected == false)
                    {
                        hasSelected = true;
                        selectedSlot = legsE.GetComponent<UI_Item>().itemIndex;
                        legsE = null;
                    }

                }
                itemPreview = GUI.tooltip;
            }
        }

        if (bootsE == null)
        {
            if (GUILayout.Button(new GUIContent(bootsEmptyChest, ""), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
            {
                if (isMovingItems == true)
                {
                    if (hasSelected == true)
                    {
                        if (allItems[selectedSlot].GetComponent<UI_Item>().itemOption == "Equipment" && allItems[selectedSlot].GetComponent<UI_Item>().equipmentOption == "Boots")
                        {
                            bootsE = allItems[selectedSlot];
                            hasSelected = false;
                            tHealth += bootsE.GetComponent<UI_Item>().health;
                            tArmor += bootsE.GetComponent<UI_Item>().armor;
                            tStrength += bootsE.GetComponent<UI_Item>().strength;
                            tAbility += bootsE.GetComponent<UI_Item>().ability;
                            tSpeed += bootsE.GetComponent<UI_Item>().speed;
                            tCold += bootsE.GetComponent<UI_Item>().cold;
                            tStealth += bootsE.GetComponent<UI_Item>().stealth;
                        }
                    }
                }
            }
            itemPreview = GUI.tooltip;
        }
        else
        {
            if (iconPreview == true)
            {
                if (GUILayout.Button(new GUIContent(bootsE.GetComponent<UI_Item>().iconPreview, bootsE.GetComponent<UI_Item>().itemIndex.ToString()), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
                {
                    if (hasSelected == false)
                    {
                        hasSelected = true;
                        selectedSlot = bootsE.GetComponent<UI_Item>().itemIndex;
                        tHealth -= bootsE.GetComponent<UI_Item>().health;
                        tArmor -= bootsE.GetComponent<UI_Item>().armor;
                        tStrength -= bootsE.GetComponent<UI_Item>().strength;
                        tAbility -= bootsE.GetComponent<UI_Item>().ability;
                        tSpeed -= bootsE.GetComponent<UI_Item>().speed;
                        tCold -= bootsE.GetComponent<UI_Item>().cold;
                        tStealth -= bootsE.GetComponent<UI_Item>().stealth;
                        bootsE = null;
                    }

                }
                itemPreview = GUI.tooltip;
            }
            else
            {
                if (GUILayout.Button(new GUIContent(bootsE.GetComponent<UI_Item>().itemName, bootsE.GetComponent<UI_Item>().itemIndex.ToString()), GUILayout.Width(equipmentSlotSize.x), GUILayout.Height(equipmentSlotSize.y)))
                {
                    if (hasSelected == false)
                    {
                        hasSelected = true;
                        selectedSlot = bootsE.GetComponent<UI_Item>().itemIndex;
                        bootsE = null;
                    }

                }
                itemPreview = GUI.tooltip;
            }
        }
    }
    int scrollY2 = 560;
    void Update()
    {
        if (Input.GetKeyDown(toggleCrafting))
        {
            crafting = !crafting;
        }
        if (Input.GetKeyDown(equipmentToggleKey))
        {
            equipT = !equipT;
        }
        if (Input.GetKeyDown(dragMode))
        {
            isMovingItems = !isMovingItems;
            if (isMovingItems == false)
            {
                hasSelected = false;
            }
        }
        if (isMovingItems)
        {
            mouseXY = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

        }
        if (fastMenu == true)
        {
            for (int i = 0; i < hotKeys.Length; i++)
            {
                if (hotKeys[i] != null)
                {
                    if (Input.GetKeyDown(hotKeys[i]))
                    {
                        ActivateButtons(i);
                        PlaySound(useSound);
                    }
                }
            }
        }
        if (currentItems.Length <= 100)
        {
            scrollY = (currentItems.Length / 2) * 55;
        }
        else if (currentItems.Length > 100 && currentItems.Length <= 200)
        {
            scrollY = (currentItems.Length / 2) * 52;
        }
        else if (currentItems.Length > 200 && currentItems.Length <= 300)
        {
            scrollY = (currentItems.Length / 2) * 52;
        }
        if (numberOfWSlots <= 100)
        {
            scrollY2 = (numberOfWSlots / 2) * 55;
        }
        else if (numberOfWSlots > 100 && numberOfWSlots <= 200)
        {
            scrollY2 = (numberOfWSlots / 2) * 52;
        }
        else if (numberOfWSlots > 200 && numberOfWSlots <= 300)
        {
            scrollY2 = (numberOfWSlots / 2) * 52;
        }
        if (hotKeyItems <= 9)
        {
            screenX = (hotKeyItems) * 150;
        }
        if (dontWorkPaused == true)
        {
            if (Time.timeScale != 0f)
            {
                if (Input.GetKeyDown(toggleKey))
                {
                    ToggleInv(!isOpen);
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(toggleKey))
            {
                isOpen = !isOpen;
                
                if (autoClose == true)
                {
                    StartCoroutine(AutoToggleOf(autoCloseTime));
                }
            }
        }

        
    }
    IEnumerator AutoToggleOf(float sec)
    {
        yield return new WaitForSeconds(sec);
        isOpen = false;
    }
    public Vector2 scrollPosition = Vector2.zero;
    public Vector2 scrollPosition2 = Vector2.zero;
    public void PlaySound(AudioClip a)
    {
        GetComponent<AudioSource>().PlayOneShot(a,volume);
    }
  
    void drawCrafting()
    {
        if (allowCrafting == true)
        {
            if (crafting == true)
            {
                for (int r = 0; r < 9 / 3 + 1; r++)
                {
                    for (int s = 0; s < 3; s++)
                    {
                        int thisIndex = r * 3 + s;
                        if (thisIndex < craftingSlots.Length)
                        {
                            if (craftingSlots[thisIndex] != null)
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), craftingSlots[thisIndex].GetComponent<UI_Item>().iconPreview))
                                {
                                    if (hasSelected == false && isMovingItems == true)
                                    {
                                        hasSelected = true;
                                        selectedSlot = craftingSlots[thisIndex].GetComponent<UI_Item>().itemIndex;
                                        craftingSlots[thisIndex] = null;
                                       CraftingFuntion();
                                    }
                                }

                            }
                            else
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), "Empty"))
                                {
                                    if (isMovingItems == true)
                                    {
                                        if (hasSelected == true)
                                        {
                                            craftingSlots[thisIndex] = allItems[selectedSlot];
                                            hasSelected = false;
                                           CraftingFuntion();
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
              
            }
        }
    }

    public void ToggleInv(bool status)
    {

        isOpen = status;
            if (showHideCursor == true)
            {
                if (isOpen == true)
                {
                    Cursor.visible = true;
                    Screen.lockCursor = false;
                }
                else if (isOpen == false)
                {
                    Cursor.visible = false;
                    Screen.lockCursor = true;
                }
            }
            if (disableMouseLook == true)
            {

                if (isOpen == true)
                {
                    x1 = mainPlayer.GetComponent<MouseLook>().sensitivityX;
                    y1 = mainPlayer.GetComponent<MouseLook>().sensitivityY;
                    x2 = Camera.main.gameObject.GetComponent<MouseLook>().sensitivityX;
                    y2 = Camera.main.gameObject.GetComponent<MouseLook>().sensitivityY;

                    mainPlayer.GetComponent<MouseLook>().sensitivityX = 0;
                    mainPlayer.GetComponent<MouseLook>().sensitivityY = 0;
                    Camera.main.gameObject.GetComponent<MouseLook>().sensitivityX = 0;
                    Camera.main.gameObject.GetComponent<MouseLook>().sensitivityY = 0;
                }
                else
                {
                    mainPlayer.GetComponent<MouseLook>().sensitivityX = x1;
                    mainPlayer.GetComponent<MouseLook>().sensitivityY = y1;
                    Camera.main.gameObject.GetComponent<MouseLook>().sensitivityX = x2;
                    Camera.main.gameObject.GetComponent<MouseLook>().sensitivityY = y2;
                }
            }
            if (autoClose == true)
            {
                StartCoroutine(AutoToggleOf(autoCloseTime));
            }
    }
    private void DropItem(int slot)
    {
     //   if (called == false)
      //  {
             if (inventoryMode == "Weight")
                {
                    currentWeight -= currentItems[slot].GetComponent<UI_Item>().itemWeight;

                    if (currentWeight < 0)
                        currentWeight = 0;
                }
                Instantiate(currentItems[slot].gameObject, GameObject.FindGameObjectWithTag("UDropPos").transform.position, currentItems[slot].gameObject.transform.rotation);
                if (allowStacking == true)
                {
                    if (stackArray[slot] == 1)
                    {
                        currentItems[slot] = null;
                        stackArray[slot] = 0;
                    }
                    else
                    {
                        stackArray[slot]--;
                    }
                }
                else
                {
                    currentItems[slot] = null;
                }
                PlaySound(dropSound);
                if (closeAfterDrop == true)
                {
                    isOpen = false;
                }
                PlaySound(dropSound);

             
       // }
    }

    int lastButNum = -1;
    float lastClickTime = -99;
    const float D_CLICK_DELAY = 0.25f;

    void ActivateButtons(int slot)
    {
        if (currentItems[slot] != null)
        {
            StartCoroutine(waitForActivation(slot));
        }
    }

    bool CheckForItem(int id, bool deleteIfExist)
    {
        try
        {
            for (int i = 0; i < currentItems.Length; i++)
            {
                if (id == currentItems[i].GetComponent<UI_Item>().itemIndex)
                {
                    if (deleteIfExist == false)
                    {
                        return true;
                    }
                    else
                    {
                        currentItems[i] = null;
                        return true;
                    }
                }
                else
                {
                    continue;
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool CheckForKey(int keyID)
    {
        for (int i = 0; i < currentItems.Length; i++)
        {
            if (currentItems[i] != null)
            {
                if (currentItems[i].GetComponent<UI_Key>() != null)
                {
                    if (currentItems[i].GetComponent<UI_Key>().KeyID == keyID)
                    {
                        return true;
                    }
                }
            }

        }
        return false;
    }

    IEnumerator waitForActivation(int index)
    {
        yield return new WaitForSeconds(0.15f);
        if (currentItems[index] != null)
        {
            if (inventoryMode == "Weight" && currentItems[index].GetComponent<UI_Item>().destroyOnUse == true)
            {
                currentWeight -= currentItems[index].GetComponent<UI_Item>().itemWeight;

                if (currentWeight < 0)
                    currentWeight = 0;
            }
            if (customActivate == true)
            {
               transform.GetComponent<UI_CustomActivation>().CustomActivate(index,currentItems[index].GetComponent<UI_Item>().destroyOnUse);
            }
            else
            {
                transform.GetComponent<UI_CustomActivation>().DefaultActivate(index, currentItems[index].GetComponent<UI_Item>().destroyOnUse);
            }
            PlaySound(useSound);
        }
       
    }

    void LoadButtons()
    {
        if (Event.current.button > 0 && Event.current.type != EventType.Repaint
&& Event.current.type != EventType.Layout)
            Event.current.Use();
       
        for (int r = 0; r < currentItems.Length / rowLength + 1; r++)
        {
            for (int s = 0; s < rowLength; s++)
            {
                int thisIndex = r * rowLength + s;
                if (thisIndex < currentItems.Length)
                {
                    try
                    {
                        if (currentItems[thisIndex] != null)
                        {
                          
                            if (iconPreview == false)
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), new GUIContent(currentItems[thisIndex].GetComponent<UI_Item>().itemName, currentItems[thisIndex].GetComponent<UI_Item>().itemIndex.ToString())))
                                {
                                    if (lastClickTime > Time.time - D_CLICK_DELAY && lastButNum == 1)
                                    {
                                        DropItem(thisIndex);
                                    }
                                     
                                    // double-click
                                    else
                                    {
                                        // highlight or something
                                        lastClickTime = Time.time; lastButNum = 1; // wait for 2nd click
                                        if (isMovingItems == false)
                                        {
                                            ActivateButtons(thisIndex);
                                        }
                                        else
                                        {
                                            if (hasSelected == false)
                                            {
                                                hasSelected = true;
                                                selectedSlot = currentItems[thisIndex].GetComponent<UI_Item>().itemIndex;
                                                if (allowStacking == true)
                                                {
                                                    if (stackArray[thisIndex] == 1)
                                                    {
                                                        stackArray[thisIndex] = 0;
                                                        currentItems[thisIndex] = null;
                                                    }
                                                    else
                                                    {
                                                        stackArray[thisIndex]--;
                                                    }
                                                }
                                                else
                                                {
                                                    currentItems[thisIndex] = null;
                                                }
                                            }
                                            else if (hasSelected == true)
                                            {
                                                if (allowStacking == true)
                                                {
                                                    if (currentItems[thisIndex].GetComponent<UI_Item>().itemIndex == allItems[selectedSlot].GetComponent<UI_Item>().itemIndex && stackArray[thisIndex] < allItems[selectedSlot].GetComponent<UI_Item>().maxStack)
                                                    {
                                                        stackArray[thisIndex]++;
                                                        hasSelected = false;
                                                    }
                                                }
                                            }

                                        }
                                        itemPreview = GUI.tooltip;
                                    }
                                }
                                if (allowStacking == true)
                                {
                                    GUI.Label(new Rect((s * slotSize.x) + 8, (r * slotSize.y) + (slotSize.y / 2 + 15), 80, 32), stackArray[thisIndex].ToString());
                                }
                            }
                            else
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), new GUIContent(currentItems[thisIndex].GetComponent<UI_Item>().iconPreview, currentItems[thisIndex].GetComponent<UI_Item>().itemIndex.ToString())))
                                {
                                    if (lastClickTime > Time.time - D_CLICK_DELAY && lastButNum == 1)
                                    {
                                        DropItem(thisIndex);
                                    }
                                        
                                    // double-click
                                    else
                                    {
                                        // highlight or something
                                        lastClickTime = Time.time; lastButNum = 1; // wait for 2nd click
                                        if (isMovingItems == false)
                                        {
                                            ActivateButtons(thisIndex);
                                        }
                                        else
                                        {
                                            if (hasSelected == false)
                                            {
                                                hasSelected = true;
                                                selectedSlot = currentItems[thisIndex].GetComponent<UI_Item>().itemIndex;
                                                if (allowStacking == true)
                                                {
                                                    if (stackArray[thisIndex] == 1)
                                                    {
                                                        stackArray[thisIndex] = 0;
                                                        currentItems[thisIndex] = null;
                                                    }
                                                    else
                                                    {
                                                        stackArray[thisIndex]--;
                                                    }
                                                }
                                                else
                                                {
                                                    currentItems[thisIndex] = null;
                                                }
                                            }
                                            else if (hasSelected == true)
                                            {
                                                if (allowStacking == true)
                                                {
                                                    if (currentItems[thisIndex].GetComponent<UI_Item>().itemIndex == allItems[selectedSlot].GetComponent<UI_Item>().itemIndex && stackArray[thisIndex] < allItems[selectedSlot].GetComponent<UI_Item>().maxStack)
                                                    {
                                                        stackArray[thisIndex]++;
                                                        hasSelected = false;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    itemPreview = GUI.tooltip;
                                }
                                if (allowStacking == true)
                                {
                                    GUI.Label(new Rect((s * slotSize.x) + 8, (r * slotSize.y) + (slotSize.y / 2 + 15), 80, 32), stackArray[thisIndex].ToString());
                                }
                            }
                        }
                        else
                        {
                            if (inventoryMode == "Slot")
                            {
                                if (iconPreview == false)
                                {
                                    if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), emptySlotText))
                                    {
                                        if (isMovingItems == true)
                                        {
                                            if (hasSelected == true)
                                            {
                                                currentItems[thisIndex] = allItems[selectedSlot];
                                                if (allowStacking == true)
                                                {
                                                    stackArray[thisIndex] = 1;
                                                }
                                                hasSelected = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (emptyIcon != null)
                                    {
                                        if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), emptyIcon))
                                        {
                                            if (isMovingItems == true)
                                            {
                                                if (hasSelected == true)
                                                {
                                                    currentItems[thisIndex] = allItems[selectedSlot];
                                                    if (allowStacking == true)
                                                    {
                                                        stackArray[thisIndex] = 1;
                                                    }
                                                    hasSelected = false;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), emptySlotText))
                                        {
                                            if (isMovingItems == true)
                                            {
                                                if (hasSelected == true)
                                                {
                                                    currentItems[thisIndex] = allItems[selectedSlot];
                                                    if (allowStacking == true)
                                                    {
                                                        stackArray[thisIndex] = 1;
                                                    }
                                                    hasSelected = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                    catch { }
                }
            }
        }
    }

    void LoadButtonsWeight()
    {
        for (int r = 0; r < currentItems.Length / rowLength + 1; r++)
        {
            for (int s = 0; s < rowLength; s++)
            {
                int thisIndex = r * rowLength + s;
                if (thisIndex < currentItems.Length)
                {
                    try
                    {
                        if (currentItems[thisIndex] != null)
                        {
                            if (iconPreview == false)
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), currentItems[thisIndex].GetComponent<UI_Item>().itemName))
                                {
                                    if (lastClickTime > Time.time - D_CLICK_DELAY && lastButNum == 1)
                                    {
                                        DropItem(thisIndex);
                                    }
                                    // double-click
                                    else
                                    {
                                        // highlight or something
                                        lastClickTime = Time.time; lastButNum = 1; // wait for 2nd click
                                        if (isMovingItems == false)
                                        {
                                            ActivateButtons(thisIndex);
                                        }
                                        else
                                        {
                                            if (hasSelected == false)
                                            {
                                                hasSelected = true;
                                                selectedSlot = currentItems[thisIndex].GetComponent<UI_Item>().itemIndex;
                                                currentWeight -= currentItems[thisIndex].GetComponent<UI_Item>().itemWeight;
                                                if (allowStacking == true)
                                                {
                                                    if (stackArray[thisIndex] == 1)
                                                    {
                                                        stackArray[thisIndex] = 0;
                                                        currentItems[thisIndex] = null;
                                                    }
                                                    else
                                                    {
                                                        stackArray[thisIndex]--;
                                                    }
                                                }
                                                else
                                                {
                                                    currentItems[thisIndex] = null;
                                                }
                                            }
                                            else if (hasSelected == true)
                                            {
                                                if (allowStacking == true)
                                                {
                                                    if (currentItems[thisIndex].GetComponent<UI_Item>().itemIndex == allItems[selectedSlot].GetComponent<UI_Item>().itemIndex && stackArray[thisIndex] < allItems[selectedSlot].GetComponent<UI_Item>().maxStack)
                                                    {
                                                        stackArray[thisIndex]++;
                                                        hasSelected = false;
                                                    }
                                                }
                                            }

                                        }
                                        itemPreview = GUI.tooltip;
                                    }
                                }
                                GUI.Label(new Rect(s * slotSize.x, r * slotSize.y, 80, 32), currentItems[thisIndex].GetComponent<UI_Item>().itemWeight.ToString() + "kg");
                                if (allowStacking == true)
                                {
                                    GUI.Label(new Rect((s * slotSize.x) + 8, (r * slotSize.y) + (slotSize.y / 2 + 15), 80, 32), stackArray[thisIndex].ToString());
                                }
                            }
                            else
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), currentItems[thisIndex].GetComponent<UI_Item>().iconPreview))
                                {
                                    if (lastClickTime > Time.time - D_CLICK_DELAY && lastButNum == 1)
                                    {
                                        DropItem(thisIndex);
                                    }
                                    // double-click
                                    else
                                    {
                                        // highlight or something
                                        lastClickTime = Time.time; lastButNum = 1; // wait for 2nd click
                                        if (isMovingItems == false)
                                        {
                                            ActivateButtons(thisIndex);
                                        }
                                        else
                                        {
                                            if (isMovingItems == false)
                                            {
                                                ActivateButtons(thisIndex);
                                            }
                                            else
                                            {
                                                if (hasSelected == false)
                                                {
                                                    hasSelected = true;
                                                    selectedSlot = currentItems[thisIndex].GetComponent<UI_Item>().itemIndex;
                                                    currentWeight -= currentItems[thisIndex].GetComponent<UI_Item>().itemWeight;
                                                    if (allowStacking == true)
                                                    {
                                                        if (stackArray[thisIndex] == 1)
                                                        {
                                                            stackArray[thisIndex] = 0;
                                                            currentItems[thisIndex] = null;
                                                        }
                                                        else
                                                        {
                                                            stackArray[thisIndex]--;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        currentItems[thisIndex] = null;
                                                    }
                                                }
                                                else if (hasSelected == true)
                                                {
                                                    if (allowStacking == true)
                                                    {
                                                        if (currentItems[thisIndex].GetComponent<UI_Item>().itemIndex == allItems[selectedSlot].GetComponent<UI_Item>().itemIndex && stackArray[thisIndex] < allItems[selectedSlot].GetComponent<UI_Item>().maxStack)
                                                        {
                                                            stackArray[thisIndex]++;
                                                            hasSelected = false;
                                                        }
                                                    }
                                                }

                                            }
                                            itemPreview = GUI.tooltip;

                                        }
                                    }
                                }
                                GUI.Label(new Rect(s *  slotSize.x, r *  slotSize.y, 80, 32), currentItems[thisIndex].GetComponent<UI_Item>().itemWeight.ToString() + "kg");
                                if (allowStacking == true)
                                {
                                    GUI.Label(new Rect((s * slotSize.x) + 8, (r * slotSize.y) + (slotSize.y / 2 + 15), 80, 32), stackArray[thisIndex].ToString());
                                }
                            }
                        }
                        else
                        {
                            if (emptyIcon == null)
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), emptySlotText))
                                {
                                    if (isMovingItems == true)
                                    {
                                        if (hasSelected == true)
                                        {
                                           
                                            if (currentWeight + allItems[selectedSlot].GetComponent<UI_Item>().itemWeight <= maxWeight)
                                            {
                                                currentWeight += allItems[selectedSlot].GetComponent<UI_Item>().itemWeight;
                                                currentItems[thisIndex] = allItems[selectedSlot];
                                                hasSelected = false;
                                            }
                                            else if (currentWeight + allItems[selectedSlot].GetComponent<UI_Item>().itemWeight > maxWeight)
                                            {
                                                DropItem(currentItems[thisIndex].GetComponent<UI_Item>().itemIndex);
                                                hasSelected = false;
                                                Debug.Log("DROP");
                                            }
     
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), emptyIcon))
                                {
                                    if (isMovingItems == true)
                                    {
                                        if (hasSelected == true)
                                        {

                                            if (currentWeight + allItems[selectedSlot].GetComponent<UI_Item>().itemWeight <= maxWeight)
                                            {
                                                currentWeight += allItems[selectedSlot].GetComponent<UI_Item>().itemWeight;
                                                currentItems[thisIndex] = allItems[selectedSlot];
                                                 hasSelected = false;
                                            }
                                            else if (currentWeight + allItems[selectedSlot].GetComponent<UI_Item>().itemWeight > maxWeight)
                                            {
                                                DropItem(currentItems[thisIndex].GetComponent<UI_Item>().itemIndex);
                                                hasSelected = false;
                                                Debug.Log("DROP");
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
        }
    }

    public int scrollY = 560;

    void LoadHotKeys()
    {
        if (hotKeyItems <= currentItems.Length)
        {
            for (int i = 0; i < hotKeyItems; i++)
            {
                if (currentItems[i] != null)
                {
                    if (iconPreview == false)
                    {
                        if (GUILayout.Button(currentItems[i].GetComponent<UI_Item>().itemName,GUILayout.Width(slotSize2.x),GUILayout.Height(slotSize2.y)))
                        {
                            if (lastClickTime > Time.time - D_CLICK_DELAY && lastButNum == 1)
                            {
                                DropItem(i);
                            }
                            // double-click
                            else
                            {
                                // highlight or something
                                lastClickTime = Time.time; lastButNum = 1; // wait for 2nd click
                                ActivateButtons(i);
                            }
                           
                        }
                        if (allowStacking == true)
                        {
                            GUI.Label(new Rect((i * slotSize.x) + 8,(slotSize.y / 2 + 15), 80, 32), stackArray[i].ToString());
                        }
                    }
                    else
                    {
                        if (GUILayout.Button(currentItems[i].GetComponent<UI_Item>().iconPreview,GUILayout.Width(slotSize2.x),GUILayout.Height(slotSize2.y)))
                        {
                            if (lastClickTime > Time.time - D_CLICK_DELAY && lastButNum == 1)
                            {
                                DropItem(i);
                            }
                            // double-click
                            else
                            {
                                // highlight or something
                                lastClickTime = Time.time; lastButNum = 1; // wait for 2nd click
                                ActivateButtons(i);
                            }

                        }
                        if (allowStacking == true)
                        {
                            GUI.Label(new Rect((i * slotSize2.x) + 25, (slotSize2.y / 2 ) + 25, 80, 32), stackArray[i].ToString());
                        }
                    }
                }
                else
                {
                    if (dontDrawEmpty == false)
                    {
                        if (iconPreview == true && emptyIcon != null)
                        {
                            GUILayout.Button(emptyIcon,GUILayout.Width(slotSize2.x),GUILayout.Height(slotSize2.y));
                        }
                        else
                        {
                            GUILayout.Button(emptySlotText,GUILayout.Width(slotSize2.x),GUILayout.Height(slotSize2.y));
                        }
                    }
                }
            }
       }
    }

    Vector2 scrollPosition3 = Vector2.zero;

    void displayItemPreview(int itemIndex)
    {
        itemPreviewPos = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y);
        GUI.Box(new Rect(itemPreviewPos.x,itemPreviewPos.y, itemPreviewSize.x, itemPreviewSize.y), allItems[itemIndex].GetComponent<UI_Item>().itemName);
        string toDisplay = allItems[itemIndex].GetComponent<UI_Item>().itemDescription;
        if (displayStats == true)
        {
          //  if (allItems[itemIndex].GetComponent<UI_Item>().itemOption == "Equipment")
          //  {
            toDisplay += System.Environment.NewLine;
                if (aHealth == true)
                {
                    if (allItems[itemIndex].GetComponent<UI_Item>().health != 0)
                    {
                        toDisplay += System.Environment.NewLine + equipmentDataString1 + " : " + allItems[itemIndex].GetComponent<UI_Item>().health.ToString();
                    }
                }
                if (aArmor == true)
                {
                    if (allItems[itemIndex].GetComponent<UI_Item>().armor != 0)
                    {
                        toDisplay += System.Environment.NewLine + equipmentDataString2 + " : " + allItems[itemIndex].GetComponent<UI_Item>().armor.ToString();
                    }
                }
                if (aStrength == true)
                {
                    if (allItems[itemIndex].GetComponent<UI_Item>().strength != 0)
                    {
                        toDisplay += System.Environment.NewLine + equipmentDataString3 + " : " + allItems[itemIndex].GetComponent<UI_Item>().strength.ToString();
                    }
                }
                if (aAbility == true)
                {
                    if (allItems[itemIndex].GetComponent<UI_Item>().ability != 0)
                    {
                        toDisplay += System.Environment.NewLine + equipmentDataString4 + " : " + allItems[itemIndex].GetComponent<UI_Item>().ability.ToString();
                    }
                }
                if (aCold == true)
                {
                    if (allItems[itemIndex].GetComponent<UI_Item>().cold != 0)
                    {
                        toDisplay += System.Environment.NewLine + equipmentDataString5 + " : " + allItems[itemIndex].GetComponent<UI_Item>().cold.ToString();
                    }
                }
                if (aSpeed == true)
                {
                    if (allItems[itemIndex].GetComponent<UI_Item>().speed != 0)
                    {
                        toDisplay += System.Environment.NewLine + equipmentDataString6 + " : " + allItems[itemIndex].GetComponent<UI_Item>().speed.ToString();
                    }
                }
                if (aStealth == true)
                {
                    if (allItems[itemIndex].GetComponent<UI_Item>().stealth != 0)
                    {
                        toDisplay += System.Environment.NewLine + equipmentDataString7 + " : " + allItems[itemIndex].GetComponent<UI_Item>().stealth.ToString();
                    }
                }
         //   }//
        }
        GUI.TextArea(new Rect(itemPreviewPos.x + 11, (itemPreviewPos.y)+ 19, itemPreviewSize.x - 20, itemPreviewSize.y - 20), toDisplay);
    }

    void OnGUI()
    {
        GUI.skin = GUISkin;

        if (fastMenu == true && isOpen == false)
        {
            if (quickBackGroundIMG != null)
            {
                GUI.DrawTexture(new Rect(Screen.width / 2 + (quickBGPos.x), Screen.height / 2 + (quickBGPos.y), quickBGSize.x, quickBGSize.y), quickBackGroundIMG);
            }
            if (hotKeyGUIPos == "Top Left")
            {
                scrollPosition2 = GUI.BeginScrollView(new Rect(0, 0, Screen.width, 90), scrollPosition2, new Rect(0, 0, 0, 0));
                GUILayout.BeginHorizontal();
                LoadHotKeys();
                GUILayout.EndHorizontal();
                GUI.EndScrollView();
            }
            else if (hotKeyGUIPos == "Custom")
            {
                scrollPosition2 = GUI.BeginScrollView(new Rect(Screen.width / 2 + (PixelOffSet2.x), Screen.height / 2 + (PixelOffSet2.y), Screen.width, 90), scrollPosition2, new Rect(0, 0, 0, 0));
                GUILayout.BeginHorizontal();
                LoadHotKeys();
                GUILayout.EndHorizontal();
                GUI.EndScrollView();
            }
            else if (hotKeyGUIPos == "Bottom Left")
            {
                scrollPosition2 = GUI.BeginScrollView(new Rect(0, Screen.height - 80, Screen.width, 90), scrollPosition2, new Rect(0, 0, 0, 0));
                GUILayout.BeginHorizontal();
                LoadHotKeys();
                GUILayout.EndHorizontal();
                GUI.EndScrollView();
            }
            else if (hotKeyGUIPos == "Bottom Center")
            {
                scrollPosition2 = GUI.BeginScrollView(new Rect(Screen.width / 2 - 340, Screen.height - 80, Screen.width, 90), scrollPosition2, new Rect(0, 0, 0, 0));
                GUILayout.BeginHorizontal();
                LoadHotKeys();
                GUILayout.EndHorizontal();
                GUI.EndScrollView();
            }
            else if (hotKeyGUIPos == "Top Center")
            {
                scrollPosition2 = GUI.BeginScrollView(new Rect(Screen.width / 2 - 340,10, Screen.width, 90), scrollPosition2, new Rect(0, 0, 0, 0));
                GUILayout.BeginHorizontal();
                LoadHotKeys();
                GUILayout.EndHorizontal();
                GUI.EndScrollView();
            }
            
        }
        if (isOpen == true)
        {
            if (inventoryMode == "Slot")
            {
                if (useIMG == false)
                {
                    if (mainGUIPos == "Center Screen")
                    {
                        if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 300, 395, 580), invName);
                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }
                       
                        
                        //Draw Buttons
                        {
                            // GUI.BeginGroup(new Rect(Screen.width / 2 - 230, Screen.height / 2 - 300, 500, 560), "");
                            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 202, Screen.height / 2 - 260, 500, 520), scrollPosition, new Rect(-50, 0, 480, scrollY));
                            LoadButtons();
                            GUI.EndScrollView();
                            //  GUI.EndGroup();
                        }
                    }
                    else if (mainGUIPos == "Custom")
                    {
                        if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(Screen.width / 2 + (PixelOffSet.x - 2), Screen.height / 2 + (PixelOffSet.y - 40), 395, 580), invName);
                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }
                       
                        scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 + (PixelOffSet.x), Screen.height / 2 + (PixelOffSet.y), scrollSize.x, scrollSize.y), scrollPosition, new Rect(-50, 0, 00, scrollY));
                        LoadButtons();
                        GUI.EndScrollView();
                    }
                    else if (mainGUIPos == "Top Left")
                    {
                        if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(0, 0, 395, 580), invName);
                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }
                       
                       
                        scrollPosition = GUI.BeginScrollView(new Rect(-4, +40, 500, 520), scrollPosition, new Rect(-50, 0, 480, scrollY));
                        LoadButtons();
                        GUI.EndScrollView();
                    }
                    else if (mainGUIPos == "Top Right")
                    {
                        if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(Screen.width - 395, 0, 395, 580), invName);
                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }                     
                        scrollPosition = GUI.BeginScrollView(new Rect(Screen.width - 395, +40, 500, 520), scrollPosition, new Rect(-50, 0, 480, scrollY));
                        LoadButtons();
                        GUI.EndScrollView();
                    }
                    else if (mainGUIPos == "Bottom Left")
                    {
                        if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(0, Screen.height - 580, 395, 580), invName);
                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }  
                        scrollPosition = GUI.BeginScrollView(new Rect(-4, Screen.height - 540, 500, 520), scrollPosition, new Rect(-50, 0, 480, scrollY));
                        LoadButtons();
                        GUI.EndScrollView();
                    }
                    else if (mainGUIPos == "Bottom Right")
                    {
                        if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(Screen.width - 395, Screen.height - 580, 395, 580), invName);

                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }  
                        scrollPosition = GUI.BeginScrollView(new Rect(Screen.width - 399, Screen.height - 540, 500, 520), scrollPosition, new Rect(-50, 0, 480, scrollY));
                        LoadButtons();
                        GUI.EndScrollView();
                    }
                }
            }
            else if (inventoryMode == "Weight")
            {
                if (useIMG == false)
                {
                    if (mainGUIPos == "Center Screen")
                    {
                        if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(Screen.width / 2 - 280, Screen.height / 2 - 350, 500, 580), invName);

                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }  
                        //Draw Buttons
                        {
                            // GUI.BeginGroup(new Rect(Screen.width / 2 - 230, Screen.height / 2 - 300, 500, 560), "");
                            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 - 282, Screen.height / 2 - 310, 500, 500), scrollPosition, new Rect(-50, 0, 480, scrollY));
                            LoadButtonsWeight();
                            GUI.EndScrollView();
                            //  GUI.EndGroup();
                            GUI.Label(new Rect(Screen.width / 2 - 250, Screen.height / 2 + 190, 250, 35), currentWeight.ToString() + "kg / " + maxWeight.ToString() + "kg");
                        }
                    }
                    else if (mainGUIPos == "Custom")
                    {
                         if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(Screen.width / 2 + (PixelOffSet.x - 2), Screen.height / 2 + (PixelOffSet.y - 40), 500, 580), invName);

                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }
                         scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 + (PixelOffSet.x), Screen.height / 2 + (PixelOffSet.y), scrollSize.x, scrollSize.y), scrollPosition, new Rect(-50, 0, 0, scrollY));
                        LoadButtonsWeight();
                        GUI.EndScrollView();
                        GUI.Label(new Rect(Screen.width / 2 + (totalWeightLabel.x), Screen.height / 2 + (totalWeightLabel.y), 250, 35), currentWeight.ToString() + "kg / " + maxWeight.ToString() + "kg");
                    }
                    else if (mainGUIPos == "Top Left")
                    {
                        if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(0, 0, 500, 580), invName);

                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }  
                        scrollPosition = GUI.BeginScrollView(new Rect(-4, +40, 500, 500), scrollPosition, new Rect(-50, 0, 480, scrollY));
                        LoadButtonsWeight();
                        GUI.EndScrollView();
                        GUI.Label(new Rect(10, Screen.height / 2 + 115, 250, 35), currentWeight.ToString() + "kg / " + maxWeight.ToString() + "kg");
                       
                    }
                    else if (mainGUIPos == "Top Right")
                    {
                        if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(Screen.width - 500, 0, 500, 580), invName);

                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }  
                        scrollPosition = GUI.BeginScrollView(new Rect(Screen.width - 504, +40, 500, 500), scrollPosition, new Rect(-50, 0, 480, scrollY));
                        LoadButtonsWeight();
                        GUI.EndScrollView();
                        GUI.Label(new Rect(Screen.width - 480, Screen.height / 2 + 115, 250, 35), currentWeight.ToString() + "kg / " + maxWeight.ToString() + "kg");
                    }
                    else if (mainGUIPos == "Bottom Left")
                    {
                        if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(0, Screen.height - 580, 500, 580), invName);

                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }  
                        scrollPosition = GUI.BeginScrollView(new Rect(-4, Screen.height - 540, 500, 500), scrollPosition, new Rect(-50, 0, 480, scrollY));
                        LoadButtonsWeight();
                        GUI.EndScrollView();
                        GUI.Label(new Rect(10, Screen.height / 2 + 385, 250, 35), currentWeight.ToString() + "kg / " + maxWeight.ToString() + "kg");
                    }
                    else if (mainGUIPos == "Bottom Right")
                    {
                        if (backGroundIMG == null)
                        {
                            GUI.Box(new Rect(Screen.width - 500, Screen.height - 580, 500, 580), invName);

                        }
                        else
                        {
                            GUI.DrawTexture(new Rect(Screen.width / 2 + (PixelOffSet3.x - 2), Screen.height / 2 + (PixelOffSet3.y - 40), mainBGSize.x, mainBGSize.y), backGroundIMG);
                        }
                        scrollPosition = GUI.BeginScrollView(new Rect(Screen.width - 504, Screen.height - 540, 500, 500), scrollPosition, new Rect(-50, 0, 480, scrollY));
                        LoadButtonsWeight();
                        GUI.EndScrollView();
                        GUI.Label(new Rect(Screen.width - 480, Screen.height / 2 + 385, 250, 35), currentWeight.ToString() + "kg / " + maxWeight.ToString() + "kg");
                    }
                   

                }
            }

        }

        if (allowCrafting == true)
        {
            if (crafting == true)
            {
                
                GUI.DrawTexture(new Rect(Screen.width / 2 + (craftingBGpos.x), Screen.height / 2 + (craftingBGpos.y), craftingBGSize.x, craftingBGSize.y), craftingBG);
                try
                {
                    GUI.TextField(new Rect(Screen.width / 2 + (infoPos.x), Screen.height / 2 + (infoPos.y), infoSize.x, infoSize.y), currentINFO);
                }
                catch { }
                scrollCrafting = GUI.BeginScrollView(new Rect(Screen.width / 2 + (craftingGUIPos.x), Screen.height / 2 + (craftingGUIPos.y), craftingSize.x, craftingSize.y), scrollCrafting, new Rect(0, 0, 0, 0));
                drawCrafting();
                GUI.EndScrollView();
                if (craftedItem != null)
                {
                    if (GUI.Button(new Rect(Screen.width / 2 + (craftingResultPos.x), Screen.height / 2 + (craftingResultPos.y), slotSize.x, slotSize.y), craftedItem.GetComponent<UI_Item>().iconPreview))
                    {
                        if (hasSelected == false)
                        {
                            hasSelected = true;
                            selectedSlot = craftedItem.GetComponent<UI_Item>().itemIndex;
                            craftedItem = null;
                            for (int i = 0; i < 9; i++)
                            {
                                craftingSlots[i] = null;
                            }
                            currentINFO = "";
                        }
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(Screen.width / 2 + (craftingResultPos.x), Screen.height / 2 + (craftingResultPos.y), slotSize.x, slotSize.y), ""))
                    {
                    }
                }


            }
        }

        //DRAG AND DROP
      
        //

        if (allowPlayerEquipment == true)
        {
            if (equipT == true)
            {
                if (equipmentWindowBG != null)
                {
                    GUI.DrawTexture(new Rect(Screen.width / 2 + (equipmentWindowPos.x), Screen.height / 2 + (equipmentWindowPos.y), equipmentWindowSize.x, equipmentWindowSize.y), equipmentWindowBG);
                }
                scrollPosition3 = GUI.BeginScrollView(new Rect(Screen.width / 2 + (slotsPosition.x),Screen.height / 2 + (slotsPosition.y), 220,500), scrollPosition3, new Rect(0, 0, 0, 0));
                DrawEquipmentTab();
                GUI.EndScrollView();
                GUI.Label(new Rect(Screen.width / 2 + equipmentDataPos8.x, Screen.height / 2 + equipmentDataPos8.y, equipmentDataSize8.x,equipmentDataSize8.y), equipmentDataString8);
                GUI.Label(new Rect(Screen.width / 2 + equipmentDataPos7.x, Screen.height / 2 + equipmentDataPos1.y, equipmentDataSize1.x, equipmentDataSize1.y), equipmentDataString1 + " : " + tHealth.ToString());
                GUI.Label(new Rect(Screen.width / 2 + equipmentDataPos6.x, Screen.height / 2 + equipmentDataPos2.y, equipmentDataSize2.x, equipmentDataSize2.y), equipmentDataString2 + " : " + tArmor.ToString());
                GUI.Label(new Rect(Screen.width / 2 + equipmentDataPos5.x, Screen.height / 2 + equipmentDataPos3.y, equipmentDataSize3.x, equipmentDataSize3.y), equipmentDataString3 + " : " + tStrength.ToString());
                GUI.Label(new Rect(Screen.width / 2 + equipmentDataPos4.x, Screen.height / 2 + equipmentDataPos4.y, equipmentDataSize4.x, equipmentDataSize4.y), equipmentDataString4 + " : " + tAbility.ToString());
                GUI.Label(new Rect(Screen.width / 2 + equipmentDataPos3.x, Screen.height / 2 + equipmentDataPos5.y, equipmentDataSize5.x, equipmentDataSize5.y), equipmentDataString5 + " : " + tCold.ToString());
                GUI.Label(new Rect(Screen.width / 2 + equipmentDataPos2.x, Screen.height / 2 + equipmentDataPos6.y, equipmentDataSize6.x, equipmentDataSize6.y), equipmentDataString6 + " : " + tSpeed.ToString());
                GUI.Label(new Rect(Screen.width / 2 + equipmentDataPos1.x, Screen.height / 2 + equipmentDataPos7.y, equipmentDataSize7.x, equipmentDataSize7.y), equipmentDataString7 + " : " + tStealth.ToString());
                if (itemPreview != "")
                {
                    try
                    {
                        displayItemPreview(int.Parse(itemPreview));
                        if (itemPreview != previousPreview)
                        {

                            previousPreview = itemPreview;
                        }
                    }
                    catch { }
                }
            }
        }
        if (isMovingItems == true)
        {

                if (hasSelected == true)
                {
                    if (allItems[selectedSlot].GetComponent<UI_Item>().iconPreview != null)
                    {
                        GUI.DrawTexture(new Rect(mouseXY.x - dragIconSize.x / 2, mouseXY.y - dragIconSize.y / 2, dragIconSize.x, dragIconSize.y), allItems[selectedSlot].GetComponent<UI_Item>().iconPreview);
                    }
                }

        }
    }
    
}
