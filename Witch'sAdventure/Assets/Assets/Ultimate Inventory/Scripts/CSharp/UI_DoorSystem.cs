using UnityEngine;
using System.Collections;

public class UI_DoorSystem : MonoBehaviour
{

    public bool isLocked = false;
    public bool isOpen = false;
    public int keyID = 0;
    public KeyCode toggleKey = KeyCode.Mouse1;
    public string mode = "MovingDoor"; //MovingDoor , RotatingDoor,
    public Vector3 ClosedPosition, OpenedPosition;
    public Vector3 ClosedRot, OpenedRot;
    public Transform doorPanel;
    public float moveSpeed, rotSpeed;
    bool canUse = false;
    public AudioClip openDoor, closeDoor, doorLocked, unlockDoor;

    Vector3 targetPosition;
    Vector3 targetRotation;

    string toChange = "";

    void Start()
    {
        if (isOpen == true)
            targetPosition = OpenedPosition;
        else
            targetPosition = ClosedPosition;
    }

    void Update()
    {
        if (canUse == true && Input.GetKeyDown(toggleKey))
        {
            DoorSystem();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Collider>().tag == "Player")
        {
            canUse = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.GetComponent<Collider>().tag == "Player")
        {
            canUse = false;
        }
    }

    void DoorSystem()
    {
        if (isLocked == false)
        {
            if (isOpen == true)
            {
                if (mode == "MovingDoor")
                {
                    targetPosition = ClosedPosition;
                }
                else if (mode == "RotatingDoor")
                {
                    targetRotation = ClosedRot;
                }
                GetComponent<AudioSource>().PlayOneShot(openDoor);
                toChange = "False";
            }
            else
            {
                if (mode == "MovingDoor")
                {
                    targetPosition = OpenedPosition;
                }
                else if (mode == "RotatingDoor")
                {
                    targetRotation = OpenedRot;
                }
                GetComponent<AudioSource>().PlayOneShot(closeDoor);
                toChange = "True";
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("UInventory").GetComponent<UInventory>().CheckForKey(keyID) == true)
            {
                isLocked = false;
                GetComponent<AudioSource>().PlayOneShot(unlockDoor);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(doorLocked);
            }
        }
    }

    void FixedUpdate()
    {
        if (mode == "MovingDoor")
        {
            if (V3Equal(doorPanel.localPosition, targetPosition) == false)
            {
                doorPanel.localPosition = Vector3.Slerp(doorPanel.localPosition, targetPosition, moveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                if (toChange != "")
                {
                    isOpen = bool.Parse(toChange);
                    toChange = "";
                }
            }
        }
        else if (mode == "RotatingDoor")
        {
            if (V3Equal(doorPanel.localRotation.eulerAngles,targetRotation) == false)
            {
                doorPanel.localRotation = Quaternion.Euler(Vector3.Slerp(doorPanel.localRotation.eulerAngles,targetRotation,rotSpeed * Time.fixedDeltaTime));
            }
            else
            {
                if (toChange != "")
                {
                    isOpen = bool.Parse(toChange);
                    toChange = "";
                }
            }
        }

    }

    public bool V3Equal(Vector3 a, Vector3 b)
    {
        return Vector3.SqrMagnitude(a - b) < 0.0001;
    }

}