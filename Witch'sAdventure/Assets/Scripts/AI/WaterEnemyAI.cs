using UnityEngine;
using System.Collections;

public class WaterEnemyAI : MonoBehaviour {
	
	public GameObject salt;
	//public Vector3 CircleWorldPositionCenter;
	//public int CircleRadius;
	private GameObject RitualCircle;
	private GameObject Boundary;
	public enum WaterAIState {Attracted, Avoiding};
	public WaterAIState myState;
	public float movementSpeed, rotationSpeed;
	public float waitTime;
	public float wanderDistance;
	public bool inBoundaryZone = false;
	public bool needDestination = true;
	public bool inRitualCircle = false;
	private Vector3 destination;
	private Quaternion endRotationQuat;
	private Vector3 endRotationEuler;
	public float rotationCatch = 11;

	// Use this for initialization
	void Start () {

		RitualCircle = GameObject.FindGameObjectWithTag("RitualCircle");
		Boundary = GameObject.FindGameObjectWithTag("Boundary");
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("Salt")!= null){
			salt = GameObject.FindGameObjectWithTag("Salt");
			myState = WaterAIState.Attracted;
		}
		else
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
		if(col.gameObject == Boundary)
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


				Quaternion startRot = transform.rotation;


				transform.LookAt(new Vector3(destination.x, transform.position.y, destination.y));
				endRotationQuat = transform.rotation;
				endRotationEuler = transform.rotation.eulerAngles;
				transform.rotation = startRot;
			}

			if((transform.rotation.eulerAngles.y < endRotationEuler.y + rotationCatch && transform.rotation.eulerAngles.y > endRotationEuler.y - rotationCatch))
			{
				iTween.MoveUpdate(gameObject, new Vector3(destination.x, transform.position.y, destination.y), movementSpeed);
				Debug.Log ("Destination: " + destination.x + " " +destination.y);
				if(!needDestination)
				{
					if((transform.position.x < destination.x + 1 && transform.position.x > destination.x - 1) && (transform.position.z < destination.y + 1 && transform.position.z > destination.y - 1))
						needDestination = true;
				}
			}
			else
			{
				transform.rotation = Quaternion.Lerp(transform.rotation, endRotationQuat, rotationSpeed * Time.deltaTime);
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
			if((transform.position.x - Boundary.transform.position.x) > 0)
			{
				destination.x = transform.position.x + wanderDistance;
			}
			if((transform.position.x - Boundary.transform.position.x) < 0)
			{
				destination.x = transform.position.x - wanderDistance;
			}
			if((transform.position.z - Boundary.transform.position.z) > 0)
			{
				destination.y = transform.position.z + wanderDistance;
			}
			if((transform.position.z - Boundary.transform.position.z) < 0)
			{
				destination.y = transform.position.z - wanderDistance;
			}
			Quaternion startRot = transform.rotation;
			transform.LookAt(new Vector3(destination.x, transform.position.y, destination.y));
			endRotationQuat = transform.rotation;
			endRotationEuler = transform.rotation.eulerAngles;
			transform.rotation = startRot;
			needDestination = false;
		}
		if(!needDestination)
		{
			if((transform.rotation.eulerAngles.y < endRotationEuler.y + rotationCatch && transform.rotation.eulerAngles.y > endRotationEuler.y - rotationCatch))
			{
				//float xPos = Mathf.MoveTowards(transform.position.x, destination.x, movementSpeed * Time.deltaTime);
				//float zPos = Mathf.MoveTowards(transform.position.z, destination.y, movementSpeed * Time.deltaTime);
				//transform.position = new Vector3(xPos, transform.position.y, zPos);
				iTween.MoveUpdate(gameObject, new Vector3(destination.x, transform.position.y, destination.y), movementSpeed);
				
				if((transform.position.x < destination.x + 1 && transform.position.x > destination.x - 1) && (transform.position.z < destination.y + 1 && transform.position.z > destination.y - 1))
				{
					needDestination = true;
					inBoundaryZone = false;
				}
			}
			else
			{
				transform.rotation = Quaternion.Lerp(transform.rotation, endRotationQuat, rotationSpeed * Time.deltaTime);
			}
		}

	}

	public void WalkToSalt()
	{
		//transform.LookAt(salt.transform.position);
		//Mathf.MoveTowards(transform.position.x, salt.transform.position.x, movementSpeed * Time.deltaTime);
		//Mathf.MoveTowards(transform.position.y, salt.transform.position.y, movementSpeed * Time.deltaTime);
		iTween.MoveUpdate(gameObject, salt.transform.position, movementSpeed);
	}
}
