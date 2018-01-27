using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public string playerID = "P1";
    public float speedReducerValue = 0.5f;

    private void Start()
    {
    }
    void Update () {
        float moveValueX = Input.GetAxis("Horizontal_" + playerID) * speedReducerValue;
        float moveValueY = Input.GetAxis("Vertical_" + playerID) * speedReducerValue;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveValueX, gameObject.transform.position.y + moveValueY, gameObject.transform.position.z);
	}
}
