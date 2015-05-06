using UnityEngine;
using System.Collections;

public class WaterEnemyAI : MonoBehaviour {
	
	public GameObject salt;
	//public Vector3 CircleWorldPositionCenter;
	//public int CircleRadius;
	private GameObject RitualCircle;
	private GameObject RitualBoundaryZone;
	public enum WaterAIState {Attracted, Avoiding};
	public WaterAIState myState;
	public float movementSpeed;
	public float waitTime;
	public float wanderDistance;
	public bool inBoundaryZone = false;
	public bool needDestination = true;
	public bool inRitualCircle = false;
	private Vector3 destination;

	// Use this for initialization
	void Start () {

		RitualCircle = GameObject.FindGameObjectWithTag("Ritual Circle");
		RitualBoundaryZone = GameObject.FindGameObjectWithTag("Outer Ritual Area");
	
	}
	
	// Update is called once per frame
	void Update () {
		try{
			salt = GameObject.FindGameObjectWithTag("Salt");
			myState = WaterAIState.Attracted;
		}
		catch
		{
			myState = WaterAIState.Avoiding;
		}

		if(myState == WaterAIState.Attracted)
		{
			WalkToSalt();
		}
		else
		{
			RandomWander();
		}
	
	}

	public void OnTriggerEnter(Collider col)
	{
		if(col.gameObject == RitualBoundaryZone)
		{
			Debug.Log("One shot");
			needDestination = true;
			inBoundaryZone = true;
			LeaveBoundaryZone();
		}
	}

	public void RandomWander()
	{
		if(!inBoundaryZone)
		{
			if(needDestination)
			{
				destination = new Vector2(transform.position.x + Random.Range((wanderDistance * -1),wanderDistance), transform.position.z + Random.Range((wanderDistance * -1),wanderDistance));
				needDestination = false;
				//transform.LookAt(destination);
			}
			transform.LookAt(new Vector3(destination.x, transform.position.y, destination.y));
			float xPos = Mathf.MoveTowards(transform.position.x, destination.x, movementSpeed * Time.deltaTime);
			float zPos = Mathf.MoveTowards(transform.position.z, destination.y, movementSpeed * Time.deltaTime);
			transform.position = new Vector3(xPos, transform.position.y, zPos);
			//Debug.Log ("Cube Position: " +transform.position.x + " " + transform.position.z);
			Debug.Log ("Destination: " + destination.x + " " +destination.y);
			if(!needDestination)
			{
				if((transform.position.x < destination.x + 1 && transform.position.x > destination.x - 1) && (transform.position.z < destination.y + 1 && transform.position.z > destination.y - 1))
					needDestination = true;
			}
		}
		else
		{
			LeaveBoundaryZone();
		}
	}

	public void LeaveBoundaryZone()
	{
		if(needDestination)
		{
			if((transform.position.x - RitualBoundaryZone.transform.position.x) > 0)
			{
				destination.x = transform.position.x + wanderDistance;
			}
			if((transform.position.x - RitualBoundaryZone.transform.position.x) < 0)
			{
				destination.x = transform.position.x - wanderDistance;
			}
			if((transform.position.z - RitualBoundaryZone.transform.position.z) > 0)
			{
				destination.y = transform.position.z + wanderDistance;
			}
			if((transform.position.z - RitualBoundaryZone.transform.position.z) < 0)
			{
				destination.y = transform.position.z - wanderDistance;
			}
			needDestination = false;
		}
		if(!needDestination)
		{
			transform.LookAt(new Vector3(destination.x, transform.position.y, destination.y));

			float xPos = Mathf.MoveTowards(transform.position.x, destination.x, movementSpeed * Time.deltaTime);
			float zPos = Mathf.MoveTowards(transform.position.z, destination.y, movementSpeed * Time.deltaTime);
			transform.position = new Vector3(xPos, transform.position.y, zPos);

			if((transform.position.x < destination.x + 1 && transform.position.x > destination.x - 1) && (transform.position.z < destination.y + 1 && transform.position.z > destination.y - 1))
			{
				needDestination = true;
				inBoundaryZone = false;
			}
		}

	}

	public void WalkToSalt()
	{
		transform.LookAt(salt.transform.position);
		Mathf.MoveTowards(transform.position.x, salt.transform.position.x, movementSpeed * Time.deltaTime);
		Mathf.MoveTowards(transform.position.y, salt.transform.position.y, movementSpeed * Time.deltaTime);
	}
}
