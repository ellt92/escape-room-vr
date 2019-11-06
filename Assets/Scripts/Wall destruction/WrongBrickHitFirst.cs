using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongBrickHitFirstAsset : MonoBehaviour {
    public GameObject breakableBrick;
    public GameObject sledgeHammer;
    public Transform initialTransform;

	// Use this for initialization
	void Start () {
        initialTransform = sledgeHammer.transform;

    }

    private void OnCollisionEnter(Collision collision) {
        bool wasHitFirst = !breakableBrick.GetComponent<handleHealthCollision>().hasBeenHit;
        bool hammerHead = (collision.collider is CapsuleCollider);
        
        if (collision.gameObject.name.Contains("sledge") && hammerHead && wasHitFirst) {
            sledgeHammer.transform.position = initialTransform.position;

            sledgeHammer.transform.rotation = initialTransform.rotation;


        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
