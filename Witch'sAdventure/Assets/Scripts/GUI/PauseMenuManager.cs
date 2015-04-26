using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenuManager : MonoBehaviour 
{
	public Canvas pauseMenuCanvas;
	
	public void EnableOverlay(bool enabled)
	{
		pauseMenuCanvas.enabled = enabled;
		if(enabled)
		{
			//Initialization code goes here
		}
	}
}

