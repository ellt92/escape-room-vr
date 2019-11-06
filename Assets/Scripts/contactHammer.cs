using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contactHammer : MonoBehaviour {
    public GameObject table;
    public Transform tableTransform;
	// Use this for initialization
	void Start () {
        //rb = GetComponent<Rigidbody>();
        tableTransform = table.transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision) {
        Debug.Log("COLLISION ");
        foreach (ContactPoint contact in collision.contacts) {
            Debug.Log("contact = " + contact);
            Debug.Log("collision.collider.name = "+ collision.collider.name);
            if (collision.collider.name.StartsWith("Stone")){
                Debug.Log("COLLISION STONE");
                foreach (Transform brick in tableTransform) {
                    Rigidbody rb = brick.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                }
            }
        }
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log("TRIGGER ");
        Debug.Log("trigger.name = " + other.name);
        if (other.name.StartsWith("Stone")) {
            Debug.Log("TRIGGER STONE");
            foreach (Transform brick in tableTransform) {
                Rigidbody rb = brick.GetComponent<Rigidbody>();
                rb.isKinematic = false;
            }
        }
    }
}
