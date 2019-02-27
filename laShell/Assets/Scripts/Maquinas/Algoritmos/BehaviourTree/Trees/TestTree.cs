using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTree : MonoBehaviour
{
    [SerializeField]
    GameObject mine;
    List<BNode> FirstLayer;
    List<BNode> SecondLayer;
    NSecuence moveSecuence;
    NSelector playerSelector;
    NDecorator imNotInMine;
    ActionNode isOnMineOne;
    ActionNode isOnMineTwo;
    ActionNode move;
    // Start is called before the first frame update
    void Start()
    {
        FirstLayer = new List<BNode>();
        SecondLayer = new List<BNode>();
        move = new ActionNode(MoveToMine);
        isOnMineOne = new ActionNode(CheckInMine);
        isOnMineTwo = new ActionNode(CheckInMine);
        imNotInMine = new NDecorator(isOnMineTwo);
        SecondLayer.Add(imNotInMine);
        SecondLayer.Add(move);
        moveSecuence = new NSecuence(SecondLayer);
        FirstLayer.Add(isOnMineOne);
        FirstLayer.Add(moveSecuence);
        playerSelector = new NSelector(FirstLayer);
    }

    // Update is called once per frame
    void Update()
    {
        playerSelector.Evaluate();
    }

    public int CheckInMine()
    {
        if (transform.position == mine.gameObject.transform.position)
        {
            return 1;
        }
        return 0;
    }
    public int MoveToMine()
    {
        transform.position = Vector3.MoveTowards(transform.position, mine.gameObject.transform.position, 0.05f);
        return 1;
    }
}
