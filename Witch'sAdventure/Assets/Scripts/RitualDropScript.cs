using UnityEngine;
using System.Collections;

public class RitualDropScript : MonoBehaviour {

	public string correctItemName = "";
	public bool active = false;

	void OnTriggerStay (Collider col)
	{
		if (col.gameObject.name == correctItemName) 
		{
			active = true;
		} 
			else {
					active = false;
				}
	}
}
