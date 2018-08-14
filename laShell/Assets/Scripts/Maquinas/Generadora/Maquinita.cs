using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maquinita  // Clase maquina de estado (Con esto creas maquinas de estado)
{
    int[,] maquinita;
    int state;

    public void Init(int statesCount, int eventCount)
    {
        maquinita = new int[statesCount, eventCount];

        for (int i = 0; i < statesCount; i++)
        {
            for (int o = 0; o < eventCount; o++)
            {
                maquinita[i, o] = -1;
            }
        }
    }

    public void SetRelation(int srcState, int evt, int dsrState)
    {
        maquinita[srcState, evt] = dsrState;
    }

    public int GetState()
    {
        return state;
    }

    public void SetEvent(int evt)
    {
        if (maquinita[state, evt] != -1)
        {
            state = maquinita[state, evt];
        }
    }
}
