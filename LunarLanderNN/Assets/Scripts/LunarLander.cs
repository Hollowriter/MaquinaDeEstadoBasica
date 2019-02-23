using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarLander : LunarLanderBase
{
    bool crashedWall = false;
    bool crashedLand = false;
    bool landed = false;
    bool floor = false;
    protected override void OnThink(float dt)
    {
        Vector3 distanceToPlatform = platform.transform.position - this.transform.position;
        float platformPositionX = platform.transform.position.x;
        float platformPositionY = platform.transform.position.y;
        float myDistanceX = platformPositionX - this.transform.position.x;
        float myDistanceY = platformPositionY - this.transform.position.y;

        if (myDistanceX < 0)
            myDistanceX *= -1;
        if (myDistanceY < 0)
            myDistanceY *= -1;

        Vector3 lunarSpeed = speed;
        distanceToPlatform.Normalize();
        lunarSpeed.Normalize();
        inputs[0] = lunarSpeed.x;
        inputs[1] = lunarSpeed.y;
        float[] outputs = brain.Synapsis(inputs);
        ThrottleLeft(dt, outputs[0]);
        ThrottleRight(dt, outputs[1]);
        ThrottleUp(dt, outputs[2]);
        /* if (distanceToPlatform.x < 0 && !crashedWall)
         {
             distanceToPlatform.x *= -1;
         }
         if (distanceToPlatform.y < 0 && !crashedWall)
         {
             distanceToPlatform.y *= -1;
         }*/
        float scoreX = 0;
        float scoreY = 0;
        float scoreSpeedX = 1;
        float scoreSpeedY = 1;
       /* if (lunarSpeed.x < 0 && !crashedWall)
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
            if (lunarSpeed.x < 0.5f && !crashedWall)
            {
                scoreSpeedX = 2;
            }
            else
            {
                scoreSpeedX = 1;
            }
        }*/
        if (lunarSpeed.y < 0)
        {

            if ((lunarSpeed.y * -1) < 0.5f && !crashedWall)
            {
                scoreSpeedY = 5;
            }
            else
            {
                scoreSpeedY = 2;
            }
        }
        else
        {
            /*if (lunarSpeed.y < 0.5f && !crashedWall)
            {
                scoreSpeedY = 5;
            }
            else
            {
                scoreSpeedY = 2;
            }*/
            scoreSpeedY = 0;
        }
        /*
        if (distanceToPlatform.x >= 0) //Si no estoy alineado
        {
            if (distanceToPlatform.x < 1 && !crashedWall)
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
                    Fitness += 1;
                }
            }
        }
        else if (distanceToPlatform.x < 0)
        {
            if ((distanceToPlatform.x * -1) < 1 && !crashedWall)
            {
                scoreX = 2 * scoreSpeedX;
                if ((distanceToPlatform.x * -1) < 0.8)
                {
                    scoreX = 4 * scoreSpeedX;
                }
                if ((distanceToPlatform.x * -1) < 0.4)
                {
                    scoreX = 8 * scoreSpeedX;
                }
                if ((distanceToPlatform.x * -1) < 0.2)
                {
                    scoreX = 16 * scoreSpeedX;
                }
            }
        }*/
        /* if(myDistanceX > 30)
         {
             Fitness /= 2;
         }
         */
        // if (myDistanceX > 10)
        //{
        //  scoreX /= myDistanceX;
        //}
        /*else*/ /*if (myDistanceX < 10)*/
                 /*scoreX*/ /*+*//*= 5*/ /*100/(myDistanceX+1)*//*;*/
        if (myDistanceX < 1 && !crashedWall)
        {
            scoreX = 2;
            if (myDistanceX < 0.8)
            {
                scoreX = 5;
            }
            if (myDistanceX < 0.4)
            {
                scoreX = 10;
            }
            if (myDistanceX < 0.2)
            {
                scoreX = 20;
            }
            if (myDistanceX == 0)
            {
                scoreX = 40;
            }
        }

        //if (myDistanceY > 10)
        //  scoreY /= myDistanceY;
        //else if (myDistanceY < 10)
        //  scoreY += 2/(myDistanceY+1);

        //else if (myDistanceY < 10)
        //Fitness *= 10;

        if (distanceToPlatform.y < 1 && !crashedWall)
        {
            scoreY = 2 /** scoreSpeedY*/;
            if (distanceToPlatform.y < 0.8)
            {
                scoreY = 4 /** scoreSpeedY*/;
            }
            if (distanceToPlatform.y < 0.4)
            {
                scoreY = 8 /** scoreSpeedY*/;
            }
            if (distanceToPlatform.y < 0.2)
            {
                scoreY = 16 /** scoreSpeedY*/;
            }
            if (distanceToPlatform.y == 0)
            {
                scoreY = 32 /** scoreSpeedY*/;
            }
        }
        scoreY += scoreSpeedY;
        
            // Debug.Log("scX " + scoreX);
            //Debug.Log("scY " + scoreY);


            //if (!crashedWall)
                Fitness += 1 + scoreX + scoreY;

            if (landed)
            {
                Fitness += 100;
                Fitness *= 100;
            }
       
        if (Fitness < 0)
        {
            Fitness = 0;
            Debug.Log("Llegue a -0.1");
        }
        setScore(Fitness);
    }

    protected override void OnRestart()
    {
        crashedWall = false;
        crashedLand = false;
        landed = false;
        floor = false;
        Debug.Log("RESET");
    }
    void CrashedWithWall()
    {
        if (crashedWall)
            Fitness /= 200;
    }
    void CrashedWithFloor()
    {
        Fitness /= 30;
    }
	protected override void OnCrashed() //Si choca contra la plataforma de aterrizaje
	{

        crashedLand = true;
        Debug.Log("BOOM");
        Fitness *= 500;
    }

	protected override void OnLanded()
	{
        landed = true;
        Fitness *= 10000;
        Debug.Log("Congrats");
	}
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Border")
        {
            crashedWall = true;
            CrashedWithWall();
            Debug.Log("Choca pared");
        }
        if (collider.gameObject.tag == "Floor")
        {
            floor = true;
            CrashedWithFloor();
            Debug.Log("Choca piso");
        }

    }

}
