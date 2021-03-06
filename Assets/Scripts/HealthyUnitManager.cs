﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyUnitManager : MonoBehaviour {

    public List<GameObject> unitsHealthy;

    public List<GameObject> infectedManagers;

    public GameObject unitHealthyPrefab;

    public GameObject healthyContainer;

    public int healthyAmount = 25;

    public Vector3 range = new Vector3(10, 10, 10);

    public bool seekGoal = true;
    public bool followFlockingRules = true;
    public bool willful = false;
    private bool avoiding = true;

    [Range(0, 100)]
    public float scareDistance = 50;

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
        infectedManagers = new List<GameObject>(4);

        foreach (GameObject manager in GameObject.FindGameObjectsWithTag("InfectedMng"))
        {
            infectedManagers.Add(manager);
        }

        unitsHealthy = new List<GameObject>(healthyAmount);

        for (int i = 0; i <= healthyAmount; i++)
        {
            Vector3 unitPos = new Vector3(Random.Range(-range.x, range.x),
                                          Random.Range(-range.y, range.y),
                                          Random.Range(0, 0));

            GameObject addedUnit = Instantiate(unitHealthyPrefab, this.transform.position + unitPos, Quaternion.identity, healthyContainer.transform);

            addedUnit.GetComponent<Healthy>().manager = this.gameObject;

            unitsHealthy.Add(addedUnit);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
