using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGroundSample : MonoBehaviour
{
    public float speed = 3;

    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.velocity = new Vector2(-speed, 0);
    }

    void Update()
    {
        if(GameControllerSample.instance.isGameOver)
        {
            rb2D.velocity = new Vector2(0, 0);
        }
    }
}
