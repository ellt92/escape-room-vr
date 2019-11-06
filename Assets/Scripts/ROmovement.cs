using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROmovement : MonoBehaviour {

    public float yMin = 0f;
    public float yMax = 4f;
    public float paceY = 0.5f;
    public bool ascending = true;

    public float paceRotation = 20f;

    public GameObject TVSet;
    public GameObject user;

    // Use this for initialization
    void Start() {

    }

    void updateTvRotation() {
        TVSet.transform.LookAt(user.transform);//, new Vector3(1f, 0f, 0f));
    }

    // Update is called once per frame
    void Update() {
        float currentY = this.transform.position.y;
        if (currentY > yMax) ascending = false;
        else if (currentY < yMin) ascending = true;
        float deltaT = Time.deltaTime;
        float deltaY = (ascending ? 1f : -1f) * paceY * deltaT;
        this.transform.transform.Translate(new Vector3(0f, deltaY, 0f));
        float deltaAngle = paceRotation * deltaT;
        this.transform.Rotate(new Vector3(0f, deltaAngle, 0f));

        //updateTvRotation();
    }
}