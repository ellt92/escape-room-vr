using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deactivateStart : MonoBehaviour {


	// Use this for initialization
	void Start () {
        GameObject[] appearing = GameObject.FindGameObjectsWithTag("AppearA");
        foreach (GameObject appearObj in appearing) {
            appearObj.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
