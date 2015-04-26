using UnityEngine;
using System.Collections;

public class InventoryMenuManager : MonoBehaviour {

	public Canvas inventoryMenuCanvas;
	
	public void EnableOverlay(bool enabled)
	{
		inventoryMenuCanvas.enabled = enabled;
		if(enabled)
		{
			//Initialization code goes here
		}
	}
}
