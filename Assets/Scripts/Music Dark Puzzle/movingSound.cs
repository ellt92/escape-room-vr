using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingSound : MonoBehaviour {
    public GameObject user;

    float timeLastChange = 0f;
    public float timeSwitchMode = 10f;

    public GameObject limitTracking;

    public AudioSource changePositionAudio;

    public float margin = 0.5f;

    // Use this for initialization
    void Start () {
        timeLastChange = Time.time;
        changePositionOpposite();
    }

    Vector2 newSoundPosition(Vector3 trackingCenter, Vector2 dir, float scaleX, float scaleZ) {
        Vector2 trackingCenterTwoD = new Vector2(trackingCenter.x, trackingCenter.z);
        return trackingCenterTwoD + scaleX * new Vector2(dir.x, 0f) + scaleZ * new Vector2(0f, dir.y) - margin * dir;
    }
	
    void changePositionOpposite() {
        Vector3 userPosition = user.transform.position;
        Vector3 trackingCenter = limitTracking.transform.position;
        Vector2 dir = (new Vector2(trackingCenter.x - user.transform.position.x, trackingCenter.z - user.transform.position.z)).normalized;
        float scaleX = limitTracking.transform.lossyScale.x/2f;
        float scaleZ = limitTracking.transform.lossyScale.z/2f;
        Vector2 newPos = newSoundPosition(trackingCenter, dir, scaleX, scaleZ);
        this.transform.position = new Vector3(newPos.x, this.transform.position.y, newPos.y);
    }
    // Update is called once per frame
    void Update () {
        if (!GetComponent<AudioSource>().isPlaying) {   //called when object is enabled during transition
            GetComponent<AudioSource>().Play();
            timeLastChange = Time.time;
            changePositionOpposite();
        }
        float timeSinceLastChange = Time.time - timeLastChange;
        if (timeSinceLastChange > timeSwitchMode) {
            timeLastChange = Time.time;
            changePositionOpposite();
            changePositionAudio.Play();
        }
	}
}
