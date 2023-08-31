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
        //MovementObject.MoveSide(this.GetComponent<Rigidbody2D>(), .5f, this.transform);
        //transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime*speed, 0, 0);
        if(Input.GetKeyUp(KeyCode.Space) && nJump > 0){
            MovementObject.Jump(this.GetComponent<Rigidbody2D>(), 7);
            //currentJump++;
            nJump--;
        }
    }

    void FixedUpdate(){

        transform.position += new Vector3(Input.GetAxis("Horizontal")*Time.deltaTime*speed, 0, 0);

        Debug.Log(this.gameObject.GetComponent<Rigidbody2D>().gravityScale);

        /*if(){

        }*/

    }

    void OnTriggerEnter2D(Collider2D coll){
        this.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        this.gameObject.GetComponent<Rigidbody2D>().simulated = true;

        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0f;
    }
    void OnTriggerExit2D(Collider2D coll){
        this.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}


public class Foot : MonoBehaviour{

    void OnCollisionEnter2D(Collision2D coll){
        transform.parent.parent.GetComponent<SidePlayer>().nJump = 2;
    }
    void OnCollisionExit2D(Collision2D coll){
        //Debug.Log($"Exit: {coll}");
    }

}