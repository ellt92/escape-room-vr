using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjectScript : MonoBehaviour {

    public GameObject Crack;
    public SpriteRenderer mainCrack;
    public GameObject Spark;
    public GameObject hitObject;
    Color hitObjColor;
    public Material hitObjMaterial;
    float outside = 0.001f;
    public float waitFade = 3f;
	// Use this for initialization
	void Start () {
 
    }

    void addCrack(Vector3 position) {
        float xValue = hitObject.transform.position.x -(hitObject.transform.lossyScale.x/2f + outside);
        Debug.Log("xValue = " + xValue);
        Vector3 exactPosition = new Vector3(xValue, position.y, position.z);
        GameObject crack = Instantiate(Crack, exactPosition,  Quaternion.Euler(0f, 90f, 0f), hitObject.transform);
        GameObject spark = Instantiate(Spark, exactPosition, Quaternion.Euler(0f, 90f, 0f), hitObject.transform);
    }

    void updateMainCrack(float health) {
        mainCrack.color = new Color(0f, 0f, 0f, (100f - health) / 100f);
    }

    IEnumerator fadeTinyStones(float time) {
        yield return new WaitForSeconds(time);
        foreach (Transform child in hitObject.transform)
        {
            GameObject childObj = child.gameObject;
            if (childObj.name.Contains("Tiny")) {
                time = Random.value * 3f;
                StartCoroutine(destroyStone(childObj, time));
                //Renderer rend = childObj.GetComponent<Renderer>();
                //Color stoneColor = rend.material.color;
                //rend.material.SetColor("_Color", Color.Lerp(stoneColor, Color.red, 5f));
                //yield return null;
            }
        }
    }

    IEnumerator destroyStone(GameObject stone, float time) {
        yield return new WaitForSeconds(time);
        Destroy(stone);
    }

    public void objectHit(Vector3 position, float health) {
        addCrack(position);
        updateMainCrack(health);
        if (health <= 0) {
            mainCrack.color = new Color(0f, 0f, 0f, 0f);
            //Destroy(Crack);
            foreach (Transform child in hitObject.transform) {
                if (child.name.StartsWith("Crack") || child.name.StartsWith("Spark")) {
                    Destroy(child.gameObject);
                }
            }
            StartCoroutine("fadeTinyStones", waitFade);
        }
    }
}
