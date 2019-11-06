using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTVSoundScript : MonoBehaviour
{
    //Objects that appear
    public GameObject soundSource;
    public GameObject puzzleHandler;
    public GameObject movingLights;

    public Light flashLightA;
    public Light flashLightB;
    public Light flashLightC;
    public Light flashLightD;
    public bool isDimming = false;
    float timeDimStart = 0f;
    public float originalIntensity = 2f;


    public GameObject stateManager;
    public AudioSource transitionMusic;

    public GameObject setOfLights;

    public GameObject[] destroying;
    public GameObject[] appearing;

    public bool hasActivated = false;

    public float wait1 = 2f;
    public float wait2 = 1.5f;
    public float wait3 = 2f;
    public float wait4 = 5f;
    public float wait5 = 2f;

    public GameObject cameraRig;
    public GameObject rigReferencePosition;

    // Use this for initialization
    void Start() {

    }

    IEnumerator stopTransition(float time) {
        yield return new WaitForSeconds(time);
        stateManager.GetComponent<StateManager>().state = StateManager.State.TV;

        setOfLights.SetActive(false);
        transitionMusic.Stop();
    }

    IEnumerator DestroyObjects(float time) {
        yield return new WaitForSeconds(time);
        destroying = GameObject.FindGameObjectsWithTag("DestroyB");
        foreach (GameObject destroyable in destroying) {
            //Destroy(destroyable);
            destroyable.SetActive(false);
        }
        isDimming = false;
        teleportPlayer();
    }

    IEnumerator EnableObjects(float time) {
        yield return new WaitForSeconds(time);
        if (soundSource) {
            soundSource.SetActive(true);
        }
        if (puzzleHandler) {
            puzzleHandler.SetActive(true);
        }
        if (movingLights) {
            movingLights.SetActive(true);
        }
        stateManager.GetComponent<StateManager>().state = StateManager.State.SOUND;
    }

    void teleportPlayer() {
        Vector3 endPosition = rigReferencePosition.transform.position;
        cameraRig.transform.position = new Vector3(endPosition.x, cameraRig.transform.position.y, endPosition.z);
    }

    IEnumerator LaunchLightBehavior(float time) {
        yield return new WaitForSeconds(time);
        transitionMusic.Play();
        setOfLights.SetActive(true);
    }

    IEnumerator StopLightBehavior(float time) {
        yield return new WaitForSeconds(time);
        transitionMusic.Stop();
        setOfLights.SetActive(false);
    }

    void setOriginalIntensity() {
        originalIntensity = flashLightA.intensity;
    }

    void dimFlashlights() {
        if (isDimming) {
            float t = (Time.time - timeDimStart) / wait1;
            float intensity = Mathf.Lerp(originalIntensity, 0f, t);
            flashLightA.intensity = intensity;
            flashLightB.intensity = intensity;
            flashLightC.intensity = intensity;
            flashLightD.intensity = intensity;
        }
    }

    // Update is called once per frame
    void Update() {
        bool activate = !hasActivated && (stateManager.GetComponent<StateManager>().state == StateManager.State.TRANSITION_TV_SOUND || Input.GetKeyDown(KeyCode.Q));
        if (activate) {
            hasActivated = true;
            timeDimStart = Time.time;
            isDimming = true;
            setOriginalIntensity();
            StartCoroutine("DestroyObjects", wait1);
            StartCoroutine("LaunchLightBehavior", wait1 + wait3);
            StartCoroutine("StopLightBehavior", wait1 + wait3 + wait4);
            StartCoroutine("EnableObjects", wait1 + wait3 + wait4 + wait5);
        }
        dimFlashlights();
    }
}
