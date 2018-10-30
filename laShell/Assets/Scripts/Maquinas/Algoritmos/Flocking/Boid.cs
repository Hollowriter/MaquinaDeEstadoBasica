using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    Transform thisBoidPosition;
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
        thisBoidPosition = GetComponent<Transform>();
	}

	void Update ()
    {
		
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
