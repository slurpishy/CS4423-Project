using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pirate : MonoBehaviour
{
    private Vector3 finalPos = new Vector3(0.11f, -0.28f, -7.75f);
    private int speedBase = 6;
    public GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }


    void FixedUpdate()
    {
        var step = GetEnemyMovementTime() * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, finalPos, step);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name.StartsWith("CannonBall"))
        {
            Destroy(other.gameObject);
            interactWithCannonBall();
        }

        if (String.Equals(other.name, "Cannon"))
        {
            Debug.Log("hit user.");
            gameController.decreaseEnemies();
        }
    }

    public void interactWithCannonBall()
    {
        Destroy(this.gameObject);
        gameController.decreaseEnemies();
    }

    public int GetEnemyMovementTime()
    {
        // Returns integer representing speed of enemy dependent on wave.
        // base += ((base/3.0) * (wave - 1) 
        return speedBase + (int)(speedBase / 3.0) * ((gameController.GetCurrentWave() - 1));
    }
}
