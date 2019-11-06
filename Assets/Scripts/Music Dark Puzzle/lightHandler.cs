using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightHandler : MonoBehaviour {

    public GameObject character;
    public GameObject audioSource;
    public float distance;
    public Light ambientLight;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        distance = (character.transform.position - audioSource.transform.position).magnitude;
        float lightIntensity = 5 * Mathf.Exp(-distance );
        ambientLight.intensity = lightIntensity;
	}
}
