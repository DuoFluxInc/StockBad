var grabPower = 10.0;//скорость притяжения
var throwPower = 10;//скорость толчка
var hit : RaycastHit;//луч
var RayDistance : float = 3.0;//дистанция
private var Grab : boolean = false;//ф-ция притяжения
private var Throw : boolean = false;//ф-ция толчка
var offset : Transform;
//private var open : boolean;
//private var enter : boolean;


function Update(){
    if (Input.GetKey(KeyCode.Q)){//если нажата клавиша Q

        Physics.Raycast(transform.position, transform.forward, hit, RayDistance);//физический луч

        if(hit.rigidbody){//если луч соприкасается с rigidbody
            Grab = true;
        }
    }

    if (Input.GetMouseButtonDown(0)){//если нажата лев кн мыши
        if(Grab){
            Grab = false;
            Throw = true;
        }
    }

    if(Grab){//ф-ция притяжения
        if(hit.rigidbody){
            hit.rigidbody.velocity = (offset.position - (hit.transform.position + hit.rigidbody.centerOfMass))*grabPower;
        }
    }

    if(Throw){//ф-ция толчка
        if(hit.rigidbody){
            hit.rigidbody.velocity = transform.forward * throwPower;
            Throw = false;
        }
    }
        /*if(Input.GetKeyDown("q") && enter){
            //open = !open;
        }
        }
        function OnGUI(){
            if(enter){
                GUI.Label(new Rect(Screen.width/2 - 75, Screen.height - 100, 150, 30), "Press 'q' to open the door");
            }
        }
        
            //Activate the Main function when player is near the door
        function OnTriggerEnter (other : Collider){
            if (other.gameObject.tag == "Player") {
                enter = true;
            }
        }
        
            //Deactivate the Main function when player is go away from door
            function OnTriggerExit (other : Collider){
                if (other.gameObject.tag == "Player") {
                    enter = false;
                }
            }*/
    }