using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    void Awake()
    {
        Destroy(gameObject, 15.5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destroy(collision.gameObject);
    }
}
