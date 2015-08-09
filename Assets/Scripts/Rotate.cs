using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	public float speed = 0.0f;
	public Vector3 axis = Vector3.zero;


	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.Rotate (this.axis * this.speed * Time.deltaTime);
	}
}

