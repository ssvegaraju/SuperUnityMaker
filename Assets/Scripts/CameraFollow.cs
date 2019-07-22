using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    public float smoothingSpeed;

	// Use this for initialization
	void Start () {
        if (target == null)
            target = FindObjectOfType<PlatformerMovement2D>().transform;
	}
	
	void LateUpdate () {
        transform.position = Vector3.MoveTowards(transform.position, 
            target.position + offset, smoothingSpeed * Time.deltaTime);
	}
}
