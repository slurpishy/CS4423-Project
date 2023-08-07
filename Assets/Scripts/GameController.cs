using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public SkyController skyController;
    public GameObject particlePrefab;
    private int enemiesRemaining;
    private int waveCount = 0;
    private int waveEnemyBase = 20;
    private bool hasSpecialProjectile = true;
    public Animation camAnim;

    void Awake()
    {
        StartIntermission();
    }

    void Start()
    {
        camAnim = Camera.main.GetComponent<Animation>();
        skyController = GameObject.FindGameObjectWithTag("SkyController").GetComponent<SkyController>();
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

    public void decreaseEnemies(Vector3 position)
    {
        // Generate our particle:
        generateDeathParticle(position);
        // Enemy defeated.. per Pirate object.
        enemiesRemaining -= 1;

        // Debug until UI:
        Debug.Log("Enemies remaining: " + enemiesRemaining);

        // Check if 0:
        if (enemiesRemaining <= 0)
        {
            // Debug until UI:
            Debug.Log("Wave Complete.");

            // Intermission:
            StartIntermission();

        }

    }

    void StartIntermission()
    {
        Debug.Log("===== Intermission =====");
        // No enemies left. Force moon end:
        StartCoroutine(skyController.ForceMoonEnd());

        // Start our sun:
        skyController.AutoSunRotate = true;

        // Intermission lasts 30 seconds:
        StartCoroutine(EndIntermission());
    }

    IEnumerator EndIntermission()
    {
        yield return new WaitForSeconds(30);
        Debug.Log("===== Intermission Finished =====");
    }

    private void generateDeathParticle(Vector3 position)
    {
        var particle = Instantiate(particlePrefab, position, new Quaternion(0, 0, 0, 0));
        Destroy(particle, 5);
    }

    public void playCameraHit()
    {
        camAnim.Play("CAM_HIT");
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
