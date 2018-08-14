using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour // Maquina de estado definida para un objeto
{
    Maquinita maq = new Maquinita();
    int state;

    enum States
    {
        A,
        B, 
        C
    };

    enum Events
    {
        E1,
        E2,
        E3
    };

    void Start () {
        maq.Init(3, 3);
        state = -1;
        maq.SetRelation((int)States.A, (int)Events.E1, (int)States.B);
        maq.SetRelation((int)States.B, (int)Events.E2, (int)States.A);
    }

    void Update () {
        state = maq.GetState();

        if (Input.GetKeyDown(KeyCode.A))
        {
            state = maq.GetState();
            maq.SetEvent((int)Events.E1);
            Debug.Log("Src state: " + (States)state + " - event: " + Events.E1 + " - dst state: " + (States)maq.GetState());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            state = maq.GetState();
            maq.SetEvent((int)Events.E2);
            Debug.Log("Src state: " + (States)state + " - event: " + Events.E2 + " - dst state: " + (States)maq.GetState());
        }
    }
}
