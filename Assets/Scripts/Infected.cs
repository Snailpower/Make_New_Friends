using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : MonoBehaviour {

    public GameObject target;

    public GameObject manager;

    public Vector2 location = Vector2.zero;
    public Vector2 velocity;
    public float forceMultiplier;

    [HideInInspector]
    public float startHealth;

    private float currentHealth;

    public int index;

    private Vector2 goalpos = Vector2.zero;
    private Vector2 currentForce;
    private bool attacking = false;

    public bool userControlled = false;

    private Animator anim;


    private Vector2 seek(Vector2 target)
    {
        return (target - location);
    }

    private Vector2 align()
    {
        float neighbourdis = manager.GetComponent<InfectedUnitManager>().neighbourdistance;
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach(GameObject infected in manager.GetComponent<InfectedUnitManager>().unitsInfected)
        {
            if (infected == null)
            {
                continue;
            }

            if (infected == gameObject) continue;
            {
                float d = Vector2.Distance(location, infected.GetComponent<Infected>().location);

                if (d < neighbourdis)
                {
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


    private Vector2 Attack()
    {
        float attackdis = manager.GetComponent<InfectedUnitManager>().attackdistance;
        float sumHumanD = 0;
        float sumInfectedD = 0;

        int countH = 0;
        int countI = 0;

        foreach (GameObject healthy in manager.GetComponent<InfectedUnitManager>().healthyManager.GetComponent<HealthyUnitManager>().unitsHealthy)
        {
            if (healthy == null)
            {
                continue;
            }
                float d = Vector2.Distance(location, healthy.GetComponent<Healthy>().location);     

                sumHumanD += d;
                countH++;
                
                
            
        }

        foreach (GameObject manager in manager.GetComponent<InfectedUnitManager>().infectedManagers)
        {
            foreach (GameObject infected in manager.GetComponent<InfectedUnitManager>().unitsInfected)
            {
                if (infected == null)
                {
                    continue;
                }

                if (infected.GetComponent<Infected>().index != index)
                {
                    float d = Vector2.Distance(location, infected.GetComponent<Infected>().location);

                    sumInfectedD += d;
                    countI++;

                    
                }
            }
        }

        if ((sumHumanD / countH != 0) && (sumInfectedD / countI != 0))
        {
            if ((sumHumanD / countH) < (sumInfectedD / countI))
            {
                foreach (GameObject healthy in manager.GetComponent<InfectedUnitManager>().healthyManager.GetComponent<HealthyUnitManager>().unitsHealthy)
                {
                    if (healthy == null)
                    {
                        continue;
                    }

                    float d = Vector2.Distance(location, healthy.GetComponent<Healthy>().location);

                    if (d < attackdis)
                    {
                        Vector2 steer = healthy.GetComponent<Healthy>().location - location;

                        attacking = true;

                        target = healthy;

                        return steer;
                    }
                }
            }
            else
            {
                foreach (GameObject manager in manager.GetComponent<InfectedUnitManager>().infectedManagers)
                {
                    foreach (GameObject infected in manager.GetComponent<InfectedUnitManager>().unitsInfected)
                    {
                        if (infected == null)
                        {
                            continue;
                        }

                        if (infected.GetComponent<Infected>().index != index)
                        {
                            float d = Vector2.Distance(location, infected.GetComponent<Infected>().location);

                            if (d < attackdis)
                            {
                                Vector2 steer = infected.GetComponent<Infected>().location - location;

                                attacking = true;

                                target = infected;

                                return steer;
                            }


                        }
                    }
                }
            }
        }
        else if ((sumInfectedD / countI == 0))
        {
            foreach (GameObject healthy in manager.GetComponent<InfectedUnitManager>().healthyManager.GetComponent<HealthyUnitManager>().unitsHealthy)
            {
                if (healthy == null)
                {
                    continue;
                }

                float d = Vector2.Distance(location, healthy.GetComponent<Healthy>().location);

                if (d < attackdis)
                {
                    Vector2 steer = healthy.GetComponent<Healthy>().location - location;

                    attacking = true;

                    target = healthy;

                    return steer;
                }
            }
        }
        else if ((sumHumanD / countH == 0))
        {
            foreach (GameObject manager in manager.GetComponent<InfectedUnitManager>().infectedManagers)
            {
                foreach (GameObject infected in manager.GetComponent<InfectedUnitManager>().unitsInfected)
                {
                    if (infected == null)
                    {
                        continue;
                    }

                    if (infected.GetComponent<Infected>().index != index)
                    {
                        float d = Vector2.Distance(location, infected.GetComponent<Infected>().location);

                        if (d < attackdis)
                        {
                            Vector2 steer = infected.GetComponent<Infected>().location - location;

                            attacking = true;

                            target = infected;

                            return steer;
                        }


                    }
                }
            }
        }
        


        return Vector2.zero;
    }

    //Fix this for index of infected
    private Vector2 Avoid()
    {
        if (attacking)
        {
            return Vector2.zero;
        }

        float scaredis = manager.GetComponent<InfectedUnitManager>().scaredistance;
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach (GameObject manager in manager.GetComponent<InfectedUnitManager>().infectedManagers)
        {
            foreach (GameObject infected in manager.GetComponent<InfectedUnitManager>().unitsInfected)
            {
                if (infected == null)
                {
                    continue;
                }

                if (infected.GetComponent<Infected>().index != index)
                {
                    float d = Vector2.Distance(location, infected.GetComponent<Infected>().location);

                    if (d < scaredis)
                    {
                        sum += infected.GetComponent<Infected>().velocity;
                        count++;
                    }
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
        float neighbourdis = manager.GetComponent<InfectedUnitManager>().neighbourdistance;
        Vector2 sum = Vector2.zero;
        int count = 0;

        foreach (GameObject infected in manager.GetComponent<InfectedUnitManager>().unitsInfected)
        {
            if (infected == null)
            {
                continue;
            }

            if (infected == gameObject) continue;
            {
                float d = Vector2.Distance(location, infected.GetComponent<Infected>().location);

                if (d < neighbourdis)
                {
                    sum += infected.GetComponent<Infected>().location;
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
        index = manager.GetComponent<InfectedUnitManager>().infectedIndex;

        currentHealth = startHealth;

        manager.GetComponent<InfectedUnitManager>().unitsInfected.Add(gameObject);

        velocity = new Vector2(Random.Range(0.1f, 0.1f), Random.Range(0.1f, 0.1f));
        location = new Vector2(transform.position.x, transform.position.y);
		
	}

    private void ApplyForce(Vector2 f)
    {
        Vector3 force = new Vector3(f.x, f.y, 0);

        if (force.magnitude > manager.GetComponent<InfectedUnitManager>().maxForce)
        {
            force = force.normalized;
            force *= manager.GetComponent<InfectedUnitManager>().maxForce;
        }
        GetComponent<Rigidbody2D>().AddForce(force);

        if (GetComponent<Rigidbody2D>().velocity.magnitude > manager.GetComponent<InfectedUnitManager>().maxVelocity)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized;
            GetComponent<Rigidbody2D>().velocity *= manager.GetComponent<InfectedUnitManager>().maxVelocity;
        }

        Debug.DrawRay(transform.position, force, Color.green);
    }

    private void Flock()
    {
        location = transform.position;
        velocity = GetComponent<Rigidbody2D>().velocity;

        if (manager.GetComponent<InfectedUnitManager>().followFlockingRules && Random.Range(0,50)<=1)
        {
            Vector2 ali = align();
            Vector2 coh = Cohesion();

            Vector2 gp;
            if (manager.GetComponent<InfectedUnitManager>().seekGoal)
            {
                foreach (GameObject item in manager.GetComponent<InfectedUnitManager>().unitsInfected)
                {
                    if (item == this.gameObject || item == null)
                    {
                        continue;
                    }
                    if (item.GetComponent<Infected>().userControlled)
                    {
                        target = item;
                    }
                    
                }
                gp = seek(goalpos);
                currentForce = gp + coh + ali;
            }
            else
            {
                currentForce = coh + ali;
            }

            ApplyForce(currentForce * forceMultiplier);

        }

        if (manager.GetComponent<InfectedUnitManager>().willful && Random.Range(0,50)<=1)
        {
            Vector2 atk = Attack();

            currentForce = atk;
        }

        ApplyForce(currentForce * forceMultiplier);
    }

    private void OnDestroy()
    {
        manager.GetComponent<InfectedUnitManager>().unitsInfected.Remove(gameObject);
    }

    // Update is called once per frame
    void Update ()
    {
        if (userControlled)
        {
            return;
        }



        if (!userControlled)
        {
            Flock();

            anim.SetFloat("Velocity", GetComponent<Rigidbody2D>().velocity.magnitude);
        }
        


        if (target != null)
        {
            goalpos = target.transform.position;
        }

        if (currentHealth <= 0 && !userControlled)
        {
            Camera.main.GetComponent<CameraFollow>().targets.Remove(gameObject.transform);
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObj = collision.gameObject;

        if (otherObj.tag == "Healthy")
        {

            Camera.main.GetComponent<CameraFollow>().targets.Remove(otherObj.transform);
            otherObj.GetComponent<Healthy>().manager.GetComponent<HealthyUnitManager>().unitsHealthy.Remove(otherObj);
            Destroy(otherObj);

            GameObject newInfected = Instantiate(manager.GetComponent<InfectedUnitManager>().unitInfectedPrefab, this.transform.position, Quaternion.identity, manager.GetComponent<InfectedUnitManager>().infectedContainer.transform);

            newInfected.GetComponent<Infected>().manager = manager;
            newInfected.GetComponent<Infected>().startHealth = Mathf.Round(Random.Range(manager.GetComponent<InfectedUnitManager>().minHealth, manager.GetComponent<InfectedUnitManager>().maxHealth)); 

            manager.GetComponent<InfectedUnitManager>().unitsInfected.Add(newInfected);
        }

        if (otherObj.tag == "Infected" && otherObj.GetComponent<Infected>().index != index && !userControlled)
        {
            otherObj.GetComponent<Infected>().currentHealth -= 10f;
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject otherObj = collision.gameObject;

        if (otherObj.tag == "Infected" && otherObj.GetComponent<Infected>().index != index && !userControlled)
        {
            otherObj.GetComponent<Infected>().currentHealth -= Time.deltaTime;
        }
    }



}
