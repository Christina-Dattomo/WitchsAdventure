using UnityEngine;
using System.Collections;

public class RitualCircleScript : MonoBehaviour {

	bool drop1Correct = false;
	bool drop2Correct = false;
	bool drop3Correct = false;
	bool drop4Correct = false;
	bool circleactive = false;
	public RitualDropScript Item1;
	public RitualDropScript Item2;
	public RitualDropScript Item3;
	public RitualDropScript Item4;
	public GameObject drop1;
	public GameObject drop2;
	public GameObject drop3;
	public GameObject drop4;


	void awake()
	{
		Item1 = drop1.GetComponent<RitualDropScript> ();
		Item2 = drop2.GetComponent<RitualDropScript> ();
		Item3 = drop3.GetComponent<RitualDropScript> ();
		Item4 = drop4.GetComponent<RitualDropScript> ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
				if (Item1.active == true) {
						drop1Correct = true;
				} else {
					drop1Correct = false;
				}
				if (Item2.active == true) {
						drop2Correct = true;
				}else {
					drop2Correct = false;
				}

				if (Item3.active == true) {
						drop3Correct = true;
				}else {
					drop3Correct = false;
				}
				
				if (Item4.active == true) {
						drop4Correct = true;
				}
				else{
					drop4Correct = false;
					}
				
		if (drop1Correct && drop2Correct && drop3Correct && drop4Correct) {
			circleactive = true;
			Debug.Log("Ritual Complete");
		}
	

		

		}
}
