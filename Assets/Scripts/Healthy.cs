using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthy : MonoBehaviour {

    public GameObject target;

    public GameObject manager;
    public Vector2 location = Vector2.zero;
    public Vector2 velocity;

    private Vector2 goalpos = Vector2.zero;
    private Vector2 currentForce;

    private Vector2 seek(Vector2 target)
    {
        return (target - location);
    }

	// Use this for initialization
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Finish");

        velocity = new Vector2(Random.Range(0.1f, 0.1f), Random.Range(0.1f, 0.1f));
        location = new Vector2(transform.position.x, transform.position.y);
		
	}

    private void ApplyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);
        GetComponent<Rigidbody2D>().AddForce(force);
        Debug.DrawRay(transform.position, force, Color.white);
    }

    private void Flock()
    {
        location = transform.position;
        velocity = GetComponent<Rigidbody2D>().velocity;

        Vector2 gl;
        gl = seek(goalpos);
        currentForce = gl;
        currentForce = currentForce.normalized;

        ApplyForce(currentForce);
    }


	
	// Update is called once per frame
	void Update ()
    {
        Flock();
        goalpos = target.transform.position;
		
	}
}
