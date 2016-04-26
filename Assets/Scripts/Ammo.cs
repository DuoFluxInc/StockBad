using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {
    public Shoot teat;
    public Shoot2 teat2;
    void Update ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (Input.GetButtonDown("Use")& Vector3.Distance(transform.position, player.transform.position)<2)
        {
            teat.AllBullet += teat.BulletCount;
            Destroy(gameObject);
        }
        if (Input.GetButtonDown("Use") & Vector3.Distance(transform.position, player.transform.position) < 2)
        {
            teat2.AllBullet += teat2.BulletCount;
            Destroy(gameObject);
        }
    }
}
