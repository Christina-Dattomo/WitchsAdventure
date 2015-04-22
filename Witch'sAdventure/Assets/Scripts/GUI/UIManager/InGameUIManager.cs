using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InGameUIManager : MonoBehaviour 
{
	public Canvas inGameUICanvas;

	//Use this for initialization
	public void EnableOverlay(bool enabled)
	{
		inGameUICanvas.enabled = enabled;

		if(enabled)
		{

		}
	}

	void Update ()
	{
		if(inGameUICanvas.enabled)
		{

		}
	}
}