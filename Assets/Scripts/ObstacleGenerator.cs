using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject enemy;
    public GameObject powerupA;
    public GameObject powerupB;
    public GameObject powerupC;
    private Vector3 basePos = new Vector3(0.25f, 7.30f, 0.90f);
    private float verticalOffsetBounds = 8.07f;
    public ObjectPool<GameObject> _pool; 
    
    private static ObstacleGenerator m_instance;
    public static ObstacleGenerator Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Start() {
        // Pool:
        _pool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(enemy),
                actionOnGet: (obj) => obj.SetActive(true),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                false,
                10,
                10
            );
    }
    void Awake()
    {
        m_instance = this;
    }

    void OnDestroy()
    {
        m_instance = null;
    }

    public IEnumerator SpawnEnemy()
    {
        while (GameController.Instance.GetEnemiesRemaining() > 0)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-verticalOffsetBounds, verticalOffsetBounds), basePos.y, basePos.z);
            GameObject newEnemy = _pool.Get();
            newEnemy.transform.position = spawnPos;
            newEnemy.transform.rotation = Quaternion.identity;
            newEnemy.SetActive(true);
            yield return new WaitForSeconds(1.0f);
        }
    }

    private IEnumerator SetEnemyInactive(GameObject enemy, int waitTime) {
        yield return new WaitForSeconds(waitTime);
        _pool.Release(enemy);
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
