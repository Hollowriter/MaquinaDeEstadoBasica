using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDickjstra : MonoBehaviour 
{
    [SerializeField]
    NodeCreator nodeCreator;
    PathfinderDijkstra pathfinder;
    List<Node> thePath = new List<Node>();
    [SerializeField]
    GameObject mine;
    int id;
    const float speed = 3.25f;
    Vector3 destiny;

    void Start() 
    {
        pathfinder = GetComponent<PathfinderDijkstra>();
        thePath = pathfinder.GetPath(nodeCreator.GetNodeByPosition(transform.position), nodeCreator.GetNodeByPosition(mine.transform.position));
        id = 0;
        destiny = Vector3.zero;
    }

    void Update() 
    {
        float step = speed * Time.deltaTime;
        if (id < thePath.Count) 
        {
            destiny = thePath[id].transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, destiny, step);

        if ((destiny - transform.position).magnitude < 0.25f) 
        {
            id++;
        }
    }
}
