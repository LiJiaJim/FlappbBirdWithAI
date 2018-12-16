using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using MLAgents;

namespace MuNameSpace
{
    public class BirdAgent : Agent
    {
        private bool isFlying = false;                      //小鸟是否在向上飞
        private int deadCount = 0;                          //AI 死亡次数
        private int count = 0;                              //飞过的管道数
        public GameObject startPot;                         //小鸟初始化位置
        private GameObject nextPipe = null;                 //下一个管道
        private const float height = 5f;                    //地图中心到上下两端的距离
        private const float pipeGap = 2f;                   //管道中心到上下端的距离

        public GameObject deadCountText;
        public GameObject countText;
        private Rigidbody2D rb2D;
        public GameController gameController;

        

        public override void InitializeAgent()
        {
            rb2D = GetComponent<Rigidbody2D>();
            gameController.RearrangePipes();
            gameController.startLoopingPipe = true;
            gameController.UpdatePipesPosition();
            nextPipe = gameController.pipes[0];

            this.transform.localPosition = startPot.transform.localPosition;
            isFlying = false;
            count = 0;
        }

        public override void CollectObservations()
        {
            //要观测的内容（用height归一化）：
            //  小鸟位置、小鸟速度、管道的上下沿位置、是否向上
            AddVectorObs(gameObject.transform.localPosition.y / 100);
            AddVectorObs(Mathf.Clamp(rb2D.velocity.y, -height, height) / 100);
            AddVectorObs((nextPipe.transform.localPosition.y + pipeGap) / 100);
            AddVectorObs((nextPipe.transform.localPosition.y - pipeGap) / 100);
            AddVectorObs(isFlying ? 1f : -1f);

            AddVectorObs((nextPipe.transform.localPosition.x - 
                gameObject.transform.localPosition.x)/100);
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            AddReward(0.01f);

            int value = Mathf.FloorToInt(vectorAction[0]);
            if (value == 1 && !isFlying )
            {
                isFlying = true;
                FlyImplus();
            }
            else if(value == 0)
            {
                isFlying = false;
            }
        }

        private void Update()
        {

        }

        public override void AgentReset()
        {
            base.AgentReset();

            this.transform.localPosition = startPot.transform.localPosition;
            isFlying = false;
            nextPipe = gameController.pipes[0];
            count = 0;
            gameController.RearrangePipes();
        }

        private void FlyImplus()
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }

        public void FlyThrough1Pipe(int thisIndex)
        {
            //AddReward(1f);
            count++;
            nextPipe = gameController.pipes[(thisIndex+1)%GameController.PIPESTOTAL];

            if (countText != null)
            {
                countText.GetComponent<Text>().text = count.ToString();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            AddReward(-1f);
            count = 0;
            deadCount++;

            if(deadCountText != null)
            {
                deadCountText.GetComponent<Text>().text = deadCount.ToString();
            }

            if (countText != null)
            {
                countText.GetComponent<Text>().text = count.ToString();
            }

            Done();
        }
    }

}
