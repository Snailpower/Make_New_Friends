using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedUnitManager : MonoBehaviour {

    public List<GameObject> unitsInfected;

    public List<GameObject> infectedManagers;

    public GameObject healthyManager;

    public GameObject unitInfectedPrefab;

    public GameObject infectedContainer;

    public int infectedIndex;

    public int InfectedAmount = 25;

    public Vector3 range = new Vector3(10, 10, 10);

    public bool seekGoal = true;
    public bool followFlockingRules = true;
    public bool willful = false;

    private bool avoiding = false;
    private bool attacking = true;

    [Range(0, 100)]
    public float attackdistance = 50;

    [Range(0, 100)]
    public float scaredistance = 50;

    [Range(0, 100)]
    public float neighbourdistance = 50;

    [Range(0, 30)]
    public float maxForce = 0.5f;

    [Range(0, 25)]
    public float maxVelocity = 2.0f;


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

        foreach (GameObject manager in GameObject.FindGameObjectsWithTag("InfectedMng"))
        {
            if (manager == this.gameObject)
            {
                continue;
            }

            infectedManagers.Add(manager);
        }

        healthyManager = GameObject.FindGameObjectWithTag("HealthyMng");

        unitsInfected = new List<GameObject>(InfectedAmount);

        for (int i = 0; i <= InfectedAmount; i++)
        {
            Vector3 unitPos = new Vector3(Random.Range(-range.x, range.x),
                                          Random.Range(-range.y, range.y),
                                          Random.Range(0, 0));

            GameObject addedUnit = Instantiate(unitInfectedPrefab, this.transform.position + unitPos, Quaternion.identity, infectedContainer.transform);

            addedUnit.GetComponent<Infected>().manager = this.gameObject;

            unitsInfected.Add(addedUnit);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
