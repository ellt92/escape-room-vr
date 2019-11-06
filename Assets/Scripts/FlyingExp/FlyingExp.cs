using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class FlyingExp : MonoBehaviour {
    public float mass = 70;
    public GameObject fakeWingLeft;
    public GameObject fakeWingRight;
    public Vector3 dragForce;
    public bool simulate = false;

    public Vector3 acceleration;
    public Vector3 velocity;
    public float fallDown = 0.0001f;
    public float originalY;
    float gravity = 9.8f;

    public float controlGravity1 = 10f;
    public float controlGravity2 = 0.5f;

    public GameObject wingA;
    public GameObject wingB;

    public bool success = false;
    public float successHeight = 200f;
    public float endPace = 1f;
    public AudioSource successAudio;
    public AudioSource wingAudioSourceA;
    public AudioSource wingAudioSourceB;
    

    public bool hapticFeedback = true;
    public float hapticIntensityMultiplier;
    public float strengthHaptic;
    public GameObject rightController;
    public GameObject leftController;

    public float timeWaitCredits = 5f;
    public GameObject canvasCredit;
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
    // Use this for initialization
    void Start () {
        velocity = new Vector3(0f, 0f, 0f);
        originalY = this.transform.position.y;
	}

    bool checkBothWingsHeld() {
        if(leftController.transform.parent && rightController.transform.parent) {
            bool testA = wingA.transform.IsChildOf(leftController.transform.parent) || wingA.transform.IsChildOf(rightController.transform.parent);
            bool testB = wingB.transform.IsChildOf(leftController.transform.parent) || wingB.transform.IsChildOf(rightController.transform.parent);
            return testA && testB;
        }
        return false;
    }

    protected virtual void TriggerHapticPulse(VRTK_ControllerReference controllerReference, float strength, float duration, float interval) {
        float minInterval = 0.05f;
        VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, strength, duration, (interval >= minInterval ? interval : minInterval));
    }
    
    IEnumerator PlayCredits(float time) {
        yield return new WaitForSeconds(time);
        canvasCredit.SetActive(true);
    }
    // Update is called once per frame
    void Update () {
        /*
        if (!wingAudioSourceA.isPlaying) {
            wingAudioSourceA.Play();
            wingAudioSourceB.Play();
        }
        wingAudioSourceA.volume = 0f;
        wingAudioSourceB.volume = 0f;*/
        if (Input.GetKeyDown(KeyCode.S)) simulate = true;
        if (!success)
        {
            if (checkBothWingsHeld()) {
                Vector3 dragForceLeft = fakeWingLeft.GetComponent<GetDragForce>().dragForce;
                Vector3 dragForceRight = fakeWingRight.GetComponent<GetDragForce>().dragForce;
                dragForce = dragForceLeft + dragForceRight;
                float deltaT = Time.deltaTime;
                acceleration = new Vector3(0f, 0f, 0f);
                if (dragForce.y > 0) {
                    acceleration = dragForce;
                    if (hapticFeedback) {
                        strengthHaptic = 2 * Mathf.Exp(-hapticIntensityMultiplier / dragForce.y);
                        TriggerHapticPulse(controllerReferenceLeft, strengthHaptic, 0.02f, 0.05f);
                        TriggerHapticPulse(controllerReferenceRight, strengthHaptic, 0.02f, 0.05f);
                        wingAudioSourceA.volume = 5f * strengthHaptic;
                        wingAudioSourceB.volume = 5f * strengthHaptic;
                        if (!wingAudioSourceA.isPlaying) {
                            wingAudioSourceA.Play();
                            wingAudioSourceB.Play();
                        }
                    }
                }
                acceleration -= new Vector3(0, mass * gravity, 0);
                acceleration = acceleration / mass;
                velocity += acceleration * deltaT;
                if (velocity.y < 0) {
                    velocity = velocity * (1f - controlGravity2 * Mathf.Exp(-Mathf.Pow(this.transform.position.y - originalY, 1) / controlGravity1));
                }
                Vector3 deltaPosition = velocity * deltaT;
                float deltaY = deltaPosition.y;
                if (this.transform.position.y + deltaY > originalY) {
                    this.transform.Translate(new Vector3(0, deltaY, 0));
                    //this.transform.Translate(deltaPosition);
                }
            } 
        }
        if(!success && this.transform.position.y > successHeight) {
            success = true;
            endPace = velocity.y;
            successAudio.Play();
            StartCoroutine("PlayCredits", timeWaitCredits);
        }
        if (success) {
            float deltaT = Time.deltaTime;
            float deltaY = endPace * deltaT;
            this.transform.Translate(new Vector3(0, deltaY, 0));
        }
    }
}
