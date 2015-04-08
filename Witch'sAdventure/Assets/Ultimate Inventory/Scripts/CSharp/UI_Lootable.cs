using UnityEngine;
using System.Collections;

public class UI_Lootable : MonoBehaviour {

    public int containerID = 0;
    public GameObject[] containerItems;
    public GUISkin guiSkin;
    public GameObject[] slots;
    public KeyCode toggleContainerKey = KeyCode.E;
    public float maxDistance = 9f;
    public bool autoToggleDragDrop = true;
    public int scrollY = 250;

    public Texture2D containerBG;
    public Vector2 bgPosition = new Vector2(-568.9f, -208);
    public Vector2 bgSize = new Vector2(350, 250);
    public Vector2 slotPos = new Vector2(300, 230);
    public Vector2 slotSize = new Vector2(64, 64);
    public Vector2 slotScroll = new Vector2(350, 250);
    Vector2 scrollPosition = Vector2.zero;
    public bool isAnimated = true;
    public GameObject animationHolder;
    public string openAnim = "openContainer", closeAnim = "closeContainer";
    bool open = false;
    bool canOpen = false;
    GameObject uinv;
    float x1, x2, y1, y2;
    public int maxStack = 10;
    public int[] stackArray;
    int aimID = 0;
    string itemPreview = "",previousPreview = "";
    void Start()
    {
        uinv = GameObject.FindGameObjectWithTag("UInventory");
    }
    bool playOnce = false;
    RaycastHit hit;

    void Update()
    {
        if (canOpen == true && aimID == containerID)
        {
            if (Input.GetKeyDown(toggleContainerKey))
            {
                 if (isAnimated == true)
                {
                    if (open == false)
                    {
                        animationHolder.GetComponent<Animation>().Play(openAnim);
                        playOnce = false;
                       
                    }
                    else
                    {
                        animationHolder.GetComponent<Animation>().Play(closeAnim);
                       
                    }
                }
                open = !open;

                if (autoToggleDragDrop == true)
                {
                    uinv.GetComponent<UInventory>().isMovingItems = open;
                }
            }
        }

        if (Vector3.Distance(uinv.transform.position, transform.position) > maxDistance)
        {
            if (playOnce == false && open == true)
            {
                open = false;
                animationHolder.GetComponent<Animation>().Play(closeAnim);
                playOnce = true;
            }
        }

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, maxDistance))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<UI_Lootable>() != null)
                {
                    canOpen = true;
                    aimID = hit.collider.gameObject.GetComponent<UI_Lootable>().containerID;
                }
                else
                {
                    canOpen = false;
                }
            }
            else
            {
                canOpen = false;
            }
        }

    }

    void LoadButtons()
    {
        if (Event.current.button > 0 && Event.current.type != EventType.Repaint
&& Event.current.type != EventType.Layout)
            Event.current.Use();

        for (int r = 0; r < slots.Length / 4 + 1; r++)
        {
            for (int s = 0; s < 4; s++)
            {
                int thisIndex = r * 4 + s;
                if (thisIndex < slots.Length)
                {
                    try
                    {
                        if (slots[thisIndex] != null)
                        {

                            if (uinv.GetComponent<UInventory>().iconPreview == false)
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y),new GUIContent(slots[thisIndex].GetComponent<UI_Item>().itemName, slots[thisIndex].GetComponent<UI_Item>().itemIndex.ToString())))
                                {
                                    if (uinv.GetComponent<UInventory>().isMovingItems == false)
                                    {
                                        uinv.GetComponent<UInventory>().isMovingItems = true;
                                    }
                                    else
                                    {
                                        if (uinv.GetComponent<UInventory>().hasSelected == false)
                                        {
                                            uinv.GetComponent<UInventory>().hasSelected = true;
                                            uinv.GetComponent<UInventory>().selectedSlot = slots[thisIndex].GetComponent<UI_Item>().itemIndex;
                                            slots[thisIndex] = null;
                                        }

                                    }
                                    itemPreview = GUI.tooltip;
                                }
                                
                            }
                            else
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), new GUIContent(slots[thisIndex].GetComponent<UI_Item>().iconPreview, slots[thisIndex].GetComponent<UI_Item>().itemIndex.ToString())))
                                {
                                    if (uinv.GetComponent<UInventory>().isMovingItems == false)
                                    {
                                        uinv.GetComponent<UInventory>().isMovingItems = true;
                                    }
                                    else
                                    {
                                        if (uinv.GetComponent<UInventory>().hasSelected == false)
                                        {
                                            uinv.GetComponent<UInventory>().hasSelected = true;
                                            uinv.GetComponent<UInventory>().selectedSlot = slots[thisIndex].GetComponent<UI_Item>().itemIndex;
                                            slots[thisIndex] = null;
                                        }

                                    }
                                    itemPreview = GUI.tooltip;
                                    Debug.Log(itemPreview);
                                }
                                // GUI.Label(new Rect(s * 100, r * 100, 80, 32), slotsStack[thisIndex].ToString());
                            }
                        }
                        else
                        {
                            if (uinv.GetComponent<UInventory>().inventoryMode == "Slot")
                            {
                                if (uinv.GetComponent<UInventory>().iconPreview == false)
                                {
                                    if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), uinv.GetComponent<UInventory>().emptySlotText))
                                    {
                                        if (uinv.GetComponent<UInventory>().isMovingItems == true)
                                        {
                                            if (uinv.GetComponent<UInventory>().hasSelected == true)
                                            {
                                                slots[thisIndex] = uinv.GetComponent<UInventory>().allItems[uinv.GetComponent<UInventory>().selectedSlot];
                                                uinv.GetComponent<UInventory>().hasSelected = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (uinv.GetComponent<UInventory>().emptyIcon != null)
                                    {
                                        if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), uinv.GetComponent<UInventory>().emptyIcon))
                                        {
                                            if (uinv.GetComponent<UInventory>().isMovingItems == true)
                                            {
                                                if (uinv.GetComponent<UInventory>().hasSelected == true)
                                                {
                                                    slots[thisIndex] = uinv.GetComponent<UInventory>().allItems[uinv.GetComponent<UInventory>().selectedSlot];
                                                    uinv.GetComponent<UInventory>().hasSelected = false;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), uinv.GetComponent<UInventory>().emptySlotText))
                                        {
                                            if (uinv.GetComponent<UInventory>().isMovingItems == true)
                                            {
                                                if (uinv.GetComponent<UInventory>().hasSelected == true)
                                                {
                                                    slots[thisIndex] = uinv.GetComponent<UInventory>().allItems[uinv.GetComponent<UInventory>().selectedSlot];
                                                    uinv.GetComponent<UInventory>().hasSelected = false;
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
        for (int r = 0; r < slots.Length / 4 + 1; r++)
        {
            for (int s = 0; s < 4; s++)
            {
                int thisIndex = r * 4 + s;
                if (thisIndex < slots.Length)
                {
                    try
                    {
                        if (slots[thisIndex] != null)
                        {
                            if (uinv.GetComponent<UInventory>().iconPreview == false)
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), slots[thisIndex].GetComponent<UI_Item>().itemName))
                                {
                                    if (uinv.GetComponent<UInventory>().hasSelected == false)
                                    {
                                        uinv.GetComponent<UInventory>().hasSelected = true;
                                        uinv.GetComponent<UInventory>().selectedSlot = slots[thisIndex].GetComponent<UI_Item>().itemIndex;
                                        slots[thisIndex] = null;

                                    }
                                }
                                GUI.Label(new Rect(s * slotSize.x, r * slotSize.y, 80, 32), slots[thisIndex].GetComponent<UI_Item>().itemWeight.ToString() + "kg");
                            }
                            else
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), slots[thisIndex].GetComponent<UI_Item>().iconPreview))
                                {


                                  
                                        if (uinv.GetComponent<UInventory>().hasSelected == false)
                                        {
                                            uinv.GetComponent<UInventory>().hasSelected = true;
                                            uinv.GetComponent<UInventory>().selectedSlot = slots[thisIndex].GetComponent<UI_Item>().itemIndex;
                                            slots[thisIndex] = null;

                                        }

                                    GUI.Label(new Rect(s * slotSize.x, r * slotSize.y, 80, 32), slots[thisIndex].GetComponent<UI_Item>().itemWeight.ToString() + "kg");

                                }
                            }
                        }
                        else
                        {
                            if (uinv.GetComponent<UInventory>().emptyIcon == null)
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), uinv.GetComponent<UInventory>().emptySlotText))
                                {
                                    if (uinv.GetComponent<UInventory>().isMovingItems == true)
                                    {
                                        if (uinv.GetComponent<UInventory>().hasSelected == true)
                                        {
                                                slots[thisIndex] = uinv.GetComponent<UInventory>().allItems[uinv.GetComponent<UInventory>().selectedSlot];
                                                uinv.GetComponent<UInventory>().hasSelected = false;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (GUI.Button(new Rect(s * slotSize.x, r * slotSize.y, slotSize.x, slotSize.y), uinv.GetComponent<UInventory>().emptyIcon))
                                {
                                    if (uinv.GetComponent<UInventory>().isMovingItems == true)
                                    {
                                        if (uinv.GetComponent<UInventory>().hasSelected == true)
                                        {

                                           
                                            slots[thisIndex] = uinv.GetComponent<UInventory>().allItems[uinv.GetComponent<UInventory>().selectedSlot];
                                                // hasSelected = false;
                                            uinv.GetComponent<UInventory>().hasSelected = false;

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
    void displayItemPreview(int itemIndex)
    {
        uinv.GetComponent<UInventory>().itemPreviewPos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        GUI.Box(new Rect(uinv.GetComponent<UInventory>().itemPreviewPos.x, uinv.GetComponent<UInventory>().itemPreviewPos.y, uinv.GetComponent<UInventory>().itemPreviewSize.x, uinv.GetComponent<UInventory>().itemPreviewSize.y), uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().itemName);
        string toDisplay = uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().itemDescription;
        if (uinv.GetComponent<UInventory>().displayStats == true)
        {
            //  if (allItems[itemIndex].GetComponent<UI_Item>().itemOption == "Equipment")
            //  {
            toDisplay += System.Environment.NewLine;
            if (uinv.GetComponent<UInventory>().aHealth == true)
            {
                if (uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().health != 0)
                {
                    toDisplay += System.Environment.NewLine + uinv.GetComponent<UInventory>().equipmentDataString1 + " : " + uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().health.ToString();
                }
            }
            if (uinv.GetComponent<UInventory>().aArmor == true)
            {
                if (uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().armor != 0)
                {
                    toDisplay += System.Environment.NewLine + uinv.GetComponent<UInventory>().equipmentDataString2 + " : " + uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().armor.ToString();
                }
            }
            if (uinv.GetComponent<UInventory>().aStrength == true)
            {
                if (uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().strength != 0)
                {
                    toDisplay += System.Environment.NewLine + uinv.GetComponent<UInventory>().equipmentDataString3 + " : " + uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().strength.ToString();
                }
            }
            if (uinv.GetComponent<UInventory>().aAbility == true)
            {
                if (uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().ability != 0)
                {
                    toDisplay += System.Environment.NewLine + uinv.GetComponent<UInventory>().equipmentDataString4 + " : " + uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().ability.ToString();
                }
            }
            if (uinv.GetComponent<UInventory>().aCold == true)
            {
                if (uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().cold != 0)
                {
                    toDisplay += System.Environment.NewLine + uinv.GetComponent<UInventory>().equipmentDataString5 + " : " + uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().cold.ToString();
                }
            }
            if (uinv.GetComponent<UInventory>().aSpeed == true)
            {
                if (uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().speed != 0)
                {
                    toDisplay += System.Environment.NewLine + uinv.GetComponent<UInventory>().equipmentDataString6 + " : " + uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().speed.ToString();
                }
            }
            if (uinv.GetComponent<UInventory>().aStealth == true)
            {
                if (uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().stealth != 0)
                {
                    toDisplay += System.Environment.NewLine + uinv.GetComponent<UInventory>().equipmentDataString7 + " : " + uinv.GetComponent<UInventory>().allItems[itemIndex].GetComponent<UI_Item>().stealth.ToString();
                }
            }
            //   }//
        }
        GUI.TextArea(new Rect(uinv.GetComponent<UInventory>().itemPreviewPos.x + 11, (uinv.GetComponent<UInventory>().itemPreviewPos.y) + 19, uinv.GetComponent<UInventory>().itemPreviewSize.x - 20, uinv.GetComponent<UInventory>().itemPreviewSize.y - 20), toDisplay);
    }
    void OnGUI()
    {
        GUI.skin = guiSkin;
        if (open == true)
        {
            if (containerBG != null)
            {
                GUI.DrawTexture(new Rect(Screen.width / 2 + (bgPosition.x), Screen.height / 2 + (bgPosition.y), bgSize.x, bgSize.y), containerBG);
            }
            else
            {
                GUI.Box(new Rect(Screen.width / 2 + (bgPosition.x), Screen.height / 2 + (bgPosition.y), bgSize.x, bgSize.y), "");
            }
            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 + (slotPos.x), Screen.height / 2 +slotPos.y, slotScroll.x, slotScroll.y), scrollPosition, new Rect(0, 0, 350, scrollY));
            if (uinv.GetComponent<UInventory>().inventoryMode == "Slots")
            {
                LoadButtons();
            }
            else
            {
                LoadButtonsWeight();
            }
            GUI.EndScrollView();

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
}
