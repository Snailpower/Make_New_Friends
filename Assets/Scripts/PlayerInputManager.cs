using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    private InfectedUnitManager infectedUnitManager;

    private void Start()
    {
        infectedUnitManager = gameObject.GetComponent<Infected>().manager.GetComponent<InfectedUnitManager>();
    }
    void Update () {
        if(Input.GetButtonDown("ButtonA_" + gameObject.GetComponent<PlayerMovement>().playerID))
        {
            print("ASDAA");
            infectedUnitManager.followFlockingRules = true;
            infectedUnitManager.seekGoal = true;
            infectedUnitManager.willful = false;
            infectedUnitManager.attacking = false;
        }
        if (Input.GetButtonDown("ButtonB_" + gameObject.GetComponent<PlayerMovement>().playerID))
        {
            print("ASDBBB");
            infectedUnitManager.followFlockingRules = false;
            infectedUnitManager.seekGoal = false;
            infectedUnitManager.willful = true;
            infectedUnitManager.attacking = true;
        }
    }
}
