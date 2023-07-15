using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject obstacleGen;
    private int enemiesRemaining;
    private int waveCount = 0;
    private int waveEnemyBase = 20;
    private bool hasSpecialProjectile = true;

    void Awake()
    {
        StartWave();
    }

    private void StartWave()
    {
        // Increment waveCount
        waveCount++;

        // Number of enemies by wave is:
        // where base enemy count is 20.
        // base + ((base / 1.5) * wave)
        enemiesRemaining = waveEnemyBase + ((int)(waveEnemyBase / 1.5) * waveCount);

        // Debug until UI:
        Debug.Log("Starting Wave: " + waveCount);
        Debug.Log("Enemies Remaining: " + enemiesRemaining);
    }

    public void decreaseEnemies()
    {
        // Enemy defeated.. per Pirate object.
        enemiesRemaining -= 1;

        // Debug until UI:
        Debug.Log("Enemies remaining: " + enemiesRemaining);

        // Check if 0:
        if (enemiesRemaining <= 0)
        {
            // Debug until UI:
            Debug.Log("Wave Complete.");

            // TODO: At this point would be our Day intermission period.
            // though we skip that for now..

            // Move onto next wave:
            StartWave();
        }

    }

    public int GetEnemiesRemaining()
    {
        return enemiesRemaining;
    }

    public int GetCurrentWave()
    {
        return waveCount;
    }

    public bool HasSpecialProjectile()
    {
        return hasSpecialProjectile;
    }
}
