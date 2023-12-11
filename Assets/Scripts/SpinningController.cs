using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningController : MonoBehaviour
{
    [SerializeField]
    Vector3 direction = Vector3.zero;

    [SerializeField]
    float speed = 10.0f;

    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}
