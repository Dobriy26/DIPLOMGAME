using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour {
	
	[SerializeField]
	private GameObject targetObject;

	private float distanceToTarget;

	// Use this for initialization
	void Start () {
		distanceToTarget = transform.position.x - targetObject.transform.position.x;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float targetObjectX = targetObject.transform.position.x;
		
		Vector3 newCameraPosition = transform.position;
		newCameraPosition.x = targetObjectX + distanceToTarget;
		transform.position = newCameraPosition;    	
	}
}
