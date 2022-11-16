using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public float wallSpeed = 0;
    public float length = 4;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        sprite.size = new Vector2(1, length);
        collider.size = new Vector2(1, length);

        GameController.instance.onSpeedChange += SpeedChange;
        GameController.instance.onGameEnd += SelfDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        float positionDelta = wallSpeed * Time.deltaTime;
        transform.position += Vector3.left * positionDelta;
    }

    void SpeedChange(float speed)
    {
        wallSpeed = speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameController.instance.onGameEnd -= SelfDestroy;
        GameController.instance.onSpeedChange -= SpeedChange;
    }
}
