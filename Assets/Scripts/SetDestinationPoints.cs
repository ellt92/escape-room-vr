using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDestinationPoints : MonoBehaviour {

    public GameObject gameStateManager;
    public GameObject corridorDPs;
    public GameObject corridorOpenDPs;
    public GameObject mainRoomTutorialDPs;
    public GameObject wallDPs;
    public GameObject wallToTVDPs;
    public GameObject tvDPs;
    public GameObject soundDPs;
    public GameObject statueDPs;
    StateManager.State prevState;

	void Start () {
    }

    GameObject getDestinationPoints(StateManager.State state) {
        if (state == StateManager.State.CORRIDOR) {
            return corridorDPs;
        } else if (state == StateManager.State.CORRIDOR_OPEN) {
            return corridorOpenDPs;
        } else if (state == StateManager.State.MAIN_ROOM_TUTORIAL) {
            return mainRoomTutorialDPs;
        }else if (state == StateManager.State.WALL) {
            return wallDPs;
        }
        else if (state == StateManager.State.TRANSITION_WALL_TV) {
            return wallToTVDPs;
        }
        else if (state == StateManager.State.TV) {
            return tvDPs;
        }
        else if (state == StateManager.State.SOUND) {
            return soundDPs;
        }
        else if (state == StateManager.State.FLY) {
            return statueDPs;
        }
        return corridorDPs;
    }

    private void Update()
    {
        if (gameStateManager.GetComponent<StateManager>().state != prevState) {
            getDestinationPoints(prevState).SetActive(false);
            prevState = gameStateManager.GetComponent<StateManager>().state;
            getDestinationPoints(prevState).SetActive(true);
        }
        if (gameStateManager.GetComponent<StateManager>().state == StateManager.State.FLY) {
                wallDPs.SetActive(true);
                wallToTVDPs.SetActive(true);
                tvDPs.SetActive(true);
                soundDPs.SetActive(true);
}
    }
}
