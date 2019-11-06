using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade_canvas : MonoBehaviour {

	public float fadeSpeed = 1f;
	public bool fadeIn;
	public CanvasRenderer text;
	public float delayTime;
	float fadeValue = 0.01f;
	// Use this for initialization
	bool shouldFade = false;

	// Invisible on Awake
	void Awake() {
		text.SetAlpha(0f);
		StartCoroutine(fadeFunc());
		if (!fadeIn) {
			fadeValue = 1.0f;
		}
	}

	IEnumerator fadeFunc()
	{
		yield return new WaitForSeconds(delayTime);
		shouldFade = true;
	}



	// Update is called once per frame
	void Update () {
//		float Fade = 0f;
		if (shouldFade) {
			if (fadeIn) {
				fadeValue = fadeValue * 1.05f;
				if (fadeValue >= 1.0f) {
					shouldFade = false;
				}
			} else if (!fadeIn) {
				fadeValue = fadeValue / 1.05f;
				if (fadeValue <= 0.0f) {
					shouldFade = false;
				}
			}
			text.SetAlpha(fadeValue);
		}
	}
}






