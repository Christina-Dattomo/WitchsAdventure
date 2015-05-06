using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]

public class UI_Item : MonoBehaviour
{

    public string itemName = "";
    public int itemIndex = 0;
    public string itemDescription = "Des";
    public float itemWeight = 10f;
    public Texture2D iconPreview;
    public KeyCode pickUpItem = KeyCode.E;
    public bool destroyOnUse = false;
    public bool displayMessage = false;
    public string itemOption = "Item", equipmentOption = "Helmet";
    public bool isEquipment = false;
    public float health = 0f, armor = 0, strength = 0, ability = 0, cold = 0, speed = 0, stealth = 0;
    public int foodValue = 0, thirstValue = 0;
    public bool isPoisoned = false;
    public int poisonTime = 0;
    public int maxStack = 10;
    GameObject go;
    // public int itemID = 0;
    public enum itemOptions
    {
        Item = 0,
        Equipment = 1
    }

    public enum equipmentOptions
    {
        Helmet = 0,
        Chest = 1,
        Arms = 2,
        Legs = 3,
        Shoes = 4
    }
    
   //  * Code no longer needed after version 4.x
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            displayMessage = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            displayMessage = false;
        }
    }

    void OnGUI()
    {
        if (displayMessage && gameObject == go)
        {

            if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().inventoryMode == "Weight")
            {
                GUI.Label(new Rect(10, 10, 200, 30), "Press " + pickUpItem.ToString() + " To Pick " + itemName + "(" + itemWeight + "kg)");
            }
            else
            {
                GUI.Label(new Rect(10, 10, 200, 30), "Press " + pickUpItem.ToString() + " To Pick Up " + itemName);
            }
        }
    }

    RaycastHit hit;

    void Update()
    {
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, 5f))
        {
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<UI_Item>() != null)
                {
                    go = hit.collider.gameObject;
                    displayMessage = true;
                }
                else
                {
                    displayMessage = false;
                    go = null;
                }
            }
            else
            {
                displayMessage = false;
                go = null;
            }
        }
        else
        {
            displayMessage = false;
            go = null;
        }

        if (displayMessage && gameObject == go)
        {

            if (Input.GetKeyDown(pickUpItem) && GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().inventoryMode == "Slot")
            {
                int tmp = GetOpenSlot();
                if (tmp != int.MaxValue)
                {
                    if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allowStacking == true)
                    {
                        GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().stackArray[tmp] += 1;
                    }
                    GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems[tmp] = GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allItems[itemIndex].gameObject;
                    GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().PlaySound(GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().pickUpSound);
                    GameObject.Destroy(gameObject);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().PlaySound(GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().cantPickUp);

                }

            }
            else if (Input.GetKeyDown(pickUpItem) && GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().inventoryMode == "Weight")
            {

                if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentWeight + itemWeight <= GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().maxWeight)
                {
                    int tmp = GetOpenSlot();
                    if (tmp != int.MaxValue)
                    {
                        GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentWeight += itemWeight;
                        GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems[tmp] = GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allItems[itemIndex].gameObject;
                        GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().PlaySound(GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().pickUpSound);
                        if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allowStacking == true)
                        {
                            GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().stackArray[tmp] += 1;
                        }
                        GameObject.Destroy(gameObject);
                    }
                }
                else
                {
                    GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().PlaySound(GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().cantPickUp);
                }

            }
        }
    }

    private int GetOpenSlot()
    {
        for (int i = 0; i < GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems.Length; i++)
        {
            if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems[i] == null)
            {
                return i;
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().allowStacking == true)
                {
                    if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().currentItems[i].GetComponent<UI_Item>().itemIndex == transform.GetComponent<UI_Item>().itemIndex)
                    {
                        if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().stackArray[i] < maxStack)
                        {
                            return i;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        return int.MaxValue;
    }

}
