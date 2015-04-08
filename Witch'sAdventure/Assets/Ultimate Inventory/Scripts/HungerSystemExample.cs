using UnityEngine;
using System.Collections;

public class HungerSystemExample : MonoBehaviour {

    public float hunger = 50;

    void OnGUI()
    {
        GUILayout.Label("HUNGER : " + hunger.ToString());
    }
}
