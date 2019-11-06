using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSoundWingsScript : MonoBehaviour {
    //Objects that appear
    public GameObject wingA;
    public GameObject wingB;
    public GameObject sun;
    public GameObject halfRoof1;
    public GameObject halfRoof2;
    public GameObject roof;


    public bool isDimming = false;
    float timeDimStart = 0f;
    public float originalIntensity = 2f;


    public GameObject stateManager;
    public AudioSource transitionMusic;

    public GameObject setOfLights;

    public bool hasActivated = false;

    public float wait1 = 1f;
    public float wait2 = 2f;
    public float timeOpeningRoof = 3f;
    float startOpenRoof = 0f;
    public float z1a = 15f;
    public float z1b = 22f;
    public float z2a = 0.7f;
    public float z2b = -7f;
    public bool isOpening = false;
    public AudioSource openRoofSound;

    // Use this for initialization
    void Start() {

    }
    
    IEnumerator DestroyObjects(float time) {
        yield return new WaitForSeconds(time);
        GameObject[] destroying = GameObject.FindGameObjectsWithTag("DestroyC");
        foreach (GameObject destroyable in destroying) {
            //Destroy(destroyable);
            destroyable.SetActive(false);
        }
        timeDimStart = Time.time;
        isDimming = true;
    }

    IEnumerator EnableObjects(float time) {
        yield return new WaitForSeconds(time);
        if (sun) {
            sun.SetActive(true);
        }
        if (wingA) {
            wingA.SetActive(true);
        }
        if (wingB) {
            wingB.SetActive(true);
        }
        if (halfRoof1) {
            halfRoof1.SetActive(true);
        }
        if (halfRoof2) {
            halfRoof2.SetActive(true);
        }
        roof.SetActive(false);
        stateManager.GetComponent<StateManager>().state = StateManager.State.FLY;
        isOpening = true;
        startOpenRoof = Time.time;
        if (openRoofSound) {
            openRoofSound.Play();
        }
    }
    
    

    void dimFlashlights() {
        if (isDimming) {
            float t = (Time.time - timeDimStart) / wait1;
            float intensity = Mathf.Lerp(originalIntensity, 0f, t);
        }
    }

    void openRoof() {
        if (isOpening) {
            float t = (Time.time - startOpenRoof) / timeOpeningRoof;
            if (t <= 1f) {
                float z1 = Mathf.Lerp(z1a, z1b, t);
                float z2 = Mathf.Lerp(z2a, z2b, t);
                changeZdistance(halfRoof1, z1);
                changeZdistance(halfRoof2, z2);
            }
            else {
                isOpening = false;
            }
        }
    }

    void changeZdistance(GameObject go, float z) {
        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, z);
    }

    // Update is called once per frame
    void Update() {
        bool activate = !hasActivated && (stateManager.GetComponent<StateManager>().state == StateManager.State.TRANSITION_SOUND_FLY || Input.GetKeyDown(KeyCode.V));
        if (activate) {
            hasActivated = true;
            StartCoroutine("DestroyObjects", wait1);
            StartCoroutine("EnableObjects", wait1 + wait2);
        }
        openRoof();
        dimFlashlights();
    }
}
