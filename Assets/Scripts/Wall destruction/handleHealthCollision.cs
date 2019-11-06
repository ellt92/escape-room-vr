using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleHealthCollision : MonoBehaviour {

    public float health = 100f;
    public Vector3 positionImpact;
    public bool hasBeenHit = false;

    public AudioClip hit;
    public AudioClip destroy;

    public float impactMagnitude = 0f;
    public float impulse = 0f;
    public float timeLastImpact = 0f;
    public float minDelayTwoImpacts = 0.5f;

    public float b = 400f;
    public float a = 50f;

    public float volumeMultiplier = 4f;
    public AudioClip destroySound;
	// Use this for initialization
	void Start () {
        positionImpact = new Vector3(0f, 0f, 0f);
    }

    Vector3 computeAverageImpact(ContactPoint[] contacts) {
        int n = contacts.Length;
        return new Vector3(0f, 0f, 0f);
    }

    float getHealthDecreaseFromImpulse(float impulse) {
        float n = 4f;
        return (100 * Mathf.Abs(impulse - a) / (n*b));
    }

    private void OnCollisionEnter(Collision collision) {
        float timeSinceLastImpact = Time.time - timeLastImpact;
        bool hammerHead = (collision.collider is CapsuleCollider);
        if (collision.gameObject.name.Contains("sledge") && timeSinceLastImpact > minDelayTwoImpacts && hammerHead) {
            if (!hasBeenHit) hasBeenHit = true;
            Debug.Log("Collision with hammers head");
            impactMagnitude = 100f * collision.relativeVelocity.magnitude;
            Debug.Log("impactVelocityMagnitude = " + impactMagnitude);
            impulse = 10f * collision.impulse.magnitude;
            Debug.Log("impactImpulse = " + impulse); // values between 50ish for weak, goes to 400ish for very powerful
            timeLastImpact = Time.time;

            positionImpact = collision.contacts[0].point;
            // Handle texture here
            float diffHealth = getHealthDecreaseFromImpulse(impulse);
            AudioSource audioSource = GetComponent<AudioSource>();
            float soundVolume = volumeMultiplier * diffHealth / 100f;
            audioSource.volume = soundVolume;

            health -= diffHealth;
            HitObjectScript hOScript = GetComponent<HitObjectScript>();
            hOScript.objectHit(positionImpact, health);

            if (health <= 0) {
                audioSource.clip = destroySound;
                GetComponent<handleDestructionFromKeys>().triggerExplosion = true;
            }
            audioSource.Play();

        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
