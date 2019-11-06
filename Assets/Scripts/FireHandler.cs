using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHandler : MonoBehaviour {
    public float zSuccess;
    float timeTrigger;
    bool success = false;
    bool stopIncreasing = false;
    public float durationBurning = 10f;

    public float maxScale = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
    void updateFireSize() {
        if (success && !stopIncreasing) {
            float t = (Time.time - timeTrigger) / durationBurning;
            if (t > 1) {
                stopIncreasing = true;
            }
            float scale = Mathf.Lerp(0f, maxScale, t);
            foreach(Transform transform in this.transform) {
                GameObject gO = transform.gameObject;
                if (gO.name.Contains("omplex")) {
                    foreach(Transform childTransform in gO.transform) {
                        transform.localScale = new Vector3(scale, scale, scale);
                    }
                }
            }
        }
    }
	// Update is called once per frame
	void Update () {
        if (!success) {
            if (transform.position.y > zSuccess) {
                success = true;
                timeTrigger = Time.time;
                foreach (Transform transform in this.transform) {
                    GameObject gO = transform.gameObject;
                    if (gO.name.Contains("omplex")) {
                        if (!gO.active) {
                            gO.SetActive(true);
                        }
                    }
                }
            }
        }
        updateFireSize();
	}
}
