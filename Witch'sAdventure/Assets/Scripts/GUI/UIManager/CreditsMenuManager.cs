using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class CreditsMenuManager : MonoBehaviour 
{
	public Canvas creditsMenuCanvas;
	
	public void EnableOverlay(bool enabled)
	{
		creditsMenuCanvas.enabled = enabled;
		if(enabled)
		{
			//Initialization code goes here
		}
	}
}

