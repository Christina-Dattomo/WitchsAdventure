using UnityEngine;
using System.Collections;

public class ActivateDialogueInstance : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider NPC)
	{
		switch (NPC.tag) 
		{
		case "Woman1Dialogue":
			break;
		}
	}
}
