using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTransitionScriptB : MonoBehaviour {

    public GameObject gameStateManager;
    public bool isInTransition = false;
    public bool isStrobing = false;
    public bool isDimming = false;
    public bool isStopping = false;

    public float originalIntensity;
    public float timeTrigger;
    public float timeStop;

    public float dimmingDuration;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        bool triggerTransition = (Input.GetKeyDown(KeyCode.G) || (gameStateManager.GetComponent<StateManager>().state == StateManager.State.TRANSITION_TV_SOUND) && (!isInTransition));
        bool stopTransition = isInTransition && (Input.GetKeyDown(KeyCode.P) || gameStateManager.GetComponent<StateManager>().state == StateManager.State.SOUND);
        if (triggerTransition) {
            isInTransition = true;
            Debug.Log("Strobe triggered");
            timeTrigger = Time.time;
            this.isDimming = true;
        }
        else if (stopTransition) {
            Debug.Log("Strobe stopped");
            timeStop = Time.time;
            isInTransition = false;
            foreach (Transform transform in this.transform) {
                GameObject gO = transform.gameObject;
                if (gO.name.Contains("ight")) {
                    gO.GetComponent<StrobeLightScript>().isStrobing = false;
                    gO.GetComponent<Light>().intensity = originalIntensity;
                }
            }
            this.isStrobing = false;
            this.isStopping = true;
        }
        if (isDimming) {
            float timeSinceDimmingStarted = Time.time - timeTrigger;
            if (timeSinceDimmingStarted > dimmingDuration) {
                this.isStrobing = true;
                this.isDimming = false;
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
                float t = (dimmingDuration - timeSinceDimmingStarted) / dimmingDuration;
                float intensity = Mathf.Lerp(0f, originalIntensity, t);
                foreach (Transform transform in this.transform) {
                    GameObject gO = transform.gameObject;
                    if (gO.name.Contains("ight")) {
                        gO.GetComponent<Light>().intensity = intensity;
                    }
                }
            }
        }
        else if (isStopping) {
            float timeSinceStoppingStarted = Time.time - timeStop;
            if (timeSinceStoppingStarted > 2 * dimmingDuration) {
                isStopping = false;
            }
            else if (timeSinceStoppingStarted > dimmingDuration) {
                float t = (2 * dimmingDuration - timeSinceStoppingStarted) / dimmingDuration;
                float intensity = Mathf.Lerp(0f, originalIntensity, t);// Mathf.Lerp(0f, originalIntensity, t);
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