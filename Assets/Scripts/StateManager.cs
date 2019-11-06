using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {


    public enum State
    {
        CORRIDOR = 1,
        CORRIDOR_OPEN,
        MAIN_ROOM_TUTORIAL,
        WALL,
        TRANSITION_WALL_TV,
        TV,
        TRANSITION_TV_SOUND,
        SOUND,
        TRANSITION_SOUND_FLY,
        FLY
    };

    public State state = State.WALL;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G)){
            state = State.TRANSITION_TV_SOUND;
        }
        if (Input.GetKeyDown(KeyCode.P)){
            state = State.SOUND;
        }
    }
}
