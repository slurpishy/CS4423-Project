using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject enemy;
    public GameObject powerupA;
    public GameObject powerupB;
    public GameObject powerupC;
    private Vector3 basePos = new Vector3(0.25f, 7.30f, 0.90f);
    private float verticalOffsetBounds = 8.07f;


    public IEnumerator SpawnEnemy()
    {
        while (GameController.Instance.GetEnemiesRemaining() > 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-verticalOffsetBounds, verticalOffsetBounds), basePos.y, basePos.z);
            GameObject newEnemy = Instantiate(enemy, spawnPos, Quaternion.identity);
            Destroy(newEnemy, 10);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public IEnumerator SpawnPowerup() {
        int rInt;
        while (GameController.Instance.InIntermission()) {
            Vector3 spawnPos = new Vector3(Random.Range(-verticalOffsetBounds, verticalOffsetBounds), 9.30f, -3.9f);
            rInt = Random.Range(0, 100);
            GameObject prefab = powerupA;
            if (rInt > 75) {
                prefab = powerupC;
            } else if (rInt > 80) {
                prefab = powerupB;
            }
            GameObject powerup = Instantiate(prefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }
}
