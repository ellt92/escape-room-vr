using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayCastScript : MonoBehaviour {

    public GameObject redFlashlight;
    public GameObject blueFlashlight;

    public bool redHit;
    public bool blueHit;

    string blueHitName;
    string redHitName;

    public float hitRadius = 0.3f;

    public float minTime = 0.5f;
    public bool isSucceeding = false;
    public float timeStartSucced;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int layerMask = 1 << 8;
        RaycastHit hit;

        if (Physics.SphereCast(blueFlashlight.transform.position, hitRadius, blueFlashlight.transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, layerMask)) {
            Transform blueHitObject = hit.transform;
            blueHit = true;
            blueHitName = blueHitObject.name;
            //Debug.DrawRay(blueFlashlight.transform.position, 0.5f, blueFlashlight.transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
        } else {
            //Debug.DrawRay(blueFlashlight.transform.position, 0.5f, blueFlashlight.transform.TransformDirection(Vector3.up) * 1000, Color.white);
            blueHit = false;
        }

        if (Physics.SphereCast(redFlashlight.transform.position, hitRadius, redFlashlight.transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, layerMask)) {
            Transform redHitObject = hit.transform;
            redHit = true;
            redHitName = redHitObject.name;
            //Debug.DrawRay(redFlashlight.transform.position, 0.5f, redFlashlight.transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);
        }
        else {
            //Debug.DrawRay(redFlashlight.transform.position, 0.5f, redFlashlight.transform.TransformDirection(Vector3.up) * 1000, Color.white);
            redHit = false;
        }
        if (!isSucceeding) {
            if (sameLetterHit() && bothHit()) {
                isSucceeding = true;
                timeStartSucced = Time.time;
            }
        }
        if(isSucceeding && !(sameLetterHit() && bothHit())) {
            isSucceeding = false;
        }

    }

    public bool bothHit() {
        return redHit && blueHit;
    }

    public bool sameLetterHit() {
        return string.Equals(redHitName, blueHitName);
    }

    public string getLetterHitName() {
        return blueHitName;
    }

    public bool successForDeltaTime() {
        float currentTime = Time.time - timeStartSucced;
        return isSucceeding && (currentTime > minTime);
    }
}
