using UnityEngine;
using System.Collections;

public class LightBeam : MonoBehaviour
{
	private Vector3 direction = Vector3.zero;
	public Vector3 Direction {
		get { return this.direction; }
	}

	private float speed = 0.0f;
	public float Speed {
		get { return this.speed; }
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.speed > 0) {
			Vector3 velocity = this.direction * this.speed;
			this.transform.position += new Vector3 (velocity.x, velocity.y, 0.0f) * Time.deltaTime;
		}
	}


	public void SetVelocity (Vector2 velocity) {
		//this.velocity = velocity;
		this.direction = velocity.normalized;
		this.speed = velocity.magnitude;
	}


	public void SetDirection (Vector3 direction) {
		this.direction = direction;
	}


	public void SetSpeed (float speed) {
		this.speed = speed;
	}
}

