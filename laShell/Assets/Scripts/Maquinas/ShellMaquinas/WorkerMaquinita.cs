using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMaquinita : MonoBehaviour {
    Maquinita maq = new Maquinita();
    int state;
    // additional variables
    int objectsCarried;
    float timer;
    const float speedObj = 0.03f;
    const float limitTimer = 2;
    const int handTaker = 10;
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

    void Start ()
    {
		Init();
    }
	
	void Update ()
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
                break;
            case (int)States.ToDeposit:
                ToDeposit();
                break;
            case (int)States.Mining:
                Mining();
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
        transform.position = Vector3.MoveTowards(transform.position, mine.gameObject.transform.position, speedObj);
        // Debug.Log("To mine");
    }
    void ToDeposit()
    {
        transform.position = Vector3.MoveTowards(transform.position, warehouse.gameObject.transform.position, speedObj);
    }
    void Mining()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer >= limitTimer)
        {
            mine.GetComponent<MineMaquinita>().SetMinerals(mine.GetComponent<MineMaquinita>().GetMinerals() - handTaker);
            objectsCarried += handTaker;
            timer = 0;
        //    Debug.Log("EnterMission");
        }
        if (objectsCarried >= 100)
        {
            maq.SetEvent((int)Events.Full);
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
        //    Debug.Log("colisiona");
            maq.SetEvent((int)Events.OnMine);
            transform.position = Vector3.MoveTowards(transform.position, mine.gameObject.transform.position, 0);
        }
        if (other.gameObject.tag == "warehouse")
        {
        //    Debug.Log("entra");
            maq.SetEvent((int)Events.Deposit);
            objectsCarried = 0;
        }
    }
}
