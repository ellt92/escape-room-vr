using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStones : MonoBehaviour {

    public int numBricksZ = 2;
    public int numBricksX = 2;
    public int numBricksY = 2;
    public GameObject stone1;
    public GameObject stone2;
    public GameObject brick;
    public Color originalColor;
    public float range = 0.1f;


    // Use this for initialization
    void Start () {

        originalColor = new Color(1.2f * originalColor.r, 1.2f * originalColor.g, 1.2f * originalColor.b);

        Transform transform1 = stone1.transform;
        Transform transform2 = stone2.transform;
        Transform transformBrick = brick.transform;
		float scaleX = 1.0f / (4* numBricksX);
		float scaleY = 1.0f / (4* numBricksY);
		float scaleZ = 1.0f / (4* numBricksZ);
		transform1.localScale = new Vector3 (scaleX, scaleY, scaleZ);
		transform2.localScale = new Vector3 (scaleX, scaleY, scaleZ);
        for (int i=0; i< numBricksZ; i++) {
			float z0 = transformBrick.position.z;
			/*float z1 = (0.5f + 2 * i) * transform1.localScale.z * transformBrick.localScale.z;
			float z2 = (0.5f + 2 * i + 1) * transform1.localScale.z * transformBrick.localScale.z;*/
            float z1 = (0.5f + 2 * i) * transform1.localScale.z * transformBrick.lossyScale.z;
            float z2 = (0.5f + 2 * i + 1) * transform1.localScale.z * transformBrick.lossyScale.z;

            float[] zValues = new float[] { z0 + z1, z0 + z2, z0 - z2, z0 - z1 };
            for (int j=0; j<numBricksX; j++) {
				float x0 = transformBrick.position.x;
                /*float x1 = (0.5f + 2 * j) * transform1.localScale.x * transformBrick.localScale.x;
				float x2 = (0.5f + 2 * j + 1) * transform1.localScale.x * transformBrick.localScale.x;*/
                float x1 = (0.5f + 2 * j) * transform1.localScale.x * transformBrick.lossyScale.x;
                float x2 = (0.5f + 2 * j + 1) * transform1.localScale.x * transformBrick.lossyScale.x;
                float[] xValues = new float[] { x0 + x1, x0 + x2, x0 - x2, x0 - x1 };
                for (int m =0; m < numBricksY; m++) {
					float y0 = transformBrick.position.y;
                    /*float y1 = (0.5f + 2 * m) * transform1.localScale.y * transformBrick.localScale.y;
					float y2 = (0.5f + 2 * m + 1) * transform1.localScale.y  * transformBrick.localScale.y;*/
                    float y1 = (0.5f + 2 * m) * transform1.localScale.y * transformBrick.lossyScale.y;
                    float y2 = (0.5f + 2 * m + 1) * transform1.localScale.y * transformBrick.lossyScale.y;
                    float[] yValues = new float[] { y0 + y1, y0 + y2, y0 - y2, y0 - y1 };
                    for (int k = 0; k < 4; k++) {
                        float xK = xValues[k];
                        for (int l = 0; l < 4; l++) {
                            float zL = zValues[l];
                            for (int q=0; q<4; q++) {
                                float yQ = yValues[q];
                                Transform transform;
                                if (k % 2 == 0) {
                                    if (l % 2 == 0) {
                                        transform = (m % 2 == 1) ? transform1 : transform2;
                                    }
                                    else {
                                        transform = (m % 2 == 0) ? transform1 : transform2;
                                    }
                                }
                                else {
                                    if (l % 2 == 1) {
                                        transform = (m % 2 == 1) ? transform1 : transform2;
                                    }
                                    else {
                                        transform = (m % 2 == 0) ? transform1 : transform2;
                                    }
                                }
								Transform instantiated = Instantiate(transform, new Vector3(xK, yQ, zL), transform.rotation, transformBrick);
								GameObject instantiatedGO = instantiated.gameObject;
								instantiated.gameObject.SetActive (false);
                                Color color = computeRandomColor();
                                Renderer renderer = instantiatedGO.GetComponent<Renderer>();
                                renderer.material.SetColor("_Color", color);
								renderer.enabled = false;
                            }
                        }
                    }
                }
            }
        }

    }
    

    public Color computeRandomColor() {
        float x = Random.Range(-range, range);
        float r = originalColor.r + x;
        float g = originalColor.g + x;
        float b = originalColor.b + x;
        return new Color(r, g, b);
    }

    // Update is called once per frame
    void Update () {
		
	}


}
