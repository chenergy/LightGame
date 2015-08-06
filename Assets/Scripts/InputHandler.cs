using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class InputHandler : MonoBehaviour
{
	public Camera targetCamera;

	private Vector2 startPos;
	private Vector2 endPos;


	/*
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	*/

	void OnDrawGizmos (){
		Vector3 startPos3 = this.targetCamera.ScreenToWorldPoint (new Vector3 (this.startPos.x, this.startPos.y, -this.targetCamera.transform.position.z));
		Vector3 endPos3 = this.targetCamera.ScreenToWorldPoint (new Vector3 (this.endPos.x, this.endPos.y, -this.targetCamera.transform.position.z));
		//Vector3 startPos3 = this.targetCamera.ScreenToWorldPoint (this.startPos);
		//Vector3 endPos3 = this.targetCamera.ScreenToWorldPoint (this.endPos);

		Gizmos.DrawSphere (startPos3, 0.1f);
		Gizmos.DrawSphere (endPos3, 0.1f);

		//Debug.Log ("start " + startPos.ToString ());
		//Debug.Log ("end " + endPos.ToString ());
	}


	public void OnBeginDrag (BaseEventData bed){
		PointerEventData ped = (PointerEventData)bed;

		this.startPos = ped.position;

		//Debug.Log (ped.position);
	}


	public void OnDrag (BaseEventData bed){
		PointerEventData ped = (PointerEventData)bed;
		
		this.endPos = ped.position;

		//Debug.Log (ped.position);
	}


	public void OnEndDrag (BaseEventData bed){
		Vector3 startPos3 = this.targetCamera.ScreenToWorldPoint (new Vector3 (this.startPos.x, this.startPos.y, -this.targetCamera.transform.position.z));
		Vector3 endPos3 = this.targetCamera.ScreenToWorldPoint (new Vector3 (this.endPos.x, this.endPos.y, -this.targetCamera.transform.position.z));

		Vector2 velocity = new Vector2 (endPos3.x - startPos3.x, endPos3.y - startPos3.y);
		GameObject newGobj = GameObject.Instantiate (GameManager.Instance.data.lightTrailPrefab, startPos3, Quaternion.identity) as GameObject;
		LightBeam lt = newGobj.GetComponent <LightBeam> ();
		lt.SetVelocity (velocity);

		this.startPos = Vector2.zero;
		this.endPos = Vector2.zero;
	}
}

