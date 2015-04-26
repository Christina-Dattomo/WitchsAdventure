using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InGameUIManager : MonoBehaviour 
{
	public Canvas inGameUICanvas;

	public Canvas minimapCanvas = null;
	public Camera minimapCamera = null;
	public GameObject uInventoryGO = null;
	
	public void EnableOverlay(bool enabled)
	{
		inGameUICanvas.enabled = enabled;
		if (enabled) 
		{

		}

		if (GameObject.FindGameObjectWithTag ("Minimap") != null) 
		{
			GameObject minimapGO = GameObject.FindGameObjectWithTag("Minimap");
			minimapCanvas = minimapGO.GetComponent<Canvas>();
			minimapCanvas.enabled = enabled;
		}

		if(GameObject.FindGameObjectWithTag("MinimapCamera") != null)
		{
			GameObject minimapCameraGO = GameObject.FindGameObjectWithTag("MinimapCamera");
			minimapCamera = minimapCameraGO.GetComponent<Camera>();
			minimapCamera.enabled = enabled;
		}

		if (GameObject.FindGameObjectWithTag ("UInventory") != null) 
		{
			uInventoryGO = GameObject.FindGameObjectWithTag("UInventory");
			uInventoryGO.GetComponent<UInventory>().enabled = enabled;
		}
	}
}