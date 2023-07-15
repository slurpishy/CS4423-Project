using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{

    public Cannon cannon;
    Vector3 moveDir = Vector3.zero;

    void FixedUpdate()
    {
        HandleRotation();
    }

    void HandleRotation()
    {
        Vector3 mousePosition = Input.mousePosition;
        /**
        mousePosition.z = Camera.main.transform.position.y - transform.position.y;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.x = -mousePosition.x;
        transform.forward = mousePosition - transform.position;
        **/
        mousePosition += Camera.main.transform.forward * 10f;
        Vector3 aim = Camera.main.ScreenToWorldPoint(mousePosition);
        aim.x = -aim.x;
        transform.LookAt(-aim);
    }
}
