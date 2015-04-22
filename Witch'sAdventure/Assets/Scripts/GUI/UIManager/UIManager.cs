using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public static UIManager manager;

	public enum UIState { StartMenu, InGameUI, LevelFail, LevelSuccess, PauseMenu, Credits };
	public UIState uiState = UIState.StartMenu;
	public UIState previousUIState;

	private StartMenuManager startMenu;
	private InGameUIManager inGameUI;
	private PauseMenuManager pauseMenu;
	private LevelFailManager levelFail;
	private LevelSuccessManager levelSuccess;
	private CreditsMenuManager creditMenu;

	void Awake()
	{          
		if(manager == null)           //If manager doesn't exist, create one
		{
			DontDestroyOnLoad(gameObject);
			manager = this;
		} else if(manager != null)    //if manager does exist, destroy this copy
		{
			Destroy(gameObject);
		}
		manager = this;
	}

	void Start () 
	{
		manager = GetComponent<UIManager> ();
		startMenu = GetComponent<StartMenuManager> ();
		inGameUI = GetComponent<InGameUIManager> ();
		pauseMenu = GetComponent<PauseMenuManager> ();
		levelFail = GetComponent<LevelFailManager> ();
		levelSuccess = GetComponent<LevelSuccessManager> ();
		creditMenu = GetComponent<CreditsMenuManager> ();

		SetUIState(UIState.StartMenu);
	}

	public void SetUIState(UIState newUIState)
	{
		if (newUIState == UIState.StartMenu) 
		{
			uiState = UIState.StartMenu;
			startMenu.EnableOverlay (true);
		} 
		else
			startMenu.EnableOverlay (false);
		
		if (newUIState == UIState.InGameUI) 
		{
			uiState = UIState.InGameUI;
			inGameUI.EnableOverlay (true);
		} 
		else
			inGameUI.EnableOverlay (false);

		if (newUIState == UIState.PauseMenu) 
		{
			uiState = UIState.PauseMenu;
			pauseMenu.EnableOverlay(true);
		} 
		else
			pauseMenu.EnableOverlay(false);

		if (newUIState == UIState.LevelFail) 
		{
			uiState = UIState.LevelFail;
			levelFail.EnableOverlay(true);
		} 
		else
			levelFail.EnableOverlay(false);

		if (newUIState == UIState.LevelSuccess) 
		{
			uiState = UIState.LevelSuccess;
			levelSuccess.EnableOverlay(true);
		} 
		else
			levelSuccess.EnableOverlay(false);

		if (newUIState == UIState.Credits) 
		{
			uiState = UIState.Credits;
			creditMenu.EnableOverlay(true);
		}
		else
			creditMenu.EnableOverlay(false);
	}
}
