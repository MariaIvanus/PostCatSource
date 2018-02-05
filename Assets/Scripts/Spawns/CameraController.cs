using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {

	public Transform target;
    [SerializeField]
    private float smoothTime = 0.4F;
    [SerializeField]
	private float maxCameraOffset = 4.0f;
    
    private Vector3 velocity = Vector3.zero;
	private Vector3 initialPos;
    
	void Awake() {
		initialPos = transform.position;
        //target = GameObject.Find("GameController").GetComponent<GameController>().postcatPrefab.transform;
    }
    
	void FixedUpdate() {

        if (target != null) {
            Vector3 targetPosition = target.position;
            targetPosition.z = initialPos.z;
            targetPosition.y = Mathf.Clamp(targetPosition.y, -maxCameraOffset, maxCameraOffset);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
		}

    }

}
