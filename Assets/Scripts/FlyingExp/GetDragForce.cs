using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDragForce : MonoBehaviour {

    public float drag = 0.1f;
    public Vector3 previousPosition;
    public Vector3 currentPosition;

    public GameObject user;
    public Vector3 userPrevPos;
    public Vector3 userCurPos;

    public Vector3 wingVelocity;
    public Vector3 dragForce;
    
	// Use this for initialization
	void Start () {
        previousPosition = this.transform.position;
        currentPosition = this.transform.position;
        userPrevPos = user.transform.position;
        userCurPos = user.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        currentPosition = this.transform.position;
        userCurPos = user.transform.position;
        Vector3 userVelocity = (userCurPos - userPrevPos) / Time.deltaTime;
        wingVelocity = (currentPosition - previousPosition)/Time.deltaTime - userVelocity;
        previousPosition = currentPosition;
        userPrevPos = userCurPos;
        float velocityMagnitude = wingVelocity.magnitude;
        float forceMagnitude = drag * velocityMagnitude * velocityMagnitude;
        dragForce = -forceMagnitude * wingVelocity.normalized;
    }
}
