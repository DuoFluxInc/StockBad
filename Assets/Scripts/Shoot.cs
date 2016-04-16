using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shoot : MonoBehaviour{
    public Transform Bullet;
    public Transform Pos;
    public int BulletSpeed = 100;
    public int BulletCount = 7;
    public int AllBullet = 30;
    public int MaxBulletInHolder = 7;
    public Text BulletUI;
    public Text AllBulletUI;
    public AudioClip ShootSound;

	// Use this for initialization
	void Start ()
    {
        BulletUI.text = BulletCount.ToString ();
        AllBulletUI.text = AllBullet.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown (0)& BulletCount > 0)
        {
            GetComponent<AudioSource>().PlayOneShot(ShootSound);
            Transform bull = (Transform) Instantiate(Bullet, Pos.transform.position, Quaternion.identity);
            bull.GetComponent<Rigidbody>().AddForce(transform.forward * BulletSpeed);
            BulletCount = BulletCount - 1;
            BulletUI.text = BulletCount.ToString();
        }
        if (Input.GetButtonDown("Reload") && AllBullet > 0)
        {
            AllBullet = AllBullet + BulletCount - MaxBulletInHolder;
            BulletCount = MaxBulletInHolder;
            BulletUI.text = BulletCount.ToString();
            AllBulletUI.text = AllBullet.ToString();
        }
	
	}
}
