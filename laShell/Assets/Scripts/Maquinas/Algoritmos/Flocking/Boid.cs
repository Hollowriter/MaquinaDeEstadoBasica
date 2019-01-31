using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    Transform thisBoidPosition;
    List<Boid> boidsISee;
    Vector3 positionPromediate;
    Vector3 cohesive; // Center
    Vector3 separate; // Avoid
    Vector3 aligning;
    [SerializeField]
    int weightCohesive;
    [SerializeField]
    int weightSeparate;
    [SerializeField]
    int weightAligning;
    Vector3 resultant;
    // Tutorial
    public float boidSpeed = 0.001f;
    float boidRotationSpeed;
    float neighbourDistance;

	void Start ()
    {
        cohesive = Vector3.zero;
        separate = Vector3.zero;
        aligning = Vector3.zero;
        resultant = Vector3.zero;
        positionPromediate = Vector3.zero;
        boidsISee = new List<Boid>();
        thisBoidPosition = GetComponent<Transform>();
        boidRotationSpeed = 4.0f;
        neighbourDistance = 3.0f;
        boidSpeed = Random.Range(0.5f, 1);
	}

	void Update ()
    {
        if (Random.Range(0, 5) < 1)
        {
            ApplyTheRules();
        }
        thisBoidPosition.Translate(0, 0, Time.deltaTime * boidSpeed);
	}

    void ApplyTheRules()
    {
        GameObject[] gos;
        gos = TheFlock.theBoids;
        cohesive = Vector3.zero;
        separate = Vector3.zero;
        aligning = Vector3.zero;
        resultant = Vector3.zero;
        positionPromediate = Vector3.zero;
        float gSpeed = 0.1f;
        Vector3 goalPos = TheFlock.goalPos;
        float distancing;
        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                distancing = Vector3.Distance(go.transform.position, this.transform.position);
                if (distancing <= neighbourDistance)
                {
                    cohesive += go.transform.position;
                    groupSize++;
                    if (distancing < 1.0f)
                    {
                        separate = separate + (this.transform.position - go.transform.position);
                    }
                    Boid otherBoid = go.GetComponent<Boid>();
                    gSpeed = gSpeed + otherBoid.boidSpeed;
                }
            }
        }
        if (groupSize > 0)
        {
            cohesive = cohesive / groupSize + (goalPos - this.transform.position);
            boidSpeed = gSpeed / groupSize;
            aligning = (cohesive + separate) - transform.position;
            if (aligning != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, 
                                                      Quaternion.LookRotation(aligning), 
                                                      boidRotationSpeed * Time.deltaTime);
            }
        }
    }

    public void SetCohesive(Vector3 _cohesive)
    {
        cohesive = _cohesive;
    }

    public void SetSeparate(Vector3 _separate)
    {
        separate = _separate;
    }

    public void SetAligning(Vector3 _aligning)
    {
        aligning = _aligning;
    }

    public Vector3 GetCohesive()
    {
        return cohesive;
    }

    public Vector3 GetSeparate()
    {
        return separate;
    }

    public Vector3 GetAligning()
    {
        return aligning;
    }

    public void InBoidSight()
    {

    }

    public void Promediate()
    {

    }
}
