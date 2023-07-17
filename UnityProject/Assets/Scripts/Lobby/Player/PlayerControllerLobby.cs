using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GBGUnity.Utility;
public class PlayerControllerLobby : MonoBehaviour
{
    private Rigidbody2D rb;
    private PolygonCollider2D collisionPlayer;

    void Awake()
    {
        this.gameObject.AddComponent<Rigidbody2D>();
        this.gameObject.AddComponent<PolygonCollider2D>();

        rb = this.GetComponent<Rigidbody2D>();
        collisionPlayer = this.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementObject.MoveTopDown(rb, 5);
    }
}
