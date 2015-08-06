using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Collider))]
public class BasicReflectionTrigger : MonoBehaviour
{
	public GameObject lightBeam;
	public bool isRotatable;
	static float dTheta = 1.0f;

	void OnTriggerEnter( Collider other ){
		if (other.GetComponent <LightBeam> () != null) {
			LightBeam lb = other.GetComponent <LightBeam> ();
			float oldSpeed = lb.Speed;
			Vector3 oldDirection = lb.Direction;
			Vector3 reflectionPlane = this.transform.TransformDirection (Vector3.down);
			Vector3 newDirection = this.GetNewDirection (oldDirection, reflectionPlane);

			this.CreateBeam (other.transform.position, newDirection, oldSpeed);

			lb.enabled = false;
			lb.GetComponent <Collider>().enabled = false;
			lb.GetComponent <Renderer>().enabled = false;
		}
	}


	void OnDrawGizmos(){
		//Gizmos.DrawWireCube (this.transform.position, this.GetComponent<BoxCollider>().size * this.transform.localScale.x);
		Gizmos.DrawRay (this.transform.position, this.transform.TransformDirection (Vector3.down));
	}
	
	private Vector3 GetNewDirection(Vector3 incidence, Vector3 reflectionPlane){
		Vector3 localIncidence = this.transform.InverseTransformDirection (incidence);
		float angle = Mathf.Acos( Vector3.Dot( incidence, reflectionPlane ) / (incidence.magnitude * reflectionPlane.magnitude) );
		float v_perp = Mathf.Cos (angle) * incidence.magnitude;

		return this.transform.TransformDirection( new Vector3(localIncidence.x, -1.0f * Mathf.Sign(reflectionPlane.y) * v_perp, 0.0f) );
	}
	
	private void CreateBeam(Vector3 position, Vector3 direction, float speed){
		GameObject newLightBeam = GameObject.Instantiate(lightBeam, position, Quaternion.identity) as GameObject;
		newLightBeam.GetComponent <LightBeam> ().SetVelocity (direction * speed);
	}
}

