using UnityEngine;
using System.Collections;

public class AcceptorTrigger : MonoBehaviour
{
	
	void OnTriggerEnter( Collider other ){
		if (other.GetComponent <LightBeam>() != null) {
			LightBeam lb = other.GetComponent <LightBeam>();
		}
	}
	
	void OnTriggerExit( Collider other ){
		if (other.GetComponent <LightBeam>() != null) {
			LightBeam lb = other.GetComponent <LightBeam>();
		}
	}
}

