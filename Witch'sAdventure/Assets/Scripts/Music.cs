using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class Music : MonoBehaviour {

	void Start () {
		GetComponent<AudioSource>().Play ();	
	}
}
