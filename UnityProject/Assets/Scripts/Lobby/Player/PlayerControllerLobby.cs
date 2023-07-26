using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBGUnity.Utility;
public class PlayerControllerLobby : MonoBehaviour
{
    [SerializeField] public TMPro.TextMeshProUGUI textCostellationMenu;
    public GameObject Selection;
    private Rigidbody2D rb;
    private PolygonCollider2D collisionPlayer;
    public float speed;
    void Awake()
    {
        speed = 5;
        this.gameObject.AddComponent<Rigidbody2D>();
        this.gameObject.AddComponent<PolygonCollider2D>();

        rb = this.GetComponent<Rigidbody2D>();
        collisionPlayer = this.GetComponent<PolygonCollider2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 0;

        gameObject.AddComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementObject.MoveTopDown(rb, speed);
    }
}
public class Interaction : MonoBehaviour{
    private bool triggered;
    void Awake(){
        triggered = false;
    }
    void Update(){
        if(triggered && Input.GetKeyUp(KeyCode.T) && GetComponent<PlayerControllerLobby>().speed!=0){
            GetComponent<PlayerControllerLobby>().speed = 0;
            GetComponent<PlayerControllerLobby>().textCostellationMenu.gameObject.SetActive(false);
            GetComponent<PlayerControllerLobby>().Selection.gameObject.SetActive(true);
        }else{
            if(!triggered || (Input.GetKeyUp(KeyCode.T))){
                GetComponent<PlayerControllerLobby>().Selection.gameObject.SetActive(false);
                GetComponent<PlayerControllerLobby>().speed = 5;
                
            }
        }
    }
    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.transform.parent.name == "Altare" && coll.gameObject.name == "TriggerAltare"){
            Debug.Log($"ciao triggerato in ENTRATA da {coll.gameObject.name}");
            GetComponent<PlayerControllerLobby>().textCostellationMenu.text = "Click T To Enter To The Altar";
            GetComponent<PlayerControllerLobby>().textCostellationMenu.gameObject.SetActive(true);
            triggered = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll){
        if(coll.gameObject.transform.parent.name == "Altare" && coll.gameObject.name == "TriggerAltare"){
            Debug.Log($"ciao triggerato in USCITA da {coll.gameObject.name}");
            GetComponent<PlayerControllerLobby>().textCostellationMenu.gameObject.SetActive(false);
            triggered = false;
        }
    }
}