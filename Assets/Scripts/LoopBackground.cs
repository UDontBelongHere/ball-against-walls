using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    private float speed = 0;
    private float backSize;
    private float backPosition;
    // Start is called before the first frame update
    void Start()
    {
        backSize = GetComponent<SpriteRenderer>().bounds.size.x;

        GameController.instance.onGameStart += ChangeSpeed;
        GameController.instance.onSpeedChange += ChangeSpeed;
        GameController.instance.onGameEnd += StopMoving;
    }

    // Update is called once per frame
    void Update()
    {
        backPosition -= speed * Time.deltaTime;
        backPosition = Mathf.Repeat(backPosition, backSize);
        transform.position = new Vector3(backPosition, 0, 0);
    }

    void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void StopMoving()
    {
        backPosition = 0;
        speed = 0;
    }

    private void OnDestroy()
    {
        GameController.instance.onGameStart -= ChangeSpeed;
        GameController.instance.onSpeedChange -= ChangeSpeed;
        GameController.instance.onGameEnd -= StopMoving;
    }
}
