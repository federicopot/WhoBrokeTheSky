using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBGUnity.Utility;

public class SidePlayer : MonoBehaviour
{

    public float speed;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        //MovementObject.MoveSide(this.GetComponent<Rigidbody2D>(), .5f, this.transform);
        //transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime*speed, 0, 0);
        if(Input.GetKeyUp(KeyCode.Space)){
            MovementObject.Jump(this.GetComponent<Rigidbody2D>(), 7);
        }
    }

    void FixedUpdate(){

        transform.position += new Vector3(Input.GetAxis("Horizontal")*Time.deltaTime*speed, 0, 0);

    }

}
