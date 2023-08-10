using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SkyController skyController;
    public ObstacleGenerator obstacleGenerator;
    public GameObject particlePrefab;
    public GameObject HealthBar;

    private int enemiesRemaining;

    private int waveCount = 0;
    private int waveEnemyBase = 20;

    private bool intermission = false;

    public bool hasSpecialProjectile = false;
    public bool hasFiringSpeedPowerup = false;

    public int maxHealth = 10;
    private int health = 10;
    private int finalHealthBarX = -115;

    public Animation camAnim;

    private float timeRemaining = 5f;
    private bool timer = true;

    [SerializeField]
    private TMP_Text waveText;
    [SerializeField]
    private TMP_Text pirateText;
    [SerializeField]
    private TMP_Text helpText;

    // Audio:
    public AudioSource battleAudio;
    public AudioSource intermissionAudio;
    public AudioSource damageAudio;

    // Singleton:
    private static GameController m_instance;
    public static GameController Instance {
        get {
            return m_instance;
        }
    }

    void Start()
    {
        camAnim = Camera.main.GetComponent<Animation>();
    }

    void Awake() {
        m_instance = this;
    }

    void OnDestroy() {
        m_instance = null;
    }

    void Update() {
        while (timer) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            } else {
                timer = false;
                StartWave();
            }
        }
    }

    private void StartWave()
    {
        // Start our moon:
        skyController.AutoSunRotate = false;
        skyController.AutoMoonRotate = true;

        // Increment waveCount
        waveCount++;
        waveText.text = waveCount.ToString();
        helpText.text = String.Format("Wave {0}: Start!\nLeft Click to FIRE!", waveCount);
        StartCoroutine(FadeOut(helpText, 5f));

        // Number of enemies by wave is:
        // where base enemy count is 20.
        // base + ((base / 1.5) * wave)
        enemiesRemaining = waveEnemyBase + ((int)(waveEnemyBase / 1.5) * waveCount);
        pirateText.text = enemiesRemaining.ToString();

        // Start generator:
        StartCoroutine(obstacleGenerator.SpawnEnemy());

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
        pirateText.text = enemiesRemaining.ToString();

        // Debug until UI:
        Debug.Log("Enemies remaining: " + enemiesRemaining);

        // Check if 0:
        if (enemiesRemaining <= 0)
        {
            // Debug until UI:
            Debug.Log("Wave Complete.");

            // Check if next wave is 6:
            if (waveCount + 1 > 5) {
                // You win!
                EndModelScene.win = true;
                SceneManager.LoadScene("EndScene");
            } else {

                // Intermission:
                StartIntermission();
            }

        }

    }

    void StartIntermission()
    {
        Debug.Log("===== Intermission =====");
        // No enemies left. Force moon end:
        StartCoroutine(skyController.ForceBodyEnd(skyController._Moon));

        // Fade out battle music:
        StartCoroutine(AudioFade.FadeInAudio(intermissionAudio, 4.0f));
        StartCoroutine(AudioFade.FadeOutAudio(battleAudio, 3.0f));


        helpText.text = "Intermission\nFire at the Power-Ups!";
        StartCoroutine(FadeOut(helpText, 5f));

        // Starts up SpawnPowerup in ObstacleGenerator:
        intermission = true;

        // Start generator:
        StartCoroutine(obstacleGenerator.SpawnPowerup());

        // Start our sun:
        skyController.AutoSunRotate = true;
        skyController.AutoMoonRotate = false;

        // Intermission lasts 30 seconds:
        StartCoroutine(EndIntermission());
    }

    IEnumerator EndIntermission()
    {
        yield return new WaitForSeconds(30);
        Debug.Log("===== Intermission Finished =====");
        intermission = false;
        StartWave();
    
        // Fade out intermission music:
        StartCoroutine(AudioFade.FadeInAudio(battleAudio, 4.0f));
        StartCoroutine(AudioFade.FadeOutAudio(intermissionAudio, 3.0f));

        // Move our sun out:
        StartCoroutine(skyController.ForceBodyEnd(skyController._Sun));
    }

    private void generateDeathParticle(Vector3 position)
    {
        var particle = Instantiate(particlePrefab, position, new Quaternion(0, 0, 0, 0));
        Destroy(particle, 5);
    }

    public void playCameraHit()
    {
        camAnim.Play("CAM_HIT");
        damageAudio.Play();
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

    public bool InIntermission() {
        return intermission;
    }

    public int getHP() {
        return health;
    }

    public void takeHP() {
        // Check if we're dead:
        if (health == 1) {

            // You lose.
            EndModelScene.win = false;
            SceneManager.LoadScene("EndScene");
        }
        health -= 1;
        RectTransform picture = HealthBar.GetComponent<RectTransform>();
        picture.anchoredPosition = new Vector2(picture.anchoredPosition.x + (finalHealthBarX / maxHealth), picture.anchoredPosition.y);
    }

    public void addHP() {
        if (health < maxHealth) {
            health += 1;
            RectTransform picture = HealthBar.GetComponent<RectTransform>();
            picture.anchoredPosition = new Vector2(picture.anchoredPosition.x - (finalHealthBarX / maxHealth), picture.anchoredPosition.y);
        }
    }
    private IEnumerator FadeOut(TMP_Text text, float duration)
    {
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    public void showHelpText(string text, float duration) {
        helpText.text = text;
        StartCoroutine(FadeOut(helpText, duration));
    } 

}
