using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapBackHandlerScript : MonoBehaviour {

    public bool hasAssigned = false;
    Vector3 initialPosition;
    Quaternion initialRotation;

    bool isGrabbed = false;
    bool unGrabbed = false;

    public GameObject leftController;
    public GameObject rightController;

    public float waitTime = 0.2f;

    public bool test = false;

    public AudioSource soundSnapBack;
    // Use this for initialization
    void Start() {

    }

    IEnumerator BackToInitialPosition(float time) {
        yield return new WaitForSeconds(time);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (!rb.isKinematic) {
            rb.isKinematic = true;
        }
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        if (soundSnapBack) {
            soundSnapBack.Play();
        }
        if (test) {
            StartCoroutine("ResetKinematic", time);
        }
    }

    IEnumerator ResetKinematic(float time) {
        yield return new WaitForSeconds(time);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.isKinematic)
            rb.isKinematic = false;
    }

    bool isGrabbedFunc() {
        Transform leftControllerTransform = leftController.transform.parent;
        Transform rightControllerTransform = rightController.transform.parent;
        if (leftControllerTransform) {
            if (this.transform.IsChildOf(leftControllerTransform)) return true;
        }
        if (rightControllerTransform) {
            if (this.transform.IsChildOf(rightControllerTransform)) return true;
        }
        return false;
    }


    // Update is called once per frame
    void Update() {
        if (!hasAssigned) {
            hasAssigned = true;
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }
        if (isGrabbed) {
            if (!isGrabbedFunc()) {
                //just got ungrabbed
                StartCoroutine("BackToInitialPosition", waitTime);
            }
        }
        isGrabbed = isGrabbedFunc();

    }
}
