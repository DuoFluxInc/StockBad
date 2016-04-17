using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float Live = 5.0f;
    public float Timer = 0.0f;
    public Transform hole;
    public Transform holeEffect;

    void OnCollisionEnter (Collision collision)
    {
        Destroy(gameObject);
        foreach(ContactPoint contact in collision.contacts)
        {
            Instantiate(hole, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        foreach (ContactPoint contact in collision.contacts)
        {
            Instantiate(holeEffect, transform.position, Quaternion.identity);
        }
    }
	
	void Update () {
        Timer += Time.deltaTime;
        if (Live < Timer)
        {
            Destroy(gameObject);
        }
	}
}
