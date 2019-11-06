using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftCurtainNew : MonoBehaviour
{
    public GameObject curtain;
    public Vector3 positionChange;
    public Vector3 scaleChange;
    public float interpTime;
    // Use this for initialization
    Vector3 curtainPositionClosed;
    Vector3 curtainPositionOpen;
    Vector3 curtainScaleClosed;
    Vector3 curtainScaleOpen;
    float positionJourneyDistance;
    float scaleJourneyDistance;
    enum transitionType
    {
        NONE,
        OPENING,
        CLOSING
    }
    ShiftCurtainNew.transitionType transitionState = transitionType.NONE;


    // Time when the movement started.
    private float startTime;

    void Start() {
        curtainPositionClosed = curtain.transform.position;
        curtainPositionOpen = curtain.transform.position + positionChange;
        curtainScaleClosed = curtain.transform.localScale;
        curtainScaleOpen = Vector3.Scale(curtain.transform.localScale, scaleChange);
        positionJourneyDistance = Vector3.Distance(curtainPositionClosed, curtainPositionOpen);
        scaleJourneyDistance = Vector3.Distance(curtainScaleClosed, curtainScaleOpen);
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("f")) {
            transitionState = transitionType.OPENING;
            startTime = Time.time;
        }
        if (Input.GetKeyDown("g")) {
            transitionState = transitionType.CLOSING;
            startTime = Time.time;
        }
        if (transitionState == transitionType.OPENING) {
            float fracDistanceCovered = (Time.time - startTime) / interpTime;

            Debug.Log(Time.time - startTime);
            curtain.transform.position = Vector3.Lerp(curtainPositionClosed, curtainPositionOpen, fracDistanceCovered);
            curtain.transform.localScale = Vector3.Lerp(curtainScaleClosed, curtainScaleOpen, fracDistanceCovered);

            if (fracDistanceCovered >= 1.0f) {
                transitionState = transitionType.NONE;
            }
        }
        else if (transitionState == transitionType.CLOSING) {
            float fracDistanceCovered = (Time.time - startTime) / interpTime;

            Debug.Log(fracDistanceCovered);

            curtain.transform.position = Vector3.Lerp(curtainPositionOpen, curtainPositionClosed, fracDistanceCovered);
            curtain.transform.localScale = Vector3.Lerp(curtainScaleOpen, curtainScaleClosed, fracDistanceCovered);

            if (fracDistanceCovered >= 1.0f) {
                transitionState = transitionType.NONE;
            }
        }
    }
}
