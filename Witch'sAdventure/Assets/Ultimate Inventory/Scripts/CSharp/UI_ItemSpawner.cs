using UnityEngine;
using System.Collections;

public class UI_ItemSpawner : MonoBehaviour {

    public GameObject[] itemsCollection;
    public int[] Rarity; //0 = Very Common , +Infinity = Very Rare
    public float spawnFrequency = 60; //Seconds
    public GameObject spawnPoint,rayCheck;
    GameObject spawned;
    bool spawn = false;
    GameObject go;
    RaycastHit hit;
    float timer = 0;
    void Update()
    {
        if (spawn == true)
        {
            while (spawned == null)
            {
                int s = Random.Range(0, itemsCollection.Length);
                int b = Random.Range(0, Rarity[s]);
                if (b == 0)
                {
                    go = itemsCollection[s];
                    spawned = go;
                    GameObject.Instantiate(go, spawnPoint.transform.position, Quaternion.identity);
                    spawn = false;
                }
            }
        }
        if (spawned == null && spawn == false)
        {
            if (timer < spawnFrequency)
            {
                timer += 0.1f;
            }
            if (timer >= spawnFrequency)
            {
                timer = 0;
                spawn = true;
            }
        }
        Debug.DrawRay(rayCheck.transform.position, rayCheck.transform.TransformDirection(Vector3.up));
        if (Physics.Raycast(rayCheck.transform.position, rayCheck.transform.TransformDirection(Vector3.up), out hit))
        {
            if (hit.collider.GetComponent<UI_Item>() == null)
            {
               // Debug.Log("Other");
                spawned = null;
            }
        }
        else
        {
          //  Debug.Log("Empty");
            spawned = null;
        }
    }


    IEnumerator SpawnItem()
    {
        if (spawn == false)
        {
            yield return new WaitForSeconds(spawnFrequency);
            spawn = true;
        }
    }
}
