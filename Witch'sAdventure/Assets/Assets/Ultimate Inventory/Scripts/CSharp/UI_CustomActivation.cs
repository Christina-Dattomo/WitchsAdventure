using UnityEngine;
using System.Collections;

public class UI_CustomActivation : MonoBehaviour {
    string toDisplay = "";
    public GUISkin skin;
  //  public HungerSystemExample hungerSystem;

    int timer = 0;
    public void CustomActivate(int itemIndex,bool destroyAfterUse)
    {
        /*
         * Type your custom activation code here
         * use :
         * GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems[itemIndex] 
         * To Access The GameObject With Index  the itemIndex integer
         * */
        if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems[itemIndex].GetComponent<UI_Item>().itemOption == "Food")
        {
            // apply hunger effect  hungerSystem.hunger += GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems[itemIndex].GetComponent<UI_Item>().foodValue;
            if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems[itemIndex].GetComponent<UI_Item>().isPoisoned == true)
            {
                //apply poison effect
            }
        }

        //Do Not Remove This Code
        if (destroyAfterUse == true)
        {
            if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allowStacking == true)
            {
                if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().stackArray[itemIndex] > 1)
                {
                    GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().stackArray[itemIndex]--;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems[itemIndex] = null;
                }
            }
            
        }
        //End Of Not Removable Code
    }


    public void DefaultActivate(int itemIndex, bool destroyAfterUse)
    {

       toDisplay = GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems[itemIndex].GetComponent<UI_Item>().itemName;
       timer = 100;
        if (destroyAfterUse == true)
        {
            GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems[itemIndex] = null;
        }
    }
    void Update()
    {
        if (timer > 0)
        {
            timer--;
        }

        if (timer <= 0)
        {
            timer = 0;
            toDisplay = "";
        }
    }
    void OnGUI()
    {

        GUI.skin = skin;
        GUILayout.Label(toDisplay,GUILayout.Width(500));
        
    }
}
