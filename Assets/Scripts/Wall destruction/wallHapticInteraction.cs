using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class wallHapticInteraction : MonoBehaviour {

    public GameObject rightController;
    public GameObject leftController;
    protected VRTK_ControllerReference controllerReferenceLeft {
        get {
            return VRTK_ControllerReference.GetControllerReference(leftController);
        }
    }
    protected VRTK_ControllerReference controllerReferenceRight {
        get {
            return VRTK_ControllerReference.GetControllerReference(rightController);
        }
    }
    public GameObject referencePosition;
    public Vector3 targetPosition;
    public Vector2 target2D;
    public float depthMin = 0.2f;
    public float minInterval = 0.05f;
    public float distance;
    public bool isVibratingLeft = false;
    public bool isVibratingRight = false;
    public float strength;
    public float wallDepth;
    public float depthDistanceLeft;
    public float depthDistanceRight;

    public GameObject sledgehammer;
    public GameObject detector;

    // Use this for initialization
    void Start () {
        targetPosition = referencePosition.transform.position;
        wallDepth = targetPosition.x;
        target2D = new Vector2(targetPosition.y, targetPosition.z);
    }

    protected virtual void TriggerHapticPulse(VRTK_ControllerReference controllerReference, float strength, float duration, float interval) {
        VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, strength, duration, (interval >= minInterval ? interval : minInterval));
    }

    // Update is called once per frame
    void Update () {
        if (leftController) {
            Transform controllerParent = leftController.transform.parent;
            if (controllerParent) {
                bool testDetector = detector.transform.IsChildOf(controllerParent);
                if (!testDetector) {
                    //Debug.Log("Detector not in left tcontroller");
                }
                else {
                    Vector3 controllerPositionLeft = leftController.transform.position;
                    Debug.Log("LEFT =====" + controllerPositionLeft);

                    depthDistanceLeft = Mathf.Abs(detector.transform.position.x - wallDepth);
                    if (depthDistanceLeft < depthMin) {
                        Vector2 controller2D = new Vector2(detector.transform.position.y, detector.transform.position.z);
                        distance = (controller2D - target2D).magnitude;
                        strength = Mathf.Exp(-2 * distance);
                        isVibratingLeft = true;
                        TriggerHapticPulse(controllerReferenceLeft, strength, 0.02f, 0.05f);
                    }
                    else
                        isVibratingLeft = false;
                }
            }
        }
        if (rightController) {
            Transform controllerParent = rightController.transform.parent;
            if (controllerParent) {
                bool testDetector = detector.transform.IsChildOf(controllerParent);
                if (!testDetector) {
                    //Debug.Log("Detector not in right controller");
                }
                else {
                    Vector3 controllerPositionRight = rightController.transform.position;
                    Debug.Log("RIGHT =====" + controllerPositionRight);
                    depthDistanceRight = Mathf.Abs(detector.transform.position.x - wallDepth);
                    if (depthDistanceRight < depthMin) {
                        Vector2 controller2D = new Vector2(detector.transform.position.y, detector.transform.position.z);
                        distance = (controller2D - target2D).magnitude;
                        strength = Mathf.Exp(-2 * distance);
                        isVibratingRight = true;
                        TriggerHapticPulse(controllerReferenceRight, strength, 0.02f, 0.05f);
                    }
                }
            }
        }
        
	}
}
