using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Collider))]
public class ReflectionTrigger : MonoBehaviour
{
	public bool isRotatable;
	public Vector3 normal;
	static float dTheta = 1.0f;

	void OnTriggerEnter (Collider other){
		if (other.GetComponent <LightBeam> () != null) {
			LightBeam lb = other.GetComponent <LightBeam> ();
			float oldSpeed = lb.Speed;
			Vector3 oldDirection = lb.Direction;
			Vector3 newDirection = Vector3.Reflect (oldDirection, this.transform.TransformDirection (this.normal));
			lb.SetVelocity (new Vector2 (newDirection.x, newDirection.y) * oldSpeed);
		}
	}

	void OnDrawGizmos (){
		Gizmos.DrawRay (this.transform.position, this.transform.TransformDirection (this.normal));
	}
}

