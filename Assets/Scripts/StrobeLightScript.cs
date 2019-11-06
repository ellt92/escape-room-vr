using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrobeLightScript : MonoBehaviour {

    public bool isStrobing = false;
    public float timeIntervalDark = 0.2f;
    public float timeIntervalBright = 0.02f;
    public int numPatterns = 3;
    float timeLastUpdate = 0f;
    public bool isInPattern = false;


    // Prob part
    public float probabilityStrobe = 0.2f;
    int numCount = 0;

    // Use this for initialization
    void Start () {
        //timeIntervalBright = 0.5f * timeIntervalDark /(2*numPatterns -1);
	}
	
	// Update is called once per frame
	void Update () {
		if (isStrobing) {
            letLightStrobeBis();
        }
        else {
            Light light = this.gameObject.GetComponent<Light>();
            if (!light.enabled) light.enabled = true;
        }
	}

    void launchStrobePattern() {
        if (numCount < 2*numPatterns ) {
            float timeSinceLastChange = Time.time - timeLastUpdate;
            if (timeSinceLastChange > timeIntervalBright) {
                timeLastUpdate = Time.time;
                Light light = this.gameObject.GetComponent<Light>();
                light.enabled = !light.enabled;
                numCount+=1;
            }
        }
        else{
            isInPattern = false;
            numCount = 0;
        }
    }
    
    void letLightStrobe() {
        /*
        if (launchCoroutine) {
            launchCoroutine = false;
            StartCoroutine("strobePattern", timeIntervalDark);
        }*/
        float timeSinceLastUpdate = Time.time - timeLastUpdate;
        if (timeSinceLastUpdate > timeIntervalDark) {
            timeLastUpdate = Time.time;
            Light light = this.gameObject.GetComponent<Light>();
            light.enabled = !light.enabled;
        }
    }

    void letLightStrobeBis() {
        if (!isInPattern) {
            float rand = Random.Range(0f, 1f);
            bool randomTest = (rand < probabilityStrobe);
            if (randomTest) {
                isInPattern = true;
            }
        }
        if (isInPattern) {
            launchStrobePattern();
        }
        /*else {
            Light light = this.gameObject.GetComponent<Light>();
            if (light.enabled) light.enabled = false;
        }*/

    }
}
