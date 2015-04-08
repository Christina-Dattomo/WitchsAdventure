using UnityEngine;
using System.Collections;

public class UI_PlayerScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }
    RaycastHit hit, hit2;
    public bool itemOnSight = false;
    // Update is called once per frame
    void Update()
    {
        if (hit.collider.gameObject) //IF I HIT THE ITEM WITH RAYCAST
        {

            itemOnSight = true; //THIS DOESNT DO ANYTHING ON THE PLAYER SCRIPT
            hit.collider.SendMessageUpwards("PickUp", SendMessageOptions.DontRequireReceiver); //ACTIVATES THE BOOL PICKUP IN UI_ITEM
            {
                if (hit2.collider.gameObject.GetComponent<UI_Item>() == null)
                {
                    itemOnSight = false;
                }
            }
        }
    }
}