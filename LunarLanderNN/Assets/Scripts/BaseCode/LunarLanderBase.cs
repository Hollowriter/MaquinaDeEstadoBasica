using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarLanderBase : MonoBehaviour 
{
	public enum State
	{
		Flying,
		Landed,
		Destroyed
	}

	protected Vector3 speed = Vector3.zero;
	const float acceleration = 5.0f;
	const float gravity = 2.5f;

    Genome genome;
	protected NeuralNetwork brain;

	protected GameObject platform;
	
	protected const float Width = 4;
	protected const float Height = 14;

	protected const float PlatformWidth = 9;
	protected const float PlatformHeight = 2;

	protected float[] inputs;

	public State state = State.Flying;

	public void SetBrain(Genome genome, NeuralNetwork brain)
    {
        this.genome = genome;
        this.brain = brain;
        inputs = new float[brain.InputsCount];
		state = State.Flying;
		speed = Vector3.zero;

		OnRestart();
    }

	public void SetPlatform(GameObject platform)
	{
		this.platform = platform;
	}

	public void Think(float dt)
	{
		if (state != State.Flying)
			return;

		OnThink(dt);

		ApplyGravity(dt);

		ApplySpeed(dt);

		if (IsCollidingPlatform())
		{
			if (IsTouchingDown() && speed.y > -1.0f)
			{
				//Debug.Log(State.Landed);
				state = State.Landed;
				OnLanded();
			}
			else
			{
				//Debug.Log(State.Destroyed);
				state = State.Destroyed;
				OnCrashed();
			}
		}
	}

	protected virtual void OnRestart()
	{

	}
	protected virtual void OnThink(float dt)
	{

	}

	protected virtual void OnCrashed()
	{
		
	}

	protected virtual void OnLanded()
	{
		
	}

	bool IsTouchingDown()
	{
		if (IsCollidingPlatform())
		{
			Vector3 pos1 = this.transform.position;
			Vector3 pos2 = platform.transform.position;

			if (pos1.x - Width / 2.0f >= pos2.x - PlatformWidth / 2.0f &&
				pos1.x + Width / 2.0f <= pos2.x + PlatformWidth / 2.0f &&
				pos1.y - Height / 2.0f >= pos2.y)
			{
				return true;
			}
		}

		return false;
	}

	bool IsCollidingPlatform()
	{
		Vector3 dist = this.transform.position - platform.transform.position;
		
		if (Mathf.Abs(dist.x) < (Width + PlatformWidth) / 2.0f && Mathf.Abs(dist.y) < (Height + PlatformHeight) / 2.0f)
			return true;

		return false;
	}

	void ApplySpeed(float dt)
	{
		this.transform.position += speed * dt;
	}

	void ApplyGravity(float dt)
	{
		speed.y -= gravity * dt;
	}

	protected void ThrottleRight(float dt, float force)
	{
		speed.x += acceleration * dt *  force;
	}
	
	protected void ThrottleLeft(float dt, float force)
	{
		speed.x -= acceleration * dt *  force;
	}

	protected void ThrottleUp(float dt, float force)
	{
		speed.y += acceleration * dt  *  force;
	}

	public float Fitness
	{
		get { return genome.fitness; }
		set { genome.fitness = value; }
	}
}
