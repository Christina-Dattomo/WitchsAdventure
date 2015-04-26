using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class StartMenuManager : MonoBehaviour 
{
	public Canvas startMenuPanel;
	
	public void EnableOverlay(bool enabled)
	{
		startMenuPanel.enabled = enabled;
		if(enabled)
		{
			//Initialization code goes here
		}
	}

	public void StartGame()
	{
		UIManager.manager.SetUIState (UIManager.UIState.LoadingScreen);
		Application.LoadLevel ("LevelOne");
	}

	public void OpenStartMenu()
	{
		UIManager.manager.SetUIState(UIManager.UIState.StartMenu);
	}

	public void OpenCredits()
	{
		UIManager.manager.SetUIState(UIManager.UIState.Credits);
	}
}
