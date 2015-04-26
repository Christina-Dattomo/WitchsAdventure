using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public static UIManager manager;

	public enum UIState { StartMenu, InGameUI, LevelSuccess, LevelFail, Credits, LoadingScreen };
	public enum UIOverlayState { PauseMenu, JournalMenu, InventoryMenu, disabled };
	public UIState uiState = UIState.StartMenu;
	public UIOverlayState uiOverlayState = UIOverlayState.disabled;

	private StartMenuManager startMenu;
	private InGameUIManager inGameUI;
	private PauseMenuManager pauseMenu;
	private LevelSuccessManager levelSuccess;
	private CreditsMenuManager creditMenu;
	private LoadingScreenManager loadingScreen;

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
		levelSuccess = GetComponent<LevelSuccessManager> ();
		creditMenu = GetComponent<CreditsMenuManager> ();
		loadingScreen = GetComponent<LoadingScreenManager> ();

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
		
		if (newUIState == UIState.LoadingScreen) 
		{
			uiState = UIState.LoadingScreen;
			loadingScreen.EnableOverlay (true);
		} 
		else
			loadingScreen.EnableOverlay (false);
	}
}
