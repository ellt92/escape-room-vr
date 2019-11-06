using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractGlow : MonoBehaviour {

    public GameObject soundOriginGO;
    public Vector2 originPosition2D;
    public GameObject user;

    public Color originalColor;
    public float distance;
    public float intensity;

    public bool globalLightInteraction = true;
    public float minDistance;
    public float darkIntensity;
    public float brightIntensity;

    public GameObject setOfLights;
    public bool puzzleMode = false;

    public bool openRoofMode = true;
    public GameObject roof1;
    public GameObject roof2;
    public float z1a = 15f;
    public float z1b = 22f;
    public float z2a = 0.7f;
    public float z2b = -7f;

    public bool initialState = false;

    public float successDistance = 0.3f;
    public bool success = false;
    public GameObject fireball;


    public AudioSource successAudio;
    public AudioSource audioPuzzle;
    public float timeFadeOut = 1f;
    

    public GameObject gameState;

    // Use this for initialization
    void Start () {

    }

	void updateDistance() {

        originPosition2D = new Vector2(soundOriginGO.transform.position.x, soundOriginGO.transform.position.z);
        Vector2 userPosition2D = new Vector2(user.transform.position.x, user.transform.position.z);
        distance = (originPosition2D - userPosition2D).magnitude;
    }

    void changeZdistance(GameObject go, float z) {
        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, z);
    }

    IEnumerator FadeOut(float FadeTime) {
        float startVolume = audioPuzzle.volume;

        while (audioPuzzle.volume > 0) {
            audioPuzzle.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioPuzzle.Stop();
        audioPuzzle.volume = startVolume;
    }

    // Update is called once per frame
    void Update () {
        updateDistance();
        //intensity = -1f + 4f * Mathf.Exp(-distance/2);

        //Color currentColor = intensity * originalColor;
        //this.GetComponent<Renderer>().material.SetColor("_EmissionColor", currentColor);

        if (distance < successDistance) {
            success = true;
            successAudio.Play();
            gameState.GetComponent<StateManager>().state = StateManager.State.TRANSITION_SOUND_FLY;
            StartCoroutine("FadeOut", timeFadeOut);
        }

        /*

        if (puzzleMode && globalLightInteraction && !success) {
            
            else if (distance < minDistance) {
                initialState = false;
                float lightIntensity = darkIntensity + ((minDistance - distance) / minDistance) * (brightIntensity - darkIntensity)* Mathf.Exp(- distance);
                foreach (Transform lightTransform in setOfLights.transform) {
                    Light light = lightTransform.gameObject.GetComponent<Light>();
                    if (light) {
                        light.intensity = lightIntensity;
                    }
                }
                if (openRoofMode) {
                    float z1 = z1a + ((minDistance - distance)/minDistance)*(z1b - z1a) * Mathf.Exp(- distance);
                    float z2 = z2a + ((minDistance - distance) / minDistance) * (z2b - z2a) * Mathf.Exp(- distance);
                    changeZdistance(roof1, z1);
                    changeZdistance(roof2, z2);
                }
            }
            else {
                if (!initialState) {
                    initialState = true;
                    foreach (Transform lightTransform in setOfLights.transform) {
                        Light light = lightTransform.gameObject.GetComponent<Light>();
                        if (light) {
                            light.intensity = darkIntensity;
                        }
                    }
                    changeZdistance(roof1, z1a);
                    changeZdistance(roof2, z2a);
                }
            }
        }*/
    }
}
