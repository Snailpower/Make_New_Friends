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
            infectedUnitManager.followFlockingRules = !infectedUnitManager.followFlockingRules;
        }
        if (Input.GetButtonDown("ButtonB_" + gameObject.GetComponent<PlayerMovement>().playerID))
        {
            print("ASDBBB");
            infectedUnitManager.willful = !infectedUnitManager.willful;
        }
    }
}
