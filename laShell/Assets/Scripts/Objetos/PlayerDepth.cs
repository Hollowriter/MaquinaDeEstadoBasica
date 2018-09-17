using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDepth : MonoBehaviour
{
    [SerializeField]
    NodeCreator nodeCreator;
    PathfinderDepth pathfinder;
    List<Node> thePath = new List<Node>();
    [SerializeField]
    GameObject mine;
    int id;
    const float speed = 3.25f;
    Vector3 destiny;


    void Start()
    {
        pathfinder = GetComponent<PathfinderDepth>();
        thePath = pathfinder.GetPath(nodeCreator.GetNodeByPosition(transform.position), nodeCreator.GetNodeByPosition(mine.transform.position));
        id = thePath.Count - 1;
        Debug.Log(id);
        destiny = Vector3.zero;
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        // Debug.Log(id);
        if (id >= 0)
        {
            destiny = thePath[id].transform.position;
            Debug.Log("la concha");
        }

        transform.position = Vector3.MoveTowards(transform.position, destiny, step);

        if ((destiny - transform.position).magnitude < 0.25f)
        {
            id--;
        }
    }
}
