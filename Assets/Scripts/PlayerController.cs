using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float verticalSpeed = 5;
    public Transform topPoint;
    public Transform botPoint;

    private float currentSpeed = 0;
    float positionDelta;
    float topTreshold;
    float botTreshold;

    Vector3 newPosition;

    delegate void OnBallSpawn();
    OnBallSpawn onBallSpawn;
    delegate void OnBallMeetWall();
    OnBallMeetWall onBallMeetWall;
    

    // Start is called before the first frame update
    void Start()
    {
        topTreshold = topPoint.transform.position.y;
        botTreshold = botPoint.transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKey(KeyCode.UpArrow))
        {
            currentSpeed = verticalSpeed;
        } else
        {
            currentSpeed = -verticalSpeed;
        }
        Move();

    }

    void Move()
    {
        positionDelta = currentSpeed * Time.deltaTime;
        newPosition = transform.position + Vector3.up * positionDelta;
        newPosition.y = Mathf.Clamp(newPosition.y, botTreshold, topTreshold);
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "wall")
        {
            GameController.instance.onGameEnd();
        }
    }

}
