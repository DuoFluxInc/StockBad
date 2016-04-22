using UnityEngine;
using System.Collections;

public class infighting : MonoBehaviour {
    private float Timeout;
    public Transform sparks;

	void Update ()
    {
        if (Timeout > 0)
        {
            Timeout -= Time.deltaTime;
        }
	if (Input.GetMouseButton(0) & Timeout <0)
        {
            Timeout = 0.3f;
            Vector3 DirectionRay = transform.TransformDirection(Vector3.forward);
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, DirectionRay, out Hit, 1.2f))
            {
                if (Hit.rigidbody)
                {
                    Hit.rigidbody.AddForceAtPosition(DirectionRay * 4000f, Hit.point);
                }
                if (Hit.collider.CompareTag("Untagged"))
                {
                    Instantiate(sparks, Hit.point, Quaternion.identity);
                }
            }
        }
	}
}
