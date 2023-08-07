using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cannon : MonoBehaviour
{
    Rigidbody rigidBody;

    public GameController gameController;
    public GameObject cannonBallPrefab;
    public Transform spawnPoint;
    public Material mat;
    public float projectileSpeed = 1.5f;
    private float _nextProjectile = 0.1f;
    float _previousFireT;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Awake()
    {
        // rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        // Quaternion deltaRotation = Quaternion.Euler(direction * Time.fixedDeltaTime);
        // rigidBody.MoveRotation(rigidBody.rotation * deltaRotation);
    }

    void FixedUpdate()
    {
        if(Time.time - _previousFireT < _nextProjectile) {
            return;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            var cannonBall = Instantiate(cannonBallPrefab, spawnPoint.position, spawnPoint.rotation);
            cannonBall.GetComponent<Rigidbody>().velocity += transform.forward * projectileSpeed;
            _previousFireT = Time.time;
        }

    
        if (Input.GetKey(KeyCode.Mouse2) && gameController.HasSpecialProjectile())
        {
            var cannonBall = Instantiate(cannonBallPrefab, spawnPoint.position, spawnPoint.rotation);
            // We don't interact with anything with a special projectile.
            // instead.. we simulate the interaction.
            Destroy(cannonBall.GetComponent<SphereCollider>());
            cannonBall.GetComponent<Rigidbody>().velocity += transform.forward * (projectileSpeed * 1.5f);

            // Cam anim:
            // gameController.camAnim.Play("CAM_ATTACK");

            // Special cannon ball has a different mat:
            cannonBall.GetComponent<Renderer>().material = mat;

            // In a few seconds, we destroy all enemy pirates.
            Invoke("ClearEnemyPirates", 0.6f);

            // TODO: clear special projectile from gc, but not doing that yet for testing purposes.
            _previousFireT = Time.time;
        }
    }

    public void ClearEnemyPirates()
    {
        // Get all enemies:
        foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
        {
            // -> Pirate.
            if (gameObj.name.StartsWith("Pirate"))
            {
                gameObj.GetComponent<Pirate>().interactWithCannonBall();
            }
        }
    }
}
