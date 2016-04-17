using UnityEngine;
using System.Collections;
using UnityEngine.UI;
// pragma_warning.cs
/*using System;

#pragma warning disable 1061
[CLSCompliant(false)]
public class C
{
    int i = 1;
    static void Main()
    {
    }
}
#pragma warning restore 1061
[CLSCompliant(false)]  // CS3021
public class D
{
    int i = 1;
    public static void F()
    {
    }
}*/

public class Shoot : MonoBehaviour{
    public Transform Bullet;
    public Transform BulletSpawn;
    public int BulletSpeed = 100;
    public int BulletCount = 7;
    public int AllBullet = 30;
    public int MaxBulletInHolder = 7;
    public float Timer = 0.0f;
    public Transform Flash;
    public Text BulletUI;
    public Text AllBulletUI;
    public AudioClip ShootSound;
    public AudioClip ReloadSound;
    private float FlashTimer = 0.0f;

    void Start ()
    {
        BulletUI.text = BulletCount.ToString();
        AllBulletUI.text = AllBullet.ToString();
        /*Flash.active = false;*/ // ПОФИСИТЬ!!!!!!!!!!!!!!!!!!!!!!!!

    }

	void Update ()
    {
        if (Input.GetMouseButtonDown(0) & BulletCount > 0 & Timer <= 0) //Стрельба
        {
            GetComponent<AudioSource>().PlayOneShot(ShootSound); //Звук стрельбы
            GetComponent<AudioSource>().PlayOneShot(ReloadSound); //Звук перезарядки

            Transform bull = (Transform) Instantiate(Bullet, BulletSpawn.transform.position, Quaternion.identity);
            bull.GetComponent<Rigidbody>().AddForce(transform.forward * BulletSpeed);
            BulletCount = BulletCount - 1;
            BulletUI.text = BulletCount.ToString();
            /*Flash.active = true; ФИКСИТЬ!!!!!!!!!!!!!!!!!!!!
            FlashTimer = 0.1f;
            if (FlashTimer > 0)
            {
                FlashTimer -= Time.deltaTime; 
            }
            if (FlashTimer <= 0)
            {
                Flash.active = false;
            }*/ //ФИКСИТЬ!!!!!!!!!!!!!!!!!!!!
        }
        if (Input.GetButtonDown("Reload") & AllBullet > 0 & Timer <= 0) //Перезарядка
        {
            Timer = 1.5f;
            AllBullet = AllBullet + BulletCount - MaxBulletInHolder;
            BulletCount = MaxBulletInHolder;
            BulletUI.text = BulletCount.ToString();
            AllBulletUI.text = AllBullet.ToString();
        }
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
	
	}
}
