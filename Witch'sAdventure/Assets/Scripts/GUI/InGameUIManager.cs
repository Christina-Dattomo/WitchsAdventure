using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InGameUIManager : MonoBehaviour 
{
	public Canvas inGameUICanvas;
	
	public void EnableOverlay(bool enabled)
	{
		inGameUICanvas.enabled = enabled;
		if (enabled) 
		{

		}
	}
}