using UnityEngine;
using System.Collections;

public class UI_BuildingSystemUI : MonoBehaviour {

    public UI_BuildingSystem buildingSystem;
    public Texture2D bookbg;
    public Texture2D[] buildingPreview;
    public Texture2D defaultIcon;
    public string ItemsInfo; // <name>String name</name> <des>String description</des> int X,int Y [X = item index, Y = item quantity]
    public Vector2 bgPosition = new Vector2(0, 0);
    public Vector2 bgSize = new Vector2(0, 0);
    public int pages = 1;
    public int currentPage = 1;
    public bool isOpen = false;
    public KeyCode toggle = KeyCode.B;
    public GUISkin skin,skin2;
    public bool showHideCursor = true;
    public bool disableMouseLook = true;
    private string[] itemsInfo;
    public int totalBuildings = 4;

   

    void GetInfo()
    {
        itemsInfo = ItemsInfo.Split('|');
    }

    void Start()
    {
        GetInfo();
        LoadBuildings();
        if (bgSize.x == 0 || bgSize.y == 0)
        {
            bgSize.x = bookbg.width;
            bgSize.y = bookbg.height;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(toggle))
        {
            ToggleMenu();
        }
    }

    void DrawBuild(int index)
    {
       
    }

    void CallBuild(int index,string needItems)
    {
        if (needItems.Split('@').Length != 0)
        {
            string[] lines = needItems.Split('@');
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "")
                {
                    GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().RemoveItems(int.Parse( lines[i].Split(',')[0]),int.Parse( lines[i].Split(',')[1]));
                }
            }
        }
        else
        {
           
        }
    }

    Vector2 scrollPosition = Vector2.zero;
    public Vector2[] iconSize;
    public Vector2[] textPreviewSize;
    public Vector2[] buildButtonSize;
    public Vector2[] buildingItemsPos = new Vector2[4];
    public Vector2[] boxPosition = new Vector2[4];
    public Vector2[] iconPosition = new Vector2[4];
    public Vector2[] buttonPos = new Vector2[4];
    public int[] callID = new int[4];
    void OnGUI()
    {
        GUI.skin = skin;
        if (isOpen == true)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 + bgPosition.x, Screen.height / 2 + bgPosition.y, bgSize.x, bgSize.y), bookbg);
            //First Slot
            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 + buildingItemsPos[0].x, Screen.height / 2 + buildingItemsPos[0].y, 430, 310), scrollPosition, new Rect(0, 0, 0, 0));
            GUILayout.BeginHorizontal();
            GUI.Box(new Rect(0, 0, 430, 310), buttonNames[0]);
            if (buildingPreview[0] == null)
            {
                buildingPreview[0] = defaultIcon;
            }
            GUI.DrawTexture(new Rect(150, 30,iconSize[0].x, iconSize[0].y), buildingPreview[0]);
            string needItems = "";
            string removeItems = "";
            bool canBuild = false;
            string[] nd = new string[999];
            string[] ha = new string[999];
            for (int i = 0; i < countItems[0]; i++)
            {
               string[] twoParts = GetStrBetweenTags(buttonCallItem[0], "<k" + i.ToString() + "k>", "<k" + i.ToString() + "k/>").Split(',');
               needItems += " " + GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allItems[int.Parse(twoParts[0])].GetComponent<UI_Item>().itemName + " : " + GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().CountItem(int.Parse(twoParts[0])) + " / " + twoParts[1].ToString() + System.Environment.NewLine;
               removeItems += GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allItems[int.Parse(twoParts[0])].GetComponent<UI_Item>().itemIndex + "," + twoParts[1].ToString() + "@";
               nd[i] = twoParts[1].ToString();
               ha[i] = GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().CountItem(int.Parse(twoParts[0])).ToString();
            }
            int S = 0;
            for (int l = 0; l < countItems[0]; l++)
            {
                if (nd[l] != "" && ha[l] != "")
                {
                    if (int.Parse(ha[l]) >= int.Parse(nd[l]))
                    {
                        S++;
                    }
                    else
                    {
                        S--;
                    }
                }
            }
            if (S == countItems[0])
            {
                canBuild = true;
            }
            else
                canBuild = false;
            GUI.TextArea(new Rect(15, 165, textPreviewSize[0].x, textPreviewSize[0].y), descriptions[0] + System.Environment.NewLine + System.Environment.NewLine + needItems);
            GUI.enabled = canBuild;
            if (GUI.Button(new Rect(385, 267, buildButtonSize[0].x, buildButtonSize[0].y),""))
            {
                CallBuild(callID[0],removeItems);
            }
            GUI.enabled = true;
            GUILayout.EndHorizontal();
            GUI.EndScrollView();
            //Second Slot
            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 + buildingItemsPos[1].x, Screen.height / 2 + buildingItemsPos[1].y, 430, 310), scrollPosition, new Rect(0, 0, 0, 0));
            GUILayout.BeginHorizontal();
            GUI.Box(new Rect(0, 0, 430, 310), buttonNames[1]);
            if (buildingPreview[1] == null)
            {
                buildingPreview[1] = defaultIcon;
            }
            GUI.DrawTexture(new Rect(150, 30, iconSize[1].x, iconSize[1].y), buildingPreview[1]);
            string needItems2 = "";
            bool canBuild2 = false;
            string[] nd2 = new string[999];
            string[] ha2 = new string[999];
            string removeItems2 = "";
            for (int i = 0; i < countItems[1]; i++)
            {
                string[] twoParts = GetStrBetweenTags(buttonCallItem[1], "<k" + i.ToString() + "k>", "<k" + i.ToString() + "k/>").Split(',');
                needItems2 += " " + GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allItems[int.Parse(twoParts[0])].GetComponent<UI_Item>().itemName + " : " + GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().CountItem(int.Parse(twoParts[0])) + " / " + twoParts[1].ToString() + System.Environment.NewLine;
                removeItems2 += GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allItems[int.Parse(twoParts[0])].GetComponent<UI_Item>().itemIndex + "," + twoParts[1].ToString() + "@";
                nd2[i] = twoParts[1].ToString();
                ha2[i] = GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().CountItem(int.Parse(twoParts[0])).ToString();
            }
            int S2 = 0;
            for (int l = 0; l < countItems[1]; l++)
            {
                if (nd2[l] != "" && ha2[l] != "")
                {
                    if (int.Parse(ha2[l]) >= int.Parse(nd2[l]))
                    {
                        S2++;
                    }
                    else
                    {
                        S2--;
                    }
                }
            }
            if (S2 == countItems[1])
            {
                canBuild2 = true;
            }
            else
                canBuild2 = false;
            GUI.TextArea(new Rect(15, 165, textPreviewSize[1].x, textPreviewSize[1].y), descriptions[1] + System.Environment.NewLine + System.Environment.NewLine + needItems2);
            GUI.enabled = canBuild2;
            if (GUI.Button(new Rect(385, 267, buildButtonSize[1].x, buildButtonSize[1].y), ""))
            {
                CallBuild(callID[1], removeItems2);
            }
            GUI.enabled = true;
            GUILayout.EndHorizontal();
            GUI.EndScrollView();
            //Third Item
            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 + buildingItemsPos[2].x, Screen.height / 2 + buildingItemsPos[2].y, 430, 310), scrollPosition, new Rect(0, 0, 0, 0));
            GUILayout.BeginHorizontal();
            GUI.Box(new Rect(0, 0, 430, 310), buttonNames[2]);
            if (buildingPreview[2] == null)
            {
                buildingPreview[2] = defaultIcon;
            }
            GUI.DrawTexture(new Rect(150, 30, iconSize[2].x, iconSize[2].y), buildingPreview[2]);
            
            bool canBuild3 = false;
            string[] nd3 = new string[999];
            string[] ha3 = new string[999];
            string needItems3 = "";
            string removeItems3 = "";
            for (int i = 0; i < countItems[2]; i++)
            {
               string[] twoParts = GetStrBetweenTags(buttonCallItem[2], "<k" + i.ToString() + "k>", "<k" + i.ToString() + "k/>").Split(',');
               needItems3 += " " + GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allItems[int.Parse(twoParts[0])].GetComponent<UI_Item>().itemName + " : " + GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().CountItem(int.Parse(twoParts[0])) + " / " + twoParts[1].ToString() + System.Environment.NewLine;
               removeItems3 += GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allItems[int.Parse(twoParts[0])].GetComponent<UI_Item>().itemIndex + "," + twoParts[1].ToString() + "@";
               nd3[i] = twoParts[1].ToString();
               ha3[i] = GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().CountItem(int.Parse(twoParts[0])).ToString();
            }
            int S3 = 0;
            for (int l = 0; l < countItems[2]; l++)
            {
                if (nd3[l] != "" && ha3[l] != "")
                {
                    if (int.Parse(ha3[l]) >= int.Parse(nd3[l]))
                    {
                        S3++;
                    }
                    else
                    {
                        S3--;
                    }
                }
            }
            if (S3 == countItems[2])
            {
                canBuild3 = true;
            }
            else
                canBuild3 = false;
            GUI.TextArea(new Rect(15, 165, textPreviewSize[2].x, textPreviewSize[2].y), descriptions[2] + System.Environment.NewLine + System.Environment.NewLine + needItems3);
            GUI.enabled = canBuild3;
            if (GUI.Button(new Rect(385, 267, buildButtonSize[2].x, buildButtonSize[2].y), ""))
            {
                CallBuild(callID[2], removeItems3);
            }
            GUI.enabled = true;
            GUILayout.EndHorizontal();
            GUI.EndScrollView();
            //Fourth Item
            scrollPosition = GUI.BeginScrollView(new Rect(Screen.width / 2 + buildingItemsPos[3].x, Screen.height / 2 + buildingItemsPos[3].y, 430, 310), scrollPosition, new Rect(0, 0, 0, 0));
            GUILayout.BeginHorizontal();
            GUI.Box(new Rect(0, 0, 430, 310), buttonNames[3]);
            if (buildingPreview[3] == null)
            {
                buildingPreview[3] = defaultIcon;
            }
            GUI.DrawTexture(new Rect(150, 30, iconSize[3].x, iconSize[3].y), buildingPreview[3]);
            string needItems4 = "";
            bool canBuild4 = false;
            string[] nd4 = new string[999];
            string[] ha4 = new string[999];
            string removeItems4 = "";
            for (int i = 0; i < countItems[3]; i++)
            {
                string[] twoParts = GetStrBetweenTags(buttonCallItem[3], "<k" + i.ToString() + "k>", "<k" + i.ToString() + "k/>").Split(',');
                needItems4 += " " + GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allItems[int.Parse(twoParts[0])].GetComponent<UI_Item>().itemName + " : " + GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().CountItem(int.Parse(twoParts[0])) + " / " + twoParts[1].ToString() + System.Environment.NewLine;
                removeItems4 += GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allItems[int.Parse(twoParts[0])].GetComponent<UI_Item>().itemIndex + "," + twoParts[1].ToString() + "@";
                nd4[i] = twoParts[1].ToString();
                ha4[i] = GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().CountItem(int.Parse(twoParts[0])).ToString();
            }
            int S4 = 0;
            for (int l = 0; l < countItems[3]; l++)
            {
                if (nd4[l] != "" && ha4[l] != "")
                {
                    if (int.Parse(ha4[l]) >= int.Parse(nd4[l]))
                    {
                        S4++;
                    }
                    else
                    {
                        S4--;
                    }
                }
            }
            if (S4 == countItems[3])
            {
                canBuild4 = true;
            }
            else
                canBuild4 = false;
            GUI.TextArea(new Rect(15, 165, textPreviewSize[3].x, textPreviewSize[3].y), descriptions[3] + System.Environment.NewLine + System.Environment.NewLine + needItems4);
            GUI.enabled = canBuild4;
            if (GUI.Button(new Rect(385, 267, buildButtonSize[3].x, buildButtonSize[3].y), ""))
            {
                CallBuild(callID[3], removeItems4);
            }
            GUI.enabled = true;
            GUILayout.EndHorizontal();
            GUI.EndScrollView();

            GUI.skin = skin2;
            if (currentPage < pages)
            {
                GUI.enabled = true;
            }
            else
            {
                GUI.enabled = false;
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 380, Screen.height / 2 + 310, 100, 25), "Next"))
            {
                if (currentPage < pages)
                {
                    currentPage++;
                    LoadBuildings();
                }
            }
            if (currentPage <= 1)
            {
                GUI.enabled = false;
            }
            else
            {
                GUI.enabled = true;
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 275, Screen.height / 2 + 310, 100, 25), "Previous"))
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    LoadBuildings();
                }
            }
            GUI.enabled = true;
           
        }
    }
    float x1, x2, y1, y2;
    public GameObject mainPlayer;
    void ToggleMenu()
    {
        isOpen = !isOpen;
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
    }

    string[] buttonNames = new string[4];
    string[] descriptions = new string[4];
    string[] buttonCallItem = new string[4];
    int[] countItems = new int[4];

    void LoadBuildings()
    {
        int limit = (currentPage * 4) - 1;
        int c = 0;
        int p = 0;
        for (int i = (currentPage * 4) - 4; i <= limit; i++)
        {
            string[] lines = itemsInfo[i].Split('\n');
            buttonNames[c] = GetStrBetweenTags(lines[0], "<name>", "</name>");
            descriptions[c] = GetStrBetweenTags(lines[1], "<des>", "</des>");
            callID[c] = int.Parse(GetStrBetweenTags(lines[2], "<id>", "</id>"));
            for (int x = 0; x < lines.Length; x++)
            {
                if (!lines[x].Contains("<name>") && !lines[x].Contains("<des>") && !lines[x].Contains("<id>"))
                {
                    string[] n = lines[x].Split(',');
                    buttonCallItem[c] += "<k" + p + "k>" + n[0] + " , " + n[1] + "<k" + p + "k/> ";
                    p++;
                }
            }
            countItems[c] = p;
         //   Debug.Log(buttonNames[c] + " , " + descriptions[c] + " , " + buttonCallItem[c]);
            p = 0;
            c++;
        }

        for (int i = 0; i < buildingPreview.Length; i++)
        {
            
        }
    }

    public static string GetStrBetweenTags(string value, string startTag, string endTag)
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
}
