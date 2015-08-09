using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InputHandler : MonoBehaviour
{
	public Camera targetCamera;
	public float minDistance = 1.0f;
	public float maxDistance = 10.0f;

	private LightBeam targetLB = null;
	private Vector2 startPos;
	private Vector2 endPos;
	

	void Start (){
		this.minDistance *= Screen.width * 0.1f;
		this.maxDistance *= Screen.width * 0.1f;
	}



	void OnDrawGizmos (){
		if (this.targetLB != null) {
			Vector3 offset = Vector3.ClampMagnitude ((this.endPos - this.startPos), this.maxDistance);
		
			if (offset.magnitude >= this.minDistance && offset.magnitude <= this.maxDistance) {
				Vector3 startPos3 = this.targetCamera.ScreenToWorldPoint (new Vector3 (this.startPos.x, this.startPos.y, -this.targetCamera.transform.position.z));
				Vector3 endPos3 = this.targetCamera.ScreenToWorldPoint (new Vector3 (this.endPos.x, this.endPos.y, -this.targetCamera.transform.position.z));

				Gizmos.DrawSphere (startPos3, 0.1f);
				Gizmos.DrawSphere (endPos3, 0.1f);
			}
		}

		//Debug.Log ("start " + startPos.ToString ());
		//Debug.Log ("end " + endPos.ToString ());
	}



	public void OnPointerDown (BaseEventData bed){
		PointerEventData ped = (PointerEventData)bed;

		Ray ray = this.targetCamera.ScreenPointToRay (ped.position);
		RaycastHit hit;
		
		if (Physics.Raycast (ray, out hit, 1000)) {
			LightBeam lb = hit.collider.GetComponent <LightBeam>();
			if (lb != null) {
				Debug.Log ("hit");
				this.targetLB = lb;
				this.startPos = ped.position;
			}
		}
	}


	public void OnPointerUp (BaseEventData bed){
		PointerEventData ped = (PointerEventData)bed;

		if (this.targetLB != null) {
			this.endPos = ped.position;

			Vector3 offset = Vector3.ClampMagnitude ((this.endPos - this.startPos), this.maxDistance);
			
			Vector3 startPos3 = this.targetCamera.ScreenToWorldPoint (new Vector3 (this.startPos.x, this.startPos.y, -this.targetCamera.transform.position.z));
			Vector3 endPos3 = this.targetCamera.ScreenToWorldPoint (new Vector3 (this.endPos.x, this.endPos.y, -this.targetCamera.transform.position.z));
			
			if (offset.magnitude >= this.minDistance && offset.magnitude <= this.maxDistance) {
				Vector2 velocity = new Vector2 (endPos3.x - startPos3.x, endPos3.y - startPos3.y);
				
				//GameManager.Instance.LightBeam.SetVelocity (velocity);
				this.targetLB.SetVelocity (velocity);

				//GameObject newGobj = GameObject.Instantiate (GameManager.Instance.data.lightTrailPrefab, startPos3, Quaternion.identity) as GameObject;
				//LightBeam lt = newGobj.GetComponent <LightBeam> ();
				//lt.SetVelocity (velocity);
				

			} else {
				//GameManager.Instance.LightBeam.transform.position = endPos3;
				//GameManager.Instance.LightBeam.SetVelocity (Vector2.zero);
				this.targetLB.SetVelocity (Vector2.zero);
			}
		}

		this.targetLB = null;
		this.startPos = Vector2.zero;
		this.endPos = Vector2.zero;


		/*this.endPos = ped.position;

		GameManager.Instance.LightBeam.SetVelocity (Vector2.zero);*/
		//if ((this.endPos - this.startPos).magnitude < this.minDistance) {
		/*if ((this.endPos - this.startPos) == Vector2.zero) {
			Vector3 endPos3 = this.targetCamera.ScreenToWorldPoint (new Vector3 (this.endPos.x, this.endPos.y, -this.targetCamera.transform.position.z));
			//GameManager.Instance.LightBeam.transform.position = endPos3;
		}*/
	}


	/*public void OnBeginDrag (BaseEventData bed){
		PointerEventData ped = (PointerEventData)bed;

		this.startPos = ped.position;
	}*/


	public void OnDrag (BaseEventData bed){
		PointerEventData ped = (PointerEventData)bed;

		Vector3 offset = Vector3.ClampMagnitude ((ped.position - this.startPos), this.maxDistance);
		//this.endPos = ped.position;
		this.endPos = this.startPos + new Vector2 (offset.x, offset.y);

		//Debug.Log (this.endPos);
	}


	/*public void OnEndDrag (BaseEventData bed){

	}*/
}

