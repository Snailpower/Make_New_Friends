using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthy : MonoBehaviour {

    public GameObject target;

    public GameObject manager;

    private bool scared = false;


    public Vector2 location = Vector2.zero;
    public Vector2 velocity;
    public float forceMultiplier;

    private Vector2 goalpos = Vector2.zero;
    private Vector2 currentForce;

    private float standingVelocity = .5f;

    private Animator anim;



    private Vector2 seek(Vector2 target)
    {
        return (target - location);
    }

    private Vector2 align()
    {
        float neighbourdis = manager.GetComponent<HealthyUnitManager>().neighbourdistance;
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach(GameObject healthy in manager.GetComponent<HealthyUnitManager>().unitsHealthy)
        {
            if (healthy == gameObject) continue;
            {
                if (healthy == null)
                {
                    continue;
                }
                float d = Vector2.Distance(location, healthy.GetComponent<Healthy>().location);

                if (d < neighbourdis)
                {
                    sum += healthy.GetComponent<Healthy>().velocity;
                    count++;
                }
            }
        }
        if (count > 0)
        {
            sum /= count;
            Vector2 steer = sum - velocity;
            return steer;
        }


        return Vector2.zero;
    }

    private Vector2 Avoid()
    {
        float scaredis = manager.GetComponent<HealthyUnitManager>().scareDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach (GameObject infectedManager in manager.GetComponent<HealthyUnitManager>().infectedManagers)
        {
            foreach (GameObject infected in infectedManager.GetComponent<InfectedUnitManager>().unitsInfected)
            {
                if (infected == null)
                    continue;
                float d = Vector2.Distance(location, infected.GetComponent<Infected>().location);

                if (d < scaredis)
                {
                    scared = true;
                    sum += infected.GetComponent<Infected>().velocity;
                    count++;
                }
            }
        }

        if (count > 0)
        {
            sum /= count;
            Vector2 steer = sum - velocity;
            return steer;
        }


        return Vector2.zero;
    }

    private Vector2 Cohesion()
    {
        float neighbourdis = manager.GetComponent<HealthyUnitManager>().neighbourdistance;
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach (GameObject healthy in manager.GetComponent<HealthyUnitManager>().unitsHealthy)
        {
            if (healthy == null)
            {
                continue;
            }

            if (healthy == gameObject) continue;
            {
                float d = Vector2.Distance(location, healthy.GetComponent<Healthy>().location);

                if (d < neighbourdis)
                {
                    sum += healthy.GetComponent<Healthy>().location;
                    count++;
                }
            }
        }
        if (count > 0)
        {
            sum /= count;
            return seek(sum);
        }


        return Vector2.zero;
    }

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        Camera.main.GetComponent<CameraFollow>().targets.Add(gameObject.transform);
        target = manager;

        velocity = new Vector2(Random.Range(0.1f, 0.1f), Random.Range(0.1f, 0.1f));
        location = new Vector2(transform.position.x, transform.position.y);
		
	}

    private void ApplyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);

        if (force.magnitude > manager.GetComponent<HealthyUnitManager>().maxForce)
        {
            force = force.normalized;
            force *= manager.GetComponent<HealthyUnitManager>().maxForce;
        }
        GetComponent<Rigidbody2D>().AddForce(force);

        if (GetComponent<Rigidbody2D>().velocity.magnitude > manager.GetComponent<HealthyUnitManager>().maxVelocity)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized;
            GetComponent<Rigidbody2D>().velocity *= manager.GetComponent<HealthyUnitManager>().maxVelocity;
        }

        Debug.DrawRay(transform.position, force, Color.green);
    }

    private void Flock()
    {
        location = transform.position;
        velocity = GetComponent<Rigidbody2D>().velocity;

        if (manager.GetComponent<HealthyUnitManager>().followFlockingRules && Random.Range(0,50)<=1)
        {
            Vector2 ali = align();
            Vector2 coh = Cohesion();
            Vector2 avd = Avoid();
            Vector2 gp;
            if (manager.GetComponent<HealthyUnitManager>().seekGoal)
            {
                gp = seek(goalpos);
                currentForce = gp + ali + coh + avd;
            }
            else
            {
                currentForce = ali + coh + avd;
            }

            ApplyForce(currentForce * forceMultiplier);

        }

        if (manager.GetComponent<HealthyUnitManager>().willful && Random.Range(0,50)<=1)
        {

            Vector2 avd = Avoid();

            if (!scared && Random.Range(0,50) < 1)
            {
                currentForce = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            }
            else if (scared)
            {
                currentForce = avd;
            }
        }

        ApplyForce(currentForce * forceMultiplier);
    }


	
	// Update is called once per frame
	void Update ()
    {
        anim.SetFloat("VelocityLimit", GetComponent<Rigidbody2D>().velocity.magnitude);

        Flock();
        goalpos = target.transform.position;

	}
}
