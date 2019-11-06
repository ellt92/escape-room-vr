using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleDestructionFromKeys : MonoBehaviour {

	public GameObject brick;
    public GameObject invisibleSupportBrick;
	public GameObject key;
	public Transform transform;
	public bool expload = false;
	public GameObject explosion;
	public float timeToWait = 0.5f;
	public float timeOriginalCollider = 0.7f;
    public bool triggerExplosion = false;
    public float health = 100f;
	// Use this for initialization
	void Start () {
		transform = brick.transform;
	}

	IEnumerator renderDestruction(float time){
		yield return new WaitForSeconds(time);
		Debug.Log("UP");
		brick.GetComponent<Renderer>().enabled = false;
        Destroy(invisibleSupportBrick);
        brick.GetComponent<Collider>().enabled = false;
        foreach (Transform child in transform){
			GameObject childObj = child.gameObject;
			childObj.SetActive (true);
			if(childObj.name.Contains("Tiny")) childObj.GetComponent<Renderer>().enabled = true;
			if (expload && childObj.name.Contains("Tiny")) childObj.GetComponent<Rigidbody>().isKinematic = false;
			//key.GetComponent<Renderer>().enabled = true;
		}
	}

	IEnumerator resetColliders(float time){
		yield return new WaitForSeconds(time);
		brick.GetComponent<Renderer>().enabled = false;
		foreach (Transform child in transform){
			GameObject childObj = child.gameObject;
			if (expload && childObj.name.Contains("Tiny")) childObj.GetComponent<BoxCollider>().size = new Vector3(1f,1f,1f);
		}
	}

    /*private void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision");
        if (collision.gameObject.name.Contains("sledge")) {
            Debug.Log("Collision with hammer");
            triggerExplosion = true;
        }
    }*/
    // Update is called once per frame
    void Update () {
		if (Input.GetKey("up") || triggerExplosion){
            /*Debug.Log("UP");
			brick.GetComponent<Renderer>().enabled = false;
			foreach (Transform child in transform){
				brick.GetComponent<Collider> ().enabled = false;
				GameObject childObj = child.gameObject;
				if(!childObj.name.Contains("xplo")) childObj.GetComponent<Renderer>().enabled = true;
				explosion.SetActive (true);
				if (expload && childObj.name.Contains("Tiny")) childObj.GetComponent<Rigidbody>().isKinematic = false;
				key.GetComponent<Renderer>().enabled = true;
			}*/

            Rigidbody rb = GetComponent<Rigidbody>();
            Destroy(rb);
            StartCoroutine("renderDestruction", timeToWait);
			StartCoroutine("resetColliders", timeOriginalCollider);
			if (explosion) explosion.SetActive (true);
		}
	}

}
