using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerStarMaquinita : MonoBehaviour {
    Maquinita maq = new Maquinita();
    int state;
    // additional variables
    int objectsCarried;
    float timer;
    const float speedObj = 3.25f;
    const float limitTimer = 2;
    const int handTaker = 10;
    int id;
    // bool movingToMine = false;
    Vector3 destiny;
    PathfinderStar pathfinder;
    List<Node> thePathMine;
    List<Node> thePathWarehouse;
    [SerializeField]
    NodeCreator nodes;
    [SerializeField]
    GameObject mine;
    [SerializeField]
    GameObject warehouse;

    enum States
    {
        Idle,
        ToMine,
        Mining,
        ToDeposit,
        LastTrip
    };

    enum Events
    {
        Ordered,
        OnMine,
        Full,
        Deposit,
        NoMine
    };

    void Start()
    {
        Init();
        pathfinder = GetComponent<PathfinderStar>();
        thePathMine = pathfinder.GetPath(nodes.GetNodeByPosition(transform.position), nodes.GetNodeByPosition(mine.transform.position));
        thePathWarehouse = pathfinder.GetPath(nodes.GetNodeByPosition(transform.position), nodes.GetNodeByPosition(warehouse.transform.position));
        Debug.Log("PathMine: " + thePathMine.Count);
        Debug.Log("PathWarehouse: " + thePathWarehouse.Count);
        id = 0;
        destiny = Vector3.zero;
    }

    void Update()
    {
        Behaviour();
    }
    void Init()
    {
        state = -1;
        objectsCarried = 0;
        timer = 2;
        maq.Init(5, 5);
        maq.SetRelation((int)States.Idle, (int)Events.Ordered, (int)States.ToMine);
        maq.SetRelation((int)States.ToMine, (int)Events.OnMine, (int)States.Mining);
        maq.SetRelation((int)States.Mining, (int)Events.Full, (int)States.ToDeposit);
        maq.SetRelation((int)States.ToMine, (int)Events.Full, (int)States.ToDeposit);
        maq.SetRelation((int)States.ToDeposit, (int)Events.Deposit, (int)States.ToMine);
        maq.SetRelation((int)States.ToMine, (int)Events.NoMine, (int)States.LastTrip);
        maq.SetRelation((int)States.Mining, (int)Events.NoMine, (int)States.LastTrip);
        maq.SetRelation((int)States.LastTrip, (int)Events.Deposit, (int)States.Idle);
    }
    void Behaviour()
    {
        state = maq.GetState();
        Ordering();
        switch (state)
        {
            case (int)States.ToMine:
                ToMine();
               /*Debug.Log("asd");
                movingToMine = true;*/
                break;
            case (int)States.ToDeposit:
                ToDeposit();
                // movingToMine = false;
                break;
            case (int)States.Mining:
                Mining();
                // movingToMine = true;
                break;
        }
        NoMine();
    }
    void Ordering()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            maq.SetEvent((int)Events.Ordered);
        }
    }
    void ToMine()
    {
        // transform.position = Vector3.MoveTowards(transform.position, mine.gameObject.transform.position, speedObj);
        float step = speedObj * Time.deltaTime;
        // Debug.Log(id);
        if (id < thePathMine.Count)
        {
            destiny = thePathMine[id].transform.position;
        }
        else
        {
            maq.SetEvent((int)Events.OnMine);
            id = 0;
            Debug.Log("estoy aqui");
            pathfinder.ClearAnything();
            thePathWarehouse = pathfinder.GetPath(nodes.GetNodeByPosition(transform.position), nodes.GetNodeByPosition(warehouse.transform.position));
            Debug.Log("ToWarehouse: " + thePathWarehouse.Count);
        }
        transform.position = Vector3.MoveTowards(transform.position, destiny, step);
        if ((destiny - transform.position).magnitude < 0.25f)
        {
            id++;
        }
        /*else
        {
            maq.SetEvent((int)Events.OnMine);
            id = 0;
            Debug.Log("se termino la lista");
        }*/
    }
    void ToDeposit()
    {
        // transform.position = Vector3.MoveTowards(transform.position, warehouse.gameObject.transform.position, speedObj);
        float step = speedObj * Time.deltaTime;
        //Debug.Log(id);
        if (id < thePathWarehouse.Count)
        {
            destiny = thePathWarehouse[id].transform.position;
        }
        else
        {
            maq.SetEvent((int)Events.Deposit);
            id = 0;
            objectsCarried = 0;
            //destiny = Vector3.zero;
            pathfinder.ClearAnything();
            thePathMine = pathfinder.GetPath(nodes.GetNodeByPosition(transform.position), nodes.GetNodeByPosition(mine.transform.position));
            Debug.Log("estoy alla");
            Debug.Log("ToMine: " + thePathMine.Count);
        }
        transform.position = Vector3.MoveTowards(transform.position, destiny, step);
        if ((destiny - transform.position).magnitude < 0.25f)
        {
            id++;
        }
        /*else
        {
            maq.SetEvent((int)Events.Deposit);
            id = 0;
            Debug.Log("se termino la linea");
        }*/
    }
    void Mining()
    {
        timer += Time.deltaTime;
        if (timer >= limitTimer)
        {
            mine.GetComponent<MineMaquinita>().SetMinerals(mine.GetComponent<MineMaquinita>().GetMinerals() - handTaker);
            objectsCarried += handTaker;
            timer = 0;
            //Debug.Log("EnterMission");
        }
        if (objectsCarried >= 100)
        {
            maq.SetEvent((int)Events.Full);
            //destiny = Vector3.zero;
            pathfinder.ClearAnything();
            thePathWarehouse = pathfinder.GetPath(nodes.GetNodeByPosition(transform.position), nodes.GetNodeByPosition(warehouse.transform.position));
        }
    }
    void NoMine()
    {
        if (mine.GetComponent<MineMaquinita>().GetMinerals() <= 0)
        {
            maq.SetEvent((int)Events.NoMine);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gold")
        {
            Debug.Log("colisiona");
            maq.SetEvent((int)Events.OnMine);
            //destiny = Vector3.zero;
            transform.position = Vector3.MoveTowards(transform.position, mine.gameObject.transform.position, 0);
        }
        if (other.gameObject.tag == "warehouse")
        {
            Debug.Log("entra");
            maq.SetEvent((int)Events.Deposit);
            objectsCarried = 0;
            //destiny = Vector3.zero;
        }
    }
}
