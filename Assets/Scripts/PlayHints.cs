using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHints : MonoBehaviour {
    public GameObject gameStateManager;
    public AudioSource hintAudio;
    public AudioSource pickUp;
    public AudioSource hangUp;
    public AudioClip puzzleHintTutorial;
    public AudioClip puzzleHintHammerWall;
    public AudioClip puzzleHintUseCasette;
    public AudioClip puzzleHintFlashlights;
    public AudioClip noHint;
    bool start = false;
    public float delay = 1f;
    // Use this for initialization
    void Start () {
	}

    void playCurrentHint() {
        switch(gameStateManager.GetComponent<StateManager>().state) {
            case (StateManager.State.MAIN_ROOM_TUTORIAL):
                hintAudio.clip = puzzleHintTutorial;
                break;
            case (StateManager.State.WALL):
                hintAudio.clip = puzzleHintHammerWall;
                break;
            case (StateManager.State.TRANSITION_WALL_TV):
                hintAudio.clip = puzzleHintUseCasette;
                break;
            case (StateManager.State.TV):
                hintAudio.clip = puzzleHintFlashlights;
                break;
            default:
                hintAudio.clip = noHint;
                break;
        }
        hintAudio.PlayDelayed(delay);
    }

    bool isGrabbed() {
        return this.GetComponent<Rigidbody>().isKinematic;
    }
	
	// Update is called once per frame
	void Update () {
		if (isGrabbed() && !start) {
            playCurrentHint();
            start = true;
            pickUp.Play();
        }
        else if(!isGrabbed() && start) {
            hintAudio.Stop();
            start = false;
            hangUp.Play();
        }
	}
}
