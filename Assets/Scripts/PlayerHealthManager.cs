using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    private float playerHealth = 100f;
    public bool canBeReduced = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && canBeReduced)
            ReduceHealth(10);
    }

    public void ReduceHealth(int amount)
    {
        playerHealth -= amount;

        if (playerHealth < 0)
            KillPlayer();
    }
    public void KillPlayer()
    {
        Camera.main.GetComponent<CameraFollow>().targets.Remove(gameObject.transform);
        gameObject.GetComponent<PlayerTransferManager>().MovePlayerToNearest();
        Destroy(gameObject);
    }


}
