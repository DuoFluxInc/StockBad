using UnityEngine;
using System.Collections;

public class ChangeWeapon : MonoBehaviour {
    public GameObject Gun1;
    public GameObject Gun2;
    //public GameObject Gun3;
    private int NumberWeapon;

    void Start ()
    {
	
	}
	
	void Update ()
    {
	    if(Input.GetAxis("Mouse ScrollWheel") <0)
        {
            NumberWeapon -= 1;
            Switch();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            NumberWeapon += 1;
            Switch();
        }

        if(NumberWeapon > 2)
        {
            NumberWeapon = 1;
            Switch();
        }
        if (NumberWeapon < 1)
        {
            NumberWeapon = 2;
            Switch();
        }
        if (Input.GetKeyDown("1"))
        {
            NumberWeapon = 1;
            Switch();
        }
        if (Input.GetKeyDown("2"))
        {
            NumberWeapon = 2;
            Switch();
        }
        /*if (Input.GetKeyDown("3"))
        {
            NumberWeapon = 3;
            Switch();
        }*/

    }
    void Null()
    {
        Gun1.SetActive(false);
        Gun2.SetActive(false);
        //Gun3.SetActive(false);
    }
    void Switch()
    {

        if (NumberWeapon == 1)
        {
            Null();
            Gun1.SetActive(true);
        }
        if (NumberWeapon == 2)
        {
            Null();
            Gun2.SetActive(true);
        }
        /*if (NumberWeapon == 3)
        {
            Null();
            Gun3.SetActive(true);
        }*/
    }
}
