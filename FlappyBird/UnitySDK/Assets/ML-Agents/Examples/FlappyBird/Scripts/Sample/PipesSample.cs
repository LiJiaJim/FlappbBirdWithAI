using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesSample : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameControllerSample.instance.PassOnePip();
            SoundManager.instance.PlayPass();
        }
    }
}
