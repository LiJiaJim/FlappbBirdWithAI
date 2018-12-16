using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MuNameSpace;

public class GameController : MonoBehaviour
{
    private bool isCreatedBird = false;

    public GameObject gameOverTips;
    public bool startLoopingPipe = false;

    private float createPipesRate = 3f;
    public float minPipPosY = -5.5f;  
    public float maxPipPosY = 0;     
    public float aheadPosX = -10f;    
    public Vector2 startPipePos = new Vector2(-12f, 0f);   
    public List<GameObject> pipes = new List<GameObject>();
    public const int PIPESTOTAL = 3; 

    private float createPipeTimer = 0; 
    private int currPipesIndex = 0;

    private void Awake()
    {
    }

    private void Start()
    {
        Time.timeScale = 3f;
    }

    private void Update()
    {
        if(startLoopingPipe)
        {
            createPipeTimer += Time.deltaTime;
            if (createPipeTimer > createPipesRate)
            {
                createPipeTimer = 0;

                UpdatePipesPosition();
            }
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("SampleScene");
        }    
    }
   
    public void UpdatePipesPosition()
    {
        float randomPosY = Random.Range(minPipPosY, maxPipPosY);
        Vector2 position = new Vector2(aheadPosX, randomPosY);
        currPipesIndex = (currPipesIndex + 1) % PIPESTOTAL;
        pipes[currPipesIndex].transform.localPosition = position;
    }

    public void RearrangePipes()
    {
        for(int i = 0; i < PIPESTOTAL; i++)
        {
            if(i == 0)
            {
                float randomPosY = Random.Range(minPipPosY, maxPipPosY);
                Vector2 position = new Vector2(aheadPosX, randomPosY);
                pipes[i].transform.localPosition = position;
            }
            else
            {
                pipes[i].transform.localPosition = startPipePos;
            }
        }

        createPipeTimer = 0;
        currPipesIndex = 1;
    }
}
