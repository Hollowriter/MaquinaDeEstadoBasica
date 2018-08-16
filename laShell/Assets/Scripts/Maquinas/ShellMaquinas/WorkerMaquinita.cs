using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMaquinita : MonoBehaviour {
    Maquinita maq = new Maquinita();
    int state;
    // additional variables
    int objectsCarried;
    float timer;
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
	
	void Update ()
    {
        if (maq.GetState() == (int)States.Mining)
        {
            timer += Time.deltaTime;
            if (timer >= limitTimer)
            {
                mine.GetComponent<MineMaquinita>().SetMinerals(mine.GetComponent<MineMaquinita>().GetMinerals() - handTaker);
                objectsCarried += handTaker;
                timer = 0;
            }
            if (objectsCarried >= 100)
            {
                maq.SetEvent((int)Events.Full);
            }
        }
        if (mine.GetComponent<MineMaquinita>().GetMinerals() <= 0)
        {
            maq.SetEvent((int)Events.NoMine);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gold")
        {
            maq.SetEvent((int)Events.OnMine);
        }
        if (other.gameObject.tag == "warehouse")
        {
            maq.SetEvent((int)Events.Deposit);
            objectsCarried = 0;
        }
    }
}
