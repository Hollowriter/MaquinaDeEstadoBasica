using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMaquinita : MonoBehaviour {
    Maquinita maq = new Maquinita();
    int state;
    // additional variables
    int objectsCarried;
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
        maq.SetRelation((int)States.Idle, (int)Events.Ordered, (int)States.ToMine);
        maq.SetRelation((int)States.ToMine, (int)Events.OnMine, (int)States.Mining);
        maq.SetRelation((int)States.Mining, (int)Events.Full, (int)States.ToDeposit);
        maq.SetRelation((int)States.ToMine, (int)Events.Full, (int)States.ToDeposit);
        maq.SetRelation((int)States.ToDeposit, (int)Events.Deposit, (int)States.ToMine);
        maq.SetRelation((int)States.ToMine, (int)Events.NoMine, (int)States.LastTrip);
        maq.SetRelation((int)States.LastTrip, (int)Events.Deposit, (int)States.Idle);
    }
	
	void Update ()
    {
        if (maq.GetState() == (int)States.Mining)
        {
            if (objectsCarried >= 100)
            {
                maq.SetEvent((int)Events.Full);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gold")
        {
            maq.SetEvent((int)Events.OnMine);
        }
    }
}
