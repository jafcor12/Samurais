using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscilationController : MonoBehaviour
{
    [SerializeField]
    bool backAndForth;

    [SerializeField]
    bool reverse;

    [SerializeField]
    bool moveInX;

    [SerializeField]
    bool moveInY;

    [SerializeField]
    bool moveInZ;

    [SerializeField]
    float travelDistance;

    [SerializeField]
    float speed;

    Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if (!backAndForth)
        {
            return;
        }

        Vector3 position = transform.position;

        if (moveInX)
        {
            position.x = _startPosition.x + Mathf.PingPong(Time.time * speed, travelDistance) * Helper.BoolToInt(reverse);
            transform.position = position;
        }

        if (moveInY)
        {
            position.y = _startPosition.y + Mathf.PingPong(Time.time * speed, travelDistance) * Helper.BoolToInt(reverse);
            transform.position = position;
        }

        if (moveInX)
        {
            position.x = _startPosition.x + Mathf.PingPong(Time.time * speed, travelDistance) * Helper.BoolToInt(reverse);
            transform.position = position;
        }

    }
}
