using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeFlyingExp : MonoBehaviour {

    public float pace = 0.2f;
    public bool fly = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F)) fly = true;
        if (fly)
        {
            float deltaY = pace * Time.deltaTime;
            this.transform.Translate(new Vector3(0, deltaY, 0));
        }

    }
}
