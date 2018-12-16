using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuNameSpace
{
    public class Pipes : MonoBehaviour
    {
        public int index = -1;  //在GameController.pipes里的索引

       void Start()
       {
           
       }

       private void OnTriggerEnter2D(Collider2D collision)
       {
           // 管道的中间位置的探测区域，如果碰撞物体为小鸟（被标记为了Player），计分
           if (collision.tag == "Player")
           {
               collision.gameObject.GetComponent<BirdAgent>().FlyThrough1Pipe(index);
               //SoundManager.instance.PlayPass();
           }
       }
    }
}