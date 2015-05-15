using UnityEngine;
using System.Collections;

public class RitualScript : MonoBehaviour {


	public GameObject RitualTrigger;
	public GameObject RitualCircle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay (Collider col)
	{


		if (col.gameObject.tag == "RitualTrigger" && Input.GetKey(KeyCode.C)) {

			RitualCircle.gameObject.SetActive(true);
		}
	}
}
