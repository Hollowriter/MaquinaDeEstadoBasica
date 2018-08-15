﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMaquinita : MonoBehaviour {
    Maquinita maq = new Maquinita();
    int state;
    // additional variables
    int minerals;

    enum States
    {
        Mineralized,
        Depleted
    };

    enum Events
    {
        Deplete
    };
    void Start ()
    {
        state = -1;
        minerals = 1000;
        maq.SetRelation((int)States.Mineralized, (int)Events.Deplete, (int)States.Depleted);
	}
	
	void Update ()
    {
        if (minerals <= 0)
        {
            maq.SetEvent((int)Events.Deplete);
        }

        if (maq.GetState() == (int)States.Depleted)
        {
            this.gameObject.SetActive(false);
        }
	}
}
