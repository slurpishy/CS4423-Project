using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameController gameController;
    public GameObject enemy;
    private Vector3 basePos = new Vector3(0.25f, 7.30f, 0.90f);
    private float verticalOffsetBounds = 7.07f;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (gameController.GetEnemiesRemaining() > 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-verticalOffsetBounds, verticalOffsetBounds), basePos.y, basePos.z);
            GameObject newEnemy = Instantiate(enemy, spawnPos, Quaternion.identity);
            Destroy(newEnemy, 10);
            yield return new WaitForSeconds(0.6f);
        }
    }
}
