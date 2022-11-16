using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void OnGameStart(float speed);
    public OnGameStart onGameStart;

    public delegate void OnGameEnd();
    public OnGameEnd onGameEnd;

    public delegate void OnSpeedChange(float speed);
    public OnSpeedChange onSpeedChange;

    [SerializeField] GameObject ballPrefab;
    GameObject ball;
    [SerializeField] Transform xTresholdTop;
    [SerializeField] Transform xTresholdBot;
    [SerializeField] Transform spawnPoint;
    public float gameTime;
    private float maxGameTime;

    public static GameController instance;  //probably bad realization
    private float currentSpeed = 0;
    private bool gameStarted = false;
    private Coroutine speedUpRoutine;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        onGameStart += StartGame;
        onGameEnd += EndGame;
        maxGameTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            gameTime += Time.deltaTime;
        }   
    }

    void StartGame(float speed)
    {
        Debug.Log("GameStarted");
        currentSpeed = speed;
        gameStarted = true;
        gameTime = 0;
        speedUpRoutine = StartCoroutine(SpeedUp());

        ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        ball.GetComponent<PlayerController>().topPoint = xTresholdTop;
        ball.GetComponent<PlayerController>().botPoint = xTresholdBot;
    }
    
    void EndGame()
    {
        Debug.Log("GameEnded");
        StopCoroutine(speedUpRoutine);
        gameStarted = false;
        if (ball is not null)
        {
            Destroy(ball);
        }
    }

    IEnumerator SpeedUp()
    {
        while (true)
        {
            currentSpeed = currentSpeed * 1.2f;
            onSpeedChange(currentSpeed);
            yield return new WaitForSeconds(15f);
        }
    }
}
