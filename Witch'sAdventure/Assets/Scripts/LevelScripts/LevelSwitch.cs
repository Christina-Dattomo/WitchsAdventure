using UnityEngine;
using System.Collections;

public class LevelSwitch : MonoBehaviour {

	public void OnTitleButton(){
		Application.LoadLevel ("LevelOne");
	}

	public void OnTriggerEnter(Collider other){
		switch(other.tag)
		{
		case "Castle Entrance":
			Application.LoadLevel ("Castle Interior");
			break;

		case "Castle Exit":
			Application.LoadLevel ("LevelOne");
			break;

		default:
			break;
		}

	}

}
