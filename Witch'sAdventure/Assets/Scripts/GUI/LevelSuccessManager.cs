using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class LevelSuccessManager : MonoBehaviour 
{
	public Canvas levelSuccessCanavs;

	public void EnableOverlay(bool enabled)
	{
		levelSuccessCanavs.enabled = enabled;
		if(enabled)
		{

		}
	}
}