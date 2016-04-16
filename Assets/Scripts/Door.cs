using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{

    float DoorOpenAngle = 90.0f;
    private bool open;
    private bool enter;

    private Vector3 defaultRot;
    private Vector3 openRot;

    void Start()
    {
        defaultRot = transform.position;
        openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
    }
    void Update()
    {
        if (open)
            transform.eulerAngles = Vector3.Slerp(transform.position, openRot, Time.deltaTime * 2);
        else
            transform.eulerAngles = Vector3.Slerp(transform.position, defaultRot, Time.deltaTime * 2);

        if (Input.GetKeyDown("f") && enter) open = !open;
    }

    void OnGUI()
    {
        if (enter)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 150, 30), "Press 'F' to open the door");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            enter = true;
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            enter = false;
    }
}
