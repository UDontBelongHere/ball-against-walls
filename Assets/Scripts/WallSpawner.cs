using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] GameObject wallPrefab;
    [SerializeField] Transform topPoint;
    [SerializeField] Transform botPoint;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float emptyCells = 7f;

    float topSpawnTreshold;
    float botSpawnTreshold;
    float spawnX;


    float speed = 0;
    private float emptySpace;
    private Transform lastWall;
    // Start is called before the first frame update
    void Start()
    {
        float wallSizeX = wallPrefab.GetComponent<BoxCollider2D>().size.x;
        float wallSizeY = wallPrefab.GetComponent<BoxCollider2D>().size.y;
        topSpawnTreshold = topPoint.position.y - wallSizeY / 2;
        botSpawnTreshold = botPoint.position.y + wallSizeY / 2;
        spawnX = spawnPoint.position.x;

        emptySpace = wallSizeX * emptyCells;  // space between walls

        GameController.instance.onGameStart += SpeedChange;
        GameController.instance.onSpeedChange += SpeedChange;
        GameController.instance.onGameEnd += StopSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed == 0) // disable spawning while game not started / stop
        {
            return;
        }
        if(lastWall == null)
        {
            SpawnWall(spawnX);
        } else if (spawnX - lastWall.position.x > emptySpace)
        {
            SpawnWall(lastWall.position.x + emptySpace);
        }
    }

    void SpawnWall(float x)
    {
        float newWallPosition = Random.Range(botSpawnTreshold, topSpawnTreshold);
        GameObject newWall = Instantiate(wallPrefab, new Vector3(x, newWallPosition, 0), Quaternion.identity);
        WallController newWallController = newWall.GetComponent<WallController>();
        newWallController.wallSpeed = speed;

        lastWall = newWall.transform;
    }

    void SpeedChange(float newSpeed)
    {
        speed = newSpeed;
    }

    void StopSpawn()
    {
        speed = 0;
    }

    private void OnDestroy()
    {
        GameController.instance.onGameStart -= SpeedChange;
        GameController.instance.onSpeedChange -= SpeedChange;
        GameController.instance.onGameEnd -= StopSpawn;
    }
}
