using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTransitionScript : MonoBehaviour {

    public GameObject gameStateManager;
    public bool isInTransition = false;
    public bool isDimming = false;

    public float originalIntensity;
    public float timeTrigger;
    public float timeStop;

    public float dimmingDuration;


    public bool triggerTransition = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (triggerTransition && !isInTransition) {
            isInTransition = true;
            isDimming = true;
            timeTrigger = Time.time;
        }
        /*if (triggerTransition) {
            isInTransition = true;
            Debug.Log("Strobe triggered");
            timeTrigger = Time.time;
            this.isDimming = true;
        }*/
        /*else if (stopTransition) {
            Debug.Log("Strobe stopped");
            timeStop = Time.time;
            isInTransition = false;
            foreach (Transform transform in this.transform) {
                GameObject gO = transform.gameObject;
                if (gO.name.Contains("ight")) {
                    gO.GetComponent<StrobeLightScript>().isStrobing = false;
                    gO.GetComponent<Light>().intensity = 0f;
                }
            }
            this.isStrobing = false;
            this.isStopping = true;
        }*/
        if (isDimming) {
            float timeSinceDimmingStarted = Time.time - timeTrigger;
            if (timeSinceDimmingStarted > dimmingDuration) {
                isDimming = false;
                foreach (Transform transform in this.transform) {
                    GameObject gO = transform.gameObject;
                    if (gO.name.Contains("ight")) {
                        gO.GetComponent<Light>().intensity = originalIntensity;
                        gO.GetComponent<Light>().enabled = false;
                        gO.GetComponent<StrobeLightScript>().isStrobing = true;
                    }
                }
            }
            else {
                float t = timeSinceDimmingStarted / dimmingDuration;
                float intensity = Mathf.Lerp(originalIntensity, 0f, t);
                foreach (Transform transform in this.transform) {
                    GameObject gO = transform.gameObject;
                    if (gO.name.Contains("ight")) {
                        gO.GetComponent<Light>().intensity = intensity;
                    }
                }
            }
        }
    }
}
