using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class RefractionTrigger : MonoBehaviour
{
	public bool isRotatable;
	public Vector3[] refractionDirections;
	static float dTheta = 1.0f;
	
	void OnTriggerEnter (Collider other){
		if (other.GetComponent <LightBeam> () != null) {
			LightBeam lb = other.GetComponent <LightBeam> ();
			float oldSpeed = lb.Speed;
			Vector3 oldPosition = other.transform.position;
			GameObject.Destroy (other.gameObject);

			//Vector3 oldDirection = lb.Direction;
			//Vector3 newDirection = Vector3.Reflect (oldDirection, this.transform.TransformDirection (this.normal));
			//lb.SetVelocity (new Vector2 (newDirection.x, newDirection.y) * oldSpeed);

			this.GetComponent <Collider>().enabled = false;

			foreach (Vector3 v in this.refractionDirections) {
				GameObject gobj = GameObject.Instantiate (GameManager.Instance.data.lightTrailPrefab, oldPosition, Quaternion.identity) as GameObject;
				LightBeam newLB = gobj.GetComponent <LightBeam>();
				newLB.SetVelocity (v.normalized * oldSpeed);
			}
		}
	}
	
	void OnDrawGizmos (){
		foreach (Vector3 v in this.refractionDirections) {
			Gizmos.DrawRay (this.transform.position, this.transform.TransformDirection (v));
		}
	}
}

