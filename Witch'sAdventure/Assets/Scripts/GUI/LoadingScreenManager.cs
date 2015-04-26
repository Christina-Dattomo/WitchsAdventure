using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour {

	public Canvas loadingScreenCanvas;
	
	public void EnableOverlay(bool enabled)
	{
		loadingScreenCanvas.enabled = enabled;
		if(enabled)
		{
			//Initialization code goes here
		}
	}

	void Update()
	{
		if(enabled)
		{
			if (Input.GetKeyDown(KeyCode.Space)) 
				EnableInGameUI();
		}
	}
	
	public void EnableInGameUI()
	{
		UIManager.manager.SetUIState (UIManager.UIState.InGameUI);
	}
}
