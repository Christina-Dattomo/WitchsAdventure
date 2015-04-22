using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LevelFailManager : MonoBehaviour 
{
	public Canvas levelFailCanvas;
	
	public void EnableOverlay(bool enabled)
	{
		levelFailCanvas.enabled = enabled;
		if(enabled)
		{
			//Initialization code goes here
		}
	}
}

