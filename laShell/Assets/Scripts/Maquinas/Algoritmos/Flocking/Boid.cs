using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    List<Boid> boidsISee;
    Vector3 positionPromediate;
    Vector3 cohesive;
    Vector3 separate;
    Vector3 aligning;
    [SerializeField]
    int weightCohesive;
    [SerializeField]
    int weightSeparate;
    [SerializeField]
    int weightAligning;
    Vector3 resultant;

	void Start ()
    {
        cohesive = Vector3.zero;
        separate = Vector3.zero;
        aligning = Vector3.zero;
        resultant = Vector3.zero;
        positionPromediate = Vector3.zero;
        boidsISee = new List<Boid>();
	}

	void Update ()
    {
		
	}

    public void InBoidSight()
    {

    }

    public void Promediate()
    {

    }
}
