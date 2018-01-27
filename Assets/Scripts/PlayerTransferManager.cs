﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransferManager : MonoBehaviour
{

    public int playerIndex = 0;

    public void MovePlayerToNearest()
    {
        GameObject[] myInfectedObjects = GameObject.FindGameObjectsWithTag("Infected");
        GameObject nearestInfected;
        if (myInfectedObjects.Length == 0 || myInfectedObjects == null)
            PlayerKilled();
        nearestInfected = myInfectedObjects[0];
        foreach (GameObject infected in myInfectedObjects)
        {
            //if (infected.index == playerIndex)
                if (Vector3.Distance(infected.transform.position, gameObject.transform.position) < Vector3.Distance(gameObject.transform.position, nearestInfected.transform.position))
                    nearestInfected = infected;
        }
        nearestInfected.AddComponent<PlayerMovement>();
        nearestInfected.AddComponent<PlayerHealthManager>();
        nearestInfected.AddComponent<PlayerTransferManager>();
        nearestInfected.tag = "Player";
    }
    private void PlayerKilled()
    {
        //TODO show some screen that player died
    }
}
