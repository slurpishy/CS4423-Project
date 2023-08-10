using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pirate : MonoBehaviour
{
    private Vector3 finalPos = new Vector3(0.11f, -0.28f, -7.75f);
    private int speedBase = 6;
    public GameController gameController;
    public AudioSource disappearAudio;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Color color = GetComponent<Renderer>().material.color;
        color.a = 0.0f;
        meshRenderer.materials[0].color = color;
        while (color.a < 1)
        {
            color.a += 0.03f;
            meshRenderer.materials[0].color = color;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitUntil(() => meshRenderer.materials[0].color.a >= 1f);
    }

    void FixedUpdate()
    {
        var step = GetEnemyMovementTime() * Time.fixedDeltaTime;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, finalPos, step);
        if (newPosition == transform.position)
        {
            interactWithCannon();
        }
        transform.position = newPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("CannonBall"))
        {
            interactWithCannonBall();
        }

    }

    public void interactWithCannonBall()
    {
        disappearAudio.Play();
        Destroy(this.gameObject);
        gameController.decreaseEnemies(transform.position);
    }

    public void interactWithCannon()
    {
        interactWithCannonBall();
        gameController.playCameraHit();
        gameController.takeHP();
    }

    public int GetEnemyMovementTime()
    {
        // Returns integer representing speed of enemy dependent on wave.
        // base += ((base/3.0) * (wave - 1) 
        return speedBase + (int)(speedBase / 3.0) * ((gameController.GetCurrentWave() - 1));
    }
}
