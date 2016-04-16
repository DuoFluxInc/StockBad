using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float Live = 10.0f;
    public float Timer = 0.0f;
    public Transform hole;
    // Use this for initialization
    void OnCollisionEnter (Collision collision) {
        Destroy(gameObject);
        foreach(ContactPoint contact in collision.contacts)
        {
            Instantiate(hole, transform.position, Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
        if (Live < Timer)
        {
            Destroy(gameObject);
        }
	}
}
