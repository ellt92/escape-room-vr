using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassettePlacementScript : MonoBehaviour {

    public GameObject gameStateObject;
    public float minDistanceY = 0.05f;
    public float minDistanceX = 0.05f;
    public float minDistanceZ = 0.15f;
    public float minDistanceZNegative = 0.1f;
    public GameObject cassette;
    public GameObject placedCassette;
    public GameObject referencePoint;
    public GameObject indication;
    public GameObject referenceMeshObject;

    public Vector3 cassettePosition;
    public Vector3 cassetteReferencePosition;

    bool readyToDrop;
    bool isDropped;

    public float angleA;
    public float angleB;
    public float angleC;
    public float maxAngle = 5f;

    public bool isPlaced = false;
    public bool needsCorrectOrientation = true;
    // Use this for initialization
    void Start() {
        readyToDrop = false;
        isDropped = false;
        cassetteReferencePosition = placedCassette.transform.position;
    }

    bool checkAxesConsistency() {
        if (!needsCorrectOrientation) return true;
        angleA = Mathf.Abs(Vector3.Angle(cassette.transform.forward, placedCassette.transform.forward));
        angleB = Mathf.Abs(Vector3.Angle(cassette.transform.up, placedCassette.transform.up));
        angleC = Mathf.Abs(Vector3.Angle(cassette.transform.right, placedCassette.transform.right));
        float angleABis = Mathf.Abs(Mathf.Abs(180f) - Mathf.Abs(angleA));
        float angleBBis = Mathf.Abs(Mathf.Abs(180f) - Mathf.Abs(angleB));
        float angleCBis = Mathf.Abs(Mathf.Abs(180f) - Mathf.Abs(angleC));
        bool test1 = (angleA < maxAngle || angleABis < maxAngle);
        bool test2 = (angleB < maxAngle || angleBBis < maxAngle);
        bool test3 = (angleC < maxAngle || angleCBis < maxAngle);

        return (test1 && test2 && test3);
    }

    void renderCorrectPlacement() {
        Renderer correctMesh = indication.GetComponent<Renderer>();
        correctMesh.material.color = Color.green;
    }

    void renderIncorrectPlacement() {
        Renderer incorrectMesh = indication.GetComponent<Renderer>();
        incorrectMesh.material.color = Color.black;
    }

    public bool getDroppingAction() { //replace this by testing if user drops (VRTK)
        bool droppedTest = !cassette.GetComponent<Rigidbody>().isKinematic;
        return (Input.GetKey(KeyCode.UpArrow) || droppedTest);
    }

    public void placeCassette() {
        referenceMeshObject.GetComponent<MeshRenderer>().enabled = true;
        Destroy(cassette);
        isPlaced = true;
        if (gameStateObject.GetComponent<StateManager>().state == StateManager.State.CORRIDOR) {
            gameStateObject.GetComponent<StateManager>().state = StateManager.State.CORRIDOR_OPEN;
        }
        else if (gameStateObject.GetComponent<StateManager>().state == StateManager.State.WALL) {
            gameStateObject.GetComponent<StateManager>().state = StateManager.State.TRANSITION_WALL_TV;
        }
        //cassette.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update() {

        if (!isPlaced) {
            cassettePosition = cassette.transform.position;
            float depthDistance = cassette.transform.position.z - referencePoint.transform.position.z;
            float yDistance = Mathf.Abs(cassette.transform.position.y - referencePoint.transform.position.y);
            float xDistance = Mathf.Abs(cassette.transform.position.x - referencePoint.transform.position.x);
            bool angleTest = checkAxesConsistency();
            readyToDrop = (depthDistance < minDistanceZ && depthDistance > -minDistanceZNegative && yDistance < minDistanceY && xDistance < minDistanceX && angleTest);

            //Debug.Log(yDistance);
            //Debug.Log(xDistance);
            if (Input.GetKeyDown(KeyCode.L)) placeCassette();
            if (readyToDrop) {
                renderCorrectPlacement();
                bool dropping = getDroppingAction();
                if (dropping) {
                    //Debug.Log("DROPPED ");
                    placeCassette();
                    renderIncorrectPlacement();
                }
            }
            else {
                renderIncorrectPlacement();
            }
        }
    }
}
