using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField]
    NodeCreator nodeCreator;
    Pathfinder pathfinder;
    List<Node> thePath = new List<Node>();
    [SerializeField]
    GameObject mine;

	void Start ()
    {
        pathfinder = GetComponent<Pathfinder>();
        thePath = pathfinder.GetPath(nodeCreator.GetNodeByPosition(this.transform.position), nodeCreator.GetNodeByPosition(mine.transform.position));
	}
	
	void Update ()
    {
		
	}
}
