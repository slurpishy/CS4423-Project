using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupTypes {
        Health,
        GoldenCB,
        IncreasedShootingSpeed,
    }

    private Vector3 finalPos = new Vector3(0.11f, -45.28f, -3.9f);
    private Vector3 lookAtPos = new Vector3(0.11f, -0.28f, -7.75f);
    public PowerupTypes powerupType = PowerupTypes.Health;
    public GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {

        transform.LookAt(lookAtPos);
        var step = 1.0f * Time.fixedDeltaTime;
        Vector3 newPosition = Vector3.MoveTowards(transform.position, finalPos, step);
        if (newPosition == transform.position)
        {
            Destroy(this.gameObject);
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

    void interactWithCannonBall() {
        Destroy(this.gameObject);
        if (powerupType == PowerupTypes.Health) {
            gameController.addHP();
        } else {
            gameController.hasSpecialProjectile = true;
            gameController.showHelpText("Golden Cannon Ball!\nUse the MIDDLE MOUSE CLICK button to kill active Pirates.", 5f);
        }
    }
}
