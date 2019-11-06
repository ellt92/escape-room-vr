using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorManager : MonoBehaviour {
    public GameObject gameState;
    StateManager.State prevState;
    // Use this for initialization
    public GameObject curtain1;
    public GameObject curtain2;
    public GameObject player;
    bool corridorOver = false;
	void Start () {
        prevState = gameState.GetComponent<StateManager>().state;
	}
	
	// Update is called once per frame
	void Update () {
        if (prevState == StateManager.State.CORRIDOR && gameState.GetComponent<StateManager>().state == StateManager.State.CORRIDOR_OPEN) {
            ShiftCurtain shiftCurtain1 = curtain1.GetComponent<ShiftCurtain>();
            shiftCurtain1.open();
            ShiftCurtain shiftCurtain2 = curtain2.GetComponent<ShiftCurtain>();
            shiftCurtain2.open();
        }
        //Debug.Log("Player Z Position ====>>" + player.transform.position.z);
        if(player.transform.position.z <= 12.5f && corridorOver == false) {
            corridorOver = true;
            gameState.GetComponent<StateManager>().state = StateManager.State.MAIN_ROOM_TUTORIAL;
            ShiftCurtain shiftCurtain1 = curtain1.GetComponent<ShiftCurtain>();
            shiftCurtain1.close();
            ShiftCurtain shiftCurtain2 = curtain2.GetComponent<ShiftCurtain>();
            shiftCurtain2.close();
        }
        prevState = gameState.GetComponent<StateManager>().state;
    }
}
