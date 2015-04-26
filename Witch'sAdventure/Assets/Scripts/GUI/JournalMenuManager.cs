using UnityEngine;
using System.Collections;

public class JournalMenuManager : MonoBehaviour {

	public Canvas journalMenuCanvas;
	
	public void EnableOverlay(bool enabled)
	{
		journalMenuCanvas.enabled = enabled;
		if(enabled)
		{
			//Initialization code goes here
		}
	}
}
