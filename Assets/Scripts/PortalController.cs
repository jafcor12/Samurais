using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    Transform destination;

    public Transform GetDestination() { return destination; }


}
