using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SkyController : MonoBehaviour
{
    [SerializeField] Transform _Sun = default;
    [SerializeField] Transform _Moon = default;

    public bool AutoMoonRotate = false;
    public bool AutoSunRotate = false;
    private float MoonEndRotDuration = 4.0f;
    // Should be parent objects.

    void Update()
    {
        if (AutoMoonRotate)
        {
            _Moon.Rotate(-0.9f * Time.deltaTime, 0, 0);
        }
        if (AutoSunRotate)
        {
            _Sun.Rotate(-1.5f * Time.deltaTime, 0, 0);
        }
    }

    public IEnumerator ForceMoonEnd()
    {
        AutoMoonRotate = false;
        // Force moon X rot to go to -180.
        float elapsed = 0;
        Quaternion start = transform.rotation;
        Quaternion end = Quaternion.Euler(360, 0, 0);
        while (elapsed < MoonEndRotDuration)
        {
            _Moon.rotation = Quaternion.Lerp(start, end, elapsed / MoonEndRotDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = end;
    }
}
