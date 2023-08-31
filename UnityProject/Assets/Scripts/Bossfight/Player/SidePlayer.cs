using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBGUnity.Utility;

public class SidePlayer : MonoBehaviour
{

    public float speed;
    public int nJump;
    private int currentJump;
    
    void Start()
    {
        nJump = 2;
        transform.Find("Legs").Find("RFoot").gameObject.AddComponent<Foot>();
        transform.Find("Legs").Find("LFoot").gameObject.AddComponent<Foot>();
    }
    
    void Update()
    {
        
        if(Input.GetKeyUp(KeyCode.Space) && nJump > 0){
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            MovementObject.Jump(this.GetComponent<Rigidbody2D>(), 7);
            nJump--;
        }
    }

    void FixedUpdate(){

        transform.position += new Vector3(Input.GetAxis("Horizontal")*Time.deltaTime*speed, 0, 0);
        //Debug.Log(this.gameObject.GetComponent<Rigidbody2D>().gravityScale);

    }

    void OnTriggerEnter2D(Collider2D coll){
        //Debug.Log(coll.gameObject.name);
        if(coll.gameObject.name != "MuroSx" && coll.gameObject.name != "MuroDx"){
            this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }
    void OnTriggerExit2D(Collider2D coll){
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}


public class Foot : MonoBehaviour{

    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.name != "MuroSx" && coll.gameObject.name != "MuroDx"){
            transform.parent.parent.GetComponent<SidePlayer>().nJump = 2;
        }
    }
    void OnCollisionExit2D(Collision2D coll){
        
    }

}