using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightClubLights : MonoBehaviour {
    public float pace = 0.3f;
    public GameObject area;

    float orientX = 1f;
    float orientZ = 1f;
    // Use this for initialization
    void Start () {
		
	}
    void checkForDirections() {
        float relativeX = this.transform.position.x - area.transform.position.x;
        float relativeZ = this.transform.position.z - area.transform.position.z;
        if (relativeX > area.transform.lossyScale.x / 2f) {
            orientX = -1f;
        }
        else if (relativeX < -(area.transform.lossyScale.x / 2f)) {
            orientX = 1f;
        }
        if (relativeZ > area.transform.lossyScale.z / 2f) {
            orientZ = -1f;
        }
        else if (relativeZ < -(area.transform.lossyScale.z / 2f)) {
            orientZ = 1f;
        }
    }
    void moveLight(float t) {
        checkForDirections();
        float dirX = orientX * Random.RandomRange(0f, 1f);
        float dirZ = orientZ * Random.RandomRange(0f, 1f);
        Vector2 dir = (new Vector2(dirX, dirZ)).normalized;
        float posX = this.transform.position.x + pace * dir.x * t;
        float posZ = this.transform.position.z + pace * dir.y * t;
        this.transform.position = new Vector3(posX, this.transform.position.y, posZ);
    }
	
	// Update is called once per frame
	void Update () {
        float t = Time.deltaTime;
        moveLight(t);
	}
}
