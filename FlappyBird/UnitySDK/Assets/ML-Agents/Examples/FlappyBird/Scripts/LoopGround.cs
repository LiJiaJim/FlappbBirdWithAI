using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopGround : MonoBehaviour
{
    private BoxCollider2D groundCollider;
    private float boxColliderWidth;

    public GameObject endPot;
    public GameObject startPot;


	void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        boxColliderWidth = groundCollider.size.x;
	}
	
	void Update ()
    {
		if (transform.localPosition.x < endPot.transform.localPosition.x)
        {
            transform.localPosition = startPot.transform.localPosition;
        }
	}
}
