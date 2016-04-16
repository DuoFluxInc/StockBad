using UnityEngine;
using System.Collections;

public class AutoPlayAnimate : MonoBehaviour
{ 
     public void Start()
    {
        GetComponent<Animator>().Play("idle");
    } 
    void Update()
    {

    }
}
