using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PaddleController : MonoBehaviour
{
    HingeJoint _hingeJoint;

    [SerializeField]
    float strenght;
    
    [SerializeField]
    float damper;

    [SerializeField]
    float targetPosition;

    [SerializeField]
    float originPosition;

    void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
    }

    void Update()
    {
        JointSpring spring = new JointSpring();

        spring.spring = strenght;
        spring.damper = damper;
        spring.targetPosition = _hingeJoint.spring.targetPosition;

        if (_hingeJoint.spring.targetPosition >= targetPosition)
        {
            spring.targetPosition = originPosition;
        }
        else if (_hingeJoint.spring.targetPosition <= originPosition)
        {
            spring.targetPosition = targetPosition;
        }

        _hingeJoint.spring = spring;
        _hingeJoint.useLimits = true;

        JointLimits limits = _hingeJoint.limits;
        limits.min = originPosition;
        limits.max = targetPosition;
        _hingeJoint.limits = limits;
    }

    //IEnumerator MoveTo(float position)
    //{
    //    _hingeJoint.spring.
    //}
}
