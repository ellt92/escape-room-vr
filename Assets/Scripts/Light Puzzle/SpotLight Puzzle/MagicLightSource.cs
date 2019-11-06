using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MagicLightSource : MonoBehaviour {

    public Material I1;
    public Material C1;
    public Material A1;
    public Material R1;
    public Material U1;
    public Material S1;

    public GameObject spriteI;
    public GameObject spriteC;
    public GameObject spriteA;
    public GameObject spriteR;
    public GameObject spriteU;
    public GameObject spriteS;

    public Light light;

    public GameObject blueFlashlight;
    public GameObject redFlashlight;

    public GameObject cube;

    public AudioSource letterRevealed;

    public bool success = false;
    public AudioSource successSound;

    public GameObject gameState;

    // Use this for initialization
    void Start () {
		
	}

    bool checkCombination() {
        //return (redFlashlight.GetComponent<Rigidbody>().isKinematic && blueFlashlight.GetComponent<Rigidbody>().isKinematic);
        return (cube.GetComponent<rayCastScript>().bothHit() && cube.GetComponent<rayCastScript>().sameLetterHit());
    }
    
    bool checkForDeltaTime() {
        return cube.GetComponent<rayCastScript>().successForDeltaTime();
    }

    void renderOnCube(Material mat) {
        Color brightColor = new Color(1f, 1f, 1f);
        mat.SetColor("_Color", brightColor);
        mat.SetVector("_LightPosition", light.transform.position);
        mat.SetVector("_LightDirection", -light.transform.forward);
        mat.SetFloat("_LightAngle", light.spotAngle);
    }

    void letterAppears(GameObject go) {
        if (!go.active) {
            go.SetActive(true);
            letterRevealed.Play();
        }
    }

    void checkForSuccess() {
        if (!success) {
            success = (spriteI.active && spriteC.active && spriteA.active && spriteR.active && spriteU.active && spriteS.active);
            if (success) {
                if (!successSound.isPlaying) successSound.Play();
                if(gameState.GetComponent<StateManager>().state != StateManager.State.TRANSITION_TV_SOUND) {
                    gameState.GetComponent<StateManager>().state = StateManager.State.TRANSITION_TV_SOUND;
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        checkForSuccess();
        if (checkCombination()) {
            Material mat;
            GameObject game;

            string hitName = cube.GetComponent<rayCastScript>().getLetterHitName();

            switch (hitName) {
                case "HitI":
                    mat = I1;
                    game = spriteI;
                    break;
                case "HitC":
                    mat = C1;
                    game = spriteC;
                    break;
                case "HitA":
                    mat = A1;
                    game = spriteA;
                    break;
                case "HitR":
                    mat = R1;
                    game = spriteR;
                    break;
                case "HitU":
                    mat = U1;
                    game = spriteU;
                    break;
                case "HitS":
                    mat = S1;
                    game = spriteS;
                    break;
                default:
                    mat = S1;
                    game = spriteS;
                    break;
            }

            renderOnCube(mat);
            if (checkForDeltaTime()) {
                letterAppears(game);
            }
        }
        else {
            Color darkColor = new Color(0f, 0f, 0f);
            I1.SetColor("_Color", darkColor);
            C1.SetColor("_Color", darkColor);
            A1.SetColor("_Color", darkColor);
            R1.SetColor("_Color", darkColor);
            U1.SetColor("_Color", darkColor);
            S1.SetColor("_Color", darkColor);
        }
    }
}
