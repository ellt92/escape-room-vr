using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionWallTVScript : MonoBehaviour {
    public GameObject flashlights;
    public GameObject revealingCube;
    public GameObject easel;


    public GameObject stateManager;
    public AudioSource transitionMusic;

    public GameObject setOfLights;

    public GameObject[] destroying;
    public GameObject[] appearing;


    public bool hasActivated = false;

    public float duration = 10f;

    public float WAITING_TIME_DESTRUCTION;
    public GameObject curtain;


	// Use this for initialization
	void Start () {

		
	}
    IEnumerator playMusic(float time) {
        yield return new WaitForSeconds(time);
        transitionMusic.Play();

    }
    IEnumerator stopTransition(float time) {
        yield return new WaitForSeconds(time);
        stateManager.GetComponent<StateManager>().state = StateManager.State.TV;

        setOfLights.SetActive(false);
        transitionMusic.Stop();
    }

    IEnumerator DestroyObjects(float time) {
        yield return new WaitForSeconds(time);
        destroying = GameObject.FindGameObjectsWithTag("DestroyA");
        foreach (GameObject destroyable in destroying) {
            //Destroy(destroyable);
            destroyable.SetActive(false);
        }
    }

    IEnumerator EnableObjects(float time) {
        yield return new WaitForSeconds(time);
        /*appearing = GameObject.FindGameObjectsWithTag("AppearA");
        foreach (GameObject appearObj in appearing) {
            appearObj.SetActive(true);
        }*/
        easel.SetActive(true);
        flashlights.SetActive(true);
        revealingCube.SetActive(true);
    }

    void LaunchLightBehavior() {
        // dimming time = Waiting
        // duration = duration
        setOfLights.GetComponent<LightTransitionScript>().dimmingDuration = WAITING_TIME_DESTRUCTION;
        setOfLights.GetComponent<LightTransitionScript>().triggerTransition = true;
    }

        // Update is called once per frame
    void Update () {
        bool activate = !hasActivated && (stateManager.GetComponent<StateManager>().state == StateManager.State.TRANSITION_WALL_TV || Input.GetKeyDown(KeyCode.L));
        if (activate) {
            curtain.GetComponent<ShiftCurtain>().close();
            StartCoroutine("DestroyObjects", WAITING_TIME_DESTRUCTION);
            LaunchLightBehavior();
            hasActivated = true;
            StartCoroutine("playMusic", WAITING_TIME_DESTRUCTION);
            StartCoroutine("EnableObjects", duration);
            StartCoroutine("stopTransition", duration);
        }

	}
}
