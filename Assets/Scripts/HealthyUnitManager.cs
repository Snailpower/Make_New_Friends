using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyUnitManager : MonoBehaviour {

    public List<GameObject> unitsHealthy;

    public GameObject unitHealthyPrefab;

    public int healthyAmount = 25;

    public Vector3 range = new Vector3(10, 10, 10);


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, range * 2);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(this.transform.position, Vector3.one * 0.2f);

    }


    // Use this for initialization
    void Start ()
    {
        unitsHealthy = new List<GameObject>(healthyAmount);

        for (int i = 0; i <= healthyAmount; i++)
        {
            Vector3 unitPos = new Vector3(Random.Range(-range.x, range.x),
                                          Random.Range(-range.y, range.y),
                                          Random.Range(0, 0));

            GameObject addedUnit = Instantiate(unitHealthyPrefab, this.transform.position + unitPos, Quaternion.identity);

            addedUnit.GetComponent<Healthy>().manager = this.gameObject;

            unitsHealthy.Add(addedUnit);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
