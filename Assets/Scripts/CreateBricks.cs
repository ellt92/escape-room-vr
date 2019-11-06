using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBricks : MonoBehaviour {

    public int numBricksZ = 20;
    public int numBricksX = 10;
    public int numBricksY = 10;
    public GameObject brick1;
    public GameObject brick2;
    public GameObject table;
    // Use this for initialization
    void Start () {
        float tX = brick1.transform.localScale.x;
        Transform transform1 = brick1.transform;
        Transform transform2 = brick2.transform;
        Transform transformTable = table.transform;
        for (int i=0; i< numBricksZ; i++) {
            float z0 = transformTable.localPosition.z;
            float z1 =  (0.5f + 2 * i) * transform1.localScale.z* transformTable.localScale.z;
            float z2 = (0.5f + 2 * i + 1) * transform1.localScale.z * transformTable.localScale.z;
            float[] zValues = new float[] { z0 + z1, z0 + z2, z0 - z2, z0 - z1 };
            for (int j=0; j<numBricksX; j++) {
                float x0 = transformTable.localPosition.x;
                float x1 = (0.5f + 2 * j) * transform1.localScale.x * transformTable.localScale.x;
                float x2 = (0.5f + 2 * j + 1) * transform1.localScale.x * transformTable.localScale.x;
                float[] xValues = new float[] { x0 + x1, x0 + x2, x0 - x2, x0 - x1 };
                for (int m =0; m < numBricksY; m++) {
                    float y0 = transformTable.localPosition.y + 0.5f * transformTable.localScale.y + 0.5f * transform1.localScale.y;
                    float y1 = (0.5f + 2 * m) * transform1.localScale.y * transformTable.localScale.y;
                    float y2 = (0.5f + 2 * m + 1) * transform1.localScale.y * transformTable.localScale.y;
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
                                        transform = (m % 2 == 0) ? transform1 : transform2;
                                    }
                                    else {
                                        transform = (m % 2 == 1) ? transform1 : transform2;
                                    }
                                }
                                else {
                                    if (l % 2 == 1) {
                                        transform = (m % 2 == 0) ? transform1 : transform2;
                                    }
                                    else {
                                        transform = (m % 2 == 1) ? transform1 : transform2;
                                    }
                                }
                                Instantiate(transform, new Vector3(xK, yQ, zL), transform.rotation, transformTable);
                            }
                        }
                    }
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
