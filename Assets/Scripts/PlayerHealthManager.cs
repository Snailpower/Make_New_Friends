using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    private float playerHealth = 100f;
    public bool canBeReduced = true;

    private void Update()
    {
        if (Input.GetButtonDown("Boost_" + gameObject.GetComponent<PlayerMovement>().playerID ) && canBeReduced)
            ReduceHealth(10);
    }

    public void ReduceHealth(float amount)
    {
        playerHealth -= amount;

        if (playerHealth < 0)
            KillPlayer();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Infected" && collision.gameObject.GetComponent<Infected>().index != gameObject.GetComponent<PlayerTransferManager>().playerIndex)
        {
            //collision.game.GetComponent<Infected>().currentHealth -= Time.deltaTime;
            ReduceHealth(Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Infected" && collision.gameObject.GetComponent<Infected>().index != gameObject.GetComponent<PlayerTransferManager>().playerIndex)
        {
            //collision.game.GetComponent<Infected>().currentHealth -= Time.deltaTime;
            ReduceHealth(10f);
        }
    }
    public void KillPlayer()
    {
        Camera.main.GetComponent<CameraFollow>().targets.Remove(gameObject.transform);
        gameObject.GetComponent<PlayerTransferManager>().MovePlayerToNearest();
        Destroy(gameObject);
    }


}
