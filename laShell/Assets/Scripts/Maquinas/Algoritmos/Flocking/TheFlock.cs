using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFlock : MonoBehaviour
{

    public GameObject boids;
    static int numberOfBoids;
    public static int spaceSize;
    public static GameObject[] theBoids;

	void Start ()
    {
        numberOfBoids = 10;
        spaceSize = 5;
        theBoids = new GameObject[numberOfBoids];
        for (int i = 0; i < numberOfBoids; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-spaceSize, spaceSize),
                                      Random.Range(-spaceSize, spaceSize),
                                      Random.Range(-spaceSize, spaceSize));
            theBoids[i] = (GameObject)Instantiate(boids, pos, Quaternion.identity);
        }
	}
	
	void Update ()
    {
		
	}
}
