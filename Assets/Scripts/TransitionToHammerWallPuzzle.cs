using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToHammerWallPuzzle : MonoBehaviour {

    public GameObject gameStateManager;
    public GameObject hammer;
    public GameObject detector;
    public GameObject curtain;
    public AudioSource mainBackgroundMusic;
    // Use this for initialization
    void Start () {
        mainBackgroundMusic.Play();
    }

    bool IsMainRoomTutorial() {
        return gameStateManager.GetComponent<StateManager>().state == StateManager.State.MAIN_ROOM_TUTORIAL;
    }

    bool IsKeyObjectPickedUp() {
        return hammer.GetComponent<Rigidbody>().isKinematic || detector.GetComponent<Rigidbody>().isKinematic;
    }
	
	// Update is called once per frame
	void Update () {
        if(IsMainRoomTutorial() && IsKeyObjectPickedUp()) {
            gameStateManager.GetComponent<StateManager>().state = StateManager.State.WALL;
            curtain.GetComponent<ShiftCurtain>().open();
        }
        if (gameStateManager.GetComponent<StateManager>().state == StateManager.State.TRANSITION_WALL_TV) {
            mainBackgroundMusic.Stop();
        }
    }
}
