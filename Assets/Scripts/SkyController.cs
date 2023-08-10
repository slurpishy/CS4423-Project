using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SkyController : MonoBehaviour
{
    public Transform _Sun = default;
    public Transform _Moon = default;

    public bool AutoMoonRotate = false;
    public bool AutoSunRotate = false;
    // Should be parent objects.

    void Update()
    {
        if (AutoMoonRotate)
        {
            _Moon.Rotate(-1.5f * Time.deltaTime, 0, 0);
        }
        if (AutoSunRotate)
        {
            _Sun.Rotate(-2.9f * Time.deltaTime, 0, 0);
        }
    }

    public IEnumerator ForceBodyEnd(Transform body)
    {
        float elapsed = 0;
        float speed = 0.02f;
        Quaternion end = Quaternion.Euler(-180, 0, 0);
        while (elapsed < 3f)
        {
            body.rotation = Quaternion.Lerp(body.rotation, end, elapsed * speed);
            elapsed = elapsed + Time.deltaTime;
            yield return null;
        }
        body.rotation = Quaternion.identity;
    }
}
