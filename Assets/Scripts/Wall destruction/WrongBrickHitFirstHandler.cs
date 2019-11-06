using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
//using namespace VRTK.GrabAttachMechanics;


public class WrongBrickHitFirstHandler : MonoBehaviour {

    public GameObject breakableBrick;
    public GameObject sledgeHammer;
    public Transform initialTransform;
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    public bool wasHitFirst = false;

    public AudioSource audio;

    public bool hammerHead = false;
    public bool sledgeHammerCollision = false;

    public float timeStuck;

    // Use this for initialization
    void Start() {
        initialTransform = sledgeHammer.transform;
        initialPosition = sledgeHammer.transform.position;
        initialRotation = initialRotation;



    }

    IEnumerator resetKinematic(float time) {
        yield return new WaitForSeconds(time);
        sledgeHammer.GetComponent<Rigidbody>().isKinematic = false;
    }


    IEnumerator repositionSledgeHammer(float time) {
        yield return new WaitForSeconds(time);

        sledgeHammer.transform.position = initialPosition;
        sledgeHammer.transform.rotation = initialRotation;

        StartCoroutine("resetKinematic", 0.1f);

    }

    /*private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger from wrong brick");
        wasHitFirst = !breakableBrick.GetComponent<handleHealthCollision>().hasBeenHit;
        hammerHead = (other is CapsuleCollider);
        sledgeHammerCollision = other.gameObject.name.Contains("sledge");

        if (sledgeHammerCollision && hammerHead && wasHitFirst) {

            audio.Play();
            Debug.Log("Trigger from wrong brick with hammers head FIRST");

            // Ungrab sledgehammer
            sledgeHammer.GetComponent<VRTK_InteractableObject>().Ungrabbed();
            sledgeHammer.GetComponent<Rigidbody>().isKinematic = true;

            StartCoroutine("repositionSledgeHammer", timeStuck);


        }*/


        private void OnCollisionEnter(Collision collision) {
            wasHitFirst = !breakableBrick.GetComponent<handleHealthCollision>().hasBeenHit;
            hammerHead = (collision.collider is CapsuleCollider);
            sledgeHammerCollision = collision.gameObject.name.Contains("sledge");

            if (sledgeHammerCollision && hammerHead && wasHitFirst) {

                audio.Play();

                // Ungrab sledgehammer
                /*sledgeHammer.GetComponent<VRTK_InteractableObject>().Ungrabbed();
                sledgeHammer.GetComponent<Rigidbody>().isKinematic = true;
                StartCoroutine("repositionSledgeHammer", timeStuck);*/


            }
    }

// Update is called once per frame
void Update() {

    }
}
