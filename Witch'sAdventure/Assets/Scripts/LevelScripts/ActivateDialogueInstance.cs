using UnityEngine;
using System.Collections;

public class ActivateDialogueInstance : MonoBehaviour {
	bool canInitiateDialogue = false;
	int currentNPC = 0;

	void Update()
	{

		if (canInitiateDialogue && Input.GetKeyDown ("enter")) 
		{
			switch (currentNPC)
			{
			case 1:
				//initiate NPC1 dialogue
				break;
			case 2:
				//initiate NPC2 dialogue
				break;
			case 0:
				//do nothing
				break;
			}
		}

	}

	void OnTriggerEnter(Collider NPC)
	{
		switch (NPC.tag) 
		{
		case "Woman1Dialogue":
			canInitiateDialogue = true;
			currentNPC = 1;
			break;
		}
	}
}
