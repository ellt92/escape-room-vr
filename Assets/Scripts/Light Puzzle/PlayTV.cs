using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Video;
public class PlayTV : MonoBehaviour {

    public VideoPlayer videoPlayerNoise;
    public VideoPlayer videoPlayerPurpleRain;
    public bool playNoise = false;
    public bool playPurpleRain = false;
    public GameObject tvScreen;
    public GameObject fakeScreen;
    public GameObject gameState;
    // Use this for initialization
    void Start () {
		
	}

    public bool getCassetteState() {
        return GetComponent<CassettePlacementScript>().isPlaced;
    }
	
	// Update is called once per frame
	void Update () {
		if (!playNoise) {
            if (getCassetteState()) {
                tvScreen.GetComponent<MeshRenderer>().enabled = false;
                fakeScreen.GetComponent<MeshRenderer>().enabled = true;
                videoPlayerNoise.Play();
                playNoise = true;
            }
        }
        if (!playPurpleRain) {
            if (gameState.GetComponent<StateManager>().state == StateManager.State.TV) {
                Destroy(videoPlayerNoise);//.Stop();
                videoPlayerPurpleRain.Play();
                playPurpleRain = true;
            }
        }
        if (playPurpleRain) {
            if (gameState.GetComponent<StateManager>().state == StateManager.State.TRANSITION_TV_SOUND) {
                Destroy(videoPlayerPurpleRain);
                playPurpleRain = false;
            }
        }
	}
}
