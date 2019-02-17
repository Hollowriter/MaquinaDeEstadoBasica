using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarLander : LunarLanderBase
{
	
	protected override void OnThink(float dt)
	{
        Vector3 distanceToPlatform = platform.transform.position - this.transform.position;
        Vector3 lunarSpeed = speed;
        distanceToPlatform.Normalize();
        lunarSpeed.Normalize();
        inputs[0] = distanceToPlatform.x;
        inputs[1] = distanceToPlatform.y;
        inputs[2] = lunarSpeed.x;
        inputs[3] = lunarSpeed.y;
        float[] outputs = brain.Synapsis(inputs);
        ThrottleLeft(dt, outputs[0]);
        ThrottleRight(dt, outputs[1]);
        ThrottleUp(dt, outputs[2]);
        if (distanceToPlatform.x < 0)
        {
            distanceToPlatform.x *= -1;
        }
        if (distanceToPlatform.y < 0)
        {
            distanceToPlatform.y *= -1;
        }
        int scoreX = 0;
        int scoreY = 0;
        int scoreSpeedX = 1;
        int scoreSpeedY = 1;
        if (lunarSpeed.x < 0)
        {
            if ((lunarSpeed.x * -1) < 0.5f)
            {
                scoreSpeedX = 2;
            }
            else
            {
                scoreSpeedX = 1;
            }
        }
        else
        {
            if (lunarSpeed.x < 0.5f)
            {
                scoreSpeedX = 2;
            }
            else
            {
                scoreSpeedX = 1;
            }
        }
        if (lunarSpeed.y < 0)
        {
            if ((lunarSpeed.y * -1) < 0.5f)
            {
                scoreSpeedY = 5;
            }
            else
            {
                scoreSpeedY = 1;
            }
        }
        else
        {
            if (lunarSpeed.y < 0.5f)
            {
                scoreSpeedY = 5;
            }
            else
            {
                scoreSpeedY = 1;
            }
        }
        if (distanceToPlatform.x < 1)
        {
            scoreX = 2 * scoreSpeedX;
            if (distanceToPlatform.x < 0.8)
            {
                scoreX = 4 * scoreSpeedX;
            }
            if (distanceToPlatform.x < 0.4)
            {
                scoreX = 8 * scoreSpeedX;
            }
            if (distanceToPlatform.x < 0.2)
            {
                scoreX = 16 * scoreSpeedX;
            }
            if (distanceToPlatform.x == 0)
            {
                scoreX = 32 * scoreSpeedX;
            }
        }
        if (distanceToPlatform.y < 1)
        {
            scoreY = 2 * scoreSpeedY;
            if (distanceToPlatform.y < 0.8)
            {
                scoreY = 4 * scoreSpeedY;
            }
            if (distanceToPlatform.y < 0.4)
            {
                scoreY = 8 * scoreSpeedY;
            }
            if (distanceToPlatform.y < 0.2)
            {
                scoreY = 16 * scoreSpeedY;
            }
            if (distanceToPlatform.y == 0)
            {
                scoreY = 32 * scoreSpeedY;
            }
        }
        // Debug.Log("scX " + scoreX);
        //Debug.Log("scY " + scoreY);
        Fitness += 1 + scoreX + scoreY;
        if (Fitness < 0)
        {
            Fitness = 0;
        }
	}

	protected override void OnRestart()
	{

	}

	protected override void OnCrashed()
	{
        Fitness *= 2;
        Debug.Log("BOOM");
	}

	protected override void OnLanded()
	{
        Fitness *= 4;
	}


}
